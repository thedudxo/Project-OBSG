using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finishTutorial : MonoBehaviour
{
    [SerializeField] Image fade;

    bool fading = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER)
        {
            fading = true;
        }
    }

    private void Update()
    {
        if (fading)
        {
            Color color = fade.color;
            color.a += 0.1f;
            fade.color = color;

            if (color.a >= 1f)
            {
                PlayerManager.keys = 0;
                LoadManager.loadScene(LoadManager.LEVEL1);
            }
        }
    }
}
