using Godot;
using System;
using System.Runtime.InteropServices;

public partial class player : CharacterBody2D
{
	int speed = 100;

    string player_state;

	bool bow_equiped = false;
	bool bow_cooldown = true;

	public Camera2D camera = null;

    dynamic arw;

	Marker2D marker2d;
	Vector2 mouse_loc_from_player;
	[Export]
	Inventory inv;

    public override void _Ready()
    {
		camera = GetNode<Camera2D>("Camera2D");
		marker2d = GetNode<Marker2D>("Marker2D");
		arw = ResourceLoader.Load("res://scenes/arrow.tscn");
    }
    public override void _PhysicsProcess(double delta)
	{
		mouse_loc_from_player = GetGlobalMousePosition() - this.Position;


       Vector2 direction = Input.GetVector("left","right","up","down");

	if (direction.Length() > 0){
        player_state="walking";
	}else{
        player_state="idle";
	}

    Velocity = direction * speed; 
	
	MoveAndSlide();

	Vector2 mouse_pos = GetGlobalMousePosition();

	marker2d.LookAt(mouse_pos);


		if (Input.IsActionJustPressed("e")) {

			bow_equiped = !bow_equiped;
		
		}

		HandleArrow();



        Playanim(direction,player_state);
	}

	private async void HandleArrow() {

        if (Input.IsActionJustPressed("left_click") && bow_equiped && bow_cooldown)
        {

            dynamic arrw = arw.Instantiate();
            bow_cooldown = false;
            
            arrw.Rotation = marker2d.Rotation;
            arrw.GlobalPosition = marker2d.GlobalPosition;
            AddChild(arrw);

            await ToSignal(GetTree().CreateTimer(0.4f), "timeout");
            bow_cooldown = true;
        }

    }

	private void Playanim(Vector2 dir,string ps){
       
	   AnimatedSprite2D asp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (!bow_equiped)
		{
			if (ps == "idle")
			{
				asp.Play("idle");
			}
			else if (ps == "walking")
			{
				if
					  (dir.Y == -1)
				{
					asp.Play("n_walk");
				}

				else if (dir.Y == +1)
				{
					asp.Play("s_walk");
				}
				else if (dir.X == -1)
				{
					asp.Play("w_walk");
				}

				else if (dir.X == +1)
				{
					asp.Play("e_walk");
				}
				else if (dir.X > 0.5 && dir.Y < 0.5)
				{
					asp.Play("ne_walk");
				}
				else if (dir.X > 0.5 && dir.Y > 0.5)
				{
					asp.Play("se_walk");
				}
				else if (dir.X < 0.5 && dir.Y < 0.5)
				{
					asp.Play("nw_walk");
				}
				else if (dir.X < 0.5 && dir.Y > 0.5)
				{
					asp.Play("sw_walk");
				}
			}
		}
		else
		{
			if (mouse_loc_from_player.X >= -25 && mouse_loc_from_player.X <= 25 && mouse_loc_from_player.Y < 0)
			{
				asp.Play("n_attack");
			}
			else if (mouse_loc_from_player.Y >= -25 && mouse_loc_from_player.Y <= 25 && mouse_loc_from_player.X > 0)
			{
				asp.Play("e_attack");
			}
			else if (mouse_loc_from_player.X >= -25 && mouse_loc_from_player.X <= 25 && mouse_loc_from_player.Y > 0)
			{
				asp.Play("s_attack");
			}
			else if (mouse_loc_from_player.X >= -25 && mouse_loc_from_player.X <= 25 && mouse_loc_from_player.Y > 0)
			{
				asp.Play("w_attack");
			}
			else if (mouse_loc_from_player.X >= 25 && mouse_loc_from_player.Y <= -25)
			{
                asp.Play("ne_attack");
            }
			else if (mouse_loc_from_player.X >= 0.5 && mouse_loc_from_player.Y >= 25)
			{
                asp.Play("se_attack");
            }
			else if (mouse_loc_from_player.X <= -0.5 && mouse_loc_from_player.Y >= 25)
			{
                asp.Play("sw_attack");
            }
            else if (mouse_loc_from_player.X <= -25 && mouse_loc_from_player.Y <= -25)
            {
                asp.Play("nw_attack");
            }

        }
    }

	// private void Player(){

	// }

	public void Collect(InventoryItem item) {

        inv.Insert(item);
	}
}
