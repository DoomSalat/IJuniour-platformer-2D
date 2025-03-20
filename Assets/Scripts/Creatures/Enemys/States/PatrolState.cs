
namespace EnemyState
{
	public class PatrolState : IEnemyState
	{
		private const int RightDirection = 1;
		private const int LeftDirection = -1;

		private Enemy _enemy;
		private Mover _mover;
		private Vision _vision;
		private BoxCreatureAnimator _animator;

		public PatrolState(Enemy enemy, Mover mover, Vision vision, BoxCreatureAnimator animator)
		{
			_enemy = enemy;
			_mover = mover;
			_vision = vision;
			_animator = animator;
		}

		public void Enter()
		{
			_animator.SetRun(true);
		}

		public void Update() { }

		public void FixedUpdate()
		{
			Patrol();
		}

		public void Exit() { }

		private void Patrol()
		{
			_mover.Move(_vision.LookRight ? RightDirection : LeftDirection);

			if (_vision.HasObstacleInFront() || _vision.IsGroundBelow() == false)
			{
				_enemy.StartWaitTurn();
			}
		}
	}
}