using Godot;
using System;

public partial class HUD : CanvasLayer
{
    

    private HBoxContainer player1Hearts;
    private HBoxContainer player2Hearts;
	//private GameState gameState;

	private Timer testTimer;

    public override void _Ready()
    {
        player1Hearts = GetNode<HBoxContainer>("PlayerMargin/PlayerCenter/PlayerHearts");
        player2Hearts = GetNode<HBoxContainer>("CPUMargin/CPUCenter/CPUHearts");
		//gameState = GetNode<GameState>("../GameState");
    }

	public void InitializeHUD()
	{
			GameState gameState = GetNode<GameState>("../GameState");
			FillHearts(player1Hearts,gameState.GetPlayerLives(1)); 
			GD.Print("HUD Player 1 Lives: " + gameState.GetPlayerLives(1));
			FillHearts(player2Hearts, gameState.GetPlayerLives(2));
			GD.Print("HUD Player 2 Lives: " + gameState.GetPlayerLives(2));
	}
	public void RefreshHUD(){
		AdjustHearts();
	}
	/// <summary>
	/// Fills the specified container with the specified number of full hearts.
	/// </summary>
	/// <param name="container">The HBoxContainer to fill.</param>
	/// <param name="lives">The number of full hearts to add to the container.</param>
	private void FillHearts(HBoxContainer container, int lives)
	{
		for (int i = 0; i < lives; i++)
		{
			GD.Print($"Filling hearts {i}");
			var heart = new TextureRect();
			heart.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered; //needed?
			heart.Texture = (Texture2D)GD.Load("res://assets/HUD/heart_full.png");
			container.AddChild(heart);
		}
	}
	/// <summary>
	/// Removes all children from the specified container and fills it with full hearts.
	/// </summary>
	/// <param name="container">The HBoxContainer to reset.</param>
	/// <param name="lives">The number of full hearts to add to the container.</param>
	public void ResetHearts(HBoxContainer container, int lives)
	{
		// Remove all existing hearts
		foreach (var child in container.GetChildren())
		{
			container.RemoveChild(child as Node);
			(child as Node).QueueFree();
		}

		// Fill with full hearts
		FillHearts(container, lives);
	}
	public void AdjustHearts()
	{
		GameState gameState = GetNode<GameState>("../GameState");
		int playerLives = gameState.GetPlayerLives(1);
		int cpuLives = gameState.GetPlayerLives(2);

		// Adjust player hearts
		for (int i = 0; i < player1Hearts.GetChildCount(); i++)
		{
			UpdateHeart(true, i, i < playerLives);
		}

		// Adjust CPU hearts
		for (int i = 0; i < player2Hearts.GetChildCount(); i++)
		{
			UpdateHeart(false, i, i < cpuLives);
		}
	}
	/// <summary>
	/// Updates the texture of the heart at the specified index in the specified container.
	/// </summary>
	/// <param name="isPlayer">If true, updates a heart in the player's container; otherwise, updates a heart in the CPU's container.</param>
	/// <param name="index">The index of the heart to update.</param>
	/// <param name="isFull">If true, sets the heart to full; otherwise, sets the heart to empty.</param>
    public void UpdateHeart(bool isPlayer, int index, bool isFull)
    {
        var heart = (isPlayer ? player1Hearts : player2Hearts).GetChild<TextureRect>(index);
        heart.Texture = (Texture2D)GD.Load(isFull ? "res://assets/HUD/heart_full.png" : "res://assets/HUD/heart_empty.png");
    }


}
