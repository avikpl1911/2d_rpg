using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
public partial class Inventory : Resource
{
    

    [Export]
    public Array<Inv_slot> slots;



    [Signal]
    public delegate void UpdateEventHandler();

    public void Insert(InventoryItem item)
    {
        var existingSlot = slots.FirstOrDefault(slt => slt.item == item);

        if (existingSlot != null)
        {
            // Increase quantity of existing item
            existingSlot.amount += 1;
        }
        else
        {
            // Find an empty slot
            var emptySlot = slots.FirstOrDefault(slt => slt.item == null);

            if (emptySlot != null)
            {
                // Add the item to the empty slot
                emptySlot.item = item;
                emptySlot.amount = 1;
            }
        }

        // Emit the signal using the correct syntax
        EmitSignal(nameof(Update));
    }

    
}
