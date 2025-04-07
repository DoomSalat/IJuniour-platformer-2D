using UnityEngine;

public abstract class Bar : MonoBehaviour
{
	[SerializeField] private GameObject _changable;
	[SerializeField] protected IBarChangeable BarChangeable;

	protected virtual void Awake()
	{
		BarChangeable = _changable.GetComponent<IBarChangeable>();
	}

	private void OnEnable()
	{
		BarChangeable.Changed += Change;
	}

	private void OnDisable()
	{
		BarChangeable.Changed -= Change;
	}

	private void OnValidate()
	{
		if (_changable != null)
		{
			if (_changable.TryGetComponent<IBarChangeable>(out var barChangeable))
			{
				BarChangeable = barChangeable;
			}
			else
			{
				_changable = null;
			}
		}
	}

	protected float GetNormalizedFactor(float value, float maxValue)
	{
		if (maxValue == 0f)
			return 0f;

		return value / maxValue;
	}

	protected abstract void Change(float value, float maxValue);
}
