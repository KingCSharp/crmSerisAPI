using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace crmSeries.Core.Features.Leads.Dtos
{
    /// <summary>
    /// The DTO object representing the data to add a lead.
    /// </summary>
    public class AddLeadDto
    {
        /// <summary>
        /// The company name of the contact for this lead.  Maximum length: 100 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The leads name.  Required field.  Maximum length: 100
        /// <remarks>
        /// The API will convert this to firstName and lastName in the database if possible.
        /// If only one name is given, it will populate the firstName column of your database only.
        /// </remarks>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The lead's phone number.  Maximum length: 20
        /// <remarks>
        /// Must be a valid US phone number or start with a ‘+’ and a country code for international
        /// numbers. 
        ///
        /// Required if a valid email is NOT submitted within the request body.
        /// </remarks>
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The lead's E-Mail.  Maximum length: 100
        /// <remarks>
        /// Must be a valid E-Mail address.
        ///
        /// Required if a valid phone is NOT submitted within the request body.
        /// </remarks>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The description of the lead.  Maximum length: 250
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The street address for the company of the contact for the lead.  Maximum length: 100
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// The extra address information for the company of the contact for the lead.  Maximum length: 100
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The city for the company of the contact for the lead.  Maximum length: 50
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state for the company of the contact for the lead.  Maximum length: 50
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The zip code for the company of the contact for this lead.  Maximum length: 10
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The county/parish for the company of the contact for this lead.  Maximum length: 50
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// The country for the company of the contact for this lead.  Maximum length: 25
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The cell phone number of the contact for this lead.  Maximum length: 20
        /// <remarks>
        /// Must be a valid US phone number or start with a ‘+’ and a country code for international
        /// numbers. 
        /// </remarks>
        /// </summary>
        public string Cell { get; set; }

        /// <summary>
        /// Comments submitted by the contact for this lead.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The phone number for the company of the contact for this lead.  Maximum length: 100
        /// <remarks>
        /// Must be a valid US phone number or start with a ‘+’ and a country code for international
        /// numbers. 
        /// </remarks>
        /// </summary>
        public string CompanyPhone { get; set; }

        /// <summary>
        /// The web site URL for the company of the contact for this lead.  Maximum length: 200
        /// <remarks>
        /// Must be a valid, well-formed URI string.
        /// </remarks>
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// The fax number for the company of the contact for this lead.  Maximum length: 50
        /// <remarks>
        /// Must be a valid US phone number or start with a ‘+’ and a country code for international
        /// numbers. 
        /// </remarks>
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// The title of the contact for this lead (e.g., Mr., Mrs., Dr., etc.)  Maximum length: 10
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The name of the position in the company of the contact for this lead.  Maximum length: 100
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// The department of the company that the contact for this lead works in.  Maximum length: 100
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// The E-Mail address of the person who will own this lead.
        /// </summary>
        public string OwnerEmail { get; set; }
        
        /// <summary>
        /// The source of the lead.  This field is not case sensitive.  If the value provided does
        /// exist, the source of the lead will default to your default external lead source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The first name of the contact for this lead.  Maximum length: 50
        /// </summary>
        [JsonIgnore]
        public string FirstName => Name?.Split(' ')[0];

        /// <summary>
        /// The last name of the contact for this lead.  Maximum length: 50
        /// </summary>
        [JsonIgnore]
        public string LastName
        {
            get
            {
                if (Name == null) return null;

                var @strings = Regex.Replace(Name, @"\s+", " ")
                    .Split(' ');

                if (@strings.Length >= 2)
                {
                    var lastName = string.Join(" ", @strings.Skip(1)
                        .Select(x => x.Trim()));

                    if (string.IsNullOrWhiteSpace(lastName))
                        return null;

                    return lastName;
                }
                return null;
            }
        }
    }
}
