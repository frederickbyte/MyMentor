using MyMentor.Accounts;
using MyMentor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json.Serialization;
using Xamarin.Forms;
using User = MyMentor.Models.User;

namespace MyMentor.App.DirectMessages
{
    public class DirectMessagesViewModel : MVVM.BaseViewModel
    {
        private RestService _restService;
        public ObservableCollection<UserInfoWithAvatar> Matches { get; set; }

        public ObservableCollection<DirectMessage> Messages { get; set; }
        public List<DirectMessage> AllMessagesForUser { get; set; }

        public ICommand ViewMessagesCommand { get; set; }

        public ICommand OnSendCommand { get; set; }
        public DirectMessagesViewModel(List<UserInfoWithAvatar> matches)
        {
            AllMessagesForUser = new List<DirectMessage>();
            Messages = new ObservableCollection<DirectMessage>();
            _restService = new RestService();
            Matches = new ObservableCollection<UserInfoWithAvatar>(matches);
            //LoadMessages();
            OnSendCommand = new Command(SendMessage);
            SetupNotificationTimer();
            //ViewMessagesCommand = new Command<UserInfoWithAvatar>(ViewMessagesForSelectedMatch);
            SelectedConnection = Matches[0];
        }

        public void ViewMessagesForSelectedMatch(UserInfoWithAvatar match)
        {
            Messages.Clear();
            if (match != null)
            {
                AllMessagesForUser.Where(m => m.SenderId == match.UserInfo.User.UserId || m.RecipientId == match.UserInfo.User.UserId)
                    .OrderBy(m => m.LastUpdateTime).ToList().ForEach(m => Messages.Add(m));
            }
        }

        public async void SendMessage()
        {
            if (!string.IsNullOrEmpty(TextToSend))
            {
                // create the new message to send
                var newMessage = new DirectMessage{Id = Guid.NewGuid(), 
                    SenderId = UserInfo.User.UserId,
                    RecipientId = SelectedConnection.UserInfo.User.UserId,
                    Message = TextToSend,
                    LastUpdateTime = DateTime.Now};
                await _restService.SendMessage(newMessage);
//                Messages.Add(newMessage);
                TextToSend = string.Empty;
            }
        }

        // Setup the polling timer for getting notifications.
        private void SetupNotificationTimer()
        {
            // Start a timer that runs after 5 seconds.
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Factory.StartNew(async () =>
                {
                    // Do the actual request and wait for it to finish.
                    var notifications = await _restService.GetAllMessagesForUser(UserInfo.User.UserId);
                    // Switch back to the UI thread to update the UI
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        // Update the UI
                        if (notifications.Any())
                        {
                            // get list of current IDs to prevent adding duplicate notifications
                            AllMessagesForUser = notifications.ToList();
                            ViewMessagesForSelectedMatch(SelectedConnection);
                        }
                        // Now repeat by scheduling a new request
                        SetupNotificationTimer();
                    });
                });
                // Don't repeat the timer (we will start a new timer when the request is finished)
                return false;
            });
        }

        public void LoadMessages()
        {
            //var allMessages = await _restService.GetAllMessagesForUser(UserInfo.User.UserId);
            //foreach(var message in allMessages)
            //{
            //    AllMessagsForUser.Add(message);
            //}
            var otherPersonId = Guid.NewGuid();
            Messages.Add(new DirectMessage
            {
                Id = Guid.NewGuid(),
                SenderId = otherPersonId.ToString(),
                RecipientId = UserInfo.User.UserId,
                Message = "Hello Andrew, how was class today?",
                LastUpdateTime = DateTime.Now
            });
            Messages.Add(new DirectMessage
            {
                Id = Guid.NewGuid(),
                SenderId = UserInfo.User.UserId,
                RecipientId = otherPersonId.ToString(),
                Message = "Hey Johnny! It was good. About to head home.",
                LastUpdateTime = DateTime.Now.AddHours(1)
            });
            Messages.Add(new DirectMessage
            {
                Id = Guid.NewGuid(),
                SenderId = otherPersonId.ToString(),
                RecipientId = UserInfo.User.UserId,
                Message = "Cool, see you tomorrow!",
                LastUpdateTime = DateTime.Now.AddHours(2)
            });

        }

        private UserInfoWithAvatar _selectedConnection;
        public UserInfoWithAvatar SelectedConnection
        {
            get { return _selectedConnection; }
            set
            {
                _selectedConnection = value;
                OnPropertyChanged("SelectedConnection");
                ViewMessagesForSelectedMatch(value);
            }
        }

        private string _textToSend;
        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                OnPropertyChanged("TextToSend");
            }
        }
    }
}
