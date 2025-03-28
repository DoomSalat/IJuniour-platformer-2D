using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoxCreatureAnimator : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void SetRun(bool run)
	{
		_animator.SetBool(BoxCreatureAnimatorData.Params.IsRunnig, run);
	}

	public void SetFall(bool fall)
	{
		_animator.SetBool(BoxCreatureAnimatorData.Params.IsFalling, fall);
	}

	public void SetGround(bool ground)
	{
		_animator.SetBool(BoxCreatureAnimatorData.Params.IsGrounded, ground);
	}

	public void PlayJump()
	{
		_animator.SetTrigger(BoxCreatureAnimatorData.Params.Jump);
		SetGround(false);
	}

	public void PlayDead()
	{
		SetGround(true);
		SetFall(false);
		SetRun(false);
		_animator.Play(BoxCreatureAnimatorData.Params.Dead);
	}
}
