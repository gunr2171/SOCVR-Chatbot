﻿using CVChatbot.Bot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheCommonLibrary.Extensions;

namespace CVChatbot.Bot.ChatbotActions.Commands
{
    public class CurrentSession : UserCommand
    {
        public override void RunAction(ChatExchangeDotNet.Message userMessage, ChatExchangeDotNet.Room chatRoom)
        {
            using (CVChatBotEntities db = new CVChatBotEntities())
            {
                //find the latest open session for the user

                var latestOpenSession = db.ReviewSessions
                    .Where(x => x.RegisteredUser.ChatProfileId == userMessage.AuthorID)
                    .Where(x => x.SessionEnd == null)
                    .OrderByDescending(x => x.SessionStart)
                    .FirstOrDefault();

                if (latestOpenSession == null)
                {
                    chatRoom.PostReply(userMessage, "You don't have an ongoing review session on record.");
                    return;
                }

                var deltaTimeSpan = DateTimeOffset.Now - latestOpenSession.SessionStart;
                var formattedStartTs = latestOpenSession.SessionStart.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss 'UTC'");

                string message = "Your current review session started {0} ago at {1}"
                    .FormatInline(deltaTimeSpan.ToUserFriendlyString(), formattedStartTs);

                chatRoom.PostReply(userMessage, message);
            }
        }

        public override ActionPermissionLevel GetPermissionLevel()
        {
            return ActionPermissionLevel.Registered;
        }

        protected override string GetRegexMatchingPattern()
        {
            return @"^current session?$";
        }

        public override string GetCommandName()
        {
            return "Current Session";
        }

        public override string GetCommandDescription()
        {
            return "Tells if the user has an open session or not, and when it started";
        }

        public override string GetCommandUsage()
        {
            return "current session";
        }
    }
}
