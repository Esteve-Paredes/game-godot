using Godot;
using System;

public partial class Main : Node
{
	[Export] public PackedScene MobScene { get; set; }
	private PathFollow2D _mobSpawnLocation;
	private Timer _mobTimer;
	private Timer _scoreTimer;
	private Timer _startTimer;
	
	private int _score;
	
	public override void _Ready()
	{
		_mobSpawnLocation = GetNode<PathFollow2D>("Path2D/MobSpawnLocation");
		_mobTimer = GetNode<Timer>("MobTimer");
		_scoreTimer = GetNode<Timer>("ScoreTimer");
		_startTimer = GetNode<Timer>("StartTimer");
		
		_mobTimer.Connect("timeout", new Callable(this, nameof(_on_MobTimer_timeout)));
		_scoreTimer.Connect("timeout", new Callable(this, nameof(_on_ScoreTimer_timeout)));
		_startTimer.Connect("timeout", new Callable(this, nameof(_on_StartTimer_timeout)));
		
		NewGame();
	}
	
	private void _on_ScoreTimer_timeout()
	{
		_score++;
	}

	private void _on_StartTimer_timeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
	
	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame()
	{
		_score = 0;

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		_startTimer.Start();
	}

	private void _on_MobTimer_timeout()
	{
		// Create a new instance of the Mob scene.
		var mob = MobScene.Instantiate<EnemyQuid>();

		// Choose a random location on Path2D.
		_mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		var direction = _mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mob.Position = _mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity  = velocity.Rotated(direction);
		
		// Disable gravity for the mob.
		mob.GravityScale = 0;

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}

	public override void _Process(double delta)
	{
	}
}
