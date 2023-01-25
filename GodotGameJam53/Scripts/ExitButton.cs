using Godot;
using System;

public partial class ExitButton : Node
{
	private void _on_pressed()
	{
		GetTree().Quit();
	}
}



