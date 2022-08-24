﻿using Experimental.System.Messaging;
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
            string Body = token;

            MailMessage message = new MailMessage();
            message.From = new MailAddress("rajhindustani959@gmail.com");
            message.To.Add("rajhindustani959@gmail.com");
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port= 587,
                Credentials = new NetworkCredential("rajhindustani959@gmail.com","ejxbhgibrryybrma"),
                EnableSsl = true,
            };
            SMTP.Send("rajhindustani959@gmail.com", "rajhindustani959@gmail.com", Subject, Body);
            messageQ.BeginReceive();
        }
    }
}
 