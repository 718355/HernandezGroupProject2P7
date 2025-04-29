using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    public float moveSpeed;

    // Camera and Movement Setting 
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    // Head bobbing Settings
    public float bobSpeed = 14f;
    public float bobAmount = 0.05f;
    private float defaultYPos = 0;
    private float bobTimer = 0;

    // Footstep Audio Setting
    public AudioClip[] footstepsSounds;
    public float stepInterval = 0.5f;
    public float runStepInterval = 0.3f;
    private float stepTimer = 0f;
    private AudioSource audioSource;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;


    void Start()
    {
        characterController = GetComponent<CharacterController>();


        defaultYPos = playerCamera.transform.localPosition.y;

        audioSource = gameObject.AddComponent<AudioSource>();
    }



    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            #region Handles Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            moveSpeed = isRunning ? runSpeed : walkSpeed;
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            #endregion

            #region Handles Jumping
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            #endregion

            #region Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            #endregion

            HandleHeadBob();

            isRunning = Input.GetKey(KeyCode.LeftShift);
            HandleFootsteps(isRunning);
        }
        
    }
    void HandleHeadBob()
    {
        Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (!characterController.isGrounded || horizontalVelocity.magnitude < 0.1f)
        {
            bobTimer = 0;
            Vector3 resetPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                Mathf.Lerp(playerCamera.transform.localPosition.y, defaultYPos, Time.deltaTime * bobSpeed),
                playerCamera.transform.localPosition.z);
            playerCamera.transform.localPosition = resetPosition;
            return;
        }
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentBobSpeed = isRunning ? bobSpeed * 1.5f : bobSpeed;

        bobTimer += Time.deltaTime * currentBobSpeed;
        float bobOffset = Mathf.Sin(bobTimer) * bobAmount;

        Vector3 newPosition = new Vector3(
            playerCamera.transform.localPosition.x,
            defaultYPos + bobOffset,
            playerCamera.transform.localPosition.z);

        playerCamera.transform.localPosition = newPosition;
        
    }

    void HandleFootsteps(bool isRunning)
    {
        if (!characterController.isGrounded) return;

        Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);

        if(horizontalVelocity.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            float currentStepInterval = isRunning ? runStepInterval : stepInterval;

            if(stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = currentStepInterval;
            }
        }

        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepsSounds.Length == 0) return;

        int index = Random.Range(0, footstepsSounds.Length);
        audioSource.PlayOneShot(footstepsSounds[index]);
    }
}
