using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locks : MonoBehaviour
{

    [SerializeField] GameObject helpText;

    [SerializeField] GameObject door;

    [SerializeField] GameObject[] keyholes = new GameObject[3];
    [SerializeField] GameObject[] preparedkeys = new GameObject[3];

    [SerializeField] int keyAmmount = 3;

    int unlocked = 0;
    


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
        if (helpText.activeSelf)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                keyholes[unlocked].GetComponent<Animator>().SetTrigger("Unlock");
                preparedkeys[unlocked].SetActive(true);
                unlocked++;
                if (unlocked >= keyAmmount)
                {
                    door.GetComponent<Animator>().SetTrigger("Open");
                }
            }
        }
    }
}