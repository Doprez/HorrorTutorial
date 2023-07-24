using Stride.Engine.Events;

namespace HorrorTutorial.Code.Player;
public class InputHandler : SyncScript
{

	public static readonly EventKey<Vector2> MoveEvent = new();
	public static readonly EventKey JumpEvent = new();
	public static readonly EventKey<Vector2> LookEvent = new();

	public override void Update()
	{
		Move();
		Look();
		UpdateMouselock();

		if (Input.IsKeyDown(Keys.Space))
			JumpEvent.Broadcast();
	}

	private void Move()
	{
		var movement = Vector2.Zero;
		if (Input.IsKeyDown(Keys.W))
			movement.Y += 1;
		if (Input.IsKeyDown(Keys.S))
			movement.Y -= 1;
		if (Input.IsKeyDown(Keys.A))
			movement.X -= 1;
		if (Input.IsKeyDown(Keys.D))
			movement.X += 1;
		MoveEvent.Broadcast(movement);
	}

	private void Look()
	{
		LookEvent.Broadcast(Input.MouseDelta);
	}

	private void UpdateMouselock()
	{
		if(Input.IsMouseButtonDown(MouseButton.Left))
		{
			Input.LockMousePosition(true);
			Game.IsMouseVisible = false;
		}
		if(Input.IsKeyPressed(Keys.Escape))
		{
			Input.UnlockMousePosition();
			Game.IsMouseVisible = true;
		}
	}

}
