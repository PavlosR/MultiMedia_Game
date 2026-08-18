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
    private InputAction jumpAction;

    public Vector2 moveVal;
    public Vector2 lookVal;

    public bool attackVal;
    public bool dashVal;
    public bool parryVal;
    public bool jumpVal;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
        moveAction = inputActions.FindAction("Move");
        lookAction = inputActions.FindAction("Look");
        attackAction = inputActions.FindAction("Attack");
        dashAction = inputActions.FindAction("Dash");
        parryAction = inputActions.FindAction("Parry");
        jumpAction = inputActions.FindAction("Jump");
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
        moveAction = null;
        lookAction = null;
        attackAction = null;
        dashAction = null;
        parryAction = null;
        jumpAction = null;
    }

    private void Update()
    {
        moveVal = moveAction.ReadValue<Vector2>();
        lookVal = lookAction.ReadValue<Vector2>();

        attackVal = attackAction.WasPressedThisFrame();
        dashVal = dashAction.IsPressed();
        parryVal = parryAction.WasPressedThisFrame();
        jumpVal = jumpAction.IsPressed();

    }
}
