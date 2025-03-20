using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Creature : MonoBehaviour, IDamagable
{
	protected const float VelocityZeroOffset = 0.1f;

	[SerializeField][Min(1)] private int _maxHealth = 100;

	protected Rigidbody2D _rigidbody;

	[ShowInInspector] private int _health;

	protected int Health
	{
		get
		{
			return _health;
		}
		set
		{
			_health = Mathf.Clamp(value, 0, _maxHealth);
		}
	}

	protected virtual void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();

		Health = _maxHealth;
	}

	public virtual void TakeDamage(int damage)
	{
		Health -= damage;
		Debug.Log(gameObject.name + " TakeDamage: " + damage);
	}

	public void Heal(int health)
	{
		Health += health;
		Debug.Log(gameObject.name + " Heal: " + health);
	}
}
