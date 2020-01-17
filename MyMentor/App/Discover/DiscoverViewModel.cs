using MyMentor.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyMentor.App.Discover
{
    public class DiscoverViewModel : MVVM.BaseViewModel
    {

        public ObservableCollection<CommunityEvent> CommunityEvents { get; set; }

        public ICommand ViewEventCommand { get; set; }
        public DiscoverViewModel()
        {
            CommunityEvents = new ObservableCollection<CommunityEvent>(InMemoryData.CommunityEvents);
            ViewEventCommand = new Command<CommunityEvent>(ViewEvent);
        }

        public void ViewEvent(CommunityEvent obj)
        {
            OnViewEvent(obj);
        }

        public Action<CommunityEvent> OnViewEvent = delegate { };
    }
}
