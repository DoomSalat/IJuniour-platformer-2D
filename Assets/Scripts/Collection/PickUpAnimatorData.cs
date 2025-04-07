using UnityEngine;

public class PickupAnimatorData : MonoBehaviour
{
	public static class Params
	{
		public static readonly int Disappear = Animator.StringToHash(nameof(Disappear));
	}
}
