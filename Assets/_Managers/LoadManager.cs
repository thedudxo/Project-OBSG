using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  static class LoadManager
{
    public const int MAINMENU = 0;
    public const int LOADING = 1;
    public const int TUTORIAL = 2;
    public const int LEVEL1 = 3;

    public static int sceneToLoad = TUTORIAL;
    public static int LoadedScene = 0;

    public static void loadScene(int scene)
    {
        sceneToLoad = scene;
        LoadedScene = scene;
        SceneManager.LoadScene(LOADING);
    }
}
