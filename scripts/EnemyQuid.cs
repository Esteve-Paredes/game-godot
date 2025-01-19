using Godot;
using System;

public partial class EnemyQuid : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D"); 
		var mobTypes = animatedSprite.SpriteFrames.GetAnimationNames();
		
		animatedSprite.Play(mobTypes[GD.Randi() % mobTypes.Length]); //reproducimos una animación aleatoria
	}
	
	private void _on_VisibilityNotifier2D_screen_exited()
	{
		QueueFree(); //liberamos la memoria del nodo cuando sale de la pantalla
	}

	public override void _Process(double delta)
	{
	}
	
	public void death()
	{
		QueueFree(); //liberamos la memoria del nodo
	}
}
