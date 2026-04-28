using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public InputActionAsset InputActions;

    private InputAction i_Navigate;
    private InputAction i_Submit;

    private Vector2 navigate;
    private bool submit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        InputActions.FindActionMap("UI").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("UI").Disable();
    }

    private void Awake()
    {
        i_Navigate = InputSystem.actions.FindAction("Navigate");
        i_Submit = InputSystem.actions.FindAction("Submit");
    }

    private void Update()
    {
        navigate = i_Navigate.ReadValue<Vector2>();
        
        if (i_Submit.WasPressedThisFrame())
        {
            Submit();
        }

    }

    private void Submit()
    {

    }
}
