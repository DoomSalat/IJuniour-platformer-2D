using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pickup : MonoBehaviour
{
	[SerializeField] private float _delayDestroy = 1;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public virtual void OnCollected(GameObject collector)
	{
		_animator.SetTrigger(PickupAnimatorData.Params.Disappear);

		Destroy(gameObject, _delayDestroy);
	}
}
