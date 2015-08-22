using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public class ImpFeeder : IFeeder
	{
		string _url;

		public ImpFeeder (string url)
		{
			_url = url;
		}

		#region IFeeder implementation

		public async Task Feed()
		{
			await Post (_url + "/feed", null);
		}

		public async Task SetSchedule(ScheduleModel schedule)
		{
			await Post (_url + "/schedule", schedule);
		}

		public async Task<ScheduleModel> GetSchedule()
		{
			return await Get<ScheduleModel> (_url + "/schedule");
		}

		private async Task Post(string url, object o)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
			request.ContentType = "text/json";
			request.Method = "POST";

			if (o != null) {
				using (Stream requestStream = await request.GetRequestStreamAsync ()) 
				{
					using (var writer = new StreamWriter (requestStream)) 
					{
						var json = JsonConvert.SerializeObject (o);
						await writer.WriteAsync (json);
					}
				}
			}

			await request.GetResponseAsync ();
		}

		private async Task<T> Get<T>(string url)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.Method = "GET";

			using (var response = await request.GetResponseAsync ()) 
			{
				using (var stream = response.GetResponseStream())
				using (var reader = new StreamReader (stream))
				using (var jsonTextReader = new JsonTextReader (reader)) 
				{
					JsonSerializer serializer = new JsonSerializer ();
					return (T)serializer.Deserialize (jsonTextReader);
				}
			}
		}

		#endregion
	}
}

