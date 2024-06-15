using Godot;
using System;

public partial class HUD : CanvasLayer
{
    [Export] public int playerLives = 3;
    [Export] public int cpuLives = 3;

    private HBoxContainer playerHearts;
    private HBoxContainer cpuHearts;

	private Timer testTimer;

    public override void _Ready()
    {
        playerHearts = GetNode<HBoxContainer>("PlayerMargin/PlayerCenter/PlayerHearts");
        cpuHearts = GetNode<HBoxContainer>("CPUMargin/CPUCenter/CPUHearts");
		
		FillHearts(playerHearts, playerLives);
		FillHearts(cpuHearts, cpuLives);
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
	/// <summary>
	/// Updates the texture of the heart at the specified index in the specified container.
	/// </summary>
	/// <param name="isPlayer">If true, updates a heart in the player's container; otherwise, updates a heart in the CPU's container.</param>
	/// <param name="index">The index of the heart to update.</param>
	/// <param name="isFull">If true, sets the heart to full; otherwise, sets the heart to empty.</param>
    public void UpdateHeart(bool isPlayer, int index, bool isFull)
    {
        var heart = (isPlayer ? playerHearts : cpuHearts).GetChild<TextureRect>(index);
        heart.Texture = (Texture2D)GD.Load(isFull ? "res://assets/HUD/heart_full.png" : "res://assets/HUD/heart_empty.png");
    }

}
