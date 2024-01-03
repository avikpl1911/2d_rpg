using Godot;
using System;
using System.Xml.Schema;

public partial class Stick_collectible : StaticBody2D
{
    [Export]
    public InventoryItem item;
    player plyr;


    private async void _on_interacteble__area_body_entered(Node2D body) { 
    if (body is CharacterBody2D player) {
            plyr = (player)body;
            PlayerCollect();
            await ToSignal(GetTree().CreateTimer(0.1f),"timeout");
            this.QueueFree();
        }
    }

    private void PlayerCollect()
    {
        plyr.Collect(item);
    }





}
