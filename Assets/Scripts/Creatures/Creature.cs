using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour
{
	protected Rigidbody2D _rigidbody;

	protected virtual void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}
}
