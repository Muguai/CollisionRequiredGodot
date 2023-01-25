using Godot;
using System;

public partial class CameraZoom : Camera2D
{
	[Export]
	private CharacterBody2D player1;

	[Export]
	private CharacterBody2D player2;

	float zoomMin = 0.2f;
	float zoomMax = 0.02f;
	
	float yOffset = 650;
	float xOffset = 600;
	float screenWidth = 1920;
	float screenHeight = 1080;

	EnemySpawner enemySpawner;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(enemySpawner.GameOver == true)
			return;
		
		Position = (player1.Position + player2.Position) / new Vector2(2,2);

		Vector2 zoom = new Vector2(0,0);
		Vector2 zoom2 = new Vector2(0,0);

		zoom.x = (screenWidth - xOffset)/ Mathf.Abs((player1.Position.x- player2.Position.x));
		zoom.y = (screenWidth - xOffset)/ Mathf.Abs((player1.Position.x- player2.Position.x));
		
		
		zoom2.x = (screenHeight - yOffset)/ Mathf.Abs((player1.Position.y- player2.Position.y));
		zoom2.y = (screenHeight - yOffset)/ Mathf.Abs((player1.Position.y- player2.Position.y));
		
		zoom = CheckZoomMin(zoom);
		zoom2 = CheckZoomMin(zoom2);

		if(zoom2 < zoom)
			Zoom = zoom2;
		else
			Zoom = zoom;

	}

	private Vector2 CheckZoomMin(Vector2 _zoom)
	{
		if(_zoom.x > zoomMin){
			_zoom.x = zoomMin;
			_zoom.y = zoomMin;
		}

		return _zoom;
	}

	public override void _Ready()
	{
		base._Ready();
		enemySpawner = (EnemySpawner)GetNode(GetTree().CurrentScene.GetPath() + "/SpawnBox") as EnemySpawner;

	}
}
