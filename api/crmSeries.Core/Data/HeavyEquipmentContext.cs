using crmSeries.Core.Domain.HeavyEquipment;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace crmSeries.Core.Data
{
    public class HeavyEquipmentContext : DbContext
    {
        public HeavyEquipmentContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<BranchAssignedUser> BranchAssignedUser { get; set; }
        public virtual DbSet<Broker> Broker { get; set; }
        public virtual DbSet<BrokerAssignedDistribution> BrokerAssignedDistribution { get; set; }
        public virtual DbSet<BrokerDistributionList> BrokerDistributionList { get; set; }
        public virtual DbSet<BusinessPlan> BusinessPlan { get; set; }
        public virtual DbSet<BusinessPlanTactic> BusinessPlanTactic { get; set; }
        public virtual DbSet<BusinessPlanType> BusinessPlanType { get; set; }
        public virtual DbSet<Call> Call { get; set; }
        public virtual DbSet<CallAssignedPurpose> CallAssignedPurpose { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyAssignedAddress> CompanyAssignedAddress { get; set; }
        public virtual DbSet<CompanyAssignedAr> CompanyAssignedAr { get; set; }
        public virtual DbSet<CompanyAssignedBusinessPlan> CompanyAssignedBusinessPlan { get; set; }
        public virtual DbSet<CompanyAssignedBusinessPlanTactic> CompanyAssignedBusinessPlanTactic { get; set; }
        public virtual DbSet<CompanyAssignedCategory> CompanyAssignedCategory { get; set; }
        public virtual DbSet<CompanyAssignedRank> CompanyAssignedRank { get; set; }
        public virtual DbSet<CompanyAssignedRevenue> CompanyAssignedRevenue { get; set; }
        public virtual DbSet<CompanyAssignedUser> CompanyAssignedUser { get; set; }
        public virtual DbSet<CompanyCategory> CompanyCategory { get; set; }
        public virtual DbSet<CompanyRank> CompanyRank { get; set; }
        public virtual DbSet<CompanyRecordType> CompanyRecordType { get; set; }
        public virtual DbSet<CompanySource> CompanySource { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactAssignedAddress> ContactAssignedAddress { get; set; }
        public virtual DbSet<ContactAssignedEmail> ContactAssignedEmail { get; set; }
        public virtual DbSet<ContactAssignedPhone> ContactAssignedPhone { get; set; }
        public virtual DbSet<ContactDepartment> ContactDepartment { get; set; }
        public virtual DbSet<ContactPosition> ContactPosition { get; set; }
        public virtual DbSet<ContactTitle> ContactTitle { get; set; }
        public virtual DbSet<CrmSeriesConnectConfiguration> CrmSeriesConnectConfiguration { get; set; }
        public virtual DbSet<CrmSeriesConnectLog> CrmSeriesConnectLog { get; set; }
        public virtual DbSet<CrmSeriesConnectQuery> CrmSeriesConnectQuery { get; set; }
        public virtual DbSet<DashboardWidget> DashboardWidget { get; set; }
        public virtual DbSet<DashboardWidgetDataSource> DashboardWidgetDataSource { get; set; }
        public virtual DbSet<DashboardWidgetGroup> DashboardWidgetGroup { get; set; }
        public virtual DbSet<DataImportJob> DataImportJob { get; set; }
        public virtual DbSet<DataImportJobLog> DataImportJobLog { get; set; }
        public virtual DbSet<Deal> Deal { get; set; }
        public virtual DbSet<DealApprovalTrigger> DealApprovalTrigger { get; set; }
        public virtual DbSet<DealApprovalType> DealApprovalType { get; set; }
        public virtual DbSet<DealAssignedApprovalHistory> DealAssignedApprovalHistory { get; set; }
        public virtual DbSet<DealAssignedApprover> DealAssignedApprover { get; set; }
        public virtual DbSet<DealAssignedAttachment> DealAssignedAttachment { get; set; }
        public virtual DbSet<DealAssignedCategory> DealAssignedCategory { get; set; }
        public virtual DbSet<DealAssignedCharge> DealAssignedCharge { get; set; }
        public virtual DbSet<DealAssignedDiscount> DealAssignedDiscount { get; set; }
        public virtual DbSet<DealAssignedDocument> DealAssignedDocument { get; set; }
        public virtual DbSet<DealAssignedOption> DealAssignedOption { get; set; }
        public virtual DbSet<DealAssignedPayment> DealAssignedPayment { get; set; }
        public virtual DbSet<DealAssignedRecordLimit> DealAssignedRecordLimit { get; set; }
        public virtual DbSet<DealAssignedReserve> DealAssignedReserve { get; set; }
        public virtual DbSet<DealAssignedSalesTax> DealAssignedSalesTax { get; set; }
        public virtual DbSet<DealAssignedTradeIn> DealAssignedTradeIn { get; set; }
        public virtual DbSet<DealAssignedWarranty> DealAssignedWarranty { get; set; }
        public virtual DbSet<DealAssignedWorkOrder> DealAssignedWorkOrder { get; set; }
        public virtual DbSet<DealCalculationType> DealCalculationType { get; set; }
        public virtual DbSet<DealConfigurationModule> DealConfigurationModule { get; set; }
        public virtual DbSet<DealCount> DealCount { get; set; }
        public virtual DbSet<DealDefault> DealDefault { get; set; }
        public virtual DbSet<DealDocumentType> DealDocumentType { get; set; }
        public virtual DbSet<DealLog> DealLog { get; set; }
        public virtual DbSet<DealOutputLog> DealOutputLog { get; set; }
        public virtual DbSet<DealReserveCalculation> DealReserveCalculation { get; set; }
        public virtual DbSet<DealStatus> DealStatus { get; set; }
        public virtual DbSet<DealThreshold> DealThreshold { get; set; }
        public virtual DbSet<DealUdf> DealUdf { get; set; }
        public virtual DbSet<DealUdfassignedValue> DealUdfassignedValue { get; set; }
        public virtual DbSet<DealUdflookup> DealUdflookup { get; set; }
        public virtual DbSet<DealUserAccess> DealUserAccess { get; set; }
        public virtual DbSet<DealUserAccessFilter> DealUserAccessFilter { get; set; }
        public virtual DbSet<DealUserApproval> DealUserApproval { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentAssignedGet> EquipmentAssignedGet { get; set; }
        public virtual DbSet<EquipmentAssignedImage> EquipmentAssignedImage { get; set; }
        public virtual DbSet<EquipmentAssignedInventoryCost> EquipmentAssignedInventoryCost { get; set; }
        public virtual DbSet<EquipmentAssignedInventoryOption> EquipmentAssignedInventoryOption { get; set; }
        public virtual DbSet<EquipmentAssignedPmhistory> EquipmentAssignedPmhistory { get; set; }
        public virtual DbSet<EquipmentAssignedPmlabor> EquipmentAssignedPmlabor { get; set; }
        public virtual DbSet<EquipmentAssignedPmpart> EquipmentAssignedPmpart { get; set; }
        public virtual DbSet<EquipmentAssignedPmservice> EquipmentAssignedPmservice { get; set; }
        public virtual DbSet<EquipmentAssignedTracking> EquipmentAssignedTracking { get; set; }
        public virtual DbSet<EquipmentAssignedWarranty> EquipmentAssignedWarranty { get; set; }
        public virtual DbSet<EquipmentAssignedWorkOrder> EquipmentAssignedWorkOrder { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public virtual DbSet<EquipmentComponent> EquipmentComponent { get; set; }
        public virtual DbSet<EquipmentComponentAssignedRating> EquipmentComponentAssignedRating { get; set; }
        public virtual DbSet<EquipmentComponentCriteria> EquipmentComponentCriteria { get; set; }
        public virtual DbSet<EquipmentComponentHistory> EquipmentComponentHistory { get; set; }
        public virtual DbSet<EquipmentComponentProfile> EquipmentComponentProfile { get; set; }
        public virtual DbSet<EquipmentComponentRating> EquipmentComponentRating { get; set; }
        public virtual DbSet<EquipmentComponentType> EquipmentComponentType { get; set; }
        public virtual DbSet<EquipmentPminterval> EquipmentPminterval { get; set; }
        public virtual DbSet<EquipmentPmlabor> EquipmentPmlabor { get; set; }
        public virtual DbSet<EquipmentPmpart> EquipmentPmpart { get; set; }
        public virtual DbSet<EquipmentPmprofile> EquipmentPmprofile { get; set; }
        public virtual DbSet<EquipmentPmschedule> EquipmentPmschedule { get; set; }
        public virtual DbSet<EquipmentPmscheduleNote> EquipmentPmscheduleNote { get; set; }
        public virtual DbSet<EquipmentRentalHistory> EquipmentRentalHistory { get; set; }
        public virtual DbSet<EquipmentSalesAttachment> EquipmentSalesAttachment { get; set; }
        public virtual DbSet<EquipmentSalesCharge> EquipmentSalesCharge { get; set; }
        public virtual DbSet<EquipmentSalesConfiguration> EquipmentSalesConfiguration { get; set; }
        public virtual DbSet<EquipmentSalesConfigurationItem> EquipmentSalesConfigurationItem { get; set; }
        public virtual DbSet<EquipmentSalesDealsheetField> EquipmentSalesDealsheetField { get; set; }
        public virtual DbSet<EquipmentSalesDefaultSelection> EquipmentSalesDefaultSelection { get; set; }
        public virtual DbSet<EquipmentSalesDiscount> EquipmentSalesDiscount { get; set; }
        public virtual DbSet<EquipmentSalesFreight> EquipmentSalesFreight { get; set; }
        public virtual DbSet<EquipmentSalesManufacturer> EquipmentSalesManufacturer { get; set; }
        public virtual DbSet<EquipmentSalesModel> EquipmentSalesModel { get; set; }
        public virtual DbSet<EquipmentSalesModelDiscount> EquipmentSalesModelDiscount { get; set; }
        public virtual DbSet<EquipmentSalesModelGroup> EquipmentSalesModelGroup { get; set; }
        public virtual DbSet<EquipmentSalesModelItem> EquipmentSalesModelItem { get; set; }
        public virtual DbSet<EquipmentSalesModelItemRelatedComponent> EquipmentSalesModelItemRelatedComponent { get; set; }
        public virtual DbSet<EquipmentSalesModelWarranty> EquipmentSalesModelWarranty { get; set; }
        public virtual DbSet<EquipmentSalesRelatedRecordSelection> EquipmentSalesRelatedRecordSelection { get; set; }
        public virtual DbSet<EquipmentSalesSpec> EquipmentSalesSpec { get; set; }
        public virtual DbSet<EquipmentSalesWarranty> EquipmentSalesWarranty { get; set; }
        public virtual DbSet<EquipmentSalesWorkOrder> EquipmentSalesWorkOrder { get; set; }
        public virtual DbSet<EquipmentSmuhistory> EquipmentSmuhistory { get; set; }
        public virtual DbSet<EquipmentUccomponent> EquipmentUccomponent { get; set; }
        public virtual DbSet<EquipmentUchistory> EquipmentUchistory { get; set; }
        public virtual DbSet<EquipmentUcreading> EquipmentUcreading { get; set; }
        public virtual DbSet<EquipmentUcreadingMeasurement> EquipmentUcreadingMeasurement { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventAssignedContact> EventAssignedContact { get; set; }
        public virtual DbSet<EventAssignedReminder> EventAssignedReminder { get; set; }
        public virtual DbSet<Inspection> Inspection { get; set; }
        public virtual DbSet<InspectionGroup> InspectionGroup { get; set; }
        public virtual DbSet<InspectionImage> InspectionImage { get; set; }
        public virtual DbSet<InspectionItem> InspectionItem { get; set; }
        public virtual DbSet<InspectionResponse> InspectionResponse { get; set; }
        public virtual DbSet<InspectionType> InspectionType { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceByMonth> InvoiceByMonth { get; set; }
        public virtual DbSet<Lead> Lead { get; set; }
        public virtual DbSet<LeadStatus> LeadStatus { get; set; }
        public virtual DbSet<LeadUserAccess> LeadUserAccess { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<MessageAddress> MessageAddress { get; set; }
        public virtual DbSet<MessageFile> MessageFile { get; set; }
        public virtual DbSet<MessageFolder> MessageFolder { get; set; }
        public virtual DbSet<MessageHeader> MessageHeader { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<NoteAssignedPurpose> NoteAssignedPurpose { get; set; }
        public virtual DbSet<NotePurpose> NotePurpose { get; set; }
        public virtual DbSet<NotePurposeAlert> NotePurposeAlert { get; set; }
        public virtual DbSet<NotePurposeAlertFilter> NotePurposeAlertFilter { get; set; }
        public virtual DbSet<NotePurposeAssignedRole> NotePurposeAssignedRole { get; set; }
        public virtual DbSet<NoteType> NoteType { get; set; }
        public virtual DbSet<NotificationServiceConnection> NotificationServiceConnection { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<Opportunity> Opportunity { get; set; }
        public virtual DbSet<OpportunityCategory> OpportunityCategory { get; set; }
        public virtual DbSet<OpportunitySaleType> OpportunitySaleType { get; set; }
        public virtual DbSet<OpportunityStatus> OpportunityStatus { get; set; }
        public virtual DbSet<OpportunityTransactionType> OpportunityTransactionType { get; set; }
        public virtual DbSet<OutputTemplate> OutputTemplate { get; set; }
        public virtual DbSet<OutputTemplateCategory> OutputTemplateCategory { get; set; }
        public virtual DbSet<ProspectTimeline> ProspectTimeline { get; set; }
        public virtual DbSet<RecordAssignedFile> RecordAssignedFile { get; set; }
        public virtual DbSet<RecordAssignedInspection> RecordAssignedInspection { get; set; }
        public virtual DbSet<RecordAssignedInspectionGroup> RecordAssignedInspectionGroup { get; set; }
        public virtual DbSet<RecordAssignedInspectionImage> RecordAssignedInspectionImage { get; set; }
        public virtual DbSet<RecordAssignedInspectionItem> RecordAssignedInspectionItem { get; set; }
        public virtual DbSet<RecordAssignedInspectionItemResponse> RecordAssignedInspectionItemResponse { get; set; }
        public virtual DbSet<RecordLog> RecordLog { get; set; }
        public virtual DbSet<RecurringTouchAssignment> RecurringTouchAssignment { get; set; }
        public virtual DbSet<RecurringTouchHistory> RecurringTouchHistory { get; set; }
        public virtual DbSet<RecurringTouchProfile> RecurringTouchProfile { get; set; }
        public virtual DbSet<RecurringTouchProfileAction> RecurringTouchProfileAction { get; set; }
        public virtual DbSet<RecurringTouchProfileValue> RecurringTouchProfileValue { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RegionAssignedBranch> RegionAssignedBranch { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportDistribution> ReportDistribution { get; set; }
        public virtual DbSet<ReportSchedule> ReportSchedule { get; set; }
        public virtual DbSet<ReportScheduleExecutionHistory> ReportScheduleExecutionHistory { get; set; }
        public virtual DbSet<RequiredFieldOption> RequiredFieldOption { get; set; }
        public virtual DbSet<SalesTax> SalesTax { get; set; }
        public virtual DbSet<SalesTaxCustomField> SalesTaxCustomField { get; set; }
        public virtual DbSet<ServiceQuote> ServiceQuote { get; set; }
        public virtual DbSet<ServiceQuoteAssignedPart> ServiceQuoteAssignedPart { get; set; }
        public virtual DbSet<SystemDefault> SystemDefault { get; set; }
        public virtual DbSet<SystemEmail> SystemEmail { get; set; }
        public virtual DbSet<SystemLanguage> SystemLanguage { get; set; }
        public virtual DbSet<Core.Domain.HeavyEquipment.Task> Task { get; set; }
        public virtual DbSet<Core.Domain.HeavyEquipment.Thread> Thread { get; set; }
        public virtual DbSet<TradeValuation> TradeValuation { get; set; }
        public virtual DbSet<TradeValuationAssignedApprover> TradeValuationAssignedApprover { get; set; }
        public virtual DbSet<TradeValuationBookPriceFactor> TradeValuationBookPriceFactor { get; set; }
        public virtual DbSet<TradeValuationRouting> TradeValuationRouting { get; set; }
        public virtual DbSet<Udf> Udf { get; set; }
        public virtual DbSet<UdfassignedValue> UdfassignedValue { get; set; }
        public virtual DbSet<Udflookup> Udflookup { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserActionHistory> UserActionHistory { get; set; }
        public virtual DbSet<UserAlertClearLog> UserAlertClearLog { get; set; }
        public virtual DbSet<UserAssignedRight> UserAssignedRight { get; set; }
        public virtual DbSet<UserAssignedRole> UserAssignedRole { get; set; }
        public virtual DbSet<UserAssignedSalesCode> UserAssignedSalesCode { get; set; }
        public virtual DbSet<UserDashBoardLayout> UserDashBoardLayout { get; set; }
        public virtual DbSet<UserDefaultRecurringTouchProfile> UserDefaultRecurringTouchProfile { get; set; }
        public virtual DbSet<UserEmailAccessToken> UserEmailAccessToken { get; set; }
        public virtual DbSet<UserEmailCalendar> UserEmailCalendar { get; set; }
        public virtual DbSet<UserEmailProfile> UserEmailProfile { get; set; }
        public virtual DbSet<UserFavoriteRecord> UserFavoriteRecord { get; set; }
        public virtual DbSet<UserGridColumnLayout> UserGridColumnLayout { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserGroupAssignedUser> UserGroupAssignedUser { get; set; }
        public virtual DbSet<UserImpersonationLog> UserImpersonationLog { get; set; }
        public virtual DbSet<UserLocation> UserLocation { get; set; }
        public virtual DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<UserReminder> UserReminder { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserSearchFilter> UserSearchFilter { get; set; }
        public virtual DbSet<UserTerritory> UserTerritory { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<WorkflowRule> WorkflowRule { get; set; }
        public virtual DbSet<WorkflowRuleAssignment> WorkflowRuleAssignment { get; set; }
        public virtual DbSet<WorkflowRuleCondition> WorkflowRuleCondition { get; set; }
        public virtual DbSet<WorkflowRuleEmail> WorkflowRuleEmail { get; set; }
        public virtual DbSet<WorkflowRuleTask> WorkflowRuleTask { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(DefaultConnectionString);
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => new { e.ActivityId, e.ActivityType });

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasIndex(e => e.BranchNo)
                    .HasName("IX_Branch_1");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Branch");

                entity.HasIndex(e => e.DivisionId)
                    .HasName("IX_Branch_2");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BranchNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DivisionId).HasColumnName("DivisionID");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Hq).HasColumnName("HQ");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Web)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BranchAssignedUser>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.BranchId)
                    .HasName("IX_BranchAssignedUser");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_BranchAssignedUser_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Broker>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Broker");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BrokerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BrokerNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cell)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Web)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BrokerAssignedDistribution>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.BrokerId)
                    .HasName("IX_BrokerAssignedDistribution_1");

                entity.HasIndex(e => e.DistributionId)
                    .HasName("IX_BrokerAssignedDistribution");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");
            });

            modelBuilder.Entity<BrokerDistributionList>(entity =>
            {
                entity.HasKey(e => e.DistributionId);

                entity.Property(e => e.DistributionId).HasColumnName("DistributionID");

                entity.Property(e => e.DistributionList)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BusinessPlan>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_BusinessPlan_3");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_BusinessPlan_2");

                entity.HasIndex(e => e.TypeId)
                    .HasName("IX_BusinessPlan_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_BusinessPlan");

                entity.Property(e => e.BusinessPlanId).HasColumnName("BusinessPlanID");

                entity.Property(e => e.BusinessPlan1)
                    .IsRequired()
                    .HasColumnName("BusinessPlan")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<BusinessPlanTactic>(entity =>
            {
                entity.HasKey(e => e.TacticId);

                entity.HasIndex(e => e.BusinessPlanId)
                    .HasName("IX_BusinessPlanTactic");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_BusinessPlanTactic_2");

                entity.HasIndex(e => e.Sequence)
                    .HasName("IX_BusinessPlanTactic_1");

                entity.Property(e => e.TacticId).HasColumnName("TacticID");

                entity.Property(e => e.Activity)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BusinessPlanId).HasColumnName("BusinessPlanID");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BusinessPlanType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_BusinessPlanType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PlanType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Call>(entity =>
            {
                entity.HasIndex(e => e.ContactId)
                    .HasName("IX_Call_1");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Call_5");

                entity.HasIndex(e => e.RelatedToRecordId)
                    .HasName("IX_Call_3");

                entity.HasIndex(e => e.RelatedToRecordType)
                    .HasName("IX_Call_4");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Call");

                entity.Property(e => e.CallId).HasColumnName("CallID");

                entity.Property(e => e.CalendarId)
                    .IsRequired()
                    .HasColumnName("calendar_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.EndTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.EventId)
                    .IsRequired()
                    .HasColumnName("event_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RelatedToRecordId).HasColumnName("RelatedToRecordID");

                entity.Property(e => e.RelatedToRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CallAssignedPurpose>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.CallId)
                    .HasName("IX_CallAssignedPurpose");

                entity.HasIndex(e => e.PurposeId)
                    .HasName("IX_CallAssignedPurpose_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CallId).HasColumnName("CallID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.AccountNo)
                    .HasName("IX_Company_1");

                entity.HasIndex(e => e.CompanyName)
                    .HasName("IX_Company_2");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Company");

                entity.HasIndex(e => e.ParentId)
                    .HasName("IX_Company_3");

                entity.HasIndex(e => e.Status)
                    .HasName("IX_Company_6");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.AccountNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.LegalName)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Mailing)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTypeId).HasColumnName("RecordTypeID");

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Web)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanyAssignedAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedAddress");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_CompanyAssignedAddress_1");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanyAssignedAr>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.ToTable("CompanyAssignedAR");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedAR");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });

            modelBuilder.Entity<CompanyAssignedBusinessPlan>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedBusinessPlan");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_CompanyAssignedBusinessPlan_3");

                entity.HasIndex(e => e.PlanId)
                    .HasName("IX_CompanyAssignedBusinessPlan_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_CompanyAssignedBusinessPlan_2");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.PlanEndDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.PlanStartDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CompanyAssignedBusinessPlanTactic>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.AssignedPlanId)
                    .HasName("IX_CompanyAssignedBusinessPlanTactic_2");

                entity.HasIndex(e => e.Complete)
                    .HasName("IX_CompanyAssignedBusinessPlanTactic_1");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_CompanyAssignedBusinessPlanTactic");

                entity.HasIndex(e => e.TacticId)
                    .HasName("IX_CompanyAssignedBusinessPlanTactic_3");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.AssignedPlanId).HasColumnName("AssignedPlanID");

                entity.Property(e => e.CompleteDate).HasColumnType("date");

                entity.Property(e => e.TacticId).HasColumnName("TacticID");

                entity.Property(e => e.TargetDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CompanyAssignedCategory>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CompanyAssignedCategory_1");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedCategory");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });

            modelBuilder.Entity<CompanyAssignedRank>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedRank");

                entity.HasIndex(e => e.RankId)
                    .HasName("IX_CompanyAssignedRank_1");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_CompanyAssignedRank_2");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<CompanyAssignedRevenue>(entity =>
            {
                entity.HasKey(e => e.RevenueId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedRevenue");

                entity.HasIndex(e => e.EndDate)
                    .HasName("IX_CompanyAssignedRevenue_2");

                entity.HasIndex(e => e.StartDate)
                    .HasName("IX_CompanyAssignedRevenue_1");

                entity.Property(e => e.RevenueId).HasColumnName("RevenueID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RevenueType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CompanyAssignedUser>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_CompanyAssignedUser_1");

                entity.HasIndex(e => e.PrimaryRep)
                    .HasName("IX_CompanyAssignedUser");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_CompanyAssignedUser_3");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_CompanyAssignedUser_2");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CompanyCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_CompanyCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanyRank>(entity =>
            {
                entity.HasKey(e => e.RankId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_CompanyRank");

                entity.Property(e => e.RankId).HasColumnName("RankID");

                entity.Property(e => e.Rank)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanyRecordType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_CompanyRecordType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanySource>(entity =>
            {
                entity.HasKey(e => e.SourceId);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.Active)
                    .HasName("IX_Contact_2");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_Contact");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Contact_1");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Cell)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ContactAssignedAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.HasIndex(e => e.ContactId)
                    .HasName("IX_ContactAssignedAddress");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_ContactAssignedAddress_1");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ContactAssignedEmail>(entity =>
            {
                entity.HasKey(e => e.EmailId);

                entity.Property(e => e.EmailId).HasColumnName("EmailID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ContactAssignedPhone>(entity =>
            {
                entity.HasKey(e => e.PhoneId);

                entity.HasIndex(e => e.ContactId)
                    .HasName("IX_ContactAssignedPhone");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_ContactAssignedPhone_1");

                entity.Property(e => e.PhoneId).HasColumnName("PhoneID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ContactDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_ContactDepartment");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ContactPosition>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_ContactPosition");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ContactTitle>(entity =>
            {
                entity.HasKey(e => e.TitleId);

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CrmSeriesConnectConfiguration>(entity =>
            {
                entity.HasKey(e => e.ConfigId);

                entity.ToTable("crmSeriesConnectConfiguration");

                entity.Property(e => e.ConfigId).HasColumnName("ConfigID");

                entity.Property(e => e.BlobContainer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ConnectionString)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FtpHost)
                    .IsRequired()
                    .HasColumnName("ftpHost")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FtpPassword)
                    .IsRequired()
                    .HasColumnName("ftpPassword")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FtpSecure).HasColumnName("ftpSecure");

                entity.Property(e => e.FtpUserName)
                    .IsRequired()
                    .HasColumnName("ftpUserName")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UploadType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CrmSeriesConnectLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("crmSeriesConnectLog");

                entity.HasIndex(e => e.QueryId)
                    .HasName("IX_crmSeriesConnectLog");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QueryId).HasColumnName("QueryID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<CrmSeriesConnectQuery>(entity =>
            {
                entity.HasKey(e => e.QueryId);

                entity.ToTable("crmSeriesConnectQuery");

                entity.Property(e => e.QueryId).HasColumnName("QueryID");

                entity.Property(e => e.Columns)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EndTime).HasDefaultValueSql("('')");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastRun).HasColumnType("datetime");

                entity.Property(e => e.NextRun).HasColumnType("datetime");

                entity.Property(e => e.Process)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QueryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuerySql)
                    .IsRequired()
                    .HasColumnName("QuerySQL")
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DashboardWidget>(entity =>
            {
                entity.HasKey(e => e.WidgetId);

                entity.HasIndex(e => e.DataSourceId)
                    .HasName("IX_DashboardWidget");

                entity.HasIndex(e => e.GroupId)
                    .HasName("IX_DashboardWidget_1");

                entity.Property(e => e.WidgetId).HasColumnName("WidgetID");

                entity.Property(e => e.DataFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataSourceId).HasColumnName("DataSourceID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WidgetSettings)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WidgetType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DashboardWidgetDataSource>(entity =>
            {
                entity.HasKey(e => e.DataSourceId);

                entity.HasIndex(e => e.CompanyForm)
                    .HasName("IX_DashboardWidgetDataSource_1");

                entity.HasIndex(e => e.Dashboard)
                    .HasName("IX_DashboardWidgetDataSource");

                entity.HasIndex(e => e.EquipmentForm)
                    .HasName("IX_DashboardWidgetDataSource_2");

                entity.Property(e => e.DataSourceId).HasColumnName("DataSourceID");

                entity.Property(e => e.DataSourceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DashboardWidgetGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupDescription)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DataImportJob>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataFields)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FileLocation)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImportType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MatchField)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SourceFields)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DataImportJobLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_DataImportJobLog");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Error)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Deal>(entity =>
            {
                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.ActualSalePrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.AmountToFinance).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.AttachmentFreight).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CalculationType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Commission).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CommissionEarned).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CommissionPaid).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CommissionPercent).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ConfigurationId).HasColumnName("ConfigurationID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.DealSheetFile)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DecisionDate).HasColumnType("date");

                entity.Property(e => e.DeliveryBranchId).HasColumnName("DeliveryBranchID");

                entity.Property(e => e.DeliveryFreight).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DownPayment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.EndUse)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipYear)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentTypeId).HasColumnName("EquipmentTypeID");

                entity.Property(e => e.FinalDiscount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.FinalDiscountPercent).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.FinalProfit).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.FinalProfitPercent).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.FinancingField4).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.FinancingField5).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Fob)
                    .IsRequired()
                    .HasColumnName("FOB")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FreightOrigin)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InboundBranchId).HasColumnName("InboundBranchID");

                entity.Property(e => e.InboundFreight).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaxDiscountPercent).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.MinimumProfitPercent).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.NewUsed)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.ProjectedDeliveryDate).HasColumnType("date");

                entity.Property(e => e.QuotedDeliveryDate).HasColumnType("date");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SalePriceAdjustment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SaleTypeId).HasColumnName("SaleTypeID");

                entity.Property(e => e.SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");

                entity.Property(e => e.StartDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StockNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TargetDeliveryDate).HasColumnType("date");

                entity.Property(e => e.TargetProfitPercent).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.TaxableAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TaxableCity)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaxableCounty)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaxableState)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaxableZip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TotalSalesTax).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TradeHoldback).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TradeInFreight).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TradeOverallowance).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TradeValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DealApprovalTrigger>(entity =>
            {
                entity.HasKey(e => e.TriggerId);

                entity.HasIndex(e => e.TypeId)
                    .HasName("IX_DealApprovalTrigger");

                entity.Property(e => e.TriggerId).HasColumnName("TriggerID");

                entity.Property(e => e.AssignmentTrigger)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<DealApprovalType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.ApprovalType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealAssignedApprovalHistory>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedApprovalHistory");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DealAssignedApprover>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedApprover");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DealAssignedAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedAttachment");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cost).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.DbattachmentId).HasColumnName("DBAttachmentID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Freight).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.List).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Margin).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.OutputDisplay)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuoteNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StockNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealAssignedCategory>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedCategory");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.DealId).HasColumnName("DealID");
            });

            modelBuilder.Entity<DealAssignedCharge>(entity =>
            {
                entity.HasKey(e => e.ChargeId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedCharge");

                entity.Property(e => e.ChargeId).HasColumnName("ChargeID");

                entity.Property(e => e.Amount).HasColumnType("decimal(14, 4)");

                entity.Property(e => e.ChargeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DbchargeId).HasColumnName("DBChargeID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EditType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SalePriceField)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Value).HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<DealAssignedDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedDiscount");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.Amount).HasColumnType("decimal(14, 4)");

                entity.Property(e => e.DbdiscountId).HasColumnName("DBDiscountID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DiscountType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EditType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProgramType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Value).HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<DealAssignedDocument>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedDocument");

                entity.HasIndex(e => e.DocumentType)
                    .HasName("IX_DealAssignedDocument_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.AbsoluteUri)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DocumentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            });

            modelBuilder.Entity<DealAssignedOption>(entity =>
            {
                entity.HasKey(e => e.OptionId);

                entity.HasIndex(e => e.BaseMachine)
                    .HasName("IX_DealAssignedOption_1");

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedOption");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.DboptionId).HasColumnName("DBOptionID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.List).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.OutputDisplay).IsUnicode(false);

                entity.Property(e => e.SalesCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UseExchange)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DealAssignedPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedPayment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EditType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Payment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Rate).HasColumnType("decimal(9, 8)");
            });

            modelBuilder.Entity<DealAssignedRecordLimit>(entity =>
            {
                entity.HasKey(e => e.LimitId);

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_DealAssignedRecordLimit");

                entity.Property(e => e.LimitId).HasColumnName("LimitID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealAssignedReserve>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReserveId).HasColumnName("ReserveID");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WorkOrderFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealAssignedSalesTax>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedSalesTax");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ExcludeReason)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxableMax).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Value).HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<DealAssignedTradeIn>(entity =>
            {
                entity.HasKey(e => e.TradeInId);

                entity.Property(e => e.TradeInId).HasColumnName("TradeInID");

                entity.Property(e => e.Attachment1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment1SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment2SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment3SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment4)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment4SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment5)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Attachment5SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BookValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerOffer).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipYear)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Field1).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field10).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field2).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field3).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field4).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field5).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field6).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field7).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field8).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field9).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Fob)
                    .IsRequired()
                    .HasColumnName("FOB")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Overallowance).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PayoffAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PayoffDate).HasColumnType("date");

                entity.Property(e => e.PayoffDetails)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PayoffInstitution)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReconditionEstimate).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StockNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TradeValue).HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<DealAssignedWarranty>(entity =>
            {
                entity.HasKey(e => e.WarrantyId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedWarranty");

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");

                entity.Property(e => e.Cost).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DbwarrantyId).HasColumnName("DBWarrantyID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndSmr).HasColumnName("EndSMR");

                entity.Property(e => e.Margin).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.OutputDisplay)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartSmr).HasColumnName("StartSMR");
            });

            modelBuilder.Entity<DealAssignedWorkOrder>(entity =>
            {
                entity.HasKey(e => e.WorkOrderId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealAssignedWorkOrder");

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.Cost).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.DbworkOrderId).HasColumnName("DBWorkOrderID");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.JobCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Margin).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.OutputDisplay)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuoteNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealCalculationType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CalculationType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealConfigurationModule>(entity =>
            {
                entity.HasKey(e => e.ModuleId);

                entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealCount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealDefault>(entity =>
            {
                entity.HasKey(e => e.DefaultId);

                entity.HasIndex(e => e.DefaultName)
                    .HasName("IX_DealDefault_1");

                entity.Property(e => e.DefaultId).HasColumnName("DefaultID");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealDocumentType>(entity =>
            {
                entity.HasKey(e => e.DocumentTypeId);

                entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealLog_2");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_DealLog_1");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_DealLog");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.NewValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OldValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PropertyName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimeStamp).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DealOutputLog>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DealReserveCalculation>(entity =>
            {
                entity.HasKey(e => e.ReserveId);

                entity.Property(e => e.ReserveId).HasColumnName("ReserveID");

                entity.Property(e => e.Amount).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WorkOrderFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.InternalStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.WinProbability).HasColumnType("decimal(5, 4)");
            });

            modelBuilder.Entity<DealThreshold>(entity =>
            {
                entity.HasKey(e => e.ThresholdId);

                entity.Property(e => e.ThresholdId).HasColumnName("ThresholdID");

                entity.Property(e => e.MachineFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Target).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Threshold).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.ThresholdType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealUdf>(entity =>
            {
                entity.HasKey(e => e.Udfid);

                entity.ToTable("DealUDF");

                entity.Property(e => e.Udfid).HasColumnName("UDFID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealUdfassignedValue>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.ToTable("DealUDFAssignedValue");

                entity.HasIndex(e => e.DealId)
                    .HasName("IX_DealUDFAssignedValue");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.DateValue).HasColumnType("date");

                entity.Property(e => e.DealId).HasColumnName("DealID");

                entity.Property(e => e.LookupId).HasColumnName("LookupID");

                entity.Property(e => e.NumericValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StringValue)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Udfid).HasColumnName("UDFID");
            });

            modelBuilder.Entity<DealUdflookup>(entity =>
            {
                entity.HasKey(e => e.UdflookupId);

                entity.ToTable("DealUDFLookup");

                entity.HasIndex(e => e.Udfid)
                    .HasName("IX_DealUDFLookup");

                entity.Property(e => e.UdflookupId).HasColumnName("UDFLookupID");

                entity.Property(e => e.Udfid).HasColumnName("UDFID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealUserAccess>(entity =>
            {
                entity.HasKey(e => e.AccessId);

                entity.HasIndex(e => e.RecordSelection)
                    .HasName("IX_DealUserAccess_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_DealUserAccess");

                entity.HasIndex(e => e.UserTypeId)
                    .HasName("IX_DealUserAccess_2");

                entity.Property(e => e.AccessId).HasColumnName("AccessID");

                entity.Property(e => e.RecordSelection)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            });

            modelBuilder.Entity<DealUserAccessFilter>(entity =>
            {
                entity.HasKey(e => e.FilterId);

                entity.HasIndex(e => e.AccessId)
                    .HasName("IX_DealUserAccessFilter");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_DealUserAccessFilter_2");

                entity.Property(e => e.FilterId).HasColumnName("FilterID");

                entity.Property(e => e.AccessId).HasColumnName("AccessID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<DealUserApproval>(entity =>
            {
                entity.HasKey(e => e.ApprovalId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_DealUserApproval_3");

                entity.HasIndex(e => e.RecordSelection)
                    .HasName("IX_DealUserApproval_2");

                entity.HasIndex(e => e.TypeId)
                    .HasName("IX_DealUserApproval_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_DealUserApproval_4");

                entity.Property(e => e.ApprovalId).HasColumnName("ApprovalID");

                entity.Property(e => e.RecordFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordSelection)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ThresholdFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ThresholdRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Body).IsUnicode(false);

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasIndex(e => e.Active)
                    .HasName("IX_Equipment_11");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Equipment_12");

                entity.HasIndex(e => e.Machine)
                    .HasName("IX_Equipment_3");

                entity.HasIndex(e => e.Make)
                    .HasName("IX_Equipment_4");

                entity.HasIndex(e => e.Model)
                    .HasName("IX_Equipment_5");

                entity.HasIndex(e => e.ParentId)
                    .HasName("IX_Equipment");

                entity.HasIndex(e => e.ParentType)
                    .HasName("IX_Equipment_1");

                entity.HasIndex(e => e.SerialNo)
                    .HasName("IX_Equipment_6");

                entity.HasIndex(e => e.StockNo)
                    .HasName("IX_Equipment_7");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.AcquisitionDate).HasColumnType("date");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AvailableForQuote)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ComponentProfileId).HasColumnName("ComponentProfileID");

                entity.Property(e => e.CurrentBranchId).HasColumnName("CurrentBranchID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipYear)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fleet)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FleetType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GetprofileId).HasColumnName("GETProfileID");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HoursPerDay).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.InOut)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InventoryNotes)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InventorySpecs)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastSmr)
                    .HasColumnName("LastSMR")
                    .HasColumnType("decimal(9, 1)");

                entity.Property(e => e.LastSmrdate)
                    .HasColumnName("LastSMRDate")
                    .HasColumnType("date");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Machine)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NewUsed)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pmactive).HasColumnName("PMActive");

                entity.Property(e => e.PminitialInterval).HasColumnName("PMInitialInterval");

                entity.Property(e => e.PmintervalStep).HasColumnName("PMIntervalStep");

                entity.Property(e => e.PmmaxHours).HasColumnName("PMMaxHours");

                entity.Property(e => e.PmprofileId).HasColumnName("PMProfileID");

                entity.Property(e => e.PmstartDate)
                    .HasColumnName("PMStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.PmstartHours)
                    .HasColumnName("PMStartHours")
                    .HasColumnType("decimal(9, 1)");

                entity.Property(e => e.SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartHours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StockNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UcstartDate)
                    .HasColumnName("UCStartDate")
                    .HasColumnType("date");

                entity.Property(e => e.UcstartHours)
                    .HasColumnName("UCStartHours")
                    .HasColumnType("decimal(9, 1)");

                entity.Property(e => e.UcwearProfileId).HasColumnName("UCWearProfileID");
            });

            modelBuilder.Entity<EquipmentAssignedGet>(entity =>
            {
                entity.HasKey(e => e.Getid);

                entity.ToTable("EquipmentAssignedGET");

                entity.Property(e => e.Getid).HasColumnName("GETID");

                entity.Property(e => e.BoltSize)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentAssignedImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedImage");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentAssignedInventoryCost>(entity =>
            {
                entity.HasKey(e => e.CostId);

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedInventoryCost");

                entity.Property(e => e.CostId).HasColumnName("CostID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.Nbvdisplay).HasColumnName("NBVDisplay");
            });

            modelBuilder.Entity<EquipmentAssignedInventoryOption>(entity =>
            {
                entity.HasKey(e => e.OptionId);

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedInventoryOption");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.SalesCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentAssignedPmhistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.ToTable("EquipmentAssignedPMHistory");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedPMHistory");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_EquipmentAssignedPMHistory_2");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.HistoryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<EquipmentAssignedPmlabor>(entity =>
            {
                entity.HasKey(e => e.LaborId);

                entity.ToTable("EquipmentAssignedPMLabor");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentAssignedPMLabor_1");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedPMLabor");

                entity.Property(e => e.LaborId).HasColumnName("LaborID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            });

            modelBuilder.Entity<EquipmentAssignedPmpart>(entity =>
            {
                entity.HasKey(e => e.PartId);

                entity.ToTable("EquipmentAssignedPMPart");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentAssignedPMPart");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedPMPart_1");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UnitOfMeasure)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentAssignedPmservice>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("EquipmentAssignedPMService");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedPMService");

                entity.HasIndex(e => e.Scheduled)
                    .HasName("IX_EquipmentAssignedPMService_2");

                entity.HasIndex(e => e.Status)
                    .HasName("IX_EquipmentAssignedPMService_1");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.JobNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ScheduleEnd).HasColumnType("datetime");

                entity.Property(e => e.ScheduleStart).HasColumnType("datetime");

                entity.Property(e => e.ScheduledUserId).HasColumnName("ScheduledUserID");

                entity.Property(e => e.ScheduledVehicleId).HasColumnName("ScheduledVehicleID");

                entity.Property(e => e.ServiceDate).HasColumnType("date");

                entity.Property(e => e.ServiceDueDate).HasColumnType("date");

                entity.Property(e => e.ServiceDueSmu)
                    .HasColumnName("ServiceDueSMU")
                    .HasColumnType("decimal(9, 1)");

                entity.Property(e => e.ServiceSmu)
                    .HasColumnName("ServiceSMU")
                    .HasColumnType("decimal(9, 1)");

                entity.Property(e => e.Smuinterval).HasColumnName("SMUInterval");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");
            });

            modelBuilder.Entity<EquipmentAssignedTracking>(entity =>
            {
                entity.HasKey(e => e.TrackingId);

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedTracking");

                entity.HasIndex(e => e.TrackingType)
                    .HasName("IX_EquipmentAssignedTracking_1");

                entity.Property(e => e.TrackingId).HasColumnName("TrackingID");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.IntervalType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NotificationPercent).HasDefaultValueSql("((100))");

                entity.Property(e => e.ScheduledComponent)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TrackingType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentAssignedWarranty>(entity =>
            {
                entity.HasKey(e => e.WarrantyId);

                entity.HasIndex(e => e.Active)
                    .HasName("IX_EquipmentAssignedWarranty_1");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentAssignedWarranty");

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.EndSmu).HasColumnName("EndSMU");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartSmu).HasColumnName("StartSMU");

                entity.Property(e => e.WarrantyType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentAssignedWorkOrder>(entity =>
            {
                entity.HasKey(e => e.WorkOrderId);

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.BillingDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RepairType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentCategory_1");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ComponentIntervalType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HasUc).HasColumnName("HasUC");

                entity.Property(e => e.InspectionIntervalType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UcinitialInterval).HasColumnName("UCInitialInterval");

                entity.Property(e => e.UcintervalType)
                    .IsRequired()
                    .HasColumnName("UCIntervalType")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UcscheduleComponent)
                    .IsRequired()
                    .HasColumnName("UCScheduleComponent")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UctrackingDefault).HasColumnName("UCTrackingDefault");
            });

            modelBuilder.Entity<EquipmentComponent>(entity =>
            {
                entity.HasKey(e => e.ComponentId);

                entity.HasIndex(e => e.ComponentTypeId)
                    .HasName("IX_EquipmentComponent_3");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentComponent_1");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentComponent");

                entity.HasIndex(e => e.Replaced)
                    .HasName("IX_EquipmentComponent_2");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.AdjustedLife).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.AverageLife).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ComponentLookupId).HasColumnName("ComponentLookupID");

                entity.Property(e => e.ComponentTypeId).HasColumnName("ComponentTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PercentUsed).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.RemainingLife).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.SerialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartHours).HasColumnType("decimal(9, 1)");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentComponent)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentComponent_Equipment");
            });

            modelBuilder.Entity<EquipmentComponentAssignedRating>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.ComponentId)
                    .HasName("IX_EquipmentComponentAssignedRating");

                entity.HasIndex(e => e.RatingId)
                    .HasName("IX_EquipmentComponentAssignedRating_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.EquipmentComponentAssignedRating)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentComponentAssignedRating_EquipmentComponent");
            });

            modelBuilder.Entity<EquipmentComponentCriteria>(entity =>
            {
                entity.HasKey(e => e.CriteriaId);

                entity.HasIndex(e => e.Category)
                    .HasName("IX_EquipmentComponentCriteria_2");

                entity.HasIndex(e => e.PartNo)
                    .HasName("IX_EquipmentComponentCriteria_1");

                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_EquipmentComponentCriteria");

                entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnterBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Qty).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<EquipmentComponentHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.HasIndex(e => e.Action)
                    .HasName("IX_EquipmentComponentHistory_1");

                entity.HasIndex(e => e.AssignedComponentId)
                    .HasName("IX_EquipmentComponentHistory");

                entity.HasIndex(e => e.ReplacementComponentId)
                    .HasName("IX_EquipmentComponentHistory_2");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ActionBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ActionHours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.AssignedComponentId).HasColumnName("AssignedComponentID");

                entity.Property(e => e.ReplacementComponentId).HasColumnName("ReplacementComponentID");
            });

            modelBuilder.Entity<EquipmentComponentProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentComponentProfile_2");

                entity.HasIndex(e => e.Make)
                    .HasName("IX_EquipmentComponentProfile");

                entity.HasIndex(e => e.ProfileName)
                    .HasName("IX_EquipmentComponentProfile_1");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnterBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EnterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifyBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProfileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SerialRange)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentComponentRating>(entity =>
            {
                entity.HasIndex(e => e.RateType)
                    .HasName("IX_EquipmentComponentRating");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RateType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RatingValue).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<EquipmentComponentType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentComponentType");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentPminterval>(entity =>
            {
                entity.HasKey(e => e.IntervalId);

                entity.ToTable("EquipmentPMInterval");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentPMInterval_2");

                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_EquipmentPMInterval");

                entity.Property(e => e.IntervalId).HasColumnName("IntervalID");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            });

            modelBuilder.Entity<EquipmentPmlabor>(entity =>
            {
                entity.HasKey(e => e.LaborId);

                entity.ToTable("EquipmentPMLabor");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentPMLabor_1");

                entity.HasIndex(e => e.IntervalId)
                    .HasName("IX_EquipmentPMLabor");

                entity.Property(e => e.LaborId).HasColumnName("LaborID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IntervalId).HasColumnName("IntervalID");
            });

            modelBuilder.Entity<EquipmentPmpart>(entity =>
            {
                entity.HasKey(e => e.PartId);

                entity.ToTable("EquipmentPMPart");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentPMPart");

                entity.HasIndex(e => e.IntervalId)
                    .HasName("IX_EquipmentPMPart_1");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IntervalId).HasColumnName("IntervalID");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UnitOfMeasure)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentPmprofile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.ToTable("EquipmentPMProfile");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentPMProfile_1");

                entity.HasIndex(e => e.Make)
                    .HasName("IX_EquipmentPMProfile");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProfileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SerialRange)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentPmschedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("EquipmentPMSchedule");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_EquipmentPMSchedule_1");

                entity.HasIndex(e => e.Complete)
                    .HasName("IX_EquipmentPMSchedule_4");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentPMSchedule");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_EquipmentPMSchedule_3");

                entity.HasIndex(e => e.VehicleId)
                    .HasName("IX_EquipmentPMSchedule_2");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            });

            modelBuilder.Entity<EquipmentPmscheduleNote>(entity =>
            {
                entity.HasKey(e => e.NoteId);

                entity.ToTable("EquipmentPMScheduleNote");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentPMScheduleNote_2");

                entity.HasIndex(e => e.ScheduleId)
                    .HasName("IX_EquipmentPMScheduleNote");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_EquipmentPMScheduleNote_1");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<EquipmentRentalHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_EquipmentRentalHistory_1");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentRentalHistory");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContractNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContractType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DateOffRent).HasColumnType("date");

                entity.Property(e => e.DateOnRent)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.ExpectedOffRent).HasColumnType("date");
            });

            modelBuilder.Entity<EquipmentSalesAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentId);

                entity.HasIndex(e => e.Category)
                    .HasName("IX_EquipmentSalesAttachment_1");

                entity.HasIndex(e => e.MfgId)
                    .HasName("IX_EquipmentSalesAttachment");

                entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Freight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.List).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.MfgId).HasColumnName("MfgID");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.OutputDisplay)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesCharge>(entity =>
            {
                entity.HasKey(e => e.ChargeId);

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesCharge");

                entity.Property(e => e.ChargeId).HasColumnName("ChargeID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ChargeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Criteria)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CriteriaRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EditType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.SalePriceField)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesConfiguration>(entity =>
            {
                entity.HasKey(e => e.ConfigurationId);

                entity.HasIndex(e => e.Active)
                    .HasName("IX_EquipmentSalesConfiguration_1");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentSalesConfiguration_2");

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesConfiguration");

                entity.Property(e => e.ConfigurationId).HasColumnName("ConfigurationID");

                entity.Property(e => e.Configuration)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");
            });

            modelBuilder.Entity<EquipmentSalesConfigurationItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.HasIndex(e => e.ConfigurationId)
                    .HasName("IX_EquipmentSalesConfigurationItem");

                entity.HasIndex(e => e.SortOrder)
                    .HasName("IX_EquipmentSalesConfigurationItem_1");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ConfigurationId).HasColumnName("ConfigurationID");

                entity.Property(e => e.ModelItemId).HasColumnName("ModelItemID");
            });

            modelBuilder.Entity<EquipmentSalesDealsheetField>(entity =>
            {
                entity.HasKey(e => e.FieldId);

                entity.HasIndex(e => e.FieldName)
                    .HasName("IX_EquipmentSalesDealsheetField");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesDefaultSelection>(entity =>
            {
                entity.HasKey(e => e.DefaultId);

                entity.HasIndex(e => e.DefaultRecordType)
                    .HasName("IX_EquipmentSalesDefaultSelection_2");

                entity.HasIndex(e => e.ParentRecordId)
                    .HasName("IX_EquipmentSalesDefaultSelection");

                entity.HasIndex(e => e.ParentRecordType)
                    .HasName("IX_EquipmentSalesDefaultSelection_1");

                entity.Property(e => e.DefaultId).HasColumnName("DefaultID");

                entity.Property(e => e.DefaultRecordId).HasColumnName("DefaultRecordID");

                entity.Property(e => e.DefaultRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParentRecordId).HasColumnName("ParentRecordID");

                entity.Property(e => e.ParentRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.HasIndex(e => e.Active)
                    .HasName("IX_EquipmentSalesDiscount_1");

                entity.HasIndex(e => e.ApplyBaseMachine)
                    .HasName("IX_EquipmentSalesDiscount_3");

                entity.HasIndex(e => e.ApplyOptions)
                    .HasName("IX_EquipmentSalesDiscount_4");

                entity.HasIndex(e => e.ExpirationDate)
                    .HasName("IX_EquipmentSalesDiscount_2");

                entity.HasIndex(e => e.MfgId)
                    .HasName("IX_EquipmentSalesDiscount");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.ApplyBaseMachine)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ApplyOptions)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DiscountType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.MfgId).HasColumnName("MfgID");

                entity.Property(e => e.ProgramType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesFreight>(entity =>
            {
                entity.HasKey(e => e.FreightId);

                entity.HasIndex(e => e.BranchId)
                    .HasName("IX_EquipmentSalesFreight_1");

                entity.HasIndex(e => e.FreightType)
                    .HasName("IX_EquipmentSalesFreight_2");

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesFreight");

                entity.Property(e => e.FreightId).HasColumnName("FreightID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.FreightType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesManufacturer>(entity =>
            {
                entity.HasKey(e => e.MfgId);

                entity.HasIndex(e => e.AttachmentMfg)
                    .HasName("IX_EquipmentSalesManufacturer");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentSalesManufacturer_2");

                entity.HasIndex(e => e.MachineMfg)
                    .HasName("IX_EquipmentSalesManufacturer_1");

                entity.Property(e => e.MfgId).HasColumnName("MfgID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.StandardFactoryDiscount).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<EquipmentSalesModel>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.HasIndex(e => e.Active)
                    .HasName("IX_EquipmentSalesModel_1");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_EquipmentSalesModel_3");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentSalesModel");

                entity.HasIndex(e => e.MfgId)
                    .HasName("IX_EquipmentSalesModel_2");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FactoryDiscount).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.FactoryDiscountType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Percent')");

                entity.Property(e => e.MfgId).HasColumnName("MfgID");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesModelDiscount>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DiscountId)
                    .HasName("IX_EquipmentSalesModelDiscount_1");

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesModelDiscount");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Criteria)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CriteriaRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.EditType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");
            });

            modelBuilder.Entity<EquipmentSalesModelGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentSalesModelGroup_1");

                entity.HasIndex(e => e.GroupCode)
                    .HasName("IX_EquipmentSalesModelGroup_4");

                entity.HasIndex(e => e.Managed)
                    .HasName("IX_EquipmentSalesModelGroup_2");

                entity.HasIndex(e => e.MandatoryFlag)
                    .HasName("IX_EquipmentSalesModelGroup_3");

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesModelGroup");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MandatoryFlag)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");
            });

            modelBuilder.Entity<EquipmentSalesModelItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.HasIndex(e => e.BaseMachine)
                    .HasName("IX_EquipmentSalesModelItem_1");

                entity.HasIndex(e => e.ControlGroupId)
                    .HasName("IX_EquipmentSalesModelItem_4");

                entity.HasIndex(e => e.ControlItemId)
                    .HasName("IX_EquipmentSalesModelItem_5");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentSalesModelItem_3");

                entity.HasIndex(e => e.GroupId)
                    .HasName("IX_EquipmentSalesModelItem");

                entity.HasIndex(e => e.Managed)
                    .HasName("IX_EquipmentSalesModelItem_2");

                entity.HasIndex(e => e.SortOrder)
                    .HasName("IX_EquipmentSalesModelItem_6");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ControlGroupId).HasColumnName("ControlGroupID");

                entity.Property(e => e.ControlItemId).HasColumnName("ControlItemID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtendedDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SalesCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesModelItemRelatedComponent>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.RelatedId).HasColumnName("RelatedID");

                entity.Property(e => e.RelatedType)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesModelWarranty>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.Hours)
                    .HasName("IX_EquipmentSalesModelWarranty_2");

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesModelWarranty");

                entity.HasIndex(e => e.Months)
                    .HasName("IX_EquipmentSalesModelWarranty_3");

                entity.HasIndex(e => e.WarrantyId)
                    .HasName("IX_EquipmentSalesModelWarranty_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Criteria)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CriteriaRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");
            });

            modelBuilder.Entity<EquipmentSalesRelatedRecordSelection>(entity =>
            {
                entity.HasKey(e => e.RelatedId);

                entity.HasIndex(e => e.ParentRecordId)
                    .HasName("IX_EquipmentSalesRelatedRecordSelection_1");

                entity.HasIndex(e => e.ParentRecordType)
                    .HasName("IX_EquipmentSalesRelatedRecordSelection");

                entity.Property(e => e.RelatedId).HasColumnName("RelatedID");

                entity.Property(e => e.ChildRecordId).HasColumnName("ChildRecordID");

                entity.Property(e => e.ChildRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParentRecordId).HasColumnName("ParentRecordID");

                entity.Property(e => e.ParentRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesSpec>(entity =>
            {
                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesSpec");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LeftCol)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.RightCol)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesWarranty>(entity =>
            {
                entity.HasKey(e => e.WarrantyId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_EquipmentSalesWarranty_1");

                entity.HasIndex(e => e.UseWithQuote)
                    .HasName("IX_EquipmentSalesWarranty");

                entity.Property(e => e.WarrantyId).HasColumnName("WarrantyID");

                entity.Property(e => e.AmountType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasDefaultValueSql("(($0.0000))");

                entity.Property(e => e.Exclusions)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inclusions)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OutputDisplay)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Warranty)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSalesWorkOrder>(entity =>
            {
                entity.HasKey(e => e.WorkOrderId);

                entity.HasIndex(e => e.ModelId)
                    .HasName("IX_EquipmentSalesWorkOrder");

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.Criteria)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CriteriaRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.JobCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.OutputDisplay)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentSmuhistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.ToTable("EquipmentSMUHistory");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentSMUHistory");

                entity.HasIndex(e => e.UpdateType)
                    .HasName("IX_EquipmentSMUHistory_1");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.ActionBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.HistoryDate).HasColumnType("date");

                entity.Property(e => e.Hours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.LastGpsoperation)
                    .HasColumnName("LastGPSOperation")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastGpsreading)
                    .HasColumnName("LastGPSReading")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastLocation)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.SatelliteProvider)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdateType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EquipmentUccomponent>(entity =>
            {
                entity.HasKey(e => e.ComponentId);

                entity.ToTable("EquipmentUCComponent");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.Component)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.HoursOfUse).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.HoursPerDay).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.LastInspectedDate).HasColumnType("date");

                entity.Property(e => e.LastInspectedHours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.LastPercentWorn).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.LifeRemaining).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProjectedMax).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.ProjectedSmu)
                    .HasColumnName("ProjectedSMU")
                    .HasColumnType("decimal(9, 1)");

                entity.Property(e => e.ProjectedWearDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartHours).HasColumnType("decimal(9, 1)");
            });

            modelBuilder.Entity<EquipmentUchistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.ToTable("EquipmentUCHistory");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ActionBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ActionHours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            });

            modelBuilder.Entity<EquipmentUcreading>(entity =>
            {
                entity.HasKey(e => e.ReadingId);

                entity.ToTable("EquipmentUCReading");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_EquipmentUCReading");

                entity.Property(e => e.ReadingId).HasColumnName("ReadingID");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.Impact)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReadingDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReadingHours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.TrackGuards)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WearProfileId).HasColumnName("WearProfileID");
            });

            modelBuilder.Entity<EquipmentUcreadingMeasurement>(entity =>
            {
                entity.HasKey(e => e.MeasurementId);

                entity.ToTable("EquipmentUCReadingMeasurement");

                entity.HasIndex(e => e.Component)
                    .HasName("IX_EquipmentUCReadingMeasurement_1");

                entity.HasIndex(e => e.Location)
                    .HasName("IX_EquipmentUCReadingMeasurement_2");

                entity.HasIndex(e => e.Method)
                    .HasName("IX_EquipmentUCReadingMeasurement_3");

                entity.HasIndex(e => e.ReadingId)
                    .HasName("IX_EquipmentUCReadingMeasurement");

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.Component)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Measurement).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PercentWorn).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.ReadingId).HasColumnName("ReadingID");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Event_1");

                entity.HasIndex(e => e.HostId)
                    .HasName("IX_Event");

                entity.HasIndex(e => e.RelatedToRecordId)
                    .HasName("IX_Event_2");

                entity.HasIndex(e => e.RelatedToRecordType)
                    .HasName("IX_Event_3");

                entity.Property(e => e.EventId)
                    .HasColumnName("EventID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EndTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.HostId).HasColumnName("HostID");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RelatedToRecordId).HasColumnName("RelatedToRecordID");

                entity.Property(e => e.RelatedToRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<EventAssignedContact>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.ContactId)
                    .HasName("IX_EventAssignedContact_1");

                entity.HasIndex(e => e.EventId)
                    .HasName("IX_EventAssignedContact");

                entity.Property(e => e.AssignedId)
                    .HasColumnName("AssignedID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.EventId).HasColumnName("EventID");
            });

            modelBuilder.Entity<EventAssignedReminder>(entity =>
            {
                entity.HasKey(e => e.ReminderId);

                entity.HasIndex(e => e.EventId)
                    .HasName("IX_EventAssignedReminder");

                entity.Property(e => e.ReminderId)
                    .HasColumnName("ReminderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EventId).HasColumnName("EventID");
            });

            modelBuilder.Entity<Inspection>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Inspection");

                entity.HasIndex(e => e.TypeId)
                    .HasName("IX_Inspection_1");

                entity.Property(e => e.InspectionId).HasColumnName("InspectionID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultTemplateId).HasColumnName("DefaultTemplateID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<InspectionGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.HasIndex(e => e.InspectionId)
                    .HasName("IX_InspectionGroup");

                entity.HasIndex(e => e.Sequence)
                    .HasName("IX_InspectionGroup_1");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InspectionId).HasColumnName("InspectionID");
            });

            modelBuilder.Entity<InspectionImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.HasIndex(e => e.InspectionId)
                    .HasName("IX_InspectionImage");

                entity.HasIndex(e => e.Sequence)
                    .HasName("IX_InspectionImage_1");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InspectionId).HasColumnName("InspectionID");
            });

            modelBuilder.Entity<InspectionItem>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.HasIndex(e => e.GroupId)
                    .HasName("IX_InspectionItem");

                entity.HasIndex(e => e.Sequence)
                    .HasName("IX_InspectionItem_1");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequirementFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<InspectionResponse>(entity =>
            {
                entity.HasKey(e => e.ResponseId);

                entity.HasIndex(e => e.ItemId)
                    .HasName("IX_InspectionResponse");

                entity.HasIndex(e => e.Sequence)
                    .HasName("IX_InspectionResponse_1");

                entity.Property(e => e.ResponseId).HasColumnName("ResponseID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<InspectionType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.EquipStockNumber)
                    .HasName("IX_Invoice_6");

                entity.HasIndex(e => e.InvoiceDate)
                    .HasName("IX_Invoice_3");

                entity.HasIndex(e => e.InvoiceNumber)
                    .HasName("IX_Invoice_5");

                entity.HasIndex(e => e.InvoiceType)
                    .HasName("IX_Invoice_2");

                entity.HasIndex(e => e.ParentId)
                    .HasName("IX_Invoice");

                entity.HasIndex(e => e.ParentType)
                    .HasName("IX_Invoice_1");

                entity.HasIndex(e => e.Status)
                    .HasName("IX_Invoice_4");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipMake)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipModel)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipSerialNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EquipStockNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvoiceType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<InvoiceByMonth>(entity =>
            {
                entity.HasIndex(e => e.InvoiceDate)
                    .HasName("IX_InvoiceByMonth_2");

                entity.HasIndex(e => e.InvoiceType)
                    .HasName("IX_InvoiceByMonth_3");

                entity.HasIndex(e => e.ParentId)
                    .HasName("IX_InvoiceByMonth");

                entity.HasIndex(e => e.ParentType)
                    .HasName("IX_InvoiceByMonth_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoiceType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ParentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Lead_2");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("IX_Lead");

                entity.HasIndex(e => e.SourceId)
                    .HasName("IX_Lead_3");

                entity.HasIndex(e => e.StatusId)
                    .HasName("IX_Lead_1");

                entity.Property(e => e.LeadId).HasColumnName("LeadID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cell)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Web)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<LeadStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_LeadStatus_1");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.InternalStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<LeadUserAccess>(entity =>
            {
                entity.HasKey(e => e.AccessId);

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_LeadUserAccess");

                entity.Property(e => e.AccessId).HasColumnName("AccessID");

                entity.Property(e => e.RecordFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordSelection)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Message_1");

                entity.HasIndex(e => e.NylasId)
                    .HasName("IX_Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.FormattedDate)
                    .HasColumnName("formatted_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NylasId)
                    .IsRequired()
                    .HasColumnName("nylas_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Snippet)
                    .IsRequired()
                    .HasColumnName("snippet")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Starred).HasColumnName("starred");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ThreadId)
                    .IsRequired()
                    .HasColumnName("thread_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Unread).HasColumnName("unread");
            });

            modelBuilder.Entity<MessageAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");
            });

            modelBuilder.Entity<MessageFile>(entity =>
            {
                entity.HasKey(e => e.FileId);

                entity.HasIndex(e => e.MessageId)
                    .HasName("IX_MessageFile");

                entity.HasIndex(e => e.NylasId)
                    .HasName("IX_MessageFile_1");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.ContentDisposition)
                    .IsRequired()
                    .HasColumnName("content_disposition")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasColumnName("content_type")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.NylasId)
                    .IsRequired()
                    .HasColumnName("nylas_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<MessageFolder>(entity =>
            {
                entity.HasKey(e => e.FolderId);

                entity.HasIndex(e => e.MessageId)
                    .HasName("IX_MessageFolder");

                entity.HasIndex(e => e.NylasId)
                    .HasName("IX_MessageFolder_1");

                entity.Property(e => e.FolderId).HasColumnName("FolderID");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NylasId)
                    .IsRequired()
                    .HasColumnName("nylas_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MessageHeader>(entity =>
            {
                entity.HasKey(e => e.HeaderId);

                entity.Property(e => e.HeaderId).HasColumnName("HeaderID");

                entity.Property(e => e.HeaderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HeaderValue)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MessageId)
                    .IsRequired()
                    .HasColumnName("MessageID")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Note_2");

                entity.HasIndex(e => e.NoteDate)
                    .HasName("IX_Note_1");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_Note_6");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_Note_7");

                entity.HasIndex(e => e.TypeId)
                    .HasName("IX_Note_5");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Note");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.NoteDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NoteAssignedPurpose>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.NoteId)
                    .HasName("IX_NoteAssignedPurpose");

                entity.HasIndex(e => e.PurposeId)
                    .HasName("IX_NoteAssignedPurpose_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");
            });

            modelBuilder.Entity<NotePurpose>(entity =>
            {
                entity.HasKey(e => e.PurposeId);

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NotePurposeAlert>(entity =>
            {
                entity.HasKey(e => e.AlertId);

                entity.HasIndex(e => e.PurposeId)
                    .HasName("IX_NotePurposeAlert");

                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NotePurposeAlertFilter>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NotePurposeAssignedRole>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.PurposeId)
                    .HasName("IX_NotePurposeAssignedRole_1");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_NotePurposeAssignedRole");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.PurposeId).HasColumnName("PurposeID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<NoteType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NotificationServiceConnection>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ConnectionId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ConnectionId)
                    .HasColumnName("ConnectionID")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LastWrite)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmailBody)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmailSubject)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NotificationType1)
                    .IsRequired()
                    .HasColumnName("NotificationType")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Opportunity>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Opportunity_2");

                entity.HasIndex(e => e.StatusId)
                    .HasName("IX_Opportunity_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Opportunity");

                entity.Property(e => e.OpportunityId).HasColumnName("OpportunityID");

                entity.Property(e => e.CloseDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OpenDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Probability).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OpportunityCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OpportunitySaleType>(entity =>
            {
                entity.HasKey(e => e.SaleTypeId);

                entity.Property(e => e.SaleTypeId).HasColumnName("SaleTypeID");

                entity.Property(e => e.SaleType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OpportunityStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OpportunityTransactionType>(entity =>
            {
                entity.HasKey(e => e.TransactionTypeId);

                entity.Property(e => e.TransactionTypeId)
                    .HasColumnName("TransactionTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OutputTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.AbsoluteUri)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Template)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TemplateType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OutputTemplateCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_OutputTemplateCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ProspectTimeline>(entity =>
            {
                entity.HasKey(e => e.TimelineId);

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_ProspectTimeline");

                entity.Property(e => e.TimelineId).HasColumnName("TimelineID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<RecordAssignedFile>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_RecordAssignedFile");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_RecordAssignedFile_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.AbsoluteUri)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uploaded).HasColumnType("datetimeoffset(0)");
            });

            modelBuilder.Entity<RecordAssignedInspection>(entity =>
            {
                entity.HasKey(e => e.AssignedInspectionId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_RecordAssignedInspection_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_RecordAssignedInspection_2");

                entity.Property(e => e.AssignedInspectionId).HasColumnName("AssignedInspectionID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InspectionDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InspectionHours).HasColumnType("decimal(9, 1)");

                entity.Property(e => e.InspectionId).HasColumnName("InspectionID");

                entity.Property(e => e.InspectionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InspectionNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InspectionType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<RecordAssignedInspectionGroup>(entity =>
            {
                entity.HasKey(e => e.AssignedGroupId);

                entity.Property(e => e.AssignedGroupId).HasColumnName("AssignedGroupID");

                entity.Property(e => e.AssignedInspectionId).HasColumnName("AssignedInspectionID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RecordAssignedInspectionImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.AssignedInspectionId).HasColumnName("AssignedInspectionID");

                entity.Property(e => e.AssignedItemId).HasColumnName("AssignedItemID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RecordAssignedInspectionItem>(entity =>
            {
                entity.HasKey(e => e.AssignedItemId);

                entity.HasIndex(e => e.AssignedGroupId)
                    .HasName("IX_RecordAssignedInspectionItem");

                entity.HasIndex(e => e.Sequence)
                    .HasName("IX_RecordAssignedInspectionItem_1");

                entity.Property(e => e.AssignedItemId).HasColumnName("AssignedItemID");

                entity.Property(e => e.AssignedGroupId).HasColumnName("AssignedGroupID");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RequirementFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RecordAssignedInspectionItemResponse>(entity =>
            {
                entity.HasKey(e => e.ResponseId);

                entity.HasIndex(e => e.AssignedItemId)
                    .HasName("IX_RecordAssignedInspectionItemResponse");

                entity.Property(e => e.ResponseId).HasColumnName("ResponseID");

                entity.Property(e => e.AssignedItemId).HasColumnName("AssignedItemID");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RecordLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_RecordLog_1");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_RecordLog");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AssociatedRecordAction)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NewValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OldValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PropertyName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimeStamp).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<RecurringTouchAssignment>(entity =>
            {
                entity.HasKey(e => e.AssignmentId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_RecurringTouchAssignment_3");

                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_RecurringTouchAssignment_2");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_RecurringTouchAssignment");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_RecurringTouchAssignment_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_RecurringTouchAssignment_5");

                entity.HasIndex(e => e.ValueId)
                    .HasName("IX_RecurringTouchAssignment_4");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.NextTargetDate).HasColumnType("date");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValueId).HasColumnName("ValueID");
            });

            modelBuilder.Entity<RecurringTouchHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.HasIndex(e => e.AssignmentId)
                    .HasName("IX_RecurringTouchHistory");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_RecurringTouchHistory_1");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TouchDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<RecurringTouchProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_RecurringTouchProfile");

                entity.HasIndex(e => e.ValueType)
                    .HasName("IX_RecurringTouchProfile_1");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ValueType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RecurringTouchProfileAction>(entity =>
            {
                entity.HasKey(e => e.ActionId);

                entity.HasIndex(e => e.Action)
                    .HasName("IX_RecurringTouchProfileAction_1");

                entity.HasIndex(e => e.Enabled)
                    .HasName("IX_RecurringTouchProfileAction_2");

                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_RecurringTouchProfileAction");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Rules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RecurringTouchProfileValue>(entity =>
            {
                entity.HasKey(e => e.ValueId);

                entity.HasIndex(e => e.ProfileId)
                    .HasName("IX_RecurringTouchProfileValue");

                entity.Property(e => e.ValueId).HasColumnName("ValueID");

                entity.Property(e => e.MaxValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MinValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.RankId).HasColumnName("RankID");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.Region1)
                    .IsRequired()
                    .HasColumnName("Region")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<RegionAssignedBranch>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.BranchId)
                    .HasName("IX_RegionAssignedBranch");

                entity.HasIndex(e => e.RegionId)
                    .HasName("IX_RegionAssignedBranch_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Report_4");

                entity.HasIndex(e => e.OwnerId)
                    .HasName("IX_Report_1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.ReportInputModel).IsUnicode(false);

                entity.Property(e => e.ReportMetaData).IsUnicode(false);

                entity.Property(e => e.ReportType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubGroup)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ReportDistribution>(entity =>
            {
                entity.HasKey(e => new { e.ScheduleId, e.UserId });

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ReportSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.DateSelection)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Format)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Frequency)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobId)
                    .IsRequired()
                    .HasColumnName("JobID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.RunDay)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RunTime)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TimeZone)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserSelection)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReportScheduleExecutionHistory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Distribution)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ErrorMessage)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RequiredFieldOption>(entity =>
            {
                entity.HasKey(e => e.RequiredId);

                entity.Property(e => e.RequiredId).HasColumnName("RequiredID");

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SalesTax>(entity =>
            {
                entity.HasKey(e => e.TaxId);

                entity.Property(e => e.TaxId).HasColumnName("TaxID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CitySalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.CityTaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CountySalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.CountyTaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other1SalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other1TaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other2SalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other2TaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other3SalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other3TaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other4SalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other4TaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other5SalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.Other5TaxOverMax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StateSalesTax).HasColumnType("decimal(9, 8)");

                entity.Property(e => e.StateTaxOverMax).HasColumnType("decimal(9, 8)");
            });

            modelBuilder.Entity<SalesTaxCustomField>(entity =>
            {
                entity.HasKey(e => e.FieldId);

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaxField)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ServiceQuote>(entity =>
            {
                entity.HasKey(e => e.QuoteId);

                entity.HasIndex(e => e.BranchId)
                    .HasName("IX_ServiceQuote_3");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("IX_ServiceQuote");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_ServiceQuote_5");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("IX_ServiceQuote_4");

                entity.HasIndex(e => e.ShipToCompanyId)
                    .HasName("IX_ServiceQuote_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_ServiceQuote_2");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuoteDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.QuoteNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SerialNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ShipToCompanyId).HasColumnName("ShipToCompanyID");

                entity.Property(e => e.StockNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ServiceQuoteAssignedPart>(entity =>
            {
                entity.HasKey(e => e.AssignedPartId);

                entity.HasIndex(e => e.QuoteId)
                    .HasName("IX_ServiceQuoteAssignedPart");

                entity.Property(e => e.AssignedPartId).HasColumnName("AssignedPartID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Discount).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Margin).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.PartNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Qty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.QuoteId).HasColumnName("QuoteID");
            });

            modelBuilder.Entity<SystemDefault>(entity =>
            {
                entity.HasKey(e => e.DefaultId);

                entity.HasIndex(e => e.Category)
                    .HasName("IX_SystemDefault");

                entity.HasIndex(e => e.DefaultName)
                    .HasName("IX_SystemDefault_1");

                entity.Property(e => e.DefaultId).HasColumnName("DefaultID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NumericValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StringValue)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SystemEmail>(entity =>
            {
                entity.HasKey(e => e.EmailId);

                entity.HasIndex(e => e.EmailType)
                    .HasName("IX_SystemEmail");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Body)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmailType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SystemLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId);

                entity.HasIndex(e => e.Language)
                    .HasName("IX_SystemLanguage");

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.Display)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FullDateTimeFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LongDateFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LongTimeFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ShortDateFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ShortTimeFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimestampFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(e => e.ContactId)
                    .HasName("IX_Task_2");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_Task_1");

                entity.HasIndex(e => e.Priority)
                    .HasName("IX_Task_6");

                entity.HasIndex(e => e.RelatedRecordId)
                    .HasName("IX_Task_3");

                entity.HasIndex(e => e.RelatedRecordType)
                    .HasName("IX_Task_4");

                entity.HasIndex(e => e.Status)
                    .HasName("IX_Task_5");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Task");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.CalendarId)
                    .IsRequired()
                    .HasColumnName("calendar_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompleteDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.EventId)
                    .IsRequired()
                    .HasColumnName("event_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Priority)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RelatedRecordId).HasColumnName("RelatedRecordID");

                entity.Property(e => e.RelatedRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReminderDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.ReminderRepeatSchedule)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StartDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Core.Domain.HeavyEquipment.Thread>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstMessageTimestamp).HasColumnName("first_message_timestamp");

                entity.Property(e => e.HasAttachments).HasColumnName("has_attachments");

                entity.Property(e => e.LastMessageReceivedTimestamp).HasColumnName("last_message_received_timestamp");

                entity.Property(e => e.LastMessageSentTimestamp).HasColumnName("last_message_sent_timestamp");

                entity.Property(e => e.LastMessageTimestamp).HasColumnName("last_message_timestamp");

                entity.Property(e => e.Object)
                    .IsRequired()
                    .HasColumnName("object")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Snippet)
                    .IsRequired()
                    .HasColumnName("snippet")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Starred).HasColumnName("starred");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasColumnName("subject")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Unread).HasColumnName("unread");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<TradeValuation>(entity =>
            {
                entity.HasKey(e => e.ValuationId);

                entity.HasIndex(e => e.Status)
                    .HasName("IX_TradeValuation_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_TradeValuation");

                entity.Property(e => e.ValuationId).HasColumnName("ValuationID");

                entity.Property(e => e.BookValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerOffer).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field1).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field10).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field2).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field3).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field4).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field5).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field6).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field7).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field8).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Field9).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Overallowance).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PayoffAmount).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.ReconditionEstimate).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TradeValue).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TradeValuationAssignedApprover>(entity =>
            {
                entity.HasKey(e => e.ApproverId);

                entity.Property(e => e.ApproverId).HasColumnName("ApproverID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ValuationId).HasColumnName("ValuationID");
            });

            modelBuilder.Entity<TradeValuationBookPriceFactor>(entity =>
            {
                entity.HasKey(e => e.FactorId);

                entity.Property(e => e.FactorId).HasColumnName("FactorID");

                entity.Property(e => e.Factor).HasColumnType("decimal(9, 4)");
            });

            modelBuilder.Entity<TradeValuationRouting>(entity =>
            {
                entity.HasKey(e => e.ApprovalId);

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_TradeValuationRouting");

                entity.Property(e => e.ApprovalId).HasColumnName("ApprovalID");

                entity.Property(e => e.RecordFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordSelection)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Udf>(entity =>
            {
                entity.ToTable("UDF");

                entity.HasIndex(e => e.Category)
                    .HasName("IX_UDF_1");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_UDF_2");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_UDF");

                entity.Property(e => e.Udfid).HasColumnName("UDFID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Editable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UdfassignedValue>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.ToTable("UDFAssignedValue");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_UDFAssignedValue_2");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_UDFAssignedValue_1");

                entity.HasIndex(e => e.Udfid)
                    .HasName("IX_UDFAssignedValue");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.DateValue).HasColumnType("date");

                entity.Property(e => e.LookupId).HasColumnName("LookupID");

                entity.Property(e => e.NumericValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StringValue)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Udfid).HasColumnName("UDFID");
            });

            modelBuilder.Entity<Udflookup>(entity =>
            {
                entity.ToTable("UDFLookup");

                entity.HasIndex(e => e.Udfid)
                    .HasName("IX_UDFLookup");

                entity.Property(e => e.UdflookupId).HasColumnName("UDFLookupID");

                entity.Property(e => e.Udfid).HasColumnName("UDFID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Active)
                    .HasName("IX_User_1");

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_User_2");

                entity.HasIndex(e => e.Email)
                    .HasName("IX_User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cell)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultCountry)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultLanguage)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultSearchType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordAccessFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordAccessRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordAccessType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimeZone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CST')");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserAccessFilter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserAccessRules)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserAccessType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserActionHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Timestamp).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserAlertClearLog>(entity =>
            {
                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_UserAlertClearLog");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_UserAlertClearLog_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserAlertClearLog_2");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClearDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserAssignedRight>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.RightId)
                    .HasName("IX_UserAssignedRight");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserAssignedRight_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.RightId).HasColumnName("RightID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserAssignedRole>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_UserAssignedRole_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserAssignedRole");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserAssignedSalesCode>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserAssignedSalesCode");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.SalesCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserDashBoardLayout>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.DashboardName)
                    .HasName("IX_UserDashBoardLayout_2");

                entity.HasIndex(e => e.DefaultLayout)
                    .HasName("IX_UserDashBoardLayout_3");

                entity.HasIndex(e => e.FormName)
                    .HasName("IX_UserDashBoardLayout");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserDashBoardLayout_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.DashboardName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FormName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GridStack)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserDefaultRecurringTouchProfile>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserDefaultRecurringTouchProfile");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserEmailAccessToken>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasColumnName("access_token")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<UserEmailCalendar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CalendarId)
                    .IsRequired()
                    .HasColumnName("calendar_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email_address")
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Events).HasColumnName("events");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReadOnly).HasColumnName("read_only");

                entity.Property(e => e.Sync).HasColumnName("sync");

                entity.Property(e => e.Tasks).HasColumnName("tasks");
            });

            modelBuilder.Entity<UserEmailProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId);

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("IX_UserEmailProfile_1");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasColumnName("access_token")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id")
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DateConnected)
                    .HasColumnName("date_connected")
                    .HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.DateDisconnected)
                    .HasColumnName("date_disconnected")
                    .HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email_address")
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastSync)
                    .HasColumnName("last_sync")
                    .HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasColumnName("provider")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('running')");

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasColumnName("token_type")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserFavoriteRecord>(entity =>
            {
                entity.HasKey(e => e.FavoriteId);

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_UserFavoriteRecord");

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_UserFavoriteRecord_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserFavoriteRecord_2");

                entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserGridColumnLayout>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => e.Grid)
                    .HasName("IX_UserGridColumnLayout");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserGridColumnLayout_1");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.Columns)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Grid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_UserGroup");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserGroupAssignedUser>(entity =>
            {
                entity.HasKey(e => e.AssignedId);

                entity.HasIndex(e => new { e.GroupId, e.UserId })
                    .HasName("IX_UserGroupAssignedUser");

                entity.Property(e => e.AssignedId).HasColumnName("AssignedID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserImpersonationLog>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.EndTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.ImpersonationUserId).HasColumnName("ImpersonationUserID");

                entity.Property(e => e.LoggedInUserId).HasColumnName("LoggedInUserID");

                entity.Property(e => e.StartTime).HasColumnType("datetimeoffset(0)");
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserLocation");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 7)");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserLoginHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserLoginHistory");

                entity.Property(e => e.HistoryId).HasColumnName("HistoryID");

                entity.Property(e => e.LoginDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);

                entity.HasIndex(e => e.TypeId)
                    .HasName("IX_UserNotification_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserNotification");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.ClearDate).HasColumnType("datetime");

                entity.Property(e => e.EmailSentDate).HasColumnType("datetime");

                entity.Property(e => e.JobId)
                    .IsRequired()
                    .HasColumnName("JobID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NotificationDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.PopupSentDate).HasColumnType("datetime");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserReminder>(entity =>
            {
                entity.HasKey(e => e.ReminderId);

                entity.HasIndex(e => e.ReminderDate)
                    .HasName("IX_UserReminder_1");

                entity.HasIndex(e => e.SendReminder)
                    .HasName("IX_UserReminder_2");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserReminder");

                entity.Property(e => e.ReminderId).HasColumnName("ReminderID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RelatedRecordId).HasColumnName("RelatedRecordID");

                entity.Property(e => e.RelatedRecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReminderDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasIndex(e => e.Deleted)
                    .HasName("IX_UserRole_1");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.DefaultRecurringTouchProfileId).HasColumnName("DefaultRecurringTouchProfileID");

                entity.Property(e => e.InternalRole)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserSearchFilter>(entity =>
            {
                entity.HasKey(e => e.FilterId);

                entity.HasIndex(e => e.RecordType)
                    .HasName("IX_UserSearchFilter_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserSearchFilter");

                entity.Property(e => e.FilterId).HasColumnName("FilterID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filter)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Scope)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserTerritory>(entity =>
            {
                entity.HasKey(e => e.TerritoryId);

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_UserTerritory_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserTerritory");

                entity.Property(e => e.TerritoryId).HasColumnName("TerritoryID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.TerritoryCriteria)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TerritoryRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserType1)
                    .IsRequired()
                    .HasColumnName("UserType")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<WorkflowRule>(entity =>
            {
                entity.HasKey(e => e.RuleId);

                entity.Property(e => e.RuleId).HasColumnName("RuleID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FieldOperator)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fields)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RuleTrigger)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<WorkflowRuleAssignment>(entity =>
            {
                entity.HasKey(e => e.AssignmentId);

                entity.HasIndex(e => new { e.ActionType, e.ActionId })
                    .HasName("IX_WorkflowRuleAssignment");

                entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AssignmentObject)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AssignmentObjectId).HasColumnName("AssignmentObjectID");
            });

            modelBuilder.Entity<WorkflowRuleCondition>(entity =>
            {
                entity.HasKey(e => e.ConditionId);

                entity.HasIndex(e => e.RuleId)
                    .HasName("IX_WorkflowRuleCondition");

                entity.Property(e => e.ConditionId).HasColumnName("ConditionID");

                entity.Property(e => e.ConditionRules)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ConditionSql)
                    .IsRequired()
                    .HasColumnName("ConditionSQL")
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ConditionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RuleId).HasColumnName("RuleID");
            });

            modelBuilder.Entity<WorkflowRuleEmail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConditionId).HasColumnName("ConditionID");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            });

            modelBuilder.Entity<WorkflowRuleTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.ConditionId).HasColumnName("ConditionID");

                entity.Property(e => e.TaskDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaskDueDateTrigger)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaskPriority)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaskStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TaskSubject)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Query<WorkflowRuleMatch>();
            modelBuilder.Query<WorkflowRuleFieldUpdateMatch>();
            modelBuilder.Query<WorkflowRuleUserAssignment>();
        }

        public List<int?> SP_WorkflowRuleMatch(string module, int recordId, string trigger)
        {
            var p_module = new SqlParameter("@Module", module);
            var p_recordId = new SqlParameter("@RecordID", recordId);
            var p_trigger = new SqlParameter("@Trigger", trigger);

            return this.Query<WorkflowRuleMatch>()
                .FromSql("EXECUTE dbo.WorkflowRuleMatch @Module, @RecordID, @Trigger",
                    p_module,
                    p_recordId,
                    p_trigger)
                .Select(x => x.ConditionID)
                .ToList();
        }

        public List<int?> GetWorkflowRuleUserAssignments(string recordType, int recordId, string actionType, int actionId)
        {
            var p_recordType = new SqlParameter("@RecordType", recordType);
            var p_recordId = new SqlParameter("@RecordID", recordId);
            var p_actionType = new SqlParameter("@ActionType", actionType);
            var p_actionId = new SqlParameter("@ActionID", actionId);

            return this.Query<WorkflowRuleUserAssignment>()
                .FromSql("EXECUTE dbo.WorkflowRuleUserAssignment @RecordType, @RecordID, @ActionType, @ActionID",
                    p_recordType,
                    p_recordId,
                    p_actionType,
                    p_actionId)
                .Select(x => x.UserID)
                .ToList();
        }

        public List<int?> SP_WorkflowRuleFieldUpdateMatch(string module, int recordId, string fields)
        {
            var p_module = new SqlParameter("@Module", module);
            var p_recordId = new SqlParameter("@RecordID", recordId);
            var p_fields = new SqlParameter("@Fields", fields);

            return this.Query<WorkflowRuleFieldUpdateMatch>()
                .FromSql("EXECUTE dbo.WorkflowRuleFieldUpdateMatch @Module, @RecordID, @Fields",
                    p_module,
                    p_recordId,
                    p_fields)
                .Select(x => x.ConditionID)
                .ToList();
        }
    }
}
