using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
	[SerializeField] private Pickup _pickUp;

	private Collider2D _collider;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
	}

	public void Collect(GameObject collecter)
	{
		_collider.enabled = false;

		if (_pickUp != null)
			_pickUp.OnCollected(collecter);
	}
}
