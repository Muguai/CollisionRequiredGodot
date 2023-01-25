using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyAttack : EnemyState
{
	private int damage = 1;
	private double waitTimer;
	private double currentAnimTimer;
	CharacterBody2D player;
	private bool Exited;
	Dictionary<string, object> _message;
	
	[Export]
	private AudioStream pickaxeAudio;
	private float checkHealth;
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		_message = message;
		Exited = false;
		player = (CharacterBody2D)message["Player"];
		var hp = (Health)player.GetNode<Node>("Health") as Health;
		checkHealth = hp.getHealth();

		hp.Damage(damage);
		ESM.anim.Play("Attack");
		currentAnimTimer = 0;
		waitTimer = 1.0;

		Random R = new Random();

		double result = (R.NextDouble() * (1.5 - 0.9)) + 0.9;
		ESM.audioSteamPlayer.PitchScale = (float)result;

		
		if(checkHealth <= 1){
			ESM.ChangeState("EnemyWalk");		
		}
	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
		waitTimer -= delta;
		currentAnimTimer += delta;
		if(ESM.anim.CurrentAnimationLength < currentAnimTimer)
			ESM.anim.Play("Idle");

		if(waitTimer < 0){

			if(Exited == true)
				ESM.ChangeState("EnemyWalk");
			else
				ESM.ChangeState("EnemyWalk", _message);

		}

	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		ESM.audioSteamPlayer.PitchScale = 1f;
		
	}
	
	private void _on_area_2d_body_exited(Node2D body)
	{
		if(ESM.CurrentState != "EnemyAttack")
			return;
		if(body.IsInGroup("Player")){
			if(body.Name == player.Name)
				Exited = true;
		}
			

		// Replace with function body.
	}
}



