using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealBox : MonoBehaviour
{
	[SerializeField][MinValue(0)] private int _health = 5;
	public int Health
	{
		get { return _health; }
		private set { _health = value; }
	}
}
