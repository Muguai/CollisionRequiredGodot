using Godot;
using System;

public partial class Line2D : Godot.Line2D
{
	[Export]
	public CharacterBody2D player1;
	[Export]
	public CharacterBody2D player2;

	private EnemySpawner enemySpawner;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddPoint(player1.Transform.origin, 0);
		this.AddPoint(player2.Transform.origin, 1);
		enemySpawner = (EnemySpawner)GetNode(GetTree().CurrentScene.GetPath() + "/SpawnBox") as EnemySpawner;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(enemySpawner.GameOver == true){
			this.ClearPoints();
			return;
		}
		this.ClearPoints();
		this.AddPoint(player1.Transform.origin, 0);
		this.AddPoint(player2.Transform.origin, 1);
	}
}
