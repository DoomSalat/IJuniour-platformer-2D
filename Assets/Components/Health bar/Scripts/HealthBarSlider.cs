using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSlider : HealthBar
{
	private const float MaxViewNumber = 1;

	private Slider _slider;

	private void Awake()
	{
		_slider = GetComponent<Slider>();
		_slider.maxValue = MaxViewNumber;
	}

	protected override void Change(float value, float maxValue)
	{
		if (maxValue <= 0)
			return;

		_slider.value = GetNormalizedFactor(value, maxValue);
	}
}
