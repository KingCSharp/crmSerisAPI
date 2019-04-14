using crmSeries.Core.Mediator;
using System.Linq;

namespace crmSeries.Core.Extensions
{
    public static class ResponseExtensions
    {
        public static Response CopyErrorsTo(this Response source, Response dest)
        {
            dest.Errors.AddRange(source.Errors.Select(x => new Error
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName
            }));

            return dest;
        }

        public static Response<T> CopyErrorsTo<T>(this Response source, Response<T> dest)
        {
            dest.Errors.AddRange(source.Errors.Select(x => new Error
            {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName
            }));

            return dest;
        }
    }
}
