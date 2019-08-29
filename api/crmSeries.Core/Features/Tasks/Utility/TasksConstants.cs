using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace crmSeries.Core.Features.Tasks.Utility
{
    public static class TasksConstants
    {
        public static class ErrorMessages
        {
            public const string TaskNotFound = "No task with this id found.";

            public static readonly string InvalidStatus =
                $"Invalid Task status.  Valid task status are {Statuses.ValidStatusesFlattened}";

            public static readonly string InvalidPriority =
                $"Invalid Task priority.  Valid priorities are {Priorities.ValidPrioritiesFlattened}";

            public static readonly string CantUpdateStatus =
                "You cannot update a task status on a completed task";
        }

        public static class Statuses
        {
            public const string InProcess = "In Process";
            public const string NotStarted = "Not Started";
            public const string Completed = "Completed";

            public static readonly IReadOnlyList<string> ValidStatuses = new List<string>()
            {
                InProcess,
                NotStarted,
                Completed
            };

            public static string ValidStatusesFlattened = ValidStatuses.Join(",");
        }

        public static class Priorities
        {
            public const string Normal = "Normal";
            public const string Medium = "Medium";
            public const string High = "High";

            public static readonly IReadOnlyList<string> ValidPriorities = new List<string>()
            {
                Normal,
                Medium,
                High
            };
            public static string ValidPrioritiesFlattened = ValidPriorities.Join(",");
        }

        public const int MaxRelatedRecordTypeLength = 50;
        public const int MaxSubjectLength = 100;
        public const int MaxStatusLength = 50;
        public const int MaxPriorityLength = 50;
        public const int MaxReminderRepeatScheduleLength = 50;
        public const int MaxEventIdLength = 250;
        public const int MaxCalendarIdLength = 250;
    }
}
