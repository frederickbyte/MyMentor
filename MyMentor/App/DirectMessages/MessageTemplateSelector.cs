using MyMentor.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyMentor.App.DirectMessages
{
    public class MessageTemplateSelector : DataTemplateSelector
    {

        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;
        public MessageTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DirectMessage messageVm = item as DirectMessage;
            if (messageVm == null)
            {
                return null;
            }
            if (MVVM.BaseViewModel.UserInfo != null)
            {
                return (messageVm.SenderId == MVVM.BaseViewModel.UserInfo.User.UserId) ? outgoingDataTemplate : incomingDataTemplate;
            }
            return outgoingDataTemplate;
        }
    }
}