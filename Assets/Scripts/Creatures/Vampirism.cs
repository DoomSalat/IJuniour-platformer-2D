using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Vampirism : MonoBehaviour
{
	[Required][SerializeField] private Health _healTarget;
	[SerializeField][MinValue(0)] private int _stealHealth = 5;
	[SerializeField][MinValue(0)] private float _stealTime = 1f;
	[SerializeField][MinValue(0)] private float _radius = 2f;
	[Space]
	public UnityEvent _activeted;
	public UnityEvent _deactivated;

	private Collider2D _collider;
	private Coroutine _stealRoutine;
	private bool _isActive = false;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
	}

	private void Start()
	{
		Deactivate();
	}

	private void OnValidate()
	{
		if (TryGetComponent<CircleCollider2D>(out var circleCollider))
		{
			_radius = circleCollider.radius;
		}
	}

	[ContextMenu(nameof(Activate))]
	public void Activate()
	{
		_collider.enabled = true;
		_isActive = true;

		_activeted?.Invoke();
	}

	[ContextMenu(nameof(Deactivate))]
	public void Deactivate()
	{
		_collider.enabled = false;
		_isActive = false;

		if (_stealRoutine != null)
		{
			StopCoroutine(_stealRoutine);
			_stealRoutine = null;
		}

		_deactivated?.Invoke();
	}

	private void FixedUpdate()
	{
		if (_isActive == false || _stealRoutine != null)
			return;

		HitBox nearestEnemy = FindNearestEnemy();

		if (nearestEnemy != null)
		{
			_stealRoutine = StartCoroutine(StealHealth(nearestEnemy));
		}
	}

	private HitBox FindNearestEnemy()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);
		HitBox nearestEnemy = null;
		float minDistance = float.MaxValue;

		foreach (Collider2D hit in hits)
		{
			if (hit.TryGetComponent<HitBox>(out var hitBox) && hit.CompareTag(tag) == false)
			{
				float distance = Vector2.Distance(transform.position, hit.transform.position);

				if (distance < minDistance)
				{
					minDistance = distance;
					nearestEnemy = hitBox;
				}
			}
		}

		return nearestEnemy;
	}

	private IEnumerator StealHealth(HitBox enemyHealth)
	{
		enemyHealth.TakeDamage(_stealHealth);
		_healTarget.Heal(_stealHealth);

		yield return new WaitForSeconds(_stealTime);

		_stealRoutine = null;
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _radius);
	}
#endif
}