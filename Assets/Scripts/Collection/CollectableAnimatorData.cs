using UnityEngine;

public class CollectableAnimatorData : MonoBehaviour
{
	public static class Params
	{
		public static readonly int Disappear = Animator.StringToHash(nameof(Disappear));
	}
}
