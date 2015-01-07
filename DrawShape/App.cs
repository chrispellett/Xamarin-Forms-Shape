using System;
using Xamarin.Forms;

namespace DrawShape
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			// XAML sample
			return new SamplePage ();

			// Code sample
//			return new ContentPage () {
//				Content = new StackLayout () {
//					Children = {
//						new Label () {
//							Text = "Hello world"
//						},
//						new BoxView () {
//							Color = Color.Red
//						},
//						new ShapeView () {
//							ShapeType = ShapeType.Circle,
//							StrokeColor = Color.Blue,
//							Color = Color.Yellow,
//							StrokeWidth = 5f
//						},
//						new ShapeView(){
//							ShapeType = ShapeType.CircleIndicator,
//							StrokeColor = Color.Silver,
//							IndicatorPercentage = 75f,
//							StrokeWidth = 3f
// 						}
//					},
//					Orientation = StackOrientation.Horizontal,
//					HorizontalOptions = LayoutOptions.FillAndExpand,
//					VerticalOptions = LayoutOptions.Center,
//				}
//			};
		}
	}
}