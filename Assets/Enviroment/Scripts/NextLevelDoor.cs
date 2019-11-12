using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField] GameObject helpText;
    [SerializeField] LoadLevel loadLevel;

    bool inTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
        helpText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        helpText.SetActive(false);
    }

    private void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKey(KeyCode.E))
            {
                loadLevel.Load(2);
            }
        }   
    }
}
