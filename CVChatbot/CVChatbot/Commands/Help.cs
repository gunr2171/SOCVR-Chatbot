﻿using ChatExchangeDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVChatbot.Commands
{
    public class Help : UserCommand
    {
        public override bool DoesInputTriggerCommand(Message userMessage)
        {
            return userMessage
                .GetContentsWithStrippedMentions()
                .ToLower()
                .Trim()
                == "help";
        }

        public override void RunCommand(Message userMessage, Room chatRoom)
        {
            string message = "This is a chat bot for the SO Close Vote Reviewers chat room, developed by [gunr2171](http://stackoverflow.com/users/1043380/gunr2171). For more information see the [github page](https://github.com/gunr2171/SOCVR-Chatbot).";
            chatRoom.PostMessage(message);
        }
    }
}