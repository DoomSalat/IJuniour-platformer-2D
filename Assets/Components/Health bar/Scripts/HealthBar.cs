using UnityEngine;

public abstract class HealthBar : MonoBehaviour
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

	protected float GetNormalizedFactor(float value, float maxValue)
	{
		if (maxValue == 0f)
			return 0f;

		return value / maxValue;
	}

	protected abstract void Change(float value, float maxValue);
}
