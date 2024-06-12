using Godot;
using System;

public partial class Main : Node
{
    public int[] score = { 0, 0 };

    public Marker2D ballPosition;

    [Export]
    public int _paddleSpeed = 500;

    public int GetSpeed()
    {
        return _paddleSpeed;
    }

    public override void _Ready()
    {
        //var player = GetNode<Player>("Player");
        var cpu = GetNode<CPU>("CPU");
        var ball = GetNode<Ball>("Ball");
        var timer = GetNode<Timer>("BallTimer")

        var playerPosition = GetNode<Marker2D>("PlayerMarker");
        var cpuPosition = GetNode<Marker2D>("CPUMarker");
        var ballPosition = GetNode<Marker2D>("BallMarker");
        
        //player.Start(playerPosition.Position);
        cpu.Start(cpuPosition.Position);
        ball.Start(ballPosition.Position);
    }


}
