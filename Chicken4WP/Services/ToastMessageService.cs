using System;
using Chicken4WP.Controls;

namespace Chicken4WP.Services
{
    public class ToastMessageService
    {
        public void HandleMessage(string message)
        {
            HandleMessage(new ToastMessage(message));
        }

        public void HandleMessage(ToastMessage message)
        {
            var prompt = new ToastPrompt();
            prompt.Message = message.Message;
            if (message.Complete != null)
            {
                prompt.Completed +=
                    (o, e) =>
                    {
                        message.Complete();
                    };
            }
            prompt.Show();
        }
    }

    public class ToastMessage
    {
        public string Message { get; set; }

        public Action Complete { get; set; }

        public ToastMessage()
        { }

        public ToastMessage(string message)
        {
            this.Message = message;
        }
    }
}
