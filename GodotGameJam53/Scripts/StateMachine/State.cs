using Godot;
using System;
using System.Collections.Generic;

public partial class State : Node
{
	private bool hasBeenInitialized = false;

	private bool onUpdateHasFired = false;

	//[Signal] public delegate void StateStartEventHandler();
	
	//[Signal] public delegate void StateUpdatedEventHandler();
	
	//[Signal] public delegate void StateExitedEventHandler();

	public virtual void OnStart(Dictionary<string, object> message)
	{
		//EmitSignal(nameof(StateStartEventHandler));
		hasBeenInitialized = true;
	}

	public virtual void OnUpdate()
	{
		if(!hasBeenInitialized)
			return;
		//EmitSignal(nameof(StateUpdatedEventHandler));
		onUpdateHasFired = true;
	}

	public virtual void UpdateState(double delta)
	{
		if(!onUpdateHasFired)
			return;
	}

	public virtual void OnExit(string nextState)
	{
		if(!hasBeenInitialized)
			return;

		//EmitSignal(nameof(StateExitedEventHandler));
		hasBeenInitialized = false;
		onUpdateHasFired = false;
	}






}
