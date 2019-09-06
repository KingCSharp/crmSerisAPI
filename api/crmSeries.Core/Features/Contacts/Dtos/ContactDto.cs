using System;
using System.Collections.Generic;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.Tasks.Dtos;

namespace crmSeries.Core.Features.Contacts.Dtos
{
    public class BaseContactDto
    {
        /// <summary>
        /// The company identifier.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// First name for the contact.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name for the contact.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Last name for the contact.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The nickname/alias for the contact.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// The contact's phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The contact's cell phone number.
        /// </summary>
        public string Cell { get; set; }

        /// <summary>
        /// The contact's fax number.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// The contact's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The contact's title. E.g., Mr., Mrs., etc.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The contact's position in their company.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// The department the contact works in.
        /// </summary>
        public string Department { get; set; }
    }

    public class GetContactDto : BaseContactDto
    {
        /// <summary>
        /// The contact identifier.
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// The company name associated with this contact.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The account number for the company associated with this contact.
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// The flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The flag for whether this contact is active or not.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Determines if the contact is a favorite for this user.
        /// </summary>
        public bool Favorite { get; set; }

        /// <summary>
        /// The date, if any, that the entity was modified.
        /// </summary>
        public DateTime? LastModified { get; set; }
    }

    public class GetFullContactDto 
    {
        /// <summary>
        /// The details of the task.
        /// </summary>
        public GetContactDto Details { get; set; }

        /// <summary>
        /// The tasks, if any, associated with this contact.
        /// </summary>
        public IEnumerable<GetTaskDto> Tasks { get; set; }

        /// <summary>
        /// The notes, if any, associated with this contact.
        /// </summary>
        public IEnumerable<GetNoteDto> Notes { get; set; }
    }
    
    public class AddContactDto : BaseContactDto
    {
    }

    public class EditContactDto : BaseContactDto
    {
        /// <summary>
        /// The contact identifier.
        /// </summary>
        public int ContactId { get; set; }

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
