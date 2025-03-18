using Sirenix.OdinInspector;
using UnityEngine;

public class BoxCreatureAnimator : MonoBehaviour
{
	[Required][SerializeField] private Animator _animator;
	[Space]
	[SerializeField] private string _animBoolRun = "Run";
	[SerializeField] private string _animTriggerJump = "Jump";
	[SerializeField] private string _animBoolGround = "Ground";
	[SerializeField] private string _animBoolFall = "Fall";

	public void SetRun(bool run)
	{
		_animator.SetBool(_animBoolRun, run);
	}

	public void SetFall(bool fall)
	{
		_animator.SetBool(_animBoolFall, fall);
	}

	public void SetGround(bool ground)
	{
		_animator.SetBool(_animBoolGround, ground);
	}

	public void Jump()
	{
		_animator.SetTrigger(_animTriggerJump);
		SetGround(false);
	}
}
