using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimerAbility : MonoBehaviour
{
	private const float Second = 1;

	[SerializeField][MinValue(0)] private int _maxValue = 10;
	[SerializeField][MinValue(0)] private int _loseActive = 2;
	[SerializeField][MinValue(0)] private int _getDeactive = 1;

	public event System.Action TimerEnded;
	public event System.Action<float, float> Changed;

	private int _currentValue;

	public int CurrentValue
	{
		get => _currentValue;
		set => _currentValue = Mathf.Clamp(value, 0, _maxValue);
	}

	private void Start()
	{
		CurrentValue = _maxValue;
		Deactivate();
	}

	public void Activate()
	{
		StopAllCoroutines();
		StartCoroutine(LoseValue());
	}

	public void Deactivate()
	{
		StopAllCoroutines();
		StartCoroutine(MakeUpValue());
	}

	private IEnumerator MakeUpValue()
	{
		var waitSecond = new WaitForSeconds(Second);

		bool isWork = true;

		while (isWork)
		{
			CurrentValue += _getDeactive;
			Changed?.Invoke(CurrentValue, _maxValue);

			if (_currentValue == _maxValue)
			{
				break;
			}

			yield return waitSecond;
		}
	}

	private IEnumerator LoseValue()
	{
		var waitSecond = new WaitForSeconds(Second);

		bool isWork = true;

		while (isWork)
		{
			CurrentValue -= _loseActive;
			Changed?.Invoke(CurrentValue, _maxValue);

			if (_currentValue == 0)
			{
				TimerEnded?.Invoke();
				break;
			}

			yield return waitSecond;
		}
	}
}
