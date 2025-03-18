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

		float verticalMove = CalculateVerticalMove(isUpBoard, isDownBoard);
		float horizontalMove = CalculateHorizontalMove(isLeftBoard, isRightBoard);
		_savedDirection = new Vector2(horizontalMove, verticalMove);

		return _savedDirection;
	}

	private float CalculateVerticalMove(bool isUpBoard, bool isDownBoard)
	{
		float verticalMove = 0f;

		if (isUpBoard && isDownBoard)
		{
			verticalMove = _savedDirection.y;

			if (!_reverseInputVertical)
			{
				_reverseInputVertical = true;
				verticalMove *= -1;
			}
		}
		else
		{
			_reverseInputVertical = false;

			if (isUpBoard)
			{
				verticalMove = 1f;
			}
			else if (isDownBoard)
			{
				verticalMove = -1f;
			}
		}

		return verticalMove;
	}

	private float CalculateHorizontalMove(bool isLeftBoard, bool isRightBoard)
	{
		float horizontalMove = 0f;

		if (isLeftBoard && isRightBoard)
		{
			horizontalMove = _savedDirection.x;

			if (_reverseInputHorizontal == false)
			{
				_reverseInputHorizontal = true;
				horizontalMove *= -1;
			}
		}
		else
		{
			_reverseInputHorizontal = false;

			if (isRightBoard)
			{
				horizontalMove = 1f;
			}
			else if (isLeftBoard)
			{
				horizontalMove = -1f;
			}
		}

		return horizontalMove;
	}
}