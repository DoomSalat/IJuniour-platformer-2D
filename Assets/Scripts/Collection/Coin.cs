using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Coin : MonoBehaviour
{
	[Required][SerializeField] private Collectable _collectible;
	[SerializeField] private string _animTriggerDisappear = "Disappear";
	[Space]
	[SerializeField] private float _delayDestroy = 1;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_collectible.Collected += Collect;
	}

	private void OnDisable()
	{
		_collectible.Collected -= Collect;
	}

	private void Collect()
	{
		_animator.SetTrigger(_animTriggerDisappear);

		Destroy(gameObject, _delayDestroy);
	}
}
