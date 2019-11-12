using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  static class LoadManager
{
    public static int sceneToLoad = 2;
    public static int LoadedScene = 0;

    public const int MAINMENU = 0;
    public const int LOADING = 1;
    public const int TUTORIAL = 2;
    public const int LEVEL1 = 3;

    public static void loadScene(int scene)
    {
        sceneToLoad = scene;
        SceneManager.LoadScene(LOADING);
    }
}
