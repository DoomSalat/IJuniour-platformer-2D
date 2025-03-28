using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class HealthBarSmoothSlider : HealthBar
{
	private const float MaxViewNumber = 1;
	private const float EndProgress = 1;

	[SerializeField][Min(0)] private float _smoothSpeed = 5f;

	private Slider _slider;
	private Coroutine _smoothChangeCoroutine;
	private float _realValue = MaxViewNumber;

	private float RealValue
	{
		get { return _realValue; }
		set { _realValue = Mathf.Clamp01(value); }
	}

	private void Awake()
	{
		_slider = GetComponent<Slider>();
		_slider.maxValue = MaxViewNumber;
	}

	protected override void Change(float value, float maxValue)
	{
		if (maxValue <= 0)
			return;

		RealValue = GetNormalizedFactor(value, maxValue);

		if (_smoothChangeCoroutine != null)
		{
			StopCoroutine(_smoothChangeCoroutine);
		}

		_smoothChangeCoroutine = StartCoroutine(SmoothUpdate(RealValue));
	}

	private IEnumerator SmoothUpdate(float targetValue)
	{
		float startValue = _slider.value;
		float progress = 0f;

		while (progress < EndProgress)
		{
			progress += Time.deltaTime * _smoothSpeed;
			_slider.value = Mathf.Lerp(startValue, targetValue, progress);

			yield return null;
		}

		_slider.value = targetValue;
		_smoothChangeCoroutine = null;
	}
}
