using Godot;
using System;

public partial class HUD : CanvasLayer
{
    

    private HBoxContainer player1Hearts;
	
    private HBoxContainer player2Hearts;
	
	private int _maxLives;

	private Timer testTimer;

    public override void _Ready()
    {
        player1Hearts = GetNode<HBoxContainer>("PlayerMargin/PlayerCenter/PlayerHearts");
        player2Hearts = GetNode<HBoxContainer>("CPUMargin/CPUCenter/CPUHearts");
    }

	public void InitializeHUD()
	{
		//get information for lives
		GameState gameState = GetNode<GameState>("../GameState");
		_maxLives = gameState.GetPlayerMaxLives();
		HandleHearts();
	}
	public void RefreshHUD(){
		HandleHearts();
	}
	private void HandleHearts(){
		GameState gameState = GetNode<GameState>("../GameState");
		ClearHearts(player1Hearts);
		ClearHearts(player2Hearts);
		FillHearts(player1Hearts,1);
		FillHearts(player2Hearts,2);

	}
	/// <summary>
	/// Removes all children from the specified container and fills it with full hearts.
	/// </summary>
	/// <param name="container">The HBoxContainer to reset.</param>
	/// <param name="lives">The number of full hearts to add to the container.</param>
	public void ClearHearts(HBoxContainer container)
	{
		// Remove all existing hearts
		foreach (var child in container.GetChildren())
		{
			container.RemoveChild(child as Node);
			(child as Node).QueueFree();
		}
	}
	/// <summary>
	/// Fills the specified container with the specified number of full hearts.
	/// </summary>
	/// <param name="container">The HBoxContainer to fill.</param>
	/// <param name="lives">The number of full hearts to add to the container.</param>
	private void FillHearts(HBoxContainer container,  int playerNumber)
	{
		GameState gameState = GetNode<GameState>("../GameState");
		int currentLives = gameState.GetPlayerCurrentLives(playerNumber); // Assuming gameState is accessible and has getPlayerCurrentLives method

		for (int i = 0; i < _maxLives; i++)
		{
			var heart = new TextureRect();
			heart.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;

			// Determine if the heart should be full or empty
			if (i < currentLives){
				heart.Texture = (Texture2D)GD.Load("res://assets/HUD/heart_full.png"); // Full heart
			}else{
				heart.Texture = (Texture2D)GD.Load("res://assets/HUD/heart_empty.png"); // Empty heart
			}

			container.AddChild(heart);
		}
	}
	
}
