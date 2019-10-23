using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{

    public GameObject turnon;

    private void Start()
    {
        Load(1);
    }

    public void Load(int sceneIndex)
    {
        turnon.SetActive(true);
        StartCoroutine(Loadlevel(sceneIndex));
        PlayerManager.keys = 0;
    }

    IEnumerator Loadlevel(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);

            yield return null;
        }
    }
}
