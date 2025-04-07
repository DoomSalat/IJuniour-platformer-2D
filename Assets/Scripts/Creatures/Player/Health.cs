using UnityEngine;

public class Health : MonoBehaviour, IDamagable, IHeallable, IBarChangeable
{
	[SerializeField] private int _maxValue = 100;

	private int _currentValue;

	public event System.Action<float, float> Changed;
	public event System.Action Died;

	public int CurrentValue
	{
		get => _currentValue;
		set => _currentValue = Mathf.Clamp(value, 0, _maxValue);
	}

	private void Awake()
	{
		_currentValue = _maxValue;
	}

	public void TakeDamage(int damage)
	{
		damage = Mathf.Max(0, damage);
		CurrentValue -= damage;

		if (CurrentValue == 0)
		{
			Died?.Invoke();
		}

		Changed?.Invoke(CurrentValue, _maxValue);
	}

	public void Heal(int health)
	{
		health = Mathf.Max(0, health);
		CurrentValue += health;

		TriggerChange(CurrentValue, _maxValue);
	}

	public void TriggerChange(float currentValue, float maxValue)
	{
		Changed?.Invoke(currentValue, maxValue);
	}
}
