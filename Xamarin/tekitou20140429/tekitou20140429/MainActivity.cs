using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace tekitou20140429 {
	[Activity (Label = "tekitou20140429", MainLauncher = true)]
	public class MainActivity : Activity {
		int countL = 1;
		int countR = 1;
		int Sum = 0;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			Button button2 = FindViewById<Button> (Resource.Id.myButton2);
			Button button3 = FindViewById<Button> (Resource.Id.myButtonL);

			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", countL++);
				Sum++;
				button3.Text = string.Format ("{0} !?", Sum);
			};
			button2.Click += delegate {
				button2.Text = string.Format ("{0} clicks!", countR++);
				Sum--;
				button3.Text = string.Format ("{0} !?", Sum);
			};
		}
	}
}


