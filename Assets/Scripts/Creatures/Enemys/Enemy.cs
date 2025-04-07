using System;
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using EnemyState;

public class Enemy : Creature
{
	[Required][SerializeField] private EnemyStateMachine _stateMachine;
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

	private Coroutine _ceaseRoutine;

	protected override void Awake()
	{
		base.Awake();
		_stateMachine.InitState(this, _mover, _vision, _animator, _chaseSpeedMultiplier);

		_idlePatrolDelay = new WaitForSeconds(_waitIdlePatrol);
		_ceaseDelay = new WaitForSeconds(_delayCeaseChase);
	}

	private void Start()
	{
		_stateMachine.SetStateByDefault();
	}

	private void Update()
	{
		_stateMachine.CurrentState?.Update();
	}

	private void FixedUpdate()
	{
		_stateMachine.CurrentState?.FixedUpdate();

		if (_stateMachine.CurrentState != _stateMachine.GetState<ChaseState>() && _vision.IsTargetBelow())
		{
			_stateMachine.SetStateChase();
		}

		FallAnimation();
	}

	public void StartWaitTurn()
	{
		StartCoroutine(WaitIdleTurn());
	}

	public void LookTarget()
	{
		if (_vision.IsTargetBelow() == false && _ceaseRoutine == null)
		{
			_ceaseRoutine = StartCoroutine(CeaseChase());
		}
		else if (_vision.IsTargetBelow() && _ceaseRoutine != null)
		{
			StopCoroutine(_ceaseRoutine);
			_ceaseRoutine = null;
		}
	}

	public void DeactiveLookTarget()
	{
		if (_ceaseRoutine != null)
		{
			StopCoroutine(_ceaseRoutine);
			_ceaseRoutine = null;
		}
	}

	private void FallAnimation()
	{
		if (SelfRigidbody.linearVelocityY < -VelocityZeroOffset)
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
		_stateMachine.SetStateIdle();

		yield return _idlePatrolDelay;
		yield return new WaitUntil(() => _vision.IsGround());

		_vision.ReverseLook();
		_stateMachine.SetStatePatrol();
	}

	private IEnumerator CeaseChase()
	{
		yield return _ceaseDelay;

		_stateMachine.SetStatePatrol();
	}
}
