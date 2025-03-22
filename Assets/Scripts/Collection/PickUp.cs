using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PickUp : MonoBehaviour, ICollectible
{
	[SerializeField] private float _delayDestroy = 1;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public virtual void OnCollected(GameObject collector)
	{
		_animator.SetTrigger(PickUpAnimatorData.Params.Disappear);

		Destroy(gameObject, _delayDestroy);
	}
}
