using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IBarChangeable
{
	[SerializeField] private int _maxHealth = 100;

	private int _currentHealth;

	public event System.Action<float, float> Changed;
	public event System.Action Died;

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

		if (CurrentHealth == 0)
		{
			Died?.Invoke();
		}

		Changed?.Invoke(CurrentHealth, _maxHealth);
	}

	public void Heal(int health)
	{
		health = Mathf.Max(0, health);
		CurrentHealth += health;

		TriggerChange(CurrentHealth, _maxHealth);
	}

	public void TriggerChange(float currentValue, float maxValue)
	{
		Changed?.Invoke(currentValue, maxValue);
	}
}
