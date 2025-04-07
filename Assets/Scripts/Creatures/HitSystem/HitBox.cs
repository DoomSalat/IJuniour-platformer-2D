using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
	[SerializeField] private GameObject _damagableObject;

	private IDamagable _damagable;

	private void Awake()
	{
		_damagableObject.TryGetComponent(out _damagable);
	}

	public void TakeDamage(int damage)
	{
		_damagable.TakeDamage(damage);
	}

	public void Heal(int value)
	{
		_damagable.Heal(value);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_damagable == null)
			return;

		if (collision.TryGetComponent<HurtBox>(out var hurtBox) && hurtBox.CompareTag(tag) == false)
		{
			TakeDamage(hurtBox.Damage);
		}
	}

	private void OnValidate()
	{
		if (_damagableObject != null && _damagableObject.TryGetComponent<IDamagable>(out _) == false)
		{
			_damagableObject = null;
		}
	}
}
