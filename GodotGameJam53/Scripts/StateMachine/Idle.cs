using Godot;
using System;
using System.Collections.Generic;

public partial class Idle : PlayerState
{
	


	
	double WaitTimer = 0;
 
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		PSM.anim.Play("Idle");

		

		
			
		
		if(PSM.LastState == "Collide"){
			PSM._Player.Velocity = PSM._Player.Velocity.Normalized() * 4000f;
			WaitTimer = 0.3;
		}
		else{
			WaitTimer = 0;
		}

	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);

		if(WaitTimer > 0){
			PSM._Player.Velocity = PSM._Player.Velocity.Normalized() * 4000f;
			WaitTimer -= delta;
			PSM._Player.MoveAndSlide();
			return;
		
		}
		Vector2 direction = PSM._Player.SetDirection();

		if(direction != Vector2.Zero){
			PSM._Player.MovePlayer();
			PSM.ChangeState("Walk");
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
}
