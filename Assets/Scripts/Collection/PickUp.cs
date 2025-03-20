using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PickUp : MonoBehaviour
{
	[Required][SerializeField] private Collectable _collectible;
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

	protected virtual void Collect(Creature collecter)
	{
		_animator.SetTrigger(CollectableAnimatorData.Params.Disappear);

		Destroy(gameObject, _delayDestroy);
	}
}
