using Godot;
using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

public partial class slime : CharacterBody2D
{
	public int speed = 50;

	public int health = 100;

    public AnimatedSprite2D animsprite;

    bool dead = false;

	bool player_in_area;

	dynamic plyr;

    Godot.CollisionShape2D colshape;

    dynamic slimeo;
    dynamic slimec;

    [Export]
    InventoryItem ItemRes;

    public override void _Ready()
    {
        animsprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        colshape = GetNode<Godot.CollisionShape2D>("detection_area/CollisionShape2D");
       dead = false;

       
        slimec = GetNode<StaticBody2D>("slime_collectible");
        slimec.Visible = false;
        
    }



    // Get the gravity from the project settings to be synced with RigidBody nodes.

   


    public override void _PhysicsProcess(double delta)
	{
        if (!dead)
        {


            
            if (player_in_area)
            {
                Position += (plyr.Position - Position) / speed;
                animsprite.Play("move");
            }
            else {
                animsprite.Play("idle");
            }
        }
        else
        {
            colshape.SetDeferred("disabled", true);
        }
    }


    public void _on_detection_area_body_entered(Node2D body)
    {
        if (body is player play) {
            player_in_area = true;
            plyr = play;
        
        }
    }

    public void _on_detection_area_body_exited(Node2D body)
    {
        if (body is player play)
        {

            player_in_area = false;


        }
    }



    public void _on_hitbox_area_entered(Area2D area) {

        int damage;
        if (area.HasMethod("ArrowDealDamage")) {

            damage = 50;

            TakeDamage(damage);

        }
    }


    public void  TakeDamage(int damage) {
        health -= damage;

        if (health <= 0 && dead==false)
        {

            Death();


        }
     
    }

    public void DropSlime()
    {

        slimec.Visible = true;
        GetNode<Godot.CollisionShape2D>("slime_collectible_area/CollisionShape2D").SetDeferred("disabled",false);
        Slimecollect();
        
    }

    public void  Death()
    {

        
        animsprite.Play("death");
        dead = true;
        DropSlime();

        animsprite.Visible = false;
        colshape.SetDeferred("disabled", true);
        GetNode<Godot.CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        GetNode<Godot.CollisionShape2D>("hitbox/CollisionShape2D").SetDeferred("disabled", true);



    }


    public async void Slimecollect() {

        
        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        
        slimec.Visible = false;

        plyr.Collect(ItemRes);

        

    }

    public void _on_slime_collectible_area_body_entered(Node2D body)
    {

        if (body is player pl)
        {
            plyr = pl;
        }

    }

}
