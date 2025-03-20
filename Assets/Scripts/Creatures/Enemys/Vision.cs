using UnityEngine;

public class Vision : MonoBehaviour
{
	[Header("Level")]
	[SerializeField] private LayerMask _obstacleLayer;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private Transform _obstacleCheck;
	[SerializeField] private float _checkDistanceObstacle = 1f;
	[SerializeField] private float _checkDistanceGround = 0.1f;

	[Header("Target")]
	[SerializeField] private LayerMask _targetLayer;
	[SerializeField] private Transform _targetCheck;
	[SerializeField] private float _checkDistanceTarget = 10f;

	private bool _lookRight = true;

	public bool LookRight => _lookRight;

	private void OnDrawGizmos()
	{
		if (_groundCheck != null)
		{
			Vector2 groundCheckPosition = _groundCheck.position + (_lookRight ? Vector3.right : Vector3.left) * _checkDistanceObstacle;
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector2.down * _checkDistanceGround);
		}

		if (_obstacleCheck != null)
		{
			Vector2 wallCheckPosition = _obstacleCheck.position;
			Vector2 wallDirection = _lookRight ? Vector2.right : Vector2.left;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(wallCheckPosition, wallCheckPosition + wallDirection * _checkDistanceObstacle);
		}

		if (_targetCheck != null)
		{
			Vector2 targetCheckPosition = _targetCheck.position;
			Vector2 targetDirection = _lookRight ? Vector2.right : Vector2.left;
			Gizmos.color = Color.red;
			Gizmos.DrawLine(targetCheckPosition, targetCheckPosition + targetDirection * _checkDistanceTarget);
		}
	}

	public void ReverseLook()
	{
		_lookRight = !_lookRight;
	}

	public bool HasObstacleInFront()
	{
		Vector2 direction = _lookRight ? Vector2.right : Vector2.left;
		RaycastHit2D hit = Physics2D.Raycast(_obstacleCheck.position, direction, _checkDistanceObstacle, _obstacleLayer);

		return hit.collider != null;
	}

	public bool IsGroundBelow()
	{
		Vector2 checkPosition = _groundCheck.position + (_lookRight ? Vector3.right : Vector3.left) * _checkDistanceObstacle;
		RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, _checkDistanceGround, _obstacleLayer);

		return hit.collider != null;
	}

	public bool IsTargetBelow()
	{
		Vector2 direction = _lookRight ? Vector2.right : Vector2.left;
		RaycastHit2D hit = Physics2D.Raycast(_targetCheck.position, direction, _checkDistanceTarget, _targetLayer);

		if (hit.collider != null && hit.collider.TryGetComponent<VisionTarget>(out _))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool IsGround()
	{
		RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, _checkDistanceGround, _obstacleLayer);

		return hit.collider != null;
	}
}
