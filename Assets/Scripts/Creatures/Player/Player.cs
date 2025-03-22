using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature
{
	[Header("Components")]
	[Required][SerializeField] private AxisHandler _axisHandler;
	[Required][SerializeField] private Mover _mover;
	[Required][SerializeField] private Jumper _jumper;
	[Required][SerializeField] private Health _health;
	[Required][SerializeField] private BoxCreatureAnimator _animator;

	private bool _canPressJump = true;
	private bool _isGrounded;
	private Vector2 _axisDirection;

	protected override void Awake()
	{
		base.Awake();

		_jumper.Grounded.Subscribe(SetGround).AddTo(this);
	}

	private void OnEnable()
	{
		_axisHandler.MainControls.Player.Jump.performed += OnJumpPerformed;
		_axisHandler.MainControls.Player.Jump.canceled += OnJumpCanceled;
	}

	private void OnDisable()
	{
		_axisHandler.MainControls.Player.Jump.performed -= OnJumpPerformed;
		_axisHandler.MainControls.Player.Jump.canceled -= OnJumpCanceled;
	}

	private void Update()
	{
		if (_health.CurrentHealth == 0)
			return;

		_axisDirection = _axisHandler.GetAxisDirection();

		if (_axisDirection.x == 0)
		{
			_mover.StopMove();
			_animator.SetRun(false);
		}
		else
		{
			_animator.SetRun(true);
		}
	}

	private void FixedUpdate()
	{
		if (_health.CurrentHealth == 0)
			return;

		_mover.Move(_axisDirection.x);
		_jumper.FixedForce(_axisDirection.y > 0);

		if (Rigidbody.linearVelocityY < -VelocityZeroOffset)
		{
			_animator.SetFall(true);
		}
		else
		{
			_animator.SetFall(false);
		}
	}

	private void SetGround(bool isGround)
	{
		_isGrounded = isGround;
		_animator.SetGround(_isGrounded);
	}

	private void OnJumpPerformed(InputAction.CallbackContext context)
	{
		if (_canPressJump && _jumper.IsGround)
		{
			_canPressJump = false;
			_jumper.Jump();
			_animator.PlayJump();
		}
	}

	private void OnJumpCanceled(InputAction.CallbackContext context)
	{
		_canPressJump = true;
	}
}
