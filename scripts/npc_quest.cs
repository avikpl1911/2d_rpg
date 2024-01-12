using Godot;
using System;

public partial class npc_quest : Control
{

    [Signal]
    public delegate void QuestMenuClosedEventHandler();
	// Called when the node enters the scene tree for the first time.

	bool quest1_active = false;

	bool quest1_complete = false;

	int stick = 0;

	NinePatchRect quest1_ui = null;


    void Quest1Chat()
	{
        quest1_ui.Visible = true;
		

	}


	void _on_yes_button_pressed()
	{
        quest1_ui.Visible = false;
        quest1_active = true;
        stick = 0;
        EmitSignal(nameof(QuestMenuClosed));
    }

	void _on_no_button_pressed()
	{
        quest1_ui.Visible = false;
        quest1_active = false;
       
        EmitSignal(nameof(QuestMenuClosed));

    }
    public override void _Ready()
	{
		quest1_ui = GetNode<NinePatchRect>("quest1_ui");
        quest1_ui.Visible = false;

    }

	public async void NextQuest()
	{
		if (!quest1_complete) {
		Quest1Chat();
		}
		else
		{
			GetNode<NinePatchRect>("no_quest").Visible = true;
			await ToSignal(GetTree().CreateTimer(3.0f),"timeout");
            GetNode<NinePatchRect>("no_quest").Visible = false;
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
        if (quest1_active) {
			if (stick == 3)
			{
				GD.Print("quest1 completed");
				quest1_active = false;
				quest1_complete = true;
			}
		}
	}
}
