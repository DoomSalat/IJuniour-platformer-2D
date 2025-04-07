using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EnemyState
{
	public class EnemyStateMachine : MonoBehaviour
	{
		[SerializeField][MinValue(0)] private float _delayCeaseChase = 2f;

		private Dictionary<Type, IEnemyState> _states;
		private IEnemyState _currentState;

		private WaitForSeconds _ceaseDelay;

		public IEnemyState CurrentState => _currentState;

		private void Awake()
		{
			_ceaseDelay = new WaitForSeconds(_delayCeaseChase);
		}

		public void InitState(Enemy enemy, Mover mover, Vision vision, BoxCreatureAnimator animator, float chaseSpeedMultiplier)
		{
			_states = new Dictionary<Type, IEnemyState>
			{
				[typeof(IdleState)] = new IdleState(mover, animator),
				[typeof(PatrolState)] = new PatrolState(enemy, mover, vision, animator),
				[typeof(ChaseState)] = new ChaseState(enemy, mover, vision, animator, chaseSpeedMultiplier)
			};
		}

		public void SetState(IEnemyState newState)
		{
			if (_currentState != newState)
			{
				_currentState?.Exit();
				_currentState = newState;
				_currentState.Enter();
			}
		}

		public IEnemyState GetState<T>() where T : IEnemyState
		{
			var type = typeof(T);
			return _states[type];
		}

		public void SetStateByDefault()
		{
			SetStatePatrol();
		}

		public void SetStateIdle()
		{
			SetState(GetState<IdleState>());
		}

		public void SetStatePatrol()
		{
			SetState(GetState<PatrolState>());
		}

		public void SetStateChase()
		{
			SetState(GetState<ChaseState>());
		}

		private IEnumerator CeaseChase()
		{
			yield return _ceaseDelay;

			SetStatePatrol();
		}
	}
}
