using System.Collections;
using UnityEngine;

namespace EnemyState
{
	public class ChaseState : IEnemyState
	{
		private const int RightDirection = 1;
		private const int LeftDirection = -1;

		private Enemy _enemy;
		private Mover _mover;
		private Vision _vision;
		private BoxCreatureAnimator _animator;

		private Coroutine _ceaseRoutine;
		private float _delayCease = 2f;

		private float _chaseMultiplier = 2f;

		public ChaseState(Enemy enemy, Mover mover, Vision vision, BoxCreatureAnimator animator)
		{
			_enemy = enemy;
			_mover = mover;
			_vision = vision;
			_animator = animator;
		}

		public void Enter()
		{
			_enemy.StopAllCoroutines();
			_animator.SetRun(true);
		}

		public void Update() { }

		public void FixedUpdate()
		{
			_mover.Move(_vision.LookRight ? RightDirection : LeftDirection, _chaseMultiplier);

			LookTarget();

			if (_vision.HasObstacleInFront())
			{
				_vision.ReverseLook();
			}
		}

		public void Exit()
		{
			if (_ceaseRoutine != null)
			{
				_enemy.StopCoroutine(_ceaseRoutine);
				_ceaseRoutine = null;
			}
		}

		private void LookTarget()
		{
			if (_vision.IsTargetBelow() == false && _ceaseRoutine == null)
			{
				_ceaseRoutine = _enemy.StartCoroutine(СeaseChase());
			}
			else if (_vision.IsTargetBelow() && _ceaseRoutine != null)
			{
				_enemy.StopCoroutine(_ceaseRoutine);
				_ceaseRoutine = null;
			}
		}

		private IEnumerator СeaseChase()
		{
			yield return new WaitForSeconds(_delayCease);

			_enemy.SetStatePatrol();
		}
	}
}
