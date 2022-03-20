using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure;
using Azure.Communication;
using Azure.Communication.Chat;
using Azure.Communication.Identity;

namespace ACS_Cosociety
{
    internal sealed class CoSocietyChat
    {
        private readonly string _endpointUrl;
        private readonly string _serviceAccessKey;
        private CommunicationUserIdentifierAndToken _identityAndToken;
        private CommunicationIdentityClient _client;
        private ChatClient _chatClient;
        private const string Topic = "Cosociety!";
        private string _chatThreadClientId;
        
        public CoSocietyChat(string endpointUrl, string serviceAccessKey)
        {
            _endpointUrl = endpointUrl;
            _serviceAccessKey = serviceAccessKey;
            _chatThreadClientId = string.Empty;


        }

        ~CoSocietyChat()
        {
            _client.DeleteUserAsync(_identityAndToken.User);
            _chatClient.DeleteChatThreadAsync(_chatThreadClientId);
        }

        //public async void GetMessages()
        //{
        //    var threadClient = _chatClient.GetChatThreadClient(threadId: _chatThreadClientId);
        //    AsyncPageable<ChatMessage>? allMessages = threadClient.GetMessagesAsync(DateTime.Now.AddSeconds(-1));
        //    await foreach (ChatMessage message in allMessages)
        //    {
        //        if (!string.IsNullOrWhiteSpace(message.Content.Message))
        //        {
        //            txt_ClientConnection.Text += $" {message.SenderDisplayName}:{message.Content.Message}{Environment.NewLine}";
        //        }
        //    }
        //}

        //async Task<Response<CommunicationUserIdentifierAndToken>> GetTokenAsync()
        //{
        //    //var identityResponse = await client.CreateUserAsync();
        //    //var identity = identityResponse.Value;
        //    //var tokenResponse = await client.GetTokenAsync(identity, scopes: new[] { CommunicationTokenScope.Chat});

        //    return identityAndToken;
        //}

        public async void CreateChatThread(string topic)
        {
            _client = new CommunicationIdentityClient(new Uri(_endpointUrl), new AzureKeyCredential(_serviceAccessKey));
            _identityAndToken = await _client.CreateUserAndTokenAsync(scopes: new[] { CommunicationTokenScope.Chat });


            // Get the token from the response
            var endpoint = new Uri(_endpointUrl);
            var communicationTokenCredential = new CommunicationTokenCredential(_identityAndToken.AccessToken.Token);
            _chatClient = new ChatClient(endpoint, communicationTokenCredential);


            var chatParticipant = new ChatParticipant(identifier: new CommunicationUserIdentifier(id: _identityAndToken.User.Id))
                {
                    DisplayName = "Server1"
                };

            CreateChatThreadResult createChatThreadResult =  await _chatClient.CreateChatThreadAsync(topic: topic, participants: new[] { chatParticipant });
            _chatThreadClientId = createChatThreadResult.ChatThread.Id;
        }



        private async Task<ChatThreadClient> AddParticipant(string displayName , string? chatThreadClientId = null)
        {
            var chatParticipant = new ChatParticipant(identifier: new CommunicationUserIdentifier(id: _identityAndToken.User.Id))
            {
                DisplayName = displayName
            };
            chatThreadClientId = chatThreadClientId ?? _chatThreadClientId;
            var threadClient = _chatClient.GetChatThreadClient(threadId: chatThreadClientId);
            await threadClient.AddParticipantAsync(chatParticipant);
            return threadClient;
        }


        private async void SendChatMessage(string chatText,string displayName, ChatThreadClient chatThreadClient)
        {

            var sendChatMessageOptions = new SendChatMessageOptions()
            {
                Content = chatText,
                MessageType = ChatMessageType.Text,
                SenderDisplayName = displayName

            };
           SendChatMessageResult sendChatMessageResult = await chatThreadClient.SendMessageAsync(sendChatMessageOptions);
        }


        //public async di  GetChatThreads()
        //{

        //    AsyncPageable<ChatThreadItem> chatThreadItems = _chatClient.GetChatThreadsAsync();
        //    var list = new List<string>();
        //    await foreach (ChatThreadItem chatThreadItem in chatThreadItems)
        //    {
        //        Console.WriteLine($"Topic:{chatThreadItem.Topic} Id:{chatThreadItem.Id}");
        //        List<st>
        //    }
        //}
    }
}
