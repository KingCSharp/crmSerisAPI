using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Active { get; set; }
        public bool Internal { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Cell { get; set; }
        public string Avatar { get; set; }
        public string DefaultCountry { get; set; }
        public string DefaultLanguage { get; set; }
        public string DefaultSearchType { get; set; }
        public string TimeZone { get; set; }
        public string RecordAccessType { get; set; }
        public string RecordAccessFilter { get; set; }
        public string RecordAccessRules { get; set; }
        public string UserAccessType { get; set; }
        public string UserAccessFilter { get; set; }
        public string UserAccessRules { get; set; }
        public bool RecurringTouchAlertEnabled { get; set; }
        public int RecurringTouchAlertDays { get; set; }
        public bool TaskAlertEnabled { get; set; }
        public int TaskAlertDays { get; set; }
        public bool NotifyNote { get; set; }
        public bool EmailNote { get; set; }
        public bool Deleted { get; set; }
    }
}
