using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyWalk : EnemyState
{
	
	
	public override void UpdateState(double delta){
		base.UpdateState(delta);
		ESM._Enemy.EnemyWalk(delta);

	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		if(ESM.CurrentState != "EnemyWalk")
			return;
		if(body.IsInGroup("Player")){
			Dictionary<string, object> message = new Dictionary<string, object>();
			message.Add("Player", body);

			GD.Print("AttackPlayer");
			ESM.ChangeState("EnemyAttack", message);
		}
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		if(ESM.LastState == "EnemyAttack"){
			if(message != null){			
				GD.Print("ReAttackPlayer");
				ESM.ChangeState("EnemyAttack", message);
			}
				
		}

		
		ESM.anim.PlaybackSpeed = 2f;
			
	}

	public override void OnExit(string nextState)
	{
		base.OnExit(nextState);
		ESM.anim.PlaybackSpeed = 1f;
	}
}



