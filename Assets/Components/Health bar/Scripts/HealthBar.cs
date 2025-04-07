using UnityEngine;

public abstract class HealthBar : SliderBar
{
	[SerializeField] private Health _health;

	private void OnEnable()
	{
		_health.Changed += Change;
	}

	private void OnDisable()
	{
		_health.Changed -= Change;
	}
}
