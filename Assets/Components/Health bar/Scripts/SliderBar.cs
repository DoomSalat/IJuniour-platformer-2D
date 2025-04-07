using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderBar : MonoBehaviour
{
	private const float MaxViewNumber = 1;

	protected Slider Slider;

	private void Awake()
	{
		Slider = GetComponent<Slider>();
		Slider.maxValue = MaxViewNumber;
	}

	protected float GetNormalizedFactor(float value, float maxValue)
	{
		if (maxValue == 0f)
			return 0f;

		return value / maxValue;
	}

	protected abstract void Change(float value, float maxValue);
}
