using Godot;
using System;

public partial class CanvasManager : Node
{

	Control GameRunningUi;
	Control DeathScreen;
	Label otherSurvived;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameRunningUi = GetChild<Control>(0);
		DeathScreen = GetChild<Control>(1);
		otherSurvived = GameRunningUi.GetChild<PanelContainer>(1).GetChild<Label>(0);
	}

	public void PlayerDeath(){
		GameRunningUi.Visible = false;
		DeathScreen.Visible = true;
		Label Survived = DeathScreen.GetChild<Label>(1);
		Survived.Text = otherSurvived.Text;
	}

	public void PlayerHurt(int whichOne, int value){
		HSlider slider = GameRunningUi.GetChild<HSlider>(1 + whichOne);
		slider.Value = value;
	}
}
