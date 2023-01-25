using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private float speed = 2100.0f;
	private float accel = 0.2f;
	private float deccel = 0.15f;
	public float CollideSpeed = 500;
	private string[] controls = new string[4];

	public Vector2 direction;

	public Sprite2D playerSprite;

	public override void _Ready(){
		if(this.Name == "FirstRock")
			controls = new string[4]{"A", "D", "W", "S"};
		else
			controls = new string[4]{"ui_left", "ui_right", "ui_up", "ui_down"};
		
		playerSprite = GetNode<Sprite2D>("Sprite2D");

	}
	

	public override void _PhysicsProcess(double delta)
	{

	}
	public Vector2 SetDirection(){
		direction = Input.GetVector(controls[0], controls[1], controls[2], controls[3]);
		return direction;
	}

	public void MovePlayer(){

		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if (direction != Vector2.Zero)
		{
			velocity.x = Mathf.Lerp(velocity.x, direction.x * speed, accel);			
			velocity.y = Mathf.Lerp(velocity.y, direction.y * speed, accel);
		}
		else
		{
			velocity.x = Mathf.Lerp(Velocity.x, 0, deccel);
			velocity.y = Mathf.Lerp(Velocity.y, 0, deccel);
		}

		if(Velocity.x > 0)	
			playerSprite.FlipH = false;
		else
			playerSprite.FlipH = true;

		Velocity = velocity;
		
		MoveAndSlide();
	}
}
