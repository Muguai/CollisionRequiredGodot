using Godot;
using System;
using System.Collections.Generic;

public partial class Walk : PlayerState
{
	double WaitTimer = 0;
	

	public override void OnStart(Dictionary<string, object> message){
		base.OnStart(message);
		

		PSM.anim.Play("RockRun");
		PSM.anim.PlaybackSpeed = 2.5f;

	}


	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
		

		Vector2 direction = PSM._Player.SetDirection();
		PSM._Player.MovePlayer();



		if(direction == Vector2.Zero){
			PSM.ChangeState("Idle");
		}

		if (Input.IsActionJustPressed("Space"))
		{
			if(PSM._Player.Position.DistanceTo(PSM.otherPlayer.Position) < 1000f){
				return;
			}
			GD.Print("Meteor");

			PSM.ChangeState("Collide");
		}

	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		PSM.anim.PlaybackSpeed = 1f;

		//PSM._Player.Velocity = Vector2.Zero;
	}
}
