using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerPlayer : MonoBehaviour
{

    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private InputAction dashAction;
    private InputAction parryAction;

    public Vector2 moveVal;
    public Vector2 lookVal;

    public bool attackVal;
    public bool dashVal;
    public bool parryVal;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        moveVal = moveAction.ReadValue<Vector2>();
        lookVal = lookAction.ReadValue<Vector2>();

        attackVal = attackAction.WasPressedThisFrame();
        dashVal = dashAction.WasPressedThisFrame();
        parryVal = parryAction.WasPressedThisFrame();
    }
}
