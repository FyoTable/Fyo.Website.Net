using SparkPost;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Fyo.Models;
using Fyo.Interfaces;

namespace Fyo.Services {

    public class EmailService : IEmailService {
        private IConfiguration _configuration;

        public EmailService(IConfiguration Configuration) {
            _configuration = Configuration;
        }

        public void Send(string[] to, string subject, string body, string text = null) {
            var emailConfiguration = _configuration.GetSection("Email");
            
            var transmission = new Transmission();
            transmission.Content.From.Email = emailConfiguration["From"];
            transmission.Content.Subject = subject;
            transmission.Content.Text = text;
            transmission.Content.Html = body;

            foreach(var email in to) {
                transmission.Recipients.Add(new Recipient
                {
                    Address = new Address { Email = email }
                });
            }

            var client = new Client(emailConfiguration["SparkPost"]);
            client.Transmissions.Send(transmission);
        }
    }
}