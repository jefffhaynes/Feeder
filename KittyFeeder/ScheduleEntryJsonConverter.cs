using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace KittyFeeder
{
	public class ScheduleEntryJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(ScheduleEntryModel).IsAssignableFrom(objectType);
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject item = JObject.Load(reader);
			var type = (ScheduleEntryType)item ["type"].Value<int> ();
			switch (type)
			{
			case ScheduleEntryType.Weekly:
				return item.ToObject<WeeklyScheduleEntryModel>();
			default:
				throw new InvalidEnumArgumentException();
			}
		}

		public override void WriteJson(JsonWriter writer, 
			object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}

