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
			ContentMode = UIViewContentMode.Redraw;
		}

		public override void Draw (System.Drawing.RectangleF rect)
		{
			var currentContext = UIGraphics.GetCurrentContext ();
			var properRect = AdjustForThickness (rect);
			HandleShapeDraw (currentContext, properRect);
		}

		protected RectangleF AdjustForThickness (RectangleF rect)
		{
			var x = rect.X + Element.Padding.Left;
			var y = rect.Y + Element.Padding.Top;
			var width = rect.Width - Element.Padding.HorizontalThickness;
			var height = rect.Height - Element.Padding.VerticalThickness;
			return new RectangleF ((float)x, (float)y, (float)width, (float)height);
		}

		protected virtual void HandleShapeDraw (CGContext currentContext, RectangleF rect)
		{
			// Only used for circles
			var centerX = rect.X + (rect.Width / 2);
			var centerY = rect.Y + (rect.Height / 2);
			var radius = rect.Width / 2;
			var startAngle = 0;
			var endAngle = (float)(Math.PI * 2);

			switch (Element.ShapeType) {
			case ShapeType.Box:
				HandleStandardDraw (currentContext, rect, () => {
					if (Element.CornerRadius > 0) {
						var path = UIBezierPath.FromRoundedRect (rect, Element.CornerRadius);
						currentContext.AddPath (path.CGPath);
					} else {
						currentContext.AddRect (rect);
					}
				});
				break;
			case ShapeType.Circle:
				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (centerX, centerY, radius, startAngle, endAngle, true));
				break;
			case ShapeType.CircleIndicator:
				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (centerX, centerY, radius, startAngle, endAngle, true));
				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (centerX, centerY, radius, QuarterTurnCounterClockwise, (float)(Math.PI * 2 * (Element.IndicatorPercentage / 100)) + QuarterTurnCounterClockwise, false), Element.StrokeWidth + 3);
				break;
			}
		}

		/// <summary>
		/// A simple method for handling our drawing of the shape. This method is called differently for each type of shape
		/// </summary>
		/// <param name="currentContext">Current context.</param>
		/// <param name="rect">Rect.</param>
		/// <param name="createPathForShape">Create path for shape.</param>
		/// <param name="lineWidth">Line width.</param>
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
