using System;
using Xamarin.Forms;

namespace DrawShape
{
	public class ShapeView : BoxView
	{
		public static readonly BindableProperty ShapeTypeProperty = BindableProperty.Create<ShapeView, ShapeType> (s => s.ShapeType, ShapeType.Box);

		public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create<ShapeView, Color> (s => s.StrokeColor, Color.Default);

		public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create<ShapeView, float> (s => s.StrokeWidth, 1f);

		public static readonly BindableProperty IndicatorPercentageProperty = BindableProperty.Create<ShapeView, float> (s => s.IndicatorPercentage, 0f);

		public ShapeType ShapeType {
			get{ return (ShapeType)GetValue (ShapeTypeProperty); }
			set{ SetValue (ShapeTypeProperty, value); }
		}

		public Color StrokeColor {
			get{ return (Color)GetValue (StrokeColorProperty); }
			set{ SetValue (StrokeColorProperty, value); }
		}

		public float StrokeWidth {
			get{ return (float)GetValue (StrokeWidthProperty); }
			set{ SetValue (StrokeWidthProperty, value); }
		}

		public float IndicatorPercentage {
			get{ return (float)GetValue (IndicatorPercentageProperty); }
			set {
				if (ShapeType != ShapeType.CircleIndicator)
					throw new ArgumentException ("Can only specify this property with CircleIndicator");
				SetValue (IndicatorPercentageProperty, value);
			}
		}

		public ShapeView ()
		{
		}
	}

	public enum ShapeType
	{
		Box,
		Circle,
		CircleIndicator
	}
}