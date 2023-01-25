using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyDeath : EnemyState
{
	[Export]
	private AudioStream enemyDeathSound;
	[Export]
	private CollisionShape2D shape1;
	[Export]
	private CollisionShape2D shape2;
	[Export]
	private Sprite2D sprite;
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		
		string path = "res://Prefabs/PlayAudioOnce.tscn";
		var packedScene = GD.Load<PackedScene>(path);
		

		PlayAudioOnce p = (PlayAudioOnce)packedScene.Instantiate();
		
		p.audio = enemyDeathSound;
		GetNode(GetTree().CurrentScene.GetPath()).CallDeferred("add_child", p);

		path = "res://Prefabs/Blood.tscn";
		packedScene = GD.Load<PackedScene>(path);
		

		Blood b = (Blood)packedScene.Instantiate();
		b.GlobalPosition = ESM._Enemy.GlobalPosition;
		GetNode(GetTree().CurrentScene.GetPath()).CallDeferred("add_child", b);
		ESM._Enemy.QueueFree();

	}
}
