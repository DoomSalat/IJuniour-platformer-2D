using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
	[SerializeField][MinValue(0)] private int _damage = 5;
	public int Damage
	{
		get { return _damage; }
		private set { _damage = value; }
	}
}
