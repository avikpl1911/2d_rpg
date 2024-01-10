using Godot;
using System;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;

public partial class world_2 : Node2D
{
	// Called when the node enters the scene tree for the first time.

	AnimationPlayer aniplayer;
	bool is_openingcutscene = false;

	bool has_player_entered_area = false;

	Camera2D cam = null;

	player plyr = null;


	bool is_path_following = false;

	bool smoke_has_happened = false;

	bool smoke_is_happening = false;


	public override void _Ready()
	{
        aniplayer = GetNode<AnimationPlayer>("world2openingCutscene/AnimationPlayer");
		cam = GetNode<Camera2D>("Path2D/PathFollow2D/Camera2D");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override async void _PhysicsProcess(double delta)
    {
		if (is_openingcutscene) {
			PathFollow2D pathfollower = GetNode<PathFollow2D>("Path2D/PathFollow2D");

            if (is_path_following)
			{
				if (pathfollower.ProgressRatio >= 0.990 ) {
					CutSceneEnding();
				
				}
				if (!smoke_is_happening)
				{
					pathfollower.ProgressRatio += 0.001f;



					if (!smoke_has_happened && pathfollower.ProgressRatio >= 0.770)
					{
						smoke_is_happening = true;

						//Toggle_smoke
						await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
						GetNode<TileMap>("world2openingCutscene/TileMapFinished").Visible = true;
						GetNode<TileMap>("world2openingCutscene/TileMapFUnfinished").Visible = false;
						//Toggle_smoke
						await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
						smoke_has_happened = true;

						smoke_is_happening = false;
					}

				}      
				
			}
		}
    }

    public void  _on_player_detection_body_entered(Node2D body)
 	{
		if (body is player player) {

			plyr = player;


			if (!has_player_entered_area) {
                has_player_entered_area = true;
				CutSceneOpening();
            }
           
        }
	}

	void CutSceneOpening()
	{
		is_path_following = true;
        is_openingcutscene = true;
		aniplayer.Play("cover_fade");
		plyr.camera.Enabled = false;
		cam.Enabled = true;
	}


	void CutSceneEnding() {
		is_path_following = false;
        is_openingcutscene = false;
		cam.Enabled =false;
		plyr.camera.Enabled = true;
		GetNode<Node2D>("world2openingCutscene").Visible = false;
        GetNode<Node2D>("world2main").Visible = true;
	}

   void Toggle_smoke()
	{
		GpuParticles2D smoke1 =GetNode<GpuParticles2D>("smokeparticles1");
        GpuParticles2D smoke2 = GetNode<GpuParticles2D>("smokeparticles2");
        GpuParticles2D smoke3 = GetNode<GpuParticles2D>("smokeparticles3");

        smoke1.Emitting = !smoke1.Emitting;
        smoke2.Emitting = !smoke2.Emitting;
        smoke3.Emitting = !smoke3.Emitting;
    }
}
