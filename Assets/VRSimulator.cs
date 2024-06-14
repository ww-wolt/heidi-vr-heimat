using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSimulator : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation

    private InputAction rotateHorizontalAction;
    private InputAction rotateVerticalAction;

    private float horizontalInput;
    private float verticalInput;

    private void OnEnable()
    {
        // Create the horizontal input action
        rotateHorizontalAction = new InputAction(type: InputActionType.Value, binding: "<Keyboard>/leftArrow, <Keyboard>/rightArrow");
        rotateHorizontalAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/leftArrow")
            .With("Positive", "<Keyboard>/rightArrow");

        rotateHorizontalAction.Enable();

        // Create the vertical input action
        rotateVerticalAction = new InputAction(type: InputActionType.Value, binding: "<Keyboard>/upArrow, <Keyboard>/downArrow");
        rotateVerticalAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/downArrow")
            .With("Positive", "<Keyboard>/upArrow");

        rotateVerticalAction.Enable();

        // Assign the callbacks
        rotateHorizontalAction.performed += OnHorizontalRotate;
        rotateHorizontalAction.canceled += OnHorizontalRotate;

        rotateVerticalAction.performed += OnVerticalRotate;
        rotateVerticalAction.canceled += OnVerticalRotate;
    }

    private void OnDisable()
    {
        // Disable the input actions
        rotateHorizontalAction.Disable();
        rotateVerticalAction.Disable();
    }

    private void OnHorizontalRotate(InputAction.CallbackContext context)
    {
        // Read the input value as a float
        horizontalInput = context.ReadValue<float>();
    }

    private void OnVerticalRotate(InputAction.CallbackContext context)
    {
        // Read the input value as a float
        verticalInput = context.ReadValue<float>();
    }

    void Update()
    {
        // Set rotation

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x + -verticalInput * rotationSpeed * Time.deltaTime,
            transform.eulerAngles.y + horizontalInput * rotationSpeed * Time.deltaTime,
            transform.eulerAngles.z
        );
    }
}