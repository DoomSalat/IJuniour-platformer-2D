using UnityEngine;

public class BoxCreatureAnimatorData : MonoBehaviour
{
	public static class Params
	{
		public static readonly int IsRunnig = Animator.StringToHash(nameof(IsRunnig));
		public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
		public static readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
		public static readonly int Jump = Animator.StringToHash(nameof(Jump));
		public static readonly int Dead = Animator.StringToHash(nameof(Dead));
	}
}
