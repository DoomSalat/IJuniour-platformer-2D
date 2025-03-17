using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
	[SerializeField] private string _collectorTag = "Player";
	[SerializeField] private float _delayDestroy = 1;

	private bool _wasCollect;

	public event System.Action Collected;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_wasCollect)
			return;

		if (collision.CompareTag(_collectorTag))
		{
			_wasCollect = true;
			Collected?.Invoke();

			Destroy(gameObject, _delayDestroy);
		}
	}
}
