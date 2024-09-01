//namespace Kurama.Controls;

//public partial class Voice : ContentView
//{
//    private double[] _amplitudes;
//    private readonly int _barCount = 50;
//    private readonly BoxView[] _bars;

//    public Voice()
//    {
//        InitializeComponent();

//        _amplitudes = new double[_barCount];
//        _bars = new BoxView[_barCount];

//        var stackLayout = new StackLayout
//        {
//            Orientation = StackOrientation.Horizontal,
//            HorizontalOptions = LayoutOptions.Center,
//            VerticalOptions = LayoutOptions.Center,
//            Spacing = 2
//        };

//        for (int i = 0; i < _barCount; i++)
//        {
//            var bar = new BoxView
//            {
//                WidthRequest = 5,
//                HeightRequest = 0,
//                BackgroundColor = Colors.HotPink,
//                VerticalOptions = LayoutOptions.End
//            };
//            _bars[i] = bar;
//            stackLayout.Children.Add(bar);
//        }

//        Content = stackLayout;
//    }

//    public void UpdateAmplitudes(double[] amplitudes)
//    {
//        _amplitudes = amplitudes;
//        for (int i = 0; i < _bars.Length; i++)
//        {
//            _bars[i].HeightRequest = _amplitudes[i] * this.Height; // Обновляем высоту полосы на основе амплитуды
//        }
//    }
//}