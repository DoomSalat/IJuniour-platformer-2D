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

		private float _chaseSpeedMultiplier;

		public ChaseState(Enemy enemy, Mover mover, Vision vision, BoxCreatureAnimator animator, float chaseMultiplier)
		{
			_enemy = enemy;
			_mover = mover;
			_vision = vision;
			_animator = animator;

			_chaseSpeedMultiplier = chaseMultiplier;
		}

		public void Enter()
		{
			_enemy.StopAllCoroutines();
			_animator.SetRun(true);
		}

		public void Update() { }

		public void FixedUpdate()
		{
			_mover.Move(_vision.LookRight ? RightDirection : LeftDirection, _chaseSpeedMultiplier);

			_enemy.LookTarget();

			if (_vision.HasObstacleInFront())
			{
				_vision.ReverseLook();
			}
		}

		public void Exit()
		{
			_enemy.DeactiveLookTarget();
		}
	}
}
