using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Deal
    {
        public int DealId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int ContactId { get; set; }
        public string Identifier { get; set; }
        public int StatusId { get; set; }
        public int PackageId { get; set; }
        public int ModelId { get; set; }
        public int InventoryId { get; set; }
        public int ConfigurationId { get; set; }
        public int EquipmentTypeId { get; set; }
        public int SaleTypeId { get; set; }
        public string CalculationType { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
        public string StockNo { get; set; }
        public string EquipYear { get; set; }
        public int Hours { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? QuotedDeliveryDate { get; set; }
        public DateTime? TargetDeliveryDate { get; set; }
        public DateTime? ProjectedDeliveryDate { get; set; }
        public int InboundBranchId { get; set; }
        public int DeliveryBranchId { get; set; }
        public decimal InboundFreight { get; set; }
        public decimal DeliveryFreight { get; set; }
        public decimal AttachmentFreight { get; set; }
        public decimal TradeInFreight { get; set; }
        public decimal TotalCost { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SalePriceAdjustment { get; set; }
        public decimal TradeOverallowance { get; set; }
        public decimal ActualSalePrice { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TotalSalesTax { get; set; }
        public decimal TradeValue { get; set; }
        public decimal DownPayment { get; set; }
        public decimal AmountToFinance { get; set; }
        public decimal FinalProfit { get; set; }
        public decimal FinalDiscount { get; set; }
        public decimal TargetProfitPercent { get; set; }
        public decimal MinimumProfitPercent { get; set; }
        public decimal FinalProfitPercent { get; set; }
        public decimal MaxDiscountPercent { get; set; }
        public decimal FinalDiscountPercent { get; set; }
        public string TaxableCity { get; set; }
        public string TaxableState { get; set; }
        public string TaxableZip { get; set; }
        public string TaxableCounty { get; set; }
        public string EndUse { get; set; }
        public bool Deleted { get; set; }
        public int TransactionTypeId { get; set; }
        public int BranchId { get; set; }
        public string NewUsed { get; set; }
        public bool Attachment { get; set; }
        public string FreightOrigin { get; set; }
        public decimal CustomerFreight { get; set; }
        public string Fob { get; set; }
        public int ShippingAddressId { get; set; }
        public bool Complete { get; set; }
        public bool Valid { get; set; }
        public bool Approved { get; set; }
        public bool Allocated { get; set; }
        public DateTimeOffset? AllocationDate { get; set; }
        public int AllocatedBy { get; set; }
        public decimal NetTradeAllowance { get; set; }
        public decimal FinancingField1 { get; set; }
        public decimal FinancingField2 { get; set; }
        public decimal FinancingField3 { get; set; }
        public decimal FinancingField4 { get; set; }
        public decimal FinancingField5 { get; set; }
        public bool Archived { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal Commission { get; set; }
        public decimal TradeHoldback { get; set; }
        public decimal CommissionPaid { get; set; }
        public decimal CommissionEarned { get; set; }
        public string DealSheetFile { get; set; }
    }
}
