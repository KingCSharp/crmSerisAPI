using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.UserFavoriteRecords.Dtos
{
    public class BaseUserFavoriteRecordDto
    {
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int UserId { get; set; }
    }

    public class GetUserFavoriteRecordDto : BaseUserFavoriteRecordDto
    {
        public int FavoriteId { get; set; }
    }

    public class AddUserFavoriteRecordDto : BaseUserFavoriteRecordDto
    {
    }
}
