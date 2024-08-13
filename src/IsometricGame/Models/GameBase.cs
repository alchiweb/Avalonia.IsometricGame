using System;
using Avalonia.Threading;

namespace IsometricGame.Models;

public abstract class GameBase
{
    public const int TicksPerSecond = 60;
    private readonly DispatcherTimer _timer = new() { Interval = new TimeSpan(0, 0, 0, 0, 1000 / TicksPerSecond) };

    public long CurrentTick { get; private set; }

    protected GameBase()
    {
        _timer.Tick += delegate { DoTick(); };
    }

    private void DoTick()
    {
        Tick();
        CurrentTick++;
    }

    protected abstract void Tick();

    public void Start()
    {
        _timer.IsEnabled = true;
    }

    public void Stop()
    {
        _timer.IsEnabled = false;
    }
}