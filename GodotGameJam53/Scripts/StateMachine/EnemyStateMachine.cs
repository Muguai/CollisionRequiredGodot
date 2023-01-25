using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class EnemyStateMachine : StateMachine
{
	[Export]
	public Enemy _Enemy;

	[Export]
	public AnimationPlayer anim;

	[Export]
	public AudioStreamPlayer audioSteamPlayer;

	private EnemySpawner enemySpawner;
	public override void _Ready()
	{
		base._Ready();

		enemySpawner = (EnemySpawner)GetNode(GetTree().CurrentScene.GetPath() + "/SpawnBox") as EnemySpawner;


		List<EnemyState> enemyManageStates = GetNode<Node>("States").GetChildren().OfType<EnemyState>().ToList();

		foreach(EnemyState ES in enemyManageStates)
		{
			ES.ESM = this;
		}

		ChangeState(enemyManageStates[0].Name);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if(State == null)
			return;

		if(enemySpawner.GameOver == true){
			anim.Play("Idle");
			return;
		}
		
		//State.UpdateState(delta);
		
	}
	
	
}
