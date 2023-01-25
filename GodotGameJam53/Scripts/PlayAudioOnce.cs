using Godot;
using System;

public partial class PlayAudioOnce : AudioStreamPlayer
{
	private bool initialize = false;
	public AudioStream audio;

	
	
	public void PlayThis(){
		this.Stream = audio;
		
		Random R = new Random();

		double result = (R.NextDouble() * (1.5 - 0.9)) + 0.9;
		this.PitchScale = (float)result;
		this.Play(0f);
		this.Playing = true;
		
		initialize = true;

	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if(initialize == false)
			return;

		if(this.Playing == false){
			GD.Print("StopPlaying");
			this.QueueFree();
		}

	}

	public override void _Ready()
	{
		PlayThis();
		GD.Print("PlayOnceSpawn");
	}	
}
