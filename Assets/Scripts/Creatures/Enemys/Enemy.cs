using System;
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using EnemyState;

public class Enemy : Creature
{
	[Required][SerializeField] private Mover _mover;
	[Required][SerializeField] private Vision _vision;
	[Required][SerializeField] private BoxCreatureAnimator _animator;

	[Header("Parameters")]
	[SerializeField][MinValue(0)] private float _waitIdlePatrol = 2;
	[Space]
	[SerializeField][MinValue(0)] private float _chaseSpeedMultiplier = 2;
	[SerializeField][MinValue(0)] private float _delayCeaseChase = 2f;

	private WaitForSeconds _idlePatrolDelay;
	private WaitForSeconds _ceaseDelay;

	private Dictionary<Type, IEnemyState> _states;
	private IEnemyState _currentState;

	protected override void Awake()
	{
		base.Awake();
		InitState();

		_idlePatrolDelay = new WaitForSeconds(_waitIdlePatrol);
		_ceaseDelay = new WaitForSeconds(_delayCeaseChase);
	}

	private void Start()
	{
		SetStateByDefault();
	}

	private void Update()
	{
		_currentState?.Update();
	}

	private void FixedUpdate()
	{
		_currentState?.FixedUpdate();

		if (_currentState != GetState<ChaseState>() && _vision.IsTargetBelow())
		{
			SetStateChase();
		}

		FallAnimation();
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

	public void StartWaitTurn()
	{
		StartCoroutine(WaitIdleTurn());
	}

	private void FallAnimation()
	{
		if (Rigidbody.linearVelocityY < -VelocityZeroOffset)
		{
			_animator.SetFall(true);
			_animator.SetGround(false);
		}
		else
		{
			_animator.SetFall(false);

			if (_vision.IsGround())
			{
				_animator.SetGround(true);
			}
		}
	}

	private IEnumerator WaitIdleTurn()
	{
		SetStateIdle();

		yield return _idlePatrolDelay;
		yield return new WaitUntil(() => _vision.IsGround());

		_vision.ReverseLook();
		SetStatePatrol();
	}

	private IEnumerator CeaseChase()
	{
		yield return _ceaseDelay;

		SetStatePatrol();
	}

	private void InitState()
	{
		_states = new Dictionary<Type, IEnemyState>
		{
			[typeof(IdleState)] = new IdleState(_mover, _animator),
			[typeof(PatrolState)] = new PatrolState(this, _mover, _vision, _animator),
			[typeof(ChaseState)] = new ChaseState(this, _mover, _vision, _animator, CeaseChase(), _chaseSpeedMultiplier)
		};
	}

	private void SetState(IEnemyState newState)
	{
		if (_currentState != newState)
		{
			_currentState?.Exit();
			_currentState = newState;
			_currentState.Enter();
		}
	}
}
