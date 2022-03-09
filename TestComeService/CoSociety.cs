using Azure;
using Azure.Core;
using MaterialSkin;
using MaterialSkin.Controls;
using Azure.Identity;
using Azure.Communication.Identity;
using Azure.Communication;
using Azure.Communication.Chat;

namespace TestComeService
{
    public delegate void GetMessagesDelegate();
    public partial class CoSociety : MaterialForm
    {
        CommunicationUserIdentifierAndToken _identityAndToken;
         CommunicationIdentityClient _client;
        ChatClient _chatClient;
        private const string Topic = "Cosociety!";
        private string chatThreadClientId = "";
        public GetMessagesDelegate getMessagesDelegate;
        public CoSociety()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            getMessagesDelegate = new GetMessagesDelegate(GetMessages);
        }

        public async void GetMessages()
        {
            var threadClient = _chatClient.GetChatThreadClient(threadId: chatThreadClientId);
            AsyncPageable<ChatMessage>? allMessages = threadClient.GetMessagesAsync(DateTime.Now.AddSeconds(-1));
            await foreach (ChatMessage message in allMessages)
            {
                if (!string.IsNullOrWhiteSpace(message.Content.Message))
                {
                    txt_ClientConnection.Text += $" {message.SenderDisplayName}:{message.Content.Message}{Environment.NewLine}";
                }
            }
        }

        //async Task<Response<CommunicationUserIdentifierAndToken>> GetTokenAsync()
        //{
        //    //var identityResponse = await client.CreateUserAsync();
        //    //var identity = identityResponse.Value;
        //    //var tokenResponse = await client.GetTokenAsync(identity, scopes: new[] { CommunicationTokenScope.Chat});



        //    return identityAndToken;
        //}

        private async void CoSociety_Load(object sender, EventArgs e)
        {
            _client = new CommunicationIdentityClient(new Uri(Settings.EndpointUrl), new AzureKeyCredential(Settings.ServiceAccessKey));
            _identityAndToken = await _client.CreateUserAndTokenAsync(scopes: new[] { CommunicationTokenScope.Chat });
            // Get the token from the response
            txt_ClientConnection.Text += $" user {_identityAndToken.User}{Environment.NewLine}";
            txt_ClientConnection.Text += $" token = {_identityAndToken.AccessToken.Token}{Environment.NewLine}";
            txt_ClientConnection.Text += $" expiresOn = {_identityAndToken.AccessToken.ExpiresOn}{Environment.NewLine}";
            txt_ClientConnection.Text += $"---------------------------------------------------------------------------";
            txt_ClientConnection.Text += $"{Environment.NewLine} {Environment.NewLine}";

            Uri endpoint = new Uri(Settings.EndpointUrl);
            CommunicationTokenCredential communicationTokenCredential = new CommunicationTokenCredential(_identityAndToken.AccessToken.Token);
            _chatClient = new ChatClient(endpoint, communicationTokenCredential);


            var chatParticipant = new ChatParticipant(identifier: new CommunicationUserIdentifier(id: _identityAndToken.User.Id))
            {
                DisplayName = "Server"
            };
            CreateChatThreadResult createChatThreadResult = await _chatClient.CreateChatThreadAsync(topic: Topic, participants: new[] { chatParticipant });
            chatThreadClientId = createChatThreadResult.ChatThread.Id;
                      

            txt_ClientConnection.Text += $"---------------------Chat Client Intiated---------------------------";
            txt_ClientConnection.Text += $"{Environment.NewLine} {Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}";
            // chat thread
            
          

        }

        private void CoSociety_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private async void CoSociety_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            await _client.DeleteUserAsync(_identityAndToken.User);
            await _chatClient.DeleteChatThreadAsync(chatThreadClientId);
        }

        private async void btn_CreateUser_Click(object sender, EventArgs e)
        {
            var chatParticipant = new ChatParticipant(identifier: new CommunicationUserIdentifier(id: _identityAndToken.User.Id))
            {
                DisplayName = txt_ChatUsers.Text
            };

            var threadClient = _chatClient.GetChatThreadClient(threadId: chatThreadClientId);
            await threadClient.AddParticipantAsync(chatParticipant);  
            
            txt_ChatUsers.Text = String.Empty;

            var chatWindow = new UserChat(txt_Client.Text, threadClient);
            chatWindow.Tag = this;
            chatWindow.Show();
          
            //AsyncPageable<ChatThreadItem> chatThreadItems = _chatClient.GetChatThreadsAsync();
            //await foreach (ChatThreadItem chatThreadItem in chatThreadItems)
            //{
            //    txt_ChatUsers.Text += $"{ chatThreadItem.Id}";
            //}


            AsyncPageable<ChatParticipant> allParticipants = threadClient.GetParticipantsAsync();
            await foreach (ChatParticipant participant in allParticipants)
            {
                txt_ChatUsers.Text += ($"DisplayName: {participant.DisplayName}{Environment.NewLine}");
            }
        }
    }
}