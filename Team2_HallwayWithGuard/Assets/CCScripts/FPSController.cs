using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FPSController : MonoBehaviour
{

    //movement speed
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    //input config
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    //look sensitivity
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


    private Camera mainCamera;
    private float verticalRotation;
    private CharacterController characterController; 

        private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
       
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speedMultiplier = Input.GetKey(sprintKey) ? sprintMultiplier : 1f;
        float verticalSpeed = Input.GetAxis(verticalMoveInput) * walkSpeed * speedMultiplier;
        float horizontalSpeed = Input.GetAxis(horizontalMoveInput) * walkSpeed * speedMultiplier;

        Vector3 speed = new Vector3 (horizontalSpeed, 0, verticalSpeed);
        speed = transform.rotation * speed;

        characterController.SimpleMove(speed); 
    }
     void HandleRotation()
    {
        float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);
        verticalRotation -= Input.GetAxis(MouseYInput) * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
