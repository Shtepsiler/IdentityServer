﻿using MassTransit.Configuration;

namespace BLL.MessageBroker
{
    public class MessageBrokerSettings
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
