using Godot;
using System;

public partial class RestartButton : Node
{
	private void _on_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
}



