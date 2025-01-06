using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export] private AnimatedSprite2D _animatedSprite;

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		
		var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (direction == Vector2.Zero)
		{
			_animatedSprite.Play("idle");
			_animatedSprite.Rotation = 0;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		else
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;

			if (direction.X != 0)
			{
				_animatedSprite.Rotation = direction.X > 0 ? Mathf.Pi / 2 : -Mathf.Pi / 2;
			}
			else if (direction.Y != 0)
			{
				_animatedSprite.Rotation = direction.Y > 0 ? Mathf.Pi : 0;
			}

			
			_animatedSprite.Play("move");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
