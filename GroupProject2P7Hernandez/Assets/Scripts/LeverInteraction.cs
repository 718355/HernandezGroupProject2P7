using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    public GameObject pressEImage;
    public Transform gate;
    public float gateOpenHeight = 5f;
    public float openSpeed = 5f;

    private bool isPlayerNear = false;
    private bool gateOpened = false;
    private Vector3 initialGatePosition;



    private void Start()
    {
        if (pressEImage != null)
        {
            pressEImage.SetActive(false);
        }

        if (gate != null)
        {
            initialGatePosition = gate.position;
        }
    }

    
    private void Update()
    {
        if (isPlayerNear && !gateOpened && Input.GetKeyDown(KeyCode.E))
        {
            gateOpened = true;
            if (pressEImage != null)
                pressEImage.SetActive(false);
        }
        if(gateOpened && gate != null)
        {
            Vector3 targetPosition = initialGatePosition + new Vector3(0, gateOpenHeight, 0);
            gate.position = Vector3.MoveTowards(gate.position, targetPosition, openSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (pressEImage != null)
                pressEImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (pressEImage != null)
                pressEImage.SetActive(false);
        }
    }
}
