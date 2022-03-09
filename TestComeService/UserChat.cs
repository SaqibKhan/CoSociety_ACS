using Azure;
using Azure.Communication;
using Azure.Communication.Chat;
using Azure.Communication.Identity;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestComeService
{
    public partial class UserChat : MaterialForm
    {  
        private string _displayName;
        private ChatThreadClient _chatThreadClient;
        public UserChat( string displayName, ChatThreadClient chatThreadClient)
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
            _displayName = displayName;
            _chatThreadClient= chatThreadClient;
            

        }

        private async void UserChat_LoadAsync(object sender, EventArgs e)
        {
             this.Text = _displayName;
        }

        private async void btnSentText_Click(object sender, EventArgs e)
        {

            SendChatMessageOptions sendChatMessageOptions = new SendChatMessageOptions()
            {
                Content = txt_SendText.Text,
                MessageType = ChatMessageType.Text,
                SenderDisplayName=_displayName
               
            };
            //sendChatMessageOptions.Metadata["hasAttachment"] = "true";
            //0sendChatMessageOptions.Metadata["attachmentUrl"] = "https://contoso.com/files/attachment.docx";

            SendChatMessageResult sendChatMessageResult = await _chatThreadClient.SendMessageAsync(sendChatMessageOptions);
            var frm = (CoSociety)(this.Tag);
            frm.GetMessages();
            GetMessages();
            txt_SendText.Text = String.Empty;
        }

        public async void GetMessages()
        {           
            AsyncPageable<ChatMessage>? allMessages = _chatThreadClient.GetMessagesAsync();
            await foreach (ChatMessage message in allMessages)
            {
                if (!string.IsNullOrWhiteSpace(message.Content.Message))
                {
                    txtMessageHistory.Text += $" {message.SenderDisplayName}:{message.Content.Message}{Environment.NewLine}";
                }
            }
        }

    }
}
