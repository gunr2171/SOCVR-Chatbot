﻿using SOCVR.Chatbot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatExchangeDotNet;
using Microsoft.Data.Entity;

namespace SOCVR.Chatbot.ChatbotActions.Commands.Stats
{
    internal class TotalReviewsToday : UserCommand
    {
        public override string ActionDescription => "Shows summary information and a table of the people who have completed reviews today.";

        public override string ActionName => "Total Reviews Today";

        public override string ActionUsage => "total reviews today";

        public override PermissionGroup? RequiredPermissionGroup => PermissionGroup.Reviewer;

        public override bool UserMustBeInAnyPermissionGroupToRun => true;

        protected override string RegexMatchingPattern => "^total reviews today$";

        public override void RunAction(Message incomingChatMessage, Room chatRoom)
        {
            using (var db = new DatabaseContext())
            {
                var reviewsToday = db.ReviewedItems
                    .Include(x => x.Reviewer)
                    .Where(x => x.ReviewedOn.Date == DateTimeOffset.UtcNow.Date)
                    .ToList();

                var usersWhoHaveReviewedToday = reviewsToday
                    .GroupBy(x => x.Reviewer)
                    .Select(x => new
                    {
                        ReviewerProfileId = x.Key.ProfileId,
                        ReviewCount = x.Count()
                    })
                    .ToList();

                if (!usersWhoHaveReviewedToday.Any())
                {
                    chatRoom.PostReplyOrThrow(incomingChatMessage, "I have no record of any reviews from any tracked user today.");
                    return;
                }

                var singularPluralPhrases = new Dictionary<string, string>();
                singularPluralPhrases.Add("member has", "members have");
                singularPluralPhrases.Add("item", "items");

                var phrase_memberhas = usersWhoHaveReviewedToday.Count == 1
                    ? "member has"
                    : "members have";

                var phrase_item = usersWhoHaveReviewedToday.Count == 1
                    ? "item"
                    : "items";

                var totalReviewedItems = usersWhoHaveReviewedToday.Sum(x => x.ReviewCount);
                chatRoom.PostReplyOrThrow(incomingChatMessage,
                    $"Today, {usersWhoHaveReviewedToday.Count} {phrase_memberhas} reviewed a total of {totalReviewedItems} {phrase_item}.");

                var dataTable = usersWhoHaveReviewedToday.ToStringTable(
                    new[]
                    {
                        "User",
                        "Review Items Today"
                    },
                    x => chatRoom.GetUser(x.ReviewerProfileId).Name,
                    x => x.ReviewCount);

                chatRoom.PostMessageOrThrow(dataTable);
            }
        }
    }
}
