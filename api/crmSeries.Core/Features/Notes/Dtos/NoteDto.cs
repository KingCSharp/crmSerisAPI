using System;

namespace crmSeries.Core.Features.Notes.Dtos
{
    public class BaseNoteDto
    {
        /// <summary>
        /// The Id of the related record.
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// The record type of the note.
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// The user Id of the note.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The type Id of the note.
        /// </summary>
        public int RecordTypeId { get; set; }

        /// <summary>
        /// The date the note was created
        /// </summary>
        public DateTime NoteDate { get; set; }

        /// <summary>
        /// The comments associated with the note.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The latitude of user when the note was modified.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The longitude of user when the note was modified.
        /// </summary>
        public decimal Longitude { get; set; }
    }

    public class GetNoteDto : BaseNoteDto
    {
        /// <summary>
        /// The note identifier.
        /// </summary>
        public int NoteId { get; set; }

        /// <summary>
        /// The flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The flag for whether this contact is active or not.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The date, if any, that the entity was modified.
        /// </summary>
        public DateTime? LastModified { get; set; }
    }

    public class AddNoteDto : BaseNoteDto
    {

    }

    public class EditNoteDto : BaseNoteDto
    {
        /// <summary>
        /// The note identifier
        /// </summary>
        public int NoteId { get; set; }

        /// <summary>
        /// The flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The flag for whether this contact is active or not.
        /// </summary>
        public bool Active { get; set; }
    }
}
