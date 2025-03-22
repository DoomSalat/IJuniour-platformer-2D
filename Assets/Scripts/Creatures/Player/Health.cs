using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
	[SerializeField][MinValue(0)] private int _maxHealth = 100;

	[ShowInInspector] private int _currentHealth;

	public int CurrentHealth
	{
		get => _currentHealth;
		set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
	}

	private void Awake()
	{
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(int damage)
	{
		damage = Mathf.Max(0, damage);
		CurrentHealth -= damage;

		Debug.Log(gameObject.name + " Take damage: " + damage);
	}

	public void Heal(int health)
	{
		health = Mathf.Max(0, health);
		CurrentHealth += health;

		Debug.Log(gameObject.name + " Heal: " + health);
	}
}
