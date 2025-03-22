using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
	protected const float VelocityZeroOffset = 0.1f;

	protected Rigidbody2D Rigidbody;

	protected virtual void Awake()
	{
		Rigidbody = GetComponent<Rigidbody2D>();
	}
}
