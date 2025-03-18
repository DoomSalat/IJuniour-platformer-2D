using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
	private Collider2D _collider;

	public event System.Action Collected;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
	}

	public void Collect()
	{
		_collider.enabled = false;
		Collected?.Invoke();
	}
}
