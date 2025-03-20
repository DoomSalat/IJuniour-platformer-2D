using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
	private Collider2D _collider;

	public event System.Action<Creature> Collected;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
	}

	public void Collect(Creature collecter)
	{
		_collider.enabled = false;
		Collected?.Invoke(collecter);
	}
}
