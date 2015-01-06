using System;
using Android.Views;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Util;

namespace DrawShape.Android
{
	public class Shape : View
	{
		private readonly float QuarterTurnCounterClockwise = -90;

		public ShapeView ShapeView{ get; set; }

		public Shape (Context context) : base (context)
		{
		}

		public Shape (Context context, IAttributeSet attributes) : base (context, attributes)
		{
		}

		public Shape (Context context, IAttributeSet attributes, int defStyle) : base (context, attributes, defStyle)
		{
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);
			HandleShapeDraw (canvas);
		}

		protected virtual void HandleShapeDraw (Canvas canvas)
		{
			switch (ShapeView.ShapeType) {
			case ShapeType.Box:
				HandleStandardDraw (canvas, p => canvas.DrawRect (this.GetX (), this.GetY (), this.GetX () + this.Width, this.GetY () + this.Height, p));
				break;
			case ShapeType.Circle:
				HandleStandardDraw (canvas, p => canvas.DrawCircle (this.GetX () + this.Width / 2, this.GetY () + this.Height / 2, (this.Width - 10) / 2, p));
				break;
			case ShapeType.CircleIndicator:
				HandleStandardDraw (canvas, p => canvas.DrawCircle (this.GetX () + this.Width / 2, this.GetY () + this.Height / 2, (this.Width - 10) / 2, p), drawFill: false);
				HandleStandardDraw (canvas, p => canvas.DrawArc (new RectF (this.GetX (), this.GetY (), this.GetX () + this.Width, this.GetY () + this.Height), QuarterTurnCounterClockwise, 360 * (ShapeView.IndicatorPercentage / 100), false, p), ShapeView.StrokeWidth + 3, false);
				//				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (rect.Width / 2, rect.Height / 2, (rect.Width - 10) / 2, 0, (float)(Math.PI * 2), true));
				//				HandleStandardDraw (currentContext, rect, () => currentContext.AddArc (rect.Width / 2, rect.Height / 2, (rect.Width - 10) / 2, QuarterTurnCounterClockwise, (float)(Math.PI * 2 * (Element.IndicatorPercentage / 100)) + QuarterTurnCounterClockwise, false), Element.StrokeWidth + 3);
				break;
			}
		}

		protected virtual void HandleStandardDraw (Canvas canvas, Action<Paint> drawShape, float? lineWidth = null, bool drawFill = true)
		{
			var strokePaint = new Paint (PaintFlags.AntiAlias);
			strokePaint.SetStyle (Paint.Style.Stroke);
			strokePaint.StrokeWidth = lineWidth ?? ShapeView.StrokeWidth;
			strokePaint.StrokeCap = Paint.Cap.Round;
			strokePaint.Color = ShapeView.StrokeColor.ToAndroid ();
			var fillPaint = new Paint ();
			fillPaint.SetStyle (Paint.Style.Fill);
			fillPaint.Color = ShapeView.Color.ToAndroid ();

			if (drawFill)
				drawShape (fillPaint);
			drawShape (strokePaint);
		}
	}
}