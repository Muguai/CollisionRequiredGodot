using Godot;
using System;

public partial class OutlineStart : Sprite2D
{
	ShaderMaterial mat;
	private bool justOnce = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mat = (ShaderMaterial)this.Material;
		mat.SetShaderParameter("width", 0f);
	}
	/*
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed(this.Name))
		{
			GD.Print("GOOO " + this.Name);
			mat.SetShaderParameter("width", 100f);
			//justOnce = true;
		}else{
			mat.SetShaderParameter("width", 0f);
			//justOnce = false;

		}
	}
	*/
}
