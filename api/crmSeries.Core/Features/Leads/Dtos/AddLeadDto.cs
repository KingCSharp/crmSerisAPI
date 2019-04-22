using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;

namespace crmSeries.Core.Features.Leads.Dtos
{
    public class AddLeadDto
    {
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
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

        public string FirstName => Name.Split(' ')[0];

        public string LastName
        {
            get
            {
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
