using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Locks : MonoBehaviour
{

    [SerializeField] GameObject helpText;

    [SerializeField] GameObject door;

    [SerializeField] GameObject[] keyholes = new GameObject[3];
    [SerializeField] GameObject[] preparedkeys = new GameObject[3];

    [SerializeField] int keyAmmount;

    [SerializeField] VisualEffect leftDoor;
    [SerializeField] VisualEffect rightDoor;


    int unlocked = 0;

    private void Start()
    {
        keyAmmount = keyholes.Length;
    }


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
                if(!(PlayerManager.keys > 0)) { return; }

                PlayerManager.keys--;

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