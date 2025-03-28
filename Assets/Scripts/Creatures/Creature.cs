using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
	protected const float VelocityZeroOffset = 0.1f;

	protected Rigidbody2D SelfRigidbody;

	protected virtual void Awake()
	{
		SelfRigidbody = GetComponent<Rigidbody2D>();
	}
}
