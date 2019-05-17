using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyAssignedAddresses.Dtos
{
    public class CompanyAssignedAddressDto
    {
        /// <summary>
        /// The address identifier.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// The company identifier.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// The description of the address.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// The city for this address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state for this address.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The zip code for this address.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The county/parish for this address.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// The country for this address.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The latitude of the address' location.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The longitude of the address' location.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Whether this address receives mail.
        /// </summary>
        public bool Mailing { get; set; }

        /// <summary>
        /// Flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
