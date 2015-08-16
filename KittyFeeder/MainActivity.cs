using System;

using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Json;
using Newtonsoft.Json;

namespace KittyFeeder
{
	[Activity (Label = "KittyFeeder", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication (new App ());

            // Set our view from the "main" layout resource
//            SetContentView (Resource.Layout.Main);
//
//			// Get our button from the layout resource,
//			// and attach an event to it
//			Button button = FindViewById<Button> (Resource.Id.myButton);
//			
//			button.Click += async delegate {
//                button.Text = string.Format("{0} clicks!", count++);
//                var endPoint = @"https://agent.electricimp.com/tKsHHkg3DT0Y";
//                await Feed(endPoint);
//            };
		}


        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
    }
}


