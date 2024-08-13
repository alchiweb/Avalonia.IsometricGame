using IsometricGame.Infrastructure;
using Avalonia;

namespace IsometricGame.ViewModels;

public abstract class GameObject : ViewModelBase
{
    private Point _location;
    public Point Location
    {
        get => _location;
        protected set
        {
            if (value.Equals(_location)) return;
            _location = value;
            OnPropertyChanged();
        }
    }

    public abstract int Layer { get; }

    protected GameObject(Point location)
    {
        Location = location;
    }
}
