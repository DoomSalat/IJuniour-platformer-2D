using Sirenix.OdinInspector;
using UnityEngine;

public class Flipper : MonoBehaviour
{
	private const float ReactVelocity = 0.1f;

	[Required][SerializeField] private Rigidbody2D _targetRigidbody;

	private void Update()
	{
		float velocityX = _targetRigidbody.linearVelocityX;

		if (Mathf.Abs(velocityX) >= ReactVelocity)
		{
			bool shouldFaceRight = velocityX > 0;
			bool isCurrentlyFacingRight = transform.localScale.x > 0;

			if (shouldFaceRight != isCurrentlyFacingRight)
			{
				Flip(shouldFaceRight);
			}
		}
	}

	private void Flip(bool faceRight)
	{
		Vector3 newScale = transform.localScale;
		newScale.x = Mathf.Abs(newScale.x) * (faceRight ? 1 : -1);

		transform.localScale = newScale;
	}
}
