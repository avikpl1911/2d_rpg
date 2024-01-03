using Godot;
using System;
using System.Runtime.InteropServices;

public partial class player : CharacterBody2D
{
	int speed = 100;

    string player_state;

	[Export]
	Inventory inv;
	

	public override void _PhysicsProcess(double delta)
	{
    Vector2 direction = Input.GetVector("left","right","up","down");

	if (direction.Length() > 0){
        player_state="walking";
	}else{
        player_state="idle";
	}

    Velocity = direction * speed; 

	MoveAndSlide();

    Playanim(direction,player_state);
	}

	private void Playanim(Vector2 dir,string ps){
       
	   AnimatedSprite2D asp = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

      if (ps == "idle"){
		asp.Play("idle");
		
			
		
	  }else if (ps == "walking"){
          if (dir.Y==-1){
             asp.Play("n_walk");
		 } 
		 
		 else if (dir.Y==+1){
             asp.Play("s_walk");
		 }
		 else if (dir.X==-1){
		     asp.Play("w_walk");        
		 }
		 
		 else if (dir.X==+1){
             asp.Play("e_walk"); 
		 }
		 else if (dir.X > 0.5 && dir.Y<0.5){
              asp.Play("ne_walk");
		 }else if (dir.X > 0.5 && dir.Y>0.5){
              asp.Play("se_walk");
		 }else if (dir.X < 0.5 && dir.Y<0.5){
               asp.Play("nw_walk");
		 }else if (dir.X < 0.5 && dir.Y>0.5){
                asp.Play("sw_walk");
		 }
	  }
	}

	// private void Player(){

	// }

	public void Collect(InventoryItem item) {

        inv.Insert(item);
	}
}
