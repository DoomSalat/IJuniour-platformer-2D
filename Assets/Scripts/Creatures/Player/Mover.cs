using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
	private const int RightDirection = 1;
	private const int LeftDirection = -1;

	[SerializeField][Min(0)] private float _speed = 5;

	private Rigidbody2D _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void StopMove()
	{
		_rigidbody.linearVelocityX = 0;
	}

	public void Move(float directionX, float multiplier = 1)
	{
		directionX = Mathf.Clamp(directionX, LeftDirection, RightDirection);
		_rigidbody.linearVelocityX = directionX * multiplier * _speed;
	}
}
