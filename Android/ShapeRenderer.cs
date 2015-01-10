using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DrawShape;
using DrawShape.Android;
using Android.Graphics;

[assembly:ExportRenderer (typeof(ShapeView), typeof(ShapeRenderer))]
namespace DrawShape.Android
{
	public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
	{
		public ShapeRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<ShapeView> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || this.Element == null)
				return;

			SetNativeControl (new Shape (Resources.DisplayMetrics.Density, Context) {
				ShapeView = Element
			});
		}
	}
}