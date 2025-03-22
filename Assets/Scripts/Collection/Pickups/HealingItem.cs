using Sirenix.OdinInspector;
using UnityEngine;

public class HealingItem : PickUp
{
	[SerializeField][MinValue(0)] private int _healAmount = 20;

	public override void OnCollected(GameObject collector)
	{
		if (collector.TryGetComponent<IDamagable>(out var health))
		{
			health.Heal(_healAmount);
		}

		base.OnCollected(collector);
	}
}
