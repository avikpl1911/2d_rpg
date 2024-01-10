using Godot;
using System;

public partial class arrow : Area2D
{

	int speed = 300;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TopLevel = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Vector2 velocity = Vector2.Right*speed;
		Position += velocity.Rotated(Rotation)*(float)delta;
	}


	public void _on_visible_on_screen_enabler_2d_screen_exited()
	{
		QueueFree();
	}


	public void ArrowDealDamage()
	{
		return;
	}
}
