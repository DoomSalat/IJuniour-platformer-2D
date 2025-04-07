using UnityEngine;
using UnityEngine.UI;

namespace SimpleBar
{
	[RequireComponent(typeof(Slider))]
	public class BarSlider : Bar
	{
		private const float MaxViewNumber = 1;

		protected Slider SelfSlider;

		protected override void Awake()
		{
			base.Awake();

			SelfSlider = GetComponent<Slider>();
			SelfSlider.maxValue = MaxViewNumber;
		}

		protected override void Change(float value, float maxValue)
		{
			if (maxValue <= 0)
				return;

			SelfSlider.value = GetNormalizedFactor(value, maxValue);
		}
	}
}