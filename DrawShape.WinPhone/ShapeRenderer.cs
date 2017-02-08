using DrawShape;
using DrawShape.WinPhone;
using Xamarin.Forms.Platform.WinRT;

[assembly:ExportRenderer (typeof(ShapeView), typeof(ShapeRenderer))]
namespace DrawShape.WinPhone
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;
    using DrawShape;
    using Xamarin.Forms.Platform.WinRT;

    public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
    {
        private Ellipse _ellipse;
        private Rectangle _rectangle;

        protected override void OnElementChanged(ElementChangedEventArgs<ShapeView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            var converter = new ColorConverter();

            var color = (SolidColorBrush)converter.Convert(Element.Color, null, null, null);
            var strockColor = (SolidColorBrush)converter.Convert(Element.StrokeColor, null, null, null);
            var margin = new Thickness(Element.Padding.Left, Element.Padding.Top, Element.Padding.Right, Element.Padding.Bottom);
            switch (Element.ShapeType)
            {
                case ShapeType.Box:
                    _rectangle = new Rectangle
                    {
                        Fill = color,
                        Stroke = strockColor,
                        StrokeThickness = Element.StrokeWidth,
                        Margin = margin,
                        RadiusX = Element.CornerRadius,
                        RadiusY = Element.CornerRadius,
                    };
                    SetNativeControl(_rectangle);
                    break;
                case ShapeType.Circle:
                    _ellipse = new Ellipse
                    {
                        Fill = color,
                        Stroke = strockColor,
                        StrokeThickness = Element.StrokeWidth,
                        Margin = margin
                    };
                    SetNativeControl(_ellipse);
                    break;
                case ShapeType.CircleIndicator:
                    // TODO WinPhone circle indicator
                    break;
            }
        }
    }
}