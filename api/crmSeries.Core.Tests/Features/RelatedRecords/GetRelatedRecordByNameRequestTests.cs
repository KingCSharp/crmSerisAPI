using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.RelatedRecords;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.RelatedRecords
{
    [TestFixture]
    public class GetRelatedRecordNameHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_RelatedRecordTypeIsCompanyName_SetsNameToCompanyName()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Foo Company"
                };

                context.Company.Add(company);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    company.CompanyId,
                    Constants.RelatedRecord.Types.Company));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data, company.CompanyName);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsCompanyAndItDoesNotExist_SetsNameToNull()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Nubitz, LLC"
                };

                context.Company.Add(company);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    company.CompanyId + 1,
                    Constants.RelatedRecord.Types.Company));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.IsNull(response.Result.Data);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsContact_SetsNameToContactFirstNameAndLastNameConcatenated()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe"
                };

                context.Contact.Add(contact);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    contact.ContactId,
                    Constants.RelatedRecord.Types.Contact));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data, $"{contact.FirstName} {contact.LastName}");
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsContactAndContactHasNoLastName_SetsNameToContactFirstName()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = ""
                };

                context.Contact.Add(contact);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    contact.ContactId,
                    Constants.RelatedRecord.Types.Contact));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data, $"{contact.FirstName} ");
            }
        }
        
        [Test]
        public void HandleAsync_RelatedRecordTypeIsContactDoesNotExist_SetsNameToNull()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = ""
                };

                context.Contact.Add(contact);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    contact.ContactId + 1,
                    Constants.RelatedRecord.Types.Contact));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.IsNull(response.Result.Data);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsLead_SetsNameToLeadFirstNameAndLastNameConcatenated()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Doe"
                };

                context.Lead.Add(lead);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    lead.LeadId,
                    Constants.RelatedRecord.Types.Lead));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data, $"{lead.FirstName} {lead.LastName}");
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsLeadAndItDoesNotExist_SetsNameToNull()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Doe"
                };

                context.Lead.Add(lead);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    lead.LeadId + 1,
                    Constants.RelatedRecord.Types.Lead));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.IsNull(response.Result.Data);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsLeadAndLastNameIsEmpty_SetsNameToFirstName()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = ""
                };

                context.Lead.Add(lead);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    lead.LeadId,
                    Constants.RelatedRecord.Types.Lead));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.AreEqual($"{lead.FirstName} ", response.Result.Data);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordTypeIsLeadAndFirstNameAndLastNameAreEmpty_SetsNameToEmpty()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "",
                    LastName = ""
                };

                context.Lead.Add(lead);
                context.SaveChanges();
                var handler = new GetRelatedRecordNameHandler(context);

                // Act
                var response = handler.HandleAsync(new GetRelatedRecordNameRequest(
                    lead.LeadId,
                    Constants.RelatedRecord.Types.Lead));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.AreEqual($" ", response.Result.Data);
            }
        }
    }
}

