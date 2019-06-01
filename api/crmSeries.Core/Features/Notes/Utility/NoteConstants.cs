using System.Collections.Generic;
using System.Data;

namespace crmSeries.Core.Features.Notes.Utility
{
    public static class NotesConstants
    {
        public static class ErrorMessages
        {
            public const string NoteNotFound = "No note with this id found.";
            public const string NoteTypeNotFound = "No note type with this id found.";
            public const string InvalidLatitude = "Invalid latitude value.  Latitudes must be between -90.0 and 90.0";
            public const string InvalidLongitude = "Invalid longitude value.  Longitudes must be between -180.0 and 180.0";
        }

        public static class NoteTypes
        {
            public const string Email = "Email";
            public const string FaceToFaceCall = "FaceToFaceCall";
            public const string PhoneCall = "PhoneCall";
            public const string SiteVisit = "SiteVisit";

            public static readonly IReadOnlyCollection<string> ValidNoteTypes = new List<string>
            {
                Email,
                FaceToFaceCall,
                PhoneCall,
                SiteVisit
            };
        }
    }
}
