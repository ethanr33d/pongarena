using Godot;
using System;

public partial class Main : Node
{
    public int[] score = { 0, 0 };
    private CPU _cpu;
    private Ball _ball;
    private Player _player;

    [Export]
    public int _paddleSpeed = 500;

    public int GetSpeed()
    {
        return _paddleSpeed;
    }
    private void OnBallTimerTimeout()
    {
        _player.Start();
        _cpu.Start();
        _ball.Start();
    }
    public override void _Ready()
    {
        _player = GetNode<Player>("Player");
        _cpu = GetNode<CPU>("CPU");
        _ball = GetNode<Ball>("Ball");
        var timer = GetNode<Timer>("BallTimer");

        var playerPosition = GetNode<Marker2D>("PlayerMarker").Position;
        var cpuPosition = GetNode<Marker2D>("CPUMarker").Position;
        var ballPosition = GetNode<Marker2D>("BallMarker").Position;

        _cpu.Position = cpuPosition;
        _ball.Position = ballPosition;
        _player.Position = playerPosition;

        timer.Start();
    }


}
