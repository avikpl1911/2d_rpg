using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class dialogue : Control
{

    [Signal]
    public delegate void DilogueFinishedEventHandler();



    public class dilg{
        
        public string Name { get; set; }
       
        public string Text { get; set; }
    }
	// Called when the node enters the scene tree for the first time.
	[Export(PropertyHint.File)]
	public string d_file = "*.json";

	bool d_active = false;

    List<dilg> dilgue = new();

	int current_dialougue_id = 0;

    public override void _Ready()
	{
		GetNode<NinePatchRect>("NinePatchRect").Visible = false;
	}


	public void Start()
	{
		if (d_active)
		{
			return;
		}
		else {
            
            d_active = true;
            GetNode<NinePatchRect>("NinePatchRect").Visible = true;
            dilgue = LoadDialogue();

            GetNode<RichTextLabel>("NinePatchRect/name").Text = dilgue[current_dialougue_id].Name;
            GetNode<RichTextLabel>("NinePatchRect/text").Text = dilgue[current_dialougue_id].Text;



        }


    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.

	void NextScript()
	{
		current_dialougue_id++;
		if (current_dialougue_id >= dilgue.Count)
		{
			GetNode<NinePatchRect>("NinePatchRect").Visible = false;
            d_active = true;
			GD.Print(GetNode<NinePatchRect>("NinePatchRect").Visible);

            
            EmitSignal(nameof(DilogueFinished));
            return;
		}
		
		GetNode<RichTextLabel>("NinePatchRect/name").Text = dilgue[current_dialougue_id].Name;
		GetNode<RichTextLabel>("NinePatchRect/text").Text = dilgue[current_dialougue_id].Text;
	}

    public override void _Input(InputEvent @event)
    {
		if (!d_active) {  return; }
		if (@event.IsActionPressed("ui_accept"))
		{
			NextScript();
		}
    }
    public override void _Process(double delta)
	{
	}

    static dynamic LoadDialogue()
	{
		var file = FileAccess.Open("res://dialogue/worker_dialogue1.json", FileAccess.ModeFlags.Read);
        List<dilg> content = JsonSerializer.Deserialize<List<dilg>>(file.GetAsText());
	
		
		
		return content;
	}
}
