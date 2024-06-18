using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSimulator : MonoBehaviour
{
    public float rotationSpeed = 30f; 
    public float lerpFactor = 2.0f; 

    private InputAction rotateHorizontalAction;
    private InputAction rotateVerticalAction;

    private float horizontalInput;
    private float verticalInput;

    private float targetRotationX;
    private float targetRotationY;

    private void Start()
    {
        targetRotationX = transform.eulerAngles.x;
        targetRotationY = transform.eulerAngles.y;
    }

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
        Debug.Log("horizontal "+ horizontalInput);
    }

    private void OnVerticalRotate(InputAction.CallbackContext context)
    {
        // Read the input value as a float
        verticalInput = context.ReadValue<float>();
        Debug.Log("vertical " + verticalInput);
    }

    void Update()
    {
        // Set rotation
        targetRotationX += -verticalInput * rotationSpeed * Time.deltaTime;
        targetRotationY += horizontalInput * rotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, targetRotationX, lerpFactor * Time.deltaTime),
            Mathf.LerpAngle(transform.eulerAngles.y, targetRotationY, lerpFactor * Time.deltaTime),
            transform.eulerAngles.z
        );
    }
}