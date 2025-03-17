using UnityEngine;

public class Movement : InputHandler
{
	private bool _reverseInputHorizontal = false;
	private bool _reverseInputVertical = false;

	private Vector2 _savedDirection;

	public Vector2 AxisMove()
	{
		Vector2 joystickDirection = _mainControls.Player.Move.ReadValue<Vector2>();

		if (joystickDirection.magnitude > 0)
		{
			return joystickDirection;
		}

		bool upBoard = CheckKeyPerformed(_mainControls.Player.Up);
		bool downBoard = CheckKeyPerformed(_mainControls.Player.Down);
		bool leftBoard = CheckKeyPerformed(_mainControls.Player.Left);
		bool rightBoard = CheckKeyPerformed(_mainControls.Player.Right);

		float verticalMove = CalculateVerticalMove(upBoard, downBoard);
		float horizontalMove = CalculateHorizontalMove(leftBoard, rightBoard);
		_savedDirection = new Vector2(horizontalMove, verticalMove);

		return _savedDirection;
	}

	private float CalculateVerticalMove(bool upBoard, bool downBoard)
	{
		float verticalMove = 0f;

		if (upBoard && downBoard)
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

			if (upBoard)
			{
				verticalMove = 1f;
			}
			else if (downBoard)
			{
				verticalMove = -1f;
			}
		}

		return verticalMove;
	}

	private float CalculateHorizontalMove(bool leftBoard, bool rightBoard)
	{
		float horizontalMove = 0f;

		if (leftBoard && rightBoard)
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

			if (rightBoard)
			{
				horizontalMove = 1f;
			}
			else if (leftBoard)
			{
				horizontalMove = -1f;
			}
		}

		return horizontalMove;
	}
}