using Godot;
using System;
using Godot.Collections;
using System.Threading.Tasks;


public partial class apple_tree : Node2D
{
	// Called when the node enters the scene tree for the first time.

	string state = "no apples";

    dynamic apple_kk = ResourceLoader.Load("res://scenes/apple_collectable.tscn");

    player plyr;


    [Export]
    InventoryItem item;

	bool player_in_area = false;
	public override void _Ready()
	{
		Timer growth_timer = GetNode<Timer>("growth_timer");

        if (state == "no apples")
        {
            growth_timer.Start();
        }
    }


    public void OnBodyEntered(CharacterBody2D body)
    {
        //dynamic player = GetNode<CharacterBody2D>("player");
        

    


        if (body is CharacterBody2D player) 
        {
            player_in_area = true;
            plyr = (player)body;
        }
    }

    public void OnBodyExited(CharacterBody2D body)

    {
        //dynamic player = GetNode<CharacterBody2D>("player");
        
        if (body is CharacterBody2D player)
        {
            player_in_area = false;
           
        }
    }

    public void OnGrowthTimeout(){
		
		if(state=="no apples"){
			state = "apples";
		}
	}

    



    public async void DropApple() {

        dynamic apple_instance = apple_kk.Instantiate();

        apple_instance.GlobalPosition = GetNode<Marker2D>("Marker2D").GlobalPosition;

        GetParent().AddChild(apple_instance);
        
        plyr.Collect(item); 
        

        await ToSignal(GetTree().CreateTimer(3),"timeout");



        GetNode<Timer>("growth_timer").Start();



    }



  

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{


		Timer growth_timer = GetNode<Timer>("growth_timer");
		AnimatedSprite2D anim = GetNode<AnimatedSprite2D>("treeani");
		if (state == "no apples")
		{
			anim.Play("no_apples");
		}
		else if (state == "apples")
		{
			anim.Play("apples");

			


            bool farmActionPressed = Input.IsActionJustPressed("farm");
           
            
        
            if (player_in_area) {
                
                if (Input.IsActionJustPressed("farm"))
                {
                    //GD.Print("hello");
                    state = "no apples";
                    DropApple();
                }
            }
            

        }


    }
	
	

	
	
	 

	

}


