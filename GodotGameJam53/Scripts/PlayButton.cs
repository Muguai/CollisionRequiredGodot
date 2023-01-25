using Godot;
using System;

public partial class PlayButton : Node
{
	private void _on_pressed()
	{
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}	
}



