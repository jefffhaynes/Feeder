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

		private async Task Post(string url, object o)
		{
			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
			request.ContentType = "text/json";
			request.Method = "POST";

			if (o != null) {
				using (Stream requestStream = await request.GetRequestStreamAsync ()) {
					using (var writer = new StreamWriter (requestStream)) {
						var json = JsonConvert.SerializeObject (o);

						await writer.WriteAsync (json);
					}
				}
			}

			await request.GetResponseAsync ();
		}

		#endregion
	}
}

