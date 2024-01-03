using Godot;
using System;
[GlobalClass]
public partial class Inv_slot : Resource
{
    [Export]
    public InventoryItem item;
    [Export]
    public int amount;

}
