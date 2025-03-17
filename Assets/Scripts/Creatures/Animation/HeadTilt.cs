using Sirenix.OdinInspector;
using UnityEngine;

public class HeadTilt : MonoBehaviour
{
	[Required][SerializeField] private Rigidbody2D _targetRigidbody;
	[Space]
	[SerializeField][Min(0)] private float _maxAngle = 25;
	[SerializeField][Min(0)] private float _maxSpeed = 2;
	[SerializeField][Min(0)] private float _smooth = 5;
	[Header("Controll")]
	[SerializeField] private bool _active = true;

	private void Update()
	{
		float currentSpeed = Mathf.Abs(_targetRigidbody.linearVelocityX);

		if (_active == false)
			currentSpeed = 0;

		float speedNormalized = Mathf.Clamp01(currentSpeed / _maxSpeed);
		float targetAngle = Mathf.Lerp(0, _maxAngle, speedNormalized);

		Quaternion targetRotation = Quaternion.Euler(0, 0, -targetAngle);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _smooth);
	}
}
