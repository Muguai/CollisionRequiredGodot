using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	CharacterBody2D player1;
	CharacterBody2D player2;

	float speed = 2000f;

	private Sprite2D enemySprite;
	
	EnemySpawner enemySpawner;

	
	[Export]
	public AnimationPlayer anim;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player1 = (CharacterBody2D)GetNode(GetTree().CurrentScene.GetPath() + "/FirstRock");
		player2 = (CharacterBody2D)GetNode(GetTree().CurrentScene.GetPath() + "/SecondRock");
		enemySpawner = (EnemySpawner)GetNode(GetTree().CurrentScene.GetPath() + "/SpawnBox") as EnemySpawner;
		enemySprite = GetNode<Sprite2D>("Sprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{


	}
	public void EnemyWalk(double delta){
		if(enemySpawner.GameOver == true){
			anim.Play("Idle");
			GD.Print("playernull");
			return;
		}
		var dir = (player1.GlobalPosition - GlobalPosition);
		if(dir.Length() > (player2.GlobalPosition - GlobalPosition).Length())
			dir =  (player2.GlobalPosition - GlobalPosition);

		Velocity = dir.Normalized() * speed;
		anim.Play("Walk");

		if(Velocity.x > 0)	
			enemySprite.FlipH = false;
		else
			enemySprite.FlipH = true;

		MoveAndSlide();
	}
	
	private void _on_area_2d_body_entered(Node2D body)
	{
	// Replace with function body.
	}
}



