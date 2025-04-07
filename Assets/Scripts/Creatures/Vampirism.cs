using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Vampirism : MonoBehaviour
{
	private const int MaxHitsVampire = 10;

	[Required][SerializeField] private Health _healTarget;
	[SerializeField][MinValue(0)] private int _stealHealth = 5;
	[SerializeField][MinValue(0)] private float _stealTime = 1f;
	[SerializeField][MinValue(0)] private float _radius = 2f;
	[SerializeField] private LayerMask _enemyLayerMask;
	[Space]
	public UnityEvent _activeted;
	public UnityEvent _deactivated;

	private IHeallable _heallableTarget;

	private readonly Collider2D[] _hits = new Collider2D[MaxHitsVampire];
	private Coroutine _stealRoutine;
	private bool _isActive = false;

	private void Awake()
	{
		_healTarget.TryGetComponent(out _heallableTarget);
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

		if (_healTarget != null && _healTarget.TryGetComponent<IHeallable>(out _) == false)
		{
			_healTarget = null;
		}
	}

	[System.Obsolete]
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _radius);
	}

	[ContextMenu(nameof(Activate))]
	public void Activate()
	{
		_isActive = true;

		_activeted?.Invoke();
	}

	[ContextMenu(nameof(Deactivate))]
	public void Deactivate()
	{
		_isActive = false;

		if (_stealRoutine != null)
		{
			StopCoroutine(_stealRoutine);
			_stealRoutine = null;
		}

		_deactivated?.Invoke();
	}

	[System.Obsolete]
	private HitBox FindNearestEnemy()
	{
		int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _hits, _enemyLayerMask);
		HitBox nearestEnemy = null;
		float minDistance = float.MaxValue;

		for (int i = 0; i < hitCount; i++)
		{
			if (_hits[i].TryGetComponent<HitBox>(out var hitBox))
			{
				Vector2 direction = (Vector2)_hits[i].transform.position - (Vector2)transform.position;
				float sqrDistance = direction.sqrMagnitude;

				if (sqrDistance < minDistance * minDistance)
				{
					minDistance = Mathf.Sqrt(sqrDistance);
					nearestEnemy = hitBox;
				}
			}
		}

		return nearestEnemy;
	}

	private IEnumerator StealHealth(HitBox enemyHealth)
	{
		enemyHealth.TakeDamage(_stealHealth);
		_heallableTarget.Heal(_stealHealth);

		yield return new WaitForSeconds(_stealTime);

		_stealRoutine = null;
	}
}