using Azure;
using Azure.WinRT.Communication;
using Azure.Communication.Calling;
using Azure.Communication.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace VoiceCallApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoChatPage : Page
    {
        CommunicationUserIdentifierAndToken _identityAndToken;
        CommunicationIdentityClient _client;

        CallClient callClient;
        CallAgent callAgent;
        Call call;
        DeviceManager deviceManager;
        LocalVideoStream[] localVideoStream;
        Dictionary<String, RemoteParticipant> remoteParticipantDictionary;



        public VideoChatPage()
        {
            this.InitializeComponent();
            this.InitCallAgentAndDeviceManager();
            remoteParticipantDictionary = new Dictionary<string, RemoteParticipant>();
        }

        private async void InitCallAgentAndDeviceManager()
        {
            // Initialize call agent and Device Manager

            CallClient callClient = new CallClient();
            deviceManager = await callClient.GetDeviceManager();

            _client = new CommunicationIdentityClient(new Uri(Settings.EndpointUrl), new AzureKeyCredential(Settings.ServiceAccessKey));
            _identityAndToken = await _client.CreateUserAndTokenAsync(scopes: new[] { CommunicationTokenScope.VoIP });

            var token_credential = new Azure.WinRT.Communication.CommunicationTokenCredential(_identityAndToken.AccessToken.Token);

            CallAgentOptions callAgentOptions = new CallAgentOptions()
            {
                DisplayName = "saqib"
            };
            callAgent = await callClient.CreateCallAgent(token_credential, callAgentOptions);
            callAgent.OnCallsUpdated += Agent_OnCallsUpdatedAsync;
            callAgent.OnIncomingCall += Agent_OnIncomingCall;

        }

        private async void Agent_OnCallsUpdatedAsync(object sender, CallsUpdatedEventArgs args)
        {
            foreach (var call in args.AddedCalls)
            {
                foreach (var remoteParticipant in call.RemoteParticipants)
                {
                    String remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                    remoteParticipantDictionary.TryAdd(remoteParticipantMRI, remoteParticipant);
                    await AddVideoStreams(remoteParticipant.VideoStreams);
                    remoteParticipant.OnVideoStreamsUpdated += async (s, a) => await AddVideoStreams(a.AddedRemoteVideoStreams);
                }
                call.OnRemoteParticipantsUpdated += Call_OnRemoteParticipantsUpdatedAsync;
                call.OnStateChanged += Call_OnStateChanged;
            }
        }

        private async void Call_OnRemoteParticipantsUpdatedAsync(object sender, ParticipantsUpdatedEventArgs args)
        {
            foreach (var remoteParticipant in args.AddedParticipants)
            {
                String remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                remoteParticipantDictionary.TryAdd(remoteParticipantMRI, remoteParticipant);
                await AddVideoStreams(remoteParticipant.VideoStreams);
                remoteParticipant.OnVideoStreamsUpdated += async (s, a) => await AddVideoStreams(a.AddedRemoteVideoStreams);
            }
        }

        private async Task AddVideoStreams(IReadOnlyList<RemoteVideoStream> streams)
        {

            foreach (var remoteVideoStream in streams)
            {
                var remoteUri = await remoteVideoStream.Start();

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    RemoteVideo.Source = remoteUri;
                    RemoteVideo.Play();
                });
            }
        }
        private async void Call_OnStateChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (((Call)sender).State)
            {
                case CallState.Disconnected:
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        LocalVideo.Source = null;
                        RemoteVideo.Source = null;
                    });
                    break;
                default:
                    Debug.WriteLine(((Call)sender).State);
                    break;
            }
        }

        private async void CallButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            // Authenticate the client and start call
            Debug.Assert(deviceManager.Microphones.Count > 0);
            Debug.Assert(deviceManager.Speakers.Count > 0);
            Debug.Assert(deviceManager.Cameras.Count > 0);

            if (deviceManager.Cameras.Count > 0)
            {
                VideoDeviceInfo videoDeviceInfo = deviceManager.Cameras[0];
                localVideoStream = new LocalVideoStream[1];
                localVideoStream[0] = new LocalVideoStream(videoDeviceInfo);

                Uri localUri = await localVideoStream[0].MediaUriAsync();

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    LocalVideo.Source = localUri;
                    LocalVideo.Play();
                });

            }

            StartCallOptions startCallOptions = new StartCallOptions();
            startCallOptions.VideoOptions = new VideoOptions(localVideoStream);
            ICommunicationIdentifier[] callees = new ICommunicationIdentifier[1]
            {
                new Azure.WinRT.Communication.CommunicationUserIdentifier(CalleeTextBox.Text)
            };

            call = await callAgent.StartCallAsync(callees, startCallOptions);
        }

        private async void Agent_OnIncomingCall(object sender, IncomingCall incomingcall)
        {
            // Accept an incoming call
            Debug.Assert(deviceManager.Microphones.Count > 0);
            Debug.Assert(deviceManager.Speakers.Count > 0);
            Debug.Assert(deviceManager.Cameras.Count > 0);

            if (deviceManager.Cameras.Count > 0)
            {
                VideoDeviceInfo videoDeviceInfo = deviceManager.Cameras[0];
                localVideoStream = new LocalVideoStream[1];
                localVideoStream[0] = new LocalVideoStream(videoDeviceInfo);

                Uri localUri = await localVideoStream[0].MediaUriAsync();

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    LocalVideo.Source = localUri;
                    LocalVideo.Play();
                });

            }
            AcceptCallOptions acceptCallOptions = new AcceptCallOptions();
            acceptCallOptions.VideoOptions = new VideoOptions(localVideoStream);

            call = await incomingcall.AcceptAsync(acceptCallOptions);

        }

        private async void HangupButton_Click(object sender, RoutedEventArgs e)
        {
            // End the current call
            var hangUpOptions = new HangUpOptions();
            await call.HangUpAsync(hangUpOptions);
        }

    
    }
}

