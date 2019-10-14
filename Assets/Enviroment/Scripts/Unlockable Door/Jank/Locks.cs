using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locks : MonoBehaviour
{

    [SerializeField] GameObject helpText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            helpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            helpText.SetActive(false);
        }
    }

    private void Update()
    {
        Debug.Log("workkkkking");
        if (helpText.activeSelf)
        {
            Debug.Log("working");
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }
}