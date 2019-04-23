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
        /// The company name of the contact for this lead.
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
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Cell { get; set; }
        public string Comments { get; set; }
        public string Address2 { get; set; }
        public string CompanyPhone { get; set; }
        public string Web { get; set; }
        public string Fax { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        [JsonIgnore]
        public string FirstName => Name?.Split(' ')[0];

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
