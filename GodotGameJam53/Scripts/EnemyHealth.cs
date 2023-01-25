using Godot;
using System;

public partial class EnemyHealth : Node
{
	public int fullHealth = 1;
	[Export]
	EnemyStateMachine ESM;
	
	public void Damage(int damage){
		fullHealth -= damage;
		GD.Print("EnemyAttacked");
		if(fullHealth <= 0)
			ESM.ChangeState("EnemyDeath");
	}
}
