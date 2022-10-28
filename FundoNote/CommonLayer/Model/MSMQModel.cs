 using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\Tokens";
            if(!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);
            }   
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            messageQ.Send(token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string Subject = "FundoNotes Reset Link";
            string Body = $"Fundoo Notes Reset Password: <a href=https://localhost:4200/User/ResetPassword/{token}> Click Here</a>";

            //


            //MailMessage message = new MailMessage();
            //message.From = new MailAddress("rajhindustani959@gmail.com");
            //message.To.Add("rajhindustani959@gmail.com");

            //message.Subject = "subject";
            //message.IsBodyHtml = true;
            //string htmlBody;

            //htmlBody = "Write some HTML Code here";

            ////HTML tags to show some message in mail
            //message.Body = "<body><p>Dear User,<br><br>" +
            //    "Please check the link below for reset password.<br>" +
            //    "Please copy the code and paste it in your swagger authentication.</body>" + token;

            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port= 587,
                Credentials = new NetworkCredential("rajhindustani959@gmail.com","ejxbhgibrryybrma"),
                EnableSsl = true,
            };
            SMTP.Send("rajhindustani959@gmail.com", "rajhindustani959@gmail.com", Subject,Body);
            messageQ.BeginReceive();
        }
    }
}
 