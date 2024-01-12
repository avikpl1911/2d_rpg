using Godot;
using System;
using System.Collections.Generic;

public partial class npc : CharacterBody2D
{

    public const float Speed = 30.0f;

    Vector2 dir = Vector2.Right;

    Vector2 start_pos;

    enum current_state
    {
        IDLE,
        NEW_DIR,
        MOVE
    }

    current_state curr = current_state.IDLE;

    bool is_roaming = true;

    bool is_chatting = false;

    AnimatedSprite2D animpl = null;

    player plyr = null;

    bool player_in_chat_zone = false;

    List<Vector2> randomvector = new List<Vector2>{ Vector2.Right , Vector2.Down , Vector2.Up , Vector2.Left };
    public override void _Ready()
	{
        animpl = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        GD.Randomize();
        start_pos = Position;
    }


    public override void _Process(double delta)
	{
        
            if (curr == current_state.IDLE || curr == current_state.NEW_DIR)
            {
                animpl.Play("idle");

            }
            else if (curr == current_state.MOVE)
            {

                if (dir.X == -1)
                {
                    animpl.Play("walk_w");
                }
                else if (dir.X == 1)
                {
                    animpl.Play("walk_e");
                }
                else if (dir.Y == -1)
                {
                    animpl.Play("walk_n");
                }
                else if (dir.Y == 1)
                {
                    animpl.Play("walk_s");
                }

            }

            if (is_roaming)
            {
                if (curr == current_state.IDLE)
                {

                }
                else if (curr == current_state.NEW_DIR)
                {
                    dir = Choose(randomvector);
        
                 }
                else if (curr == current_state.MOVE)
                {
                   Move((float)delta);
                }
            }

        if (Input.IsActionJustPressed("chat"))
        {
            GD.Print("chatting with npc");
            is_roaming = false;
            is_chatting = true;
            animpl.Play("Idle");
        }
        
    }

    Vector2 Choose(List<Vector2> vec)
    {
        Random random = new();

        int rand = random.Next(0, vec.Count);

        return vec[rand];

    }

    double Choose(List<double> dob)
    {
        Random random = new();

        int rand = random.Next(0, dob.Count);

        return dob[rand];
    }

    private current_state Choose(List<current_state> crr)
    {
        Random random = new();

        int rand = random.Next(0, crr.Count);

        return crr[rand];
    }

    void Move(float delta)
    {
        Position += dir * Speed * delta;
    }

    void _on_chat_detection_area_body_entered(Node2D body)
    {
        if (body is player ply)
        {
            plyr = ply;
            player_in_chat_zone = true;
        }
    }

    void _on_chat_detection_area_body_exited(Node2D body)
    {
        if (body is player ply)
        {
            
            player_in_chat_zone = false;
        }
    }

    void _on_timer_timeout()
    {
        GetNode<Timer>("Timer").WaitTime = Choose(new List<double>{0.5,1,1.5});

        curr = Choose(new List<current_state> { current_state.IDLE, current_state.NEW_DIR, current_state.MOVE });
    }
}

