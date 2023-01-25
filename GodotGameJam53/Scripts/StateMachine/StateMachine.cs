using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	//[Signal] public delegate void PreStartEventHandler();
	//[Signal] public delegate void PostStartEventHandler();
	//[Signal] public delegate void PreExitEventHandler();
	//[Signal] public delegate void PostExitEventHandler();

	public List<State> States;

	public string CurrentState;

	public string LastState;

	protected State State = null;

	public override void _Ready()
	{
		base._Ready();

		States = GetNode<Node>("States").GetChildren().OfType<State>().ToList();
	}

	private void SetState(State _state, Dictionary<string, object> message)
	{
		if(_state == null)
			return;

		if(State != null)
		{
			//EmitSignal(nameof(PreExitEventHandler));
			State.OnExit(_state.GetType().ToString());
			//EmitSignal(nameof(PostExitEventHandler));
		}

		LastState = CurrentState;
		CurrentState = _state.GetType().ToString();

		State = _state;
		//EmitSignal(nameof(PreStartEventHandler));
		State.OnStart(message);
		//EmitSignal(nameof(PostStartEventHandler));
		State.OnUpdate();
	}

	public void ChangeState(string stateName, Dictionary<string, object> message = null)
	{
		foreach(State _state in States)
		{
			if(stateName == _state.GetType().ToString())
			{
				SetState(_state, message);
				return;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if(State == null)
			return;
		
		State.UpdateState(delta);
		
	}



}
