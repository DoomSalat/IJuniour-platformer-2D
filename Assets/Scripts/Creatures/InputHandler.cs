using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : Creature
{
	protected MainControls _mainControls;

	protected override void Awake()
	{
		base.Awake();
		_mainControls = new MainControls();
	}

	protected virtual void OnEnable()
	{
		EnableInput();
	}

	protected virtual void OnDisable()
	{
		DisableInput();
	}

	public void EnableInput()
	{
		_mainControls.Player.Enable();
	}

	public void DisableInput()
	{
		_mainControls.Player.Disable();
	}

	public bool CheckKeyPerformed(InputAction action)
	{
		return action.phase == InputActionPhase.Performed;
	}
}