using Sirenix.OdinInspector;
using UnityEngine;

public class TimerBar : SliderBar
{
	[Required][SerializeField] private TimerAbility _timer;

	private void OnEnable()
	{
		_timer.Changed += Change;
	}

	private void OnDisable()
	{
		_timer.Changed -= Change;
	}

	protected override void Change(float value, float maxValue)
	{
		if (maxValue <= 0)
			return;

		Slider.value = GetNormalizedFactor(value, maxValue);
	}
}
