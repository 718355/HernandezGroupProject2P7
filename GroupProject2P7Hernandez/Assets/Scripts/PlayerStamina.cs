using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public Slider staminaBar;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 25f;
    public float staminaRegenRate = 15f;
    public float regenDelay = 2f;

    public float sprintSpeed = 8f;
    public float walkSpeed = 4f;


    private float lastSprintTime;
    private bool isSprinting = false;
    private CharacterController controller;

    private float currentSpeed;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
        staminaBar.gameObject.SetActive(false);
    }



    void Update()
    {
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && controller.velocity.magnitude > 0.1f;

        if (wantsToSprint && currentStamina > 0f)
        {
            isSprinting = true;
            currentSpeed = sprintSpeed;

            staminaBar.gameObject.SetActive(true);
            currentStamina -= staminaDrainRate * Time.deltaTime;
            lastSprintTime = Time.time;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);


            if(currentStamina <= 0f)
            {
                isSprinting = false;
                currentSpeed = walkSpeed;
            }
        }
        else
        {
            isSprinting = false;
            currentSpeed = walkSpeed;

            if (currentStamina >= maxStamina)
                staminaBar.gameObject.SetActive(false);

            if(Time.time - lastSprintTime > regenDelay)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }


        staminaBar.value = currentStamina;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(input) * currentSpeed;
        controller.Move(move * Time.deltaTime);

    }
}
