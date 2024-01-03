using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class inv_ui : Control
{
    //Called when the node enters the scene tree for the first time.
    bool is_open = false;


    Inventory inv = ResourceLoader.Load<Inventory>("res://inventory/Inventory.tres");
    public Array<Inv_slot> items = ResourceLoader.Load<Inventory>("res://inventory/Inventory.tres").slots;
    Array<inv_ui_slot> slots = new();
  
    public override void _Ready()
    {

        GD.Print(items);

        var mappedSlots = GetNode<GridContainer>("NinePatchRect/GridContainer").GetChildren()
        .Where(child => child is inv_ui_slot) // We only want nodes that we know are Weapon nodes
        .Select(child => child)          // Once we've got the weapons select them all
        .Cast<inv_ui_slot>();                 // Create an enumerable object containg weapons


        foreach (var slot in mappedSlots)
        {
            //Add the weapons we found to our weapons property
            slots.Add(slot);

        }

        Close();


        inv.Update += () => UpdateSlots();



        


    }
    private void Catch_Signal()
    {

    }


    private void OnInventoryUpdated()
    {
        GD.Print("inventory updated");
    }

    private void UpdateSlots()
    {
        int minsize = Math.Min(items.Count, slots.Count);
        GD.Print("Hi");



        for (int i = 0; i < minsize; i++)
        {
            if (items[i] != null) { slots[i].Update(items[i]); }
            
        }
        

        

    }

    private void Open()
    {
        this.Visible = true;
        is_open = true;
    }


    private void Close()
    {
        this.Visible = false;
        is_open = false;
    }

    //Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

        if (Input.IsActionJustPressed("i"))
        {
            if (is_open)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
}
