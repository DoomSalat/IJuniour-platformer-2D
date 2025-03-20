using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemCollector : MonoBehaviour
{
	[SerializeField] private Creature _collecter;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Collectable>(out var item))
		{
			item.Collect(_collecter);
		}
	}
}
