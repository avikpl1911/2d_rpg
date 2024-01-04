using Godot;
using System;

public partial class slime : CharacterBody2D
{
	public int speed = 50;

	public int health = 100;

	public dynamic damage;

	bool dead = false;

	bool player_in_area;

	dynamic plyr;


    public override void _Ready()
    {
        dead = false;
    }

    // Get the gravity from the project settings to be synced with RigidBody nodes.


    public override void _PhysicsProcess(double delta)
	{
        if (!dead)
        {

            GetNode< Godot.CollisionShape2D>("detection_area/CollisionShape2D").SetDeferred("disabled", false);
            if (player_in_area)
            {
                Position += (plyr.Position - Position) / speed;
                GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("move");
            }
            else {
                GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
            }
        }
        else
        {
            GetNode<Area2D>("detection_area/CollisionShape2D").SetDeferred("disabled", true);
        }
    }


    public void _on_detection_area_body_entered(Node2D body)
    {
        if (body is player play) {
            player_in_area = true;
            plyr = body;
        
        }
    }

    public void _on_detection_area_body_exited(Node2D body)
    {
        if (body is player play)
        {

            player_in_area = false;

        }
    }

    public void _on_hitbox_body_entered(Node2D body) {
        if (body is arrow arw) {
        


        }
    }


}
