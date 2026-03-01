using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{

    //movement speed
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    
    //input config
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField] private KeyCode controllerSprintKey = KeyCode.JoystickButton1;
    //look sensitivity
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";

    [SerializeField] private string controllerLookX = "RightStickX";
    [SerializeField] private string controllerLookY = "RightStickY";
    [SerializeField] private float controllerSensitivity = 100f;

    //Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] staminaSounds;
    //[SerializeField] private float sprintSoundCooldown = 5.9f;

    private bool wasSprinting = false;
    //private float lastSprintSoundTime = -999f;


    public bool isHidden = false;
    private Camera mainCamera;
    private float verticalRotation;
    private CharacterController characterController;

    
    public Image StaminaBar;
    public float Stamina, MaxStamina;
    public float RunCost;
    public float ChargeRate;
    private Coroutine recharge;


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
        bool isSprintPressed = Input.GetKey(sprintKey) || Input.GetKey(controllerSprintKey);
        bool isSprinting = Stamina > 0 && isSprintPressed;

        if (isSprinting && !wasSprinting)
        {
            PlayRandomStaminaSound();
        }

        wasSprinting = isSprinting;


    }

    void HandleMovement()
    {
        bool isSprinting = Input.GetKey(sprintKey) || Input.GetKey(controllerSprintKey);
        if (Stamina <= 0)
        {
            isSprinting = false;
        }
        float speedMultiplier = isSprinting ? sprintMultiplier : 1f;
        float verticalSpeed = Input.GetAxis(verticalMoveInput) * walkSpeed * speedMultiplier;
        float horizontalSpeed = Input.GetAxis(horizontalMoveInput) * walkSpeed * speedMultiplier;

       


            Vector3 speed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        speed = transform.rotation * speed;

        characterController.SimpleMove(speed); 

        if (isSprinting)
        {
            Stamina -= RunCost * Time.deltaTime;
            if(Stamina < 0) Stamina = 0;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            
            if(recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina()); 

        }

        

    }
     void HandleRotation()
    {
        float mouseX = Input.GetAxis(MouseXInput) * mouseSensitivity;
        float mouseY = Input.GetAxis(MouseYInput) * mouseSensitivity;
        float stickX = Input.GetAxis(controllerLookX) * controllerSensitivity * Time.deltaTime;
        float stickY = Input.GetAxis(controllerLookY) * controllerSensitivity * Time.deltaTime;
        float finalX = mouseX + stickX;
        float finalY = mouseY + stickY;

        transform.Rotate(0, finalX, 0);
        verticalRotation -= finalY;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


    }


    private IEnumerator RechargeStamina()
    {
     yield return new WaitForSeconds(1f);

        while(Stamina < MaxStamina)
        {
            Stamina += ChargeRate / 10f;
            if(Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(.1f);
        }

    
    }

    //bool CanPlaySprintSound()
   // {
       // if (Time.time < lastSprintSoundTime + sprintSoundCooldown)
        //    return false;

       // lastSprintSoundTime = Time.time;
      //  return true;
  //  }

    void PlayRandomStaminaSound()
    {

        if (staminaSounds.Length == 0) return;

        int randomIndex = Random.Range(0, staminaSounds.Length);

        AudioSource tempSource = gameObject.AddComponent<AudioSource>();
        tempSource.clip = staminaSounds[randomIndex];
        tempSource.pitch = Random.Range(0.85f, 1.15f);
        tempSource.spatialBlend = audioSource.spatialBlend;
        tempSource.volume = audioSource.volume;

        tempSource.Play();
        Destroy(tempSource, tempSource.clip.length);

    }

}
