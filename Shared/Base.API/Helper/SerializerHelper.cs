using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Base.API.Helper
{
    public static class SerializerHelper
    {
        public static string SerializeByJsonConvert(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, new JsonSerializerOptions
                {
                    WriteIndented = false,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
            }
            catch
            {
                return "[Serialization Error]";
            }
        }
    }
}
