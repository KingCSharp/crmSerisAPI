using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using crmSeries.Core.Extensions;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Common
{
    [DoNotValidate]
    public class ConvertClassWithNestedClassesToJsonRequest : IRequest<JObject>
    {
        public Type Type { get; set; }
    }

    public class ConvertClassWithNestedClassesToJsonRequestHandler : IRequestHandler<ConvertClassWithNestedClassesToJsonRequest, JObject>
    {
        public Task<Response<JObject>> HandleAsync(ConvertClassWithNestedClassesToJsonRequest message)
        {
            var json = DigTypes(message.Type) + "}";
            json = RollUpTrailingCurlyBraces(json);
            return Task.FromResult(JObject.Parse(json).AsResponse());
        }

        public string DigTypes(Type type)
        {
            var json = "{\"" + type.Name.ToCamel() + "\": {";
            var temp = "";

            var nextedTypes = type.GetNestedTypes();
            var fields = type.GetFields().ToDictionary(x => x.Name.ToCamel(), x => x.GetValue(null));

            if (nextedTypes.Any())
            {
                foreach (var nextedType in nextedTypes)
                {
                    temp = DigTypes(nextedType);
                    temp = temp.Remove(0, 1);
                    json += temp;
                }
            }

            temp = JsonConvert.SerializeObject(fields);
            temp = RemoveOneCharFromFrontAndBack(temp);

            json += temp + "},";

            return json;
        }

        private string RemoveOneCharFromFrontAndBack(string word)
        {
            word = word.Remove(0, 1);
            word = word.Remove(word.Length - 1, 1);
            return word;
        }

        private string RollUpTrailingCurlyBraces(string json)
        {
            var i = json.Length - 1;
            var count = 0;
            var foundCurly = 0;

            while (json[i] == ',' || json[i] == '}')
            {
                count++;
                if (json[i] == '}')
                {
                    foundCurly++;
                }

                i--;
            }

            json = json.Remove(json.Length - count, count);
            json += "}".Repeat(foundCurly);

            return json;
        }
    }
}
