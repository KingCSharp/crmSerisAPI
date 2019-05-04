using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace crmSeries.Core.Features.Workflows
{
    public class WorkflowConstants
    {
        public static class Modules
        {
            private static List<string> _validModules = new List<string>
            {
                Lead,
                Company,
                User
            };
            public static ReadOnlyCollection<string> ValidModules
            {
                get
                {
                    return _validModules.AsReadOnly();
                }
            }

            public const string Lead = "Lead";
            public const string Company = "Company";
            public const string User = "User";
        }

        public static class ActionTypes
        {
            private static readonly List<string> _validActionTypes = new List<string>
            {
                Created,
                Edited,
                Email,
                Task
            };
            public static ReadOnlyCollection<string> ValidActionTypes => _validActionTypes.AsReadOnly();

            public const string Created = "Created";
            public const string Edited = "Edited";
            public const string Email = "Email";
            public const string Task = "Task";
            public const string Entered = "Entered";
        }

        public static class Server
        {
            public const string BasePathKey = "Common:Server:BaseURL";
        }

        public static class Triggers
        {
            public const string Plus = "plus";
        }

        public static class Reminders
        {
            public const string None = "None";
        }
    }
}
