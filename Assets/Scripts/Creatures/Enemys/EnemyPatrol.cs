using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class EnemyPatrol : Enemy
{
	[SerializeField][Min(0)] private float _speedIdle = 5;
	[SerializeField][Min(0)] private float _speedChase = 8;
	[SerializeField][Min(0)] private float _waitTime = 2f;

	[FoldoutGroup("Vision")]
	[SerializeField] private LayerMask _obstacleLayer;
	[FoldoutGroup("Vision")][SerializeField] private Transform _groundCheck;
	[FoldoutGroup("Vision")][SerializeField] private Transform _obstacleCheck;
	[FoldoutGroup("Vision")][SerializeField] private float _checkDistanceObstacle = 1f;
	[FoldoutGroup("Vision")][SerializeField] private float _checkDistanceGround = 0.1f;

	[FoldoutGroup("Animations")]
	[Required][SerializeField] private Animator _animator;
	[Space]
	[FoldoutGroup("Animations")][SerializeField] private string _animBool_Run = "Run";

	private PatrolState _patrolState = PatrolState.Walk;
	private bool _walkRight = true;

	private void FixedUpdate()
	{
		if (_patrolState == PatrolState.Walk)
		{
			_animator.SetBool(_animBool_Run, true);
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
		float speed = _walkRight ? _speedIdle : -_speedIdle;
		_rigidbody.linearVelocityX = speed;

		if (CheckObstacle() || CheckGround() == false)
		{
			_rigidbody.linearVelocityX = 0;
			_patrolState = PatrolState.Idle;
			StartCoroutine(WaitAndTurn());
		}
	}

	private IEnumerator WaitAndTurn()
	{
		_animator.SetBool(_animBool_Run, false);
		yield return new WaitForSeconds(_waitTime);

		_walkRight = !_walkRight;
		_patrolState = PatrolState.Walk;
		_animator.SetBool(_animBool_Run, true);
	}

	private bool CheckObstacle()
	{
		Vector2 direction = _walkRight ? Vector2.right : Vector2.left;
		RaycastHit2D hit = Physics2D.Raycast(_obstacleCheck.position, direction, _checkDistanceObstacle, _obstacleLayer);

		return hit.collider != null;
	}

	private bool CheckGround()
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