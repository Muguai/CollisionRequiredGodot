using Godot;
using System;
using System.Collections.Generic;

public partial class Collide : PlayerState
{
	CharacterBody2D otherPlayer;
	
	private Vector2 target;
	private Vector2 targetDir;
	private float orginalCollideSpeed;
	[Export]
	private AudioStream collideAirforce;
	[Export]
	private AudioStream collideHit;

	
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		
		otherPlayer = PSM.otherPlayer;
			
		orginalCollideSpeed = PSM._Player.CollideSpeed;

		//target = PSM._Player.Position.Lerp(otherPlayer.Position, 0.5f);
		target = otherPlayer.Position - PSM._Player.Position;
		targetDir = target.Normalized();

		if(targetDir.x > 0)	
			PSM._Player.playerSprite.FlipH = false;
		else
			PSM._Player.playerSprite.FlipH = true;

		PSM._Player.Velocity = Vector2.Zero;

		PSM.anim.Play("Collide");
		
		PSM.anim.PlaybackSpeed = 1f;
		PSM.audioSteamPlayer.PitchScale = 1.5f;
		PSM.audioSteamPlayer.Stream = collideAirforce;
		PSM.audioSteamPlayer.Play(0);

		//PSM._Player.GetNode<CollisionShape2D>("BodyCollider").Disabled = true;

		
	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);

		Vector2 velocity = PSM._Player.Velocity;
		
		
		
		velocity.x = targetDir.x * PSM._Player.CollideSpeed ;			
		velocity.y = targetDir.y * PSM._Player.CollideSpeed;

		
		//GD.Print(newVec);
		PSM._Player.CollideSpeed += 50000f * (float)delta;

		PSM._Player.Velocity = velocity;
		//GD.Print("Target " + PSM._Player.CollideSpeed);
		PSM._Player.MoveAndSlide();
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		
		PSM.anim.PlaybackSpeed = 1f;
		//PSM._Player.GetNode<CollisionShape2D>("BodyCollider").Disabled = false;
		PSM._Player.CollideSpeed = orginalCollideSpeed;
	}

	private void _on_area_2d_area_entered(Area2D area)
	{
		if(PSM.CurrentState != "Collide")
			return;
		GD.Print("SomethingEntered");
		if(area.IsInGroup("PlayerTrigger")){
			PSM.ChangeState("Idle");
			PSM.audioSteamPlayer.Stop();
			
			PSM.audioSteamPlayer.PitchScale = 3f;
			PSM.audioSteamPlayer.Stream = collideHit;
			PSM.audioSteamPlayer.Play(0);
		}
		
		// Replace with function body.
	}

	
	private void _on_area_2d_body_entered(Node2D body)
	{
		if(PSM.CurrentState != "Collide")
			return;


		if(body.IsInGroup("Enemy")){
			GD.Print("AttackEnemy");

			var hp = (EnemyHealth)body.GetNode<Node>("Health") as EnemyHealth;
			hp.Damage(1);
		}

		// Replace with function body.
	}
}



