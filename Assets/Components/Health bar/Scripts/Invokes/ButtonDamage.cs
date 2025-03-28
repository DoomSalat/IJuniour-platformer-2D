using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ButtonDamage : MonoBehaviour
{
	[SerializeField] private Health _health;
	[SerializeField][Min(0)] private int _value = 7;
	[SerializeField] private bool _isHeal;

	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		if (_isHeal)
		{
			_button.onClick.AddListener(Heal);
		}
		else
		{
			_button.onClick.AddListener(Damage);
		}
	}

	private void OnDisable()
	{
		_button.onClick.RemoveAllListeners();
	}

	private void Damage()
	{
		_health.TakeDamage(_value);
	}

	private void Heal()
	{
		_health.Heal(_value);
	}
}
