using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Workflows
{
    public class WorkflowConstants
    {
        public static class Modules
        {
            public const string Leads = "Lead";
        }

        public static class ActionTypes
        {
            public const string Created = "Created";
            public const string Edited = "Edited";
            public const string Email = "Email";
            public const string Task = "Task";
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
