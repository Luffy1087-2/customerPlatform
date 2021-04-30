using System.Buffers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomerPlatform.WebApi.Tools
{
    internal static class ModelBinderUtility
    {
        public static async Task<string> GetJsonDtoString(ModelBindingContext bindingContext)
        {
            System.IO.Pipelines.ReadResult readResult = await bindingContext.HttpContext.Request.BodyReader.ReadAsync();

            string jsonString = Encoding.UTF8.GetString(readResult.Buffer.ToArray());

            return jsonString;
        }
    }
}
