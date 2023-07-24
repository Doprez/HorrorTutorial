
namespace HorrorTutorial.Code.Player;
public class PlayerMover : SyncScript
{
	[DataMember(0)]
	public float MoveSpeed { get; set; } = 5;
	[DataMember(1)]
	public float MouseSpeed { get; set; } = 100;

	public CharacterComponent Character { get; set; }
	public Entity CameraPivot { get; set; }

	private static EventReceiver<Vector2> _moveEvent = new(InputHandler.MoveEvent);
	private static EventReceiver<Vector2> _lookEvent = new(InputHandler.LookEvent);
	private static EventReceiver _jumpEvent = new(InputHandler.JumpEvent);

	private Vector3 _cameraDirection;

	public override void Update()
	{
		_moveEvent.TryReceive(out var movement);
		Move(movement);
		_lookEvent.TryReceive(out var look);
		Rotate(look);
		Jump();
	}

	private void Move(Vector2 movement)
	{
		var velocity = new Vector3(movement.X, 0, -movement.Y);
		velocity.Normalize();
		velocity *= MoveSpeed;

		velocity = Vector3.Transform(velocity, Character.Orientation);

		Character.SetVelocity(velocity);
	}

	private void Rotate(Vector2 look)
	{
		look *= MouseSpeed * this.DeltaTime();

		_cameraDirection.X -= look.Y;
		_cameraDirection.Y -= look.X;
		_cameraDirection.X =  MathUtil.Clamp(_cameraDirection.X, -1, 1);

		Character.Orientation = Quaternion.RotationY(_cameraDirection.Y);
		CameraPivot.Transform.Rotation = Quaternion.RotationX(_cameraDirection.X);
	}

	private void Jump()
	{
		if(_jumpEvent.TryReceive() && Character.IsGrounded)
			Character.Jump();
	}

}
