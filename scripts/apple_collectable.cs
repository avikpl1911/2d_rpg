using Godot;
using System;
using System.Runtime.InteropServices;

public partial class apple_collectable : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	
	
	public override void _Ready()
	{

		FallFromTree();

	}

    private async void FallFromTree()
    {
		dynamic aniplay = GetNode<AnimationPlayer>("AnimationPlayer");
        aniplay.Play("falling");
		await ToSignal(GetTree().CreateTimer(0.3f), "timeout");
        aniplay.Play("fade");

		//GD.Print("+1 apples");

		await ToSignal(GetTree().CreateTimer(0.3f),"timeout");





    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
