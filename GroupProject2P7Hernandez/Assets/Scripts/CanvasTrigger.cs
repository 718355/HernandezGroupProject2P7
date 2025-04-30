using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    public Canvas canvasToShow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            if (canvasToShow != null)
            {
                canvasToShow.gameObject.SetActive(true);
            }
        }
    }
}
