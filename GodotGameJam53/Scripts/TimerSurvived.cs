using Godot;
using System;

public partial class TimerSurvived : Label
{
	private float timer = 0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += (float)delta;
		string time = String.Format("{0:0.00}", timer);
		this.Text = "Survived: " + time;
	}
}
