using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyRecordTypes.Dtos
{
    public class BaseCompanyRecordTypeDto
    {
        /// <summary>
        /// The name of the company type.
        /// </summary>
        public string RecordType { get; set; }
    }

    public class GetCompanyRecordTypeDto : BaseCompanyRecordTypeDto
    {
        /// <summary>
        /// The unnique identifier of the company type.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Soft delete flag for the company type.
        /// </summary>
        public bool Deleted { get; set; }
    }

    public class AddCompanyRecordTypeDto : BaseCompanyRecordTypeDto
    {
    }

    public class EditCompanyRecordTypeDto : BaseCompanyRecordTypeDto
    {
        /// <summary>
        /// The unnique identifier of the company type.
        /// </summary>
        public int TypeId { get; set; }
    }
}
