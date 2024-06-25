using Godot;
using System;

public partial class MainMenu : Control
{
	public void OnSinglePlayerButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
	}


}
