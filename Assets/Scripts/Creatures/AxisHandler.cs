using UnityEngine;

public class AxisHandler : InputHandler
{
	private bool _reverseInputHorizontal = false;
	private bool _reverseInputVertical = false;

	private Vector2 _savedDirection;

	public Vector2 GetAxisDirection()
	{
		Vector2 joystickDirection = MainControls.Player.Move.ReadValue<Vector2>();

		if (joystickDirection.magnitude > 0)
		{
			return joystickDirection;
		}

		bool isUpBoard = IsKeyPerformed(MainControls.Player.Up);
		bool isDownBoard = IsKeyPerformed(MainControls.Player.Down);
		bool isLeftBoard = IsKeyPerformed(MainControls.Player.Left);
		bool isRightBoard = IsKeyPerformed(MainControls.Player.Right);

		float verticalMove = CalculateMove(isUpBoard, isDownBoard, ref _reverseInputVertical, _savedDirection.y);
		float horizontalMove = CalculateMove(isRightBoard, isLeftBoard, ref _reverseInputHorizontal, _savedDirection.x);
		_savedDirection = new Vector2(horizontalMove, verticalMove);

		return _savedDirection;
	}

	private float CalculateMove(bool isPositive, bool isNegative, ref bool reverseInput, float savedDirection)
	{
		float move = 0f;

		if (isPositive && isNegative)
		{
			move = savedDirection;

			if (reverseInput == false)
			{
				reverseInput = true;
				move *= -1;
			}
		}
		else
		{
			reverseInput = false;

			if (isPositive)
			{
				move = 1f;
			}
			else if (isNegative)
			{
				move = -1f;
			}
		}

		return move;
	}
}