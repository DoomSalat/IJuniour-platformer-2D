using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimerAbility : MonoBehaviour, IBarChangeable
{
	private const float Max = 1f;

	[SerializeField][MinValue(0)] private float _timeToDecrease = 5f;
	[SerializeField][MinValue(0)] private float _timeToIncrease = 3f;

	public event System.Action TimerEnded;
	public event System.Action<float, float> Changed;

	private float _currentValue;
	private Coroutine _currentCoroutine;

	private float CurrentValue
	{
		get => _currentValue;
		set => _currentValue = Mathf.Clamp(value, 0f, Max);
	}

	private void Start()
	{
		CurrentValue = Max;
		Deactivate();
	}

	public void Activate()
	{
		if (_currentCoroutine != null)
			StopCoroutine(_currentCoroutine);

		_currentCoroutine = StartCoroutine(UpdateValue(true));
	}

	public void Deactivate()
	{
		if (_currentCoroutine != null)
			StopCoroutine(_currentCoroutine);

		_currentCoroutine = StartCoroutine(UpdateValue(false));
	}

	private IEnumerator UpdateValue(bool isDecreasing)
	{
		float elapsedTime = 0f;
		float startValue = _currentValue;
		float targetValue = isDecreasing ? 0f : Max;
		float duration = isDecreasing ? _timeToDecrease : _timeToIncrease;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			CurrentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
			Changed?.Invoke(CurrentValue, Max);

			yield return null;
		}

		CurrentValue = targetValue;
		TriggerChange(CurrentValue, Max);

		if (isDecreasing && CurrentValue <= 0f)
		{
			TimerEnded?.Invoke();
		}
	}

	public void TriggerChange(float currentValue, float maxValue)
	{
		Changed?.Invoke(currentValue, maxValue);
	}
}