using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.OutputTemplateFields.Dtos
{
    public class BaseOutputTemplateFieldDto
    {
        /// <summary>
        /// The unique identifier of the OutputTemplate record this field is for.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// The name of the field on the template.
        /// </summary>
        public string TemplateField { get; set; }

        /// <summary>
        /// The type of field. E.g., textbox, checkbox, date, etc.
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// The entity field or fields that make the value of this field. E.g., [City] + " " + [State] + " " + [Zip]
        /// </summary>
        public string CrmSeriesField { get; set; }

        /// <summary>
        /// Used to denote if the CrmSeriesField is a mathematical calculation. E.g., [NetSellPrice] + [SalesTax]
        /// </summary>
        public bool Calculation { get; set; }
    }

    public class GetOutputTemplateFieldDto : BaseOutputTemplateFieldDto
    {
        /// <summary>
        /// The unnique identifier of the output template field.
        /// </summary>
        public int FieldId { get; set; }
    }

    public class AddOutputTemplateFieldDto : BaseOutputTemplateFieldDto
    {
    }

    public class EditOutputTemplateFieldDto : BaseOutputTemplateFieldDto
    {
        /// <summary>
        /// The unnique identifier of the output template field.
        /// </summary>
        public int FieldId { get; set; }
    }
}
