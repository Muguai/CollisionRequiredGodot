using Godot;
using System;

public partial class EnemySpawner : Area2D
{
	RectangleShape2D r2D;
	Vector2 orgin;
	PackedScene scene;	// Called when the node enters the scene tree for the first time.

	[Export]
	private Label waveTitle;
	[Export]
	private AnimationPlayer animTitle;
	
	[Export]
	private Node2D colUP;
	
	[Export]
	private Node2D colDOWN;
	
	[Export]
	private Node2D colLEFT;
	
	[Export]
	private Node2D colRIGHT;

	private int InitialSpawnEnemyAmount = 15;
	private int increaseWaveAmount = 5;
	private int waveSpawnAmount;
	private float minSpawnTime = 2f;
	private float maxSpawnTime = 5f;
	private float minAbsoulteSpawnTime = 0.5f;

	private float lowerSpawnTime = 0.8f;

	private int maxGroup = 5;

	private float timeToSpawn;
	private bool waveStarted = false;

	private int waveNumber = 1;

	public bool GameOver = false;
	public override void _Ready()
	{
		CollisionShape2D spawnArea = GetChild<CollisionShape2D>(0);
		
		r2D = (RectangleShape2D)spawnArea.Shape;

		orgin = new Vector2(spawnArea.GlobalPosition.x - r2D.Size.x, spawnArea.GlobalPosition.y - r2D.Size.y );
		for(int i = 0; i < 5; i++){

			SpawnEnemy();
		}
		waveSpawnAmount = InitialSpawnEnemyAmount;
		waveStarted = true;
		timeToSpawn = 1f;
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GameOver)
			return;
		
		timeToSpawn -= (float)delta;
		//GD.Print(timeToSpawn + " Spawning " + waveSpawnAmount);

		if(timeToSpawn < 0 && waveStarted){
			GD.Print("Spawn");
			Random r = new Random();
			
			int randomSpawnAmount = r.Next(1, maxGroup);

			waveSpawnAmount -= randomSpawnAmount;

			if(waveSpawnAmount < 0)
				randomSpawnAmount += waveSpawnAmount;

			for(int i = 0; i < randomSpawnAmount; i++){
				SpawnEnemy();
			}
			//waveTitle.Text = "Wave " + waveNumber + " : " + waveSpawnAmount;

			r = new Random();
			double randomTimeToSpawn = (r.NextDouble() * (maxSpawnTime - minSpawnTime)) + minSpawnTime;
			timeToSpawn = (float)randomTimeToSpawn;
		}

		if(waveSpawnAmount <= 0 && waveStarted) {
			GD.Print("New Wave");
			
			waveStarted = false;
			NewWave();

		}

	}

	public void NewWave(){
		// Increase Difficulty
		InitialSpawnEnemyAmount += increaseWaveAmount;
		waveSpawnAmount = InitialSpawnEnemyAmount;

		maxSpawnTime -= lowerSpawnTime;
		minSpawnTime -= lowerSpawnTime;
		if(minSpawnTime < minAbsoulteSpawnTime)
			minSpawnTime = minAbsoulteSpawnTime;
		if(maxSpawnTime < minAbsoulteSpawnTime)
			minSpawnTime = minAbsoulteSpawnTime;

		// Update UI;
		if(waveNumber + 1 >= 10)
			waveTitle.VisibleRatio = 0.8f;
		else
			waveTitle.VisibleRatio = 0.9f;
		animTitle.Play("Blink");

		waveNumber += 1;
		waveTitle.Text = "Wave " + waveNumber;

		//Start new Wave
		Random r = new Random();
		double randomTimeToSpawn = (r.NextDouble() * (maxSpawnTime - minSpawnTime)) + minSpawnTime;
		timeToSpawn = (float)randomTimeToSpawn;
		waveStarted = true;



	}

	public void SpawnEnemy(){

		Random r = new Random();

		double range = (double) r2D.Size.x - (double) -r2D.Size.x;
		double sample = r.NextDouble();
		double scaled = (sample * range) + -r2D.Size.x;
		float x = (float) scaled;

		
		range = (double) r2D.Size.y - (double) -r2D.Size.y;
		sample = r.NextDouble();
		scaled = (sample * range) + -r2D.Size.y;
		float y = (float) scaled;

		string path = "res://prefabs/Enemy.tscn";
		var packedScene = GD.Load<PackedScene>(path);
		

		Enemy e = (Enemy)packedScene.Instantiate();
		e.Position = new Vector2(x,y);
		GD.Print(x + ", " + y + " ShapeSize " + r2D.Size.x + ", " + r2D.Size.y);
		CallDeferred("add_child", e);	
	}
}
