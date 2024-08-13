using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;


namespace Isometric.Views;

public partial class MainView : UserControl
{
    public MainView()
    {        
        InitializeComponent();
        Focusable = true;
    }
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        Focus();
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        Keyboard.Keys.Add(e.Key);
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        Keyboard.Keys.Remove(e.Key);
        base.OnKeyUp(e);
    }
}
