using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;

namespace DrawShape.iOS
{
	[Foundation.Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow ((RectangleF)UIScreen.MainScreen.Bounds);
			
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

