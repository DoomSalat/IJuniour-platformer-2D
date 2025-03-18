using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
	[SerializeField] private float _delayDestroy = 1;

	private bool _wasCollect;

	public event System.Action Collected;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_wasCollect)
			return;

		if (collision.TryGetComponent<ItemCollector>(out _))
		{
			_wasCollect = true;
			Collected?.Invoke();

			Destroy(gameObject, _delayDestroy);
		}
	}
}
