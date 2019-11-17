using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER)
        {
            LoadManager.loadScene(LoadManager.LEVEL1);
        }
    }
}
