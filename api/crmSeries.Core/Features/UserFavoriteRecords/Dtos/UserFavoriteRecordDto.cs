using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.UserFavoriteRecords.Dtos
{
    public class BaseUserFavoriteRecordDto
    {
        /// <summary>
        /// The unique identifier of the entity being favorited.
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// The type of entity being favorited. E.g., Company, Contact, Lead, etc.
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// The unique identifier of the user that is favoriting this record.
        /// </summary>
        public int UserId { get; set; }
    }

    public class GetUserFavoriteRecordDto : BaseUserFavoriteRecordDto
    {
        /// <summary>
        /// The unique identifier of the user favorite record.
        /// </summary>
        public int FavoriteId { get; set; }
    }

    public class AddUserFavoriteRecordDto : BaseUserFavoriteRecordDto
    {
    }
}
