using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Inventory.Attributes
{
    public class MyDateConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is DateTime)
            {
                string date = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
                writer.WriteValue(date);
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}