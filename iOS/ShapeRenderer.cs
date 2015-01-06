using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using DrawShape;
using DrawShape.iOS;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

[assembly:ExportRenderer (typeof(ShapeView), typeof(ShapeRenderer))]
namespace DrawShape.iOS
{
	public class ShapeRenderer : VisualElementRenderer<ShapeView>
	{
		private readonly float QuarterTurnCounterClockwise = (float)(-1f * (Math.PI * 0.5f));

		public ShapeRenderer ()
		{
		}

		public override void Draw (System.Drawing.RectangleF rect)
		{
			var currentContext = UIGraphics.GetCurrentContext ();
			HandleShapeDraw (currentContext, rect);
		}

		protected virtual void HandleShapeDraw (CGContext currentContext, RectangleF rect)
		{
			switch (Element.ShapeType) {
			case ShapeType.Box:
				HandleStandardDraw (currentContext, rect, () => currentContext.AddRect (rect));
				break;
			case ShapeType.Circle:
				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (rect.Width / 2, rect.Height / 2, (rect.Width - 10) / 2, 0, (float)(Math.PI * 2), true));
				break;
			case ShapeType.CircleIndicator:
				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (rect.Width / 2, rect.Height / 2, (rect.Width - 10) / 2, 0, (float)(Math.PI * 2), true));
				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (rect.Width / 2, rect.Height / 2, (rect.Width - 10) / 2, QuarterTurnCounterClockwise, (float)(Math.PI * 2 * (Element.IndicatorPercentage / 100)) + QuarterTurnCounterClockwise, false), Element.StrokeWidth + 3);
				break;
			}
		}

		protected virtual void HandleStandardDraw (CGContext currentContext, RectangleF rect, Action createPathForShape, float? lineWidth = null)
		{
			currentContext.SetLineWidth (lineWidth ?? Element.StrokeWidth);
			currentContext.SetFillColor (base.Element.Color.ToCGColor ());
			currentContext.SetStrokeColor (Element.StrokeColor.ToCGColor ());

			createPathForShape ();

			currentContext.DrawPath (MonoTouch.CoreGraphics.CGPathDrawingMode.FillStroke);
		}
	}
}