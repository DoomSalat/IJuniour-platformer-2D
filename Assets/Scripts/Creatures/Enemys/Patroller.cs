using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class Patroller : Creature
{
	private const int RightDirection = 1;
	private const int LeftDirection = -1;

	[Required][SerializeField] private Mover _mover;
	[Required][SerializeField] private BoxCreatureAnimator _boxAnimator;

	[SerializeField][Min(0)] private float _speedChaseMultiplier = 1.2f;
	[SerializeField][Min(0)] private float _waitTime = 2f;

	[FoldoutGroup("Vision")]
	[SerializeField] private LayerMask _obstacleLayer;
	[FoldoutGroup("Vision")][SerializeField] private Transform _groundCheck;
	[FoldoutGroup("Vision")][SerializeField] private Transform _obstacleCheck;
	[FoldoutGroup("Vision")][SerializeField] private float _checkDistanceObstacle = 1f;
	[FoldoutGroup("Vision")][SerializeField] private float _checkDistanceGround = 0.1f;

	private PatrolState _patrolState = PatrolState.Walk;
	private bool _walkRight = true;

	private void FixedUpdate()
	{
		if (_patrolState == PatrolState.Walk)
		{
			_boxAnimator.SetRun(true);
			Patrol();
		}
	}

	private void OnDrawGizmos()
	{
		if (_groundCheck != null)
		{
			Vector2 groundCheckPosition = _groundCheck.position + (_walkRight ? Vector3.right : Vector3.left) * _checkDistanceObstacle;
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector2.down * _checkDistanceGround);
		}

		if (_obstacleCheck != null)
		{
			Vector2 wallCheckPosition = _obstacleCheck.position;
			Vector2 wallDirection = _walkRight ? Vector2.right : Vector2.left;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(wallCheckPosition, wallCheckPosition + wallDirection * _checkDistanceObstacle);
		}
	}

	private void Patrol()
	{
		_mover.Move(_walkRight ? RightDirection : LeftDirection);

		if (HasObstacleInFront() || IsGroundBelow() == false)
		{
			_mover.StopMove();
			_patrolState = PatrolState.Idle;

			StartCoroutine(WaitAndTurn());
		}
	}

	private IEnumerator WaitAndTurn()
	{
		_boxAnimator.SetRun(false);

		yield return new WaitForSeconds(_waitTime);

		_walkRight = !_walkRight;
		_patrolState = PatrolState.Walk;
	}

	private bool HasObstacleInFront()
	{
		Vector2 direction = _walkRight ? Vector2.right : Vector2.left;
		RaycastHit2D hit = Physics2D.Raycast(_obstacleCheck.position, direction, _checkDistanceObstacle, _obstacleLayer);

		return hit.collider != null;
	}

	private bool IsGroundBelow()
	{
		Vector2 checkPosition = _groundCheck.position + (_walkRight ? Vector3.right : Vector3.left) * _checkDistanceObstacle;
		RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, _checkDistanceGround, _obstacleLayer);

		return hit.collider != null;
	}
}

public enum PatrolState
{
	Idle,
	Walk,
	Chase
}