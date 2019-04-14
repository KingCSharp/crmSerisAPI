using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class SalesTax
    {
        public int TaxId { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public decimal StateSalesTax { get; set; }
        public decimal StateTaxableMax { get; set; }
        public decimal StateTaxOverMax { get; set; }
        public decimal CountySalesTax { get; set; }
        public decimal CountyTaxableMax { get; set; }
        public decimal CountyTaxOverMax { get; set; }
        public decimal CitySalesTax { get; set; }
        public decimal CityTaxableMax { get; set; }
        public decimal CityTaxOverMax { get; set; }
        public decimal Other1SalesTax { get; set; }
        public decimal Other1TaxableMax { get; set; }
        public decimal Other1TaxOverMax { get; set; }
        public decimal Other2SalesTax { get; set; }
        public decimal Other2TaxableMax { get; set; }
        public decimal Other2TaxOverMax { get; set; }
        public decimal Other3SalesTax { get; set; }
        public decimal Other3TaxableMax { get; set; }
        public decimal Other3TaxOverMax { get; set; }
        public decimal Other4SalesTax { get; set; }
        public decimal Other4TaxableMax { get; set; }
        public decimal Other4TaxOverMax { get; set; }
        public decimal Other5SalesTax { get; set; }
        public decimal Other5TaxableMax { get; set; }
        public decimal Other5TaxOverMax { get; set; }
    }
}
