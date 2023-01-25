using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class PlayerStateMachine : StateMachine
{
	[Export]
	public Player _Player;
	[Export]
	public AnimationPlayer anim;
	
	[Export]
	public AudioStreamPlayer audioSteamPlayer;

	public CharacterBody2D otherPlayer;
	public override void _Ready()
	{
		base._Ready();


		
		if(_Player.Name == "FirstRock")
			otherPlayer = (CharacterBody2D)GetNode(GetTree().CurrentScene.GetPath() + "/SecondRock");
		else if(_Player.Name == "SecondRock")
			otherPlayer = (CharacterBody2D)GetNode(GetTree().CurrentScene.GetPath() + "/FirstRock");
		
		List<PlayerState> playerManageStates = GetNode<Node>("States").GetChildren().OfType<PlayerState>().ToList();

		foreach(PlayerState PS in playerManageStates)
		{
			PS.PSM = this;
		}

		ChangeState(playerManageStates[0].Name);
	}
	
	
}
