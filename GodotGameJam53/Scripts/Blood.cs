using Godot;
using System;

public partial class Blood : CPUParticles2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Emitting = true;
	}
}
