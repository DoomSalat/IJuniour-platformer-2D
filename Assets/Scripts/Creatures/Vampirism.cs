using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Vampirism : MonoBehaviour
{
	[Required][SerializeField] private Health _healTarget;
	[SerializeField][MinValue(0)] private int _stealHealth = 5;
	[SerializeField][MinValue(0)] private int _stealTime = 1;
	[Space]
	public UnityEvent _activeted;
	public UnityEvent _deactivated;

	private Collider2D _collider;
	private Coroutine _stealRoutine;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
	}

	private void Start()
	{
		Deactivate();
	}

	[ContextMenu(nameof(Activate))]
	public void Activate()
	{
		_collider.enabled = true;
		_activeted?.Invoke();
	}

	[ContextMenu(nameof(Deactivate))]
	public void Deactivate()
	{
		_collider.enabled = false;
		_deactivated?.Invoke();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (_stealRoutine != null)
			return;

		if (collision.TryGetComponent<HitBox>(out var collisionHealth) && collisionHealth.CompareTag(tag) == false)
		{
			_stealRoutine = StartCoroutine(StealHealth(collisionHealth));
		}
	}

	private IEnumerator StealHealth(HitBox enemyHealth)
	{
		enemyHealth.TakeDamage(_stealHealth);
		_healTarget.Heal(_stealHealth);

		yield return new WaitForSeconds(_stealTime);
		_stealRoutine = null;
	}
}
