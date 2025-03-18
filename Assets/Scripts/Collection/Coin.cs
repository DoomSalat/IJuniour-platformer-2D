using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
	[Required][SerializeField] private Collectible _coin;
	[SerializeField] private string _animTriggerDisappear = "Disappear";

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_coin.Collected += Collect;
	}

	private void OnDisable()
	{
		_coin.Collected -= Collect;
	}

	private void Collect()
	{
		_animator.SetTrigger(_animTriggerDisappear);
	}
}
