using Godot;
using System;

public partial class Health : Node
{
	public int fullHealth = 5;
	CharacterBody2D otherPlayer;
	
	CanvasManager canvasLayer;
	EnemySpawner enemySpawner;

	[Export]
	AudioStream gameOverSound;
	private int sliderNumber;

	public void Damage(int damage){
		fullHealth -= damage;
		GD.Print(fullHealth);
		canvasLayer.PlayerHurt(sliderNumber , fullHealth);
		if(fullHealth <= 0){
			
			enemySpawner.GameOver = true;
			canvasLayer.PlayerDeath();
			string path = "res://Prefabs/PlayAudioOnce.tscn";
			var packedScene = GD.Load<PackedScene>(path);
		

			PlayAudioOnce p = (PlayAudioOnce)packedScene.Instantiate();
			p.audio = gameOverSound;
			GetNode(GetTree().CurrentScene.GetPath()).CallDeferred("add_child", p);

			
			this.GetParent().QueueFree();
			otherPlayer.QueueFree();
			

		}
	}

	public int getHealth(){
		return fullHealth;
	}

	public override void _Ready()
	{
		base._Ready();
		if(GetParent().Name == "FirstRock"){
			sliderNumber = 1;
			otherPlayer = (CharacterBody2D)GetNode(GetTree().CurrentScene.GetPath() + "/SecondRock");
		}
		else if(GetParent().Name == "SecondRock"){
			sliderNumber = 2;
			otherPlayer = (CharacterBody2D)GetNode(GetTree().CurrentScene.GetPath() + "/FirstRock");
		}
			

		canvasLayer = (CanvasManager)GetNode(GetTree().CurrentScene.GetPath() + "/CanvasLayer") as CanvasManager;
		enemySpawner = (EnemySpawner)GetNode(GetTree().CurrentScene.GetPath() + "/SpawnBox") as EnemySpawner;
		
	}
}
