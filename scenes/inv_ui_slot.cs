using Godot;
using Godot.Collections;
using System;

public partial class inv_ui_slot : Panel
{
	// Called when the node enters the scene tree for the first time.

	Sprite2D item_visual;

	Label amount_text;
   

    public override void _Ready()
	{

		item_visual = GetNode<Sprite2D>("CenterContainer/Panel/item_display");
		amount_text = GetNode<Label>("CenterContainer/Panel/Label");

    }

    public void Update(Inv_slot slot) {

        if (item_visual != null && amount_text != null) {
            if (slot != null)
            {
                item_visual.Visible = true;

                item_visual.Texture = slot.item.texture;

                if (slot.amount > 1)
                {
                    amount_text.Visible = true;
                }
                else
                {
                    amount_text.Visible = false;
                }

                    amount_text.Text = slot.amount.ToString();
                }
                else
                {



                    item_visual.Visible = false;
                    amount_text.Visible = false;
                }
            }


        } 

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
