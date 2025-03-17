using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Movement
{
	private const float VelocityZeroOffset = 0.1f;
	private const string PlayerTag = "Player";
	private const float BaseGravityMultiplier = 1f;

	[SerializeField][Min(0)] private float _speed = 5;

	[FoldoutGroup("Jump")]
	[SerializeField][Min(0)] private float _jumpInitialImpulse = 10;
	[FoldoutGroup("Jump")][SerializeField][Min(0)] private float _jumpForce = 5;
	[FoldoutGroup("Jump")][SerializeField][Min(0)] private float _fallMultiplier = 1.2f;
	[FoldoutGroup("Jump")][SerializeField][Min(0)] private float _maxJumpTime = 1.5f;
	[Space]
	[FoldoutGroup("Jump")][SerializeField] private Transform _groundCheckPoint;
	[FoldoutGroup("Jump")][SerializeField][Min(0)] private float _groundCheckRadius = 0.1f;
	[FoldoutGroup("Jump")][SerializeField] private LayerMask _groundLayer;

	[FoldoutGroup("Animations")]
	[Required][SerializeField] private Animator _animator;
	[Space]
	[FoldoutGroup("Animations")][SerializeField] private string _animBool_Run = "Run";
	[FoldoutGroup("Animations")][SerializeField] private string _animTrigger_Jump = "Jump";
	[FoldoutGroup("Animations")][SerializeField] private string _animBool_Ground = "Ground";
	[FoldoutGroup("Animations")][SerializeField] private string _animBool_Fall = "Fall";

	private bool _isJumping = false;
	private bool _canPressJump = true;
	private float _jumpTimeCounter;
	private Vector2 _axisDirection;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnEnable()
	{
		_mainControls.Player.Jump.performed += JumpPerformed;
		_mainControls.Player.Jump.canceled += JumpCanceled;

		base.OnEnable();
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		_mainControls.Player.Jump.performed -= JumpPerformed;
		_mainControls.Player.Jump.canceled -= JumpCanceled;
	}

	private void Update()
	{
		_axisDirection = AxisMove();

		if (_axisDirection.x == 0)
		{
			StopMove();
		}
		else
		{
			_animator.SetBool(_animBool_Run, true);
		}

		_animator.SetBool(_animBool_Ground, IsGrounded());
	}

	private void FixedUpdate()
	{
		_rigidbody.linearVelocityX = _axisDirection.x * _speed;

		if (_isJumping)
		{
			if (_axisDirection.y > 0 && _jumpTimeCounter > 0)
			{
				_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
				_jumpTimeCounter -= Time.fixedDeltaTime;
			}
			else
			{
				_isJumping = false;

				if (_rigidbody.linearVelocityY > VelocityZeroOffset)
				{
					_rigidbody.linearVelocityY = 0;
				}
			}
		}
		else
		{
			if (_rigidbody.linearVelocityY < -VelocityZeroOffset)
			{
				_rigidbody.linearVelocityY += Physics2D.gravity.y * (_fallMultiplier - BaseGravityMultiplier) * Time.fixedDeltaTime;

				_animator.SetBool(_animBool_Fall, true);
			}
			else
			{
				_animator.SetBool(_animBool_Fall, false);
			}
		}
	}

	private void StopMove()
	{
		_rigidbody.linearVelocityX = 0;

		_animator.SetBool(_animBool_Run, false);
	}

	private void JumpPerformed(InputAction.CallbackContext context)
	{
		if (CanJump())
		{
			_canPressJump = false;
			Jump();
		}
	}

	private void JumpCanceled(InputAction.CallbackContext context)
	{
		_canPressJump = true;
	}

	private void Jump()
	{
		_isJumping = true;
		_jumpTimeCounter = _maxJumpTime;

		_rigidbody.AddForce(Vector2.up * _jumpInitialImpulse, ForceMode2D.Impulse);
		_animator.SetTrigger(_animTrigger_Jump);
	}

	private bool IsGrounded()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);

		foreach (Collider2D hit in hits)
		{
			if (hit != null && hit.gameObject.CompareTag(PlayerTag) == false)
			{
				return true;
			}
		}

		return false;
	}

	private bool CanJump()
	{
		return _isJumping == false && _canPressJump && IsGrounded();
	}
}
