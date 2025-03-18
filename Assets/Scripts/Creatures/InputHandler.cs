using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	public MainControls MainControls { get; private set; }

	private void Awake()
	{
		MainControls = new MainControls();
	}

	private void OnEnable()
	{
		MainControls.Player.Enable();
	}

	private void OnDisable()
	{
		MainControls.Player.Disable();
	}

	public bool IsKeyPerformed(InputAction action)
	{
		return action.phase == InputActionPhase.Performed;
	}
}