using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KittyFeeder
{
	public class App : Xamarin.Forms.Application
    {
        public App()
		{
			var feeder = new ImpFeeder (@"https://agent.electricimp.com/tKsHHkg3DT0Y");
			var viewModel = new ViewModel (feeder);

			var mainPage = new MainPage ();
			mainPage.BindingContext = viewModel;

			MainPage = mainPage;
        }
    }
}