﻿using System;
using System.Collections.Generic;
using crmSeries.Core.Data;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Inspections.Dtos;
//using crmSeries.Core.Features.Inspection.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Threading.Tasks;
//using Microsoft.Azure.Storage;
//using Microsoft.Azure.Storage.Blob;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    public class AddInspectionRequest : AddInspectionDto, IRequest<AddResponse>
    {
    }

    public class AddInspectionHandler : IRequestHandler<AddInspectionRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        //public AddInspectionHandler(HeavyEquipmentContext context,
        //    IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        //{
        //    _context = context;
        //    _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        //}

        public AddInspectionHandler(HeavyEquipmentContext context)
        {
            _context = context;

        }

        public Task<Response<AddResponse>> HandleAsync(AddInspectionRequest request)
        {
            // Save Inspection
            var assignedInspectionId = 0;
            var recordAssignInspection = request.inspection.MapTo<Domain.HeavyEquipment.RecordAssignedInspection>();

            _context.Set<Domain.HeavyEquipment.RecordAssignedInspection>().Add(recordAssignInspection);
            _context.SaveChanges();

            assignedInspectionId = recordAssignInspection.AssignedInspectionId;

            // save groups

            List<Assign> groupObjs = saveAssignedGroups(request.group, assignedInspectionId);


            // save assigned inspection Item
            List<Assign> itemObjs = saveAssignedItems(request.inspectionItem, groupObjs);
            // save Image
            saveAssignedItemImages(request, assignedInspectionId, itemObjs);

            //Save Response
            saveAssignedItemResponses(request, itemObjs);
            return new AddResponse
            {
                Id = assignedInspectionId
            }.AsResponseAsync();
        }

        private void saveAssignedItemImages(AddInspectionRequest request, int assignedInspectionId, List<Assign> itemObjs)
        {
            var blobConfig = _context.Set<Domain.HeavyEquipment.CrmSeriesConnectConfiguration>();




            foreach (RecordAssignedInspectionImageDto image in request.image)
            {
                int assItemId = itemObjs.Find(g => g.Id == image.ItemId).assignedId;
                var recordAssignedInspectionImage = image.MapTo<Domain.HeavyEquipment.RecordAssignedInspectionImage>();
                recordAssignedInspectionImage.AssignedInspectionId = assignedInspectionId;
                recordAssignedInspectionImage.AssignedItemId = assItemId;

                _context.Set<Domain.HeavyEquipment.RecordAssignedInspectionImage>().Add(recordAssignedInspectionImage);
                _context.SaveChanges();
            }
        }
        async private static void blobConnection(HeavyEquipmentContext context)
        {
            //var blobConfig = (from b in context.Set<Domain.HeavyEquipment.CrmSeriesConnectConfiguration>() select b).ProjectTo<blobConn>()
            //    .SingleOrDefault(x => x.UploadType == "Blob");

            //CloudStorageAccount storageAccount;
            //if (CloudStorageAccount.TryParse(blobConfig.ConnectionString, out storageAccount))
            //{
            //    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            //    CloudBlobContainer cloudBlobContainer =
            //    cloudBlobClient.GetContainerReference(blobConfig.BlobContainer);

            //    BlobContainerPermissions permissions = new BlobContainerPermissions
            //    {
            //        PublicAccess = BlobContainerPublicAccessType.Blob
            //    };
            //    await cloudBlobContainer.SetPermissionsAsync(permissions);
            //}
        }
        private void saveAssignedItemResponses(AddInspectionRequest request, List<Assign> itemObjs)
        {
            foreach (RecordAssinedInspectionItemResponseDto response in request.response)
            {
                int assItemId = itemObjs.Find(g => g.Id == response.ItemId).assignedId;
                var recordAssignedInspectionItemResponse = response.MapTo<Domain.HeavyEquipment.RecordAssignedInspectionItemResponse>();
                recordAssignedInspectionItemResponse.AssignedItemId = assItemId;

                _context.Set<Domain.HeavyEquipment.RecordAssignedInspectionItemResponse>().Add(recordAssignedInspectionItemResponse);
                _context.SaveChanges();

            }
        }

        private List<Assign> saveAssignedItems(List<RecordAssignedInspectionItemDto> request, List<Assign> groupObjs)
        {
            List<Assign> itemObjs = new List<Assign>();
            foreach (RecordAssignedInspectionItemDto item in request)
            {
                int assGroupId = groupObjs.Find(g => g.Id == item.GroupID).assignedId;
                var recordAssignedInspectionItem = item.MapTo<Domain.HeavyEquipment.RecordAssignedInspectionItem>();
                recordAssignedInspectionItem.AssignedGroupId = assGroupId;

                _context.Set<Domain.HeavyEquipment.RecordAssignedInspectionItem>().Add(recordAssignedInspectionItem);
                _context.SaveChanges();

                Assign itemObj = new Assign
                {
                    Id = item.ItemID,
                    Name = item.Item,
                    assignedId = recordAssignedInspectionItem.AssignedItemId
                };
                itemObjs.Add(itemObj);
            }

            return itemObjs;
        }

        private List<Assign> saveAssignedGroups(List<RecordAssignedInspectionGroupDto> request, int assignedInspectionId)
        {
            List<Assign> groupObjs = new List<Assign>();
            foreach (RecordAssignedInspectionGroupDto group in request)
            {
                var recordAssignedInspectionGroup = group.MapTo<Domain.HeavyEquipment.RecordAssignedInspectionGroup>();
                recordAssignedInspectionGroup.AssignedInspectionId = assignedInspectionId;


                _context.Set<Domain.HeavyEquipment.RecordAssignedInspectionGroup>().Add(recordAssignedInspectionGroup);
                _context.SaveChanges();


                Assign groupObj = new Assign
                {
                    Id = group.GroupID,
                    Name = group.GroupName,
                    assignedId = recordAssignedInspectionGroup.AssignedGroupId
                };
                groupObjs.Add(groupObj);
            }

            return groupObjs;
        }
    }

    public struct Assign
    {
        public int Id;
        public string Name;
        public int assignedId;
    }

    public class blobConn
    {
        public int ConfigId { get; set; }
        public bool FtpSecure { get; set; }
        public string FtpHost { get; set; }
        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
        public string ConnectionString { get; set; }
        public string UploadType { get; set; }
        public string BlobContainer { get; set; }
    }
    public class AddInspectionValidator : AbstractValidator<AddInspectionRequest>
    {
        public AddInspectionValidator()
        {
            //Include(new BaseInspectionDtoValidator());
        }
    }
}
