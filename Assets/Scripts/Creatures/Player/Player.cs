using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature
{
	private const float VelocityZeroOffset = 0.1f;

	[Required][SerializeField] private AxisHandler _axisHandler;
	[Required][SerializeField] private Mover _mover;
	[Required][SerializeField] private Jumper _jumper;
	[Required][SerializeField] private BoxCreatureAnimator _animator;

	private bool _canPressJump = true;
	private Vector2 _axisDirection;

	private void OnEnable()
	{
		_axisHandler.MainControls.Player.Jump.performed += JumpPerformed;
		_axisHandler.MainControls.Player.Jump.canceled += JumpCanceled;
	}

	private void OnDisable()
	{
		_axisHandler.MainControls.Player.Jump.performed -= JumpPerformed;
		_axisHandler.MainControls.Player.Jump.canceled -= JumpCanceled;
	}

	private void Update()
	{
		_axisDirection = _axisHandler.AxisDirection();

		if (_axisDirection.x == 0)
		{
			_mover.StopMove();
			_animator.SetRun(false);
		}
		else
		{
			_animator.SetRun(true);
		}

		_animator.SetGround(_jumper.IsGrounded());
	}

	private void FixedUpdate()
	{
		_mover.FixedMove(_axisDirection.x);
		_jumper.FixedForce(_axisDirection.y > 0);

		if (_rigidbody.linearVelocityY < -VelocityZeroOffset)
		{
			_animator.SetFall(true);
		}
		else
		{
			_animator.SetFall(false);
		}
	}

	private void JumpPerformed(InputAction.CallbackContext context)
	{
		if (_canPressJump && _jumper.CanJump())
		{
			_canPressJump = false;
			_jumper.Jump();
			_animator.Jump();
		}
	}

	private void JumpCanceled(InputAction.CallbackContext context)
	{
		_canPressJump = true;
	}
}
