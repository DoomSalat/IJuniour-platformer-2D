namespace EnemyState
{
	public class IdleState : IEnemyState
	{
		private Mover _mover;
		private BoxCreatureAnimator _animator;

		public IdleState(Mover mover, BoxCreatureAnimator animator)
		{
			_mover = mover;
			_animator = animator;
		}

		public void Enter()
		{
			_mover.StopMove();
			_animator.SetRun(false);
		}

		public void Update() { }

		public void FixedUpdate() { }

		public void Exit() { }
	}
}
