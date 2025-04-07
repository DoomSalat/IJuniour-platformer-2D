using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSlider : HealthBar
{
	private const float MaxViewNumber = 1;

	private void Awake()
	{
		Slider = GetComponent<Slider>();
		Slider.maxValue = MaxViewNumber;
	}

	protected override void Change(float value, float maxValue)
	{
		if (maxValue <= 0)
			return;

		Slider.value = GetNormalizedFactor(value, maxValue);
	}
}
