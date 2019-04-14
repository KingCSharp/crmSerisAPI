using crmSeries.Core.Mediator;

namespace crmSeries.Core.Extensions
{
    public static class RequestExtensions
    {
        public static Response AsErrorResponse(this IRequest item, string errorMessage, string property = "")
        {
            return CreateErrorResponse<object>(errorMessage, property);
        }

        public static Response<T> AsErrorResponse<T>(this IRequest<T> item, string errorMessage, string property = "")
        {
            return CreateErrorResponse<T>(errorMessage, property);
        }

        private static Response<T> CreateErrorResponse<T>(string errorMessage, string property)
        {
            var result = new Response<T>();
            result.Errors.Add(new Error
            {
                PropertyName = property,
                ErrorMessage = errorMessage
            });

            return result;
        }
    }
}