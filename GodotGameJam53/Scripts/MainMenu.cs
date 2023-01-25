using Godot;
using System;

public partial class MainMenu : Node
{
	private void _on_pressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu.tscn");
	}
}



