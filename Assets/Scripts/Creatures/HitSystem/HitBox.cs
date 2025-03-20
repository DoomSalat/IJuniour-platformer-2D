using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
	[SerializeField] private Creature _creature;
	private IDamagable _damagable;

	private void Awake()
	{
		_damagable = _creature;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<HurtBox>(out var hurtBox))
		{
			_damagable.TakeDamage(hurtBox.Damage);
		}
		else if (collision.TryGetComponent<HealBox>(out var healBox))
		{
			_damagable.Heal(healBox.Health);
		}
	}
}
