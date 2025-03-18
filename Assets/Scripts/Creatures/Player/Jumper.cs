using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
	private const float BaseGravityMultiplier = 1f;
	private const float VelocityZeroOffset = 0.1f;

	[SerializeField][Min(0)] private float _jumpInitialImpulse = 10;
	[SerializeField][Min(0)] private float _jumpForce = 5;
	[SerializeField][Min(0)] private float _fallMultiplier = 1.2f;
	[SerializeField][Min(0)] private float _maxJumpTime = 1.5f;

	[FoldoutGroup("Ground")][SerializeField] private Transform _groundCheckPoint;
	[FoldoutGroup("Ground")][SerializeField][Min(0)] private float _groundCheckRadius = 0.1f;
	[FoldoutGroup("Ground")][SerializeField] private LayerMask _groundLayer;

	private Rigidbody2D _rigidbody;
	private List<Collider2D> _selfColliders = new List<Collider2D>();

	private bool _isJumping = false;
	private bool _isCanGrounded = true;
	private float _jumpTimeCounter;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		InitializateSelfColliders();
	}

	private void InitializateSelfColliders()
	{
		Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

		foreach (Collider2D childCollider in childColliders)
		{
			if (childCollider != null && (_groundLayer & (1 << childCollider.gameObject.layer)) != 0)
			{
				_selfColliders.Add(childCollider);
			}
		}

		if (TryGetComponent<Collider2D>(out var selfCollider) && (_groundLayer & (1 << selfCollider.gameObject.layer)) != 0)
		{
			_selfColliders.Add(selfCollider);
		}
	}

	private bool IsSelfCollider(Collider2D collider)
	{
		foreach (Collider2D selfCollider in _selfColliders)
		{
			if (collider == selfCollider)
			{
				return true;
			}
		}
		return false;
	}

	public bool CanJump()
	{
		return _isJumping == false && IsGrounded();
	}

	public void Jump()
	{
		_isJumping = true;
		_isCanGrounded = false;
		_jumpTimeCounter = _maxJumpTime;

		_rigidbody.AddForce(Vector2.up * _jumpInitialImpulse, ForceMode2D.Impulse);
	}

	public bool IsGrounded()
	{
		if (_isCanGrounded == false)
			return false;

		Collider2D[] hits = Physics2D.OverlapCircleAll(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);

		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i] != null && IsSelfCollider(hits[i]) == false)
			{
				return true;
			}
		}

		return false;
	}

	public void FixedForce(bool isJumped)
	{
		if (_isJumping)
		{
			if (_jumpTimeCounter > 0 && isJumped)
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
			}

			_isCanGrounded = true;
		}
	}
}
