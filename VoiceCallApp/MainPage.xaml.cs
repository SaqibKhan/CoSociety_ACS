using Azure;
using Azure.Communication.Calling;
using Azure.Communication.Identity;
using Azure.WinRT.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VoiceCallApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        CommunicationUserIdentifierAndToken _identityAndToken;
        CommunicationIdentityClient _client;
        CallClient call_client_;
        CallAgent call_agent_;
        Call call_;

        public MainPage()
        {
            this.InitializeComponent();
            this.InitCallAgent();
        }

        private async void InitCallAgent()
        {
            // Create Call Client and initialize Call Agent

            _client = new CommunicationIdentityClient(new Uri(Settings.EndpointUrl), new AzureKeyCredential(Settings.ServiceAccessKey));
            _identityAndToken = await _client.CreateUserAndTokenAsync(scopes: new[] { CommunicationTokenScope.VoIP});


            CommunicationTokenCredential token_credential = new Azure.WinRT.Communication.CommunicationTokenCredential(_identityAndToken.AccessToken.Token);
            call_client_ = new CallClient();
            Random random = new Random();

            CallAgentOptions callAgentOptions = new CallAgentOptions()
            {
                DisplayName = "saqib1"+ random.Next(10),
            };

            this.Caption.Text = callAgentOptions.DisplayName;

            call_agent_ = await call_client_.CreateCallAgent(token_credential, callAgentOptions);
            
        }

        private async void CallButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            // Start call

            StartCallOptions startCallOptions = new StartCallOptions();
            ICommunicationIdentifier[] callees = new ICommunicationIdentifier[1]
            {  
                new CommunicationUserIdentifier(CalleeTextBox.Text)
            };
            call_ = await call_agent_.StartCallAsync(callees, startCallOptions);
        }

        private async void HangupButton_Click(object sender, RoutedEventArgs e)
        {
            // End the current call
            await call_.HangUpAsync(new HangUpOptions());
        }

        private void btnOpenVideoChat_Click(object sender, RoutedEventArgs e)
        {
            var videochatWindow = new VideoChatPage();
            this.Frame.Navigate(typeof(VideoChatPage));
        }
    }
}
