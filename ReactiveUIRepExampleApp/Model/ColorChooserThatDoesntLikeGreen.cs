using System.Reactive.Linq;
using System.Windows.Media;
using ReactiveUI;
using ReactiveCommand = ReactiveUI.Legacy.ReactiveCommand;

namespace ReactiveUIRepExampleApp.Model
{
    public class ColorChooserThatDoesntLikeGreen : ReactiveObject
    {
        private byte _red;
        public byte Red
        {
            get { return _red; }
            set { this.RaiseAndSetIfChanged(ref _red, value); }
        }

        private byte _green;
        public byte Green
        {
            get { return _green; }
            set { this.RaiseAndSetIfChanged(ref _green, value); }
        }

        private byte _blue;
        public byte Blue
        {
            get { return _blue; }
            set { this.RaiseAndSetIfChanged(ref _blue, value); }
        }

        private readonly ObservableAsPropertyHelper<Color> _color;

        public Color Color
        {
            get { return _color.Value; }
        }

        public ReactiveCommand OkButton { get; protected set; }

        public ColorChooserThatDoesntLikeGreen()
        {
            var finalColor = this.WhenAny(x => x.Red, x => x.Green, x => x.Blue,
                (r, g, b) => Color.FromRgb(r.Value, g.Value, b.Value));

            finalColor.ToProperty(this, x => x.Color, out _color);

            OkButton = new ReactiveCommand(finalColor.Select(c => c.G != 255));
        }
    }
}
