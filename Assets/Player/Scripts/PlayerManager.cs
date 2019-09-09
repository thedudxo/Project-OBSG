using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager {

    //please use this instead of assigning player scripts in the editor
    //that causes problems with prefabs
    //these values should be set in PlayerManagerSetup

    public static GameObject player;
    public static Camera camera;


    //other player scripts
    public static PlayerMovement playerMovement;
    public static PlayerAnimations playerAnimations;
    public static PlayerDeath playerDeath;
    public static MouseLook mouseLook;
    

    public static Vector3 respawnPoint;
    public static bool alive = true;
    public static bool win = true;
    public static bool throwing = false;
    public static float maxHealth = 50;
    public static float health = 50;

    public static List<GameObject> doorKeys = new List<GameObject>();

    //interesting stats
    public static int enemiesPlayerKilled = 0;
    public static int timesPlayerPunched = 0;
    public static float SecondsPlayerAlive = 0;

    public static void ResetStats()
    {
        enemiesPlayerKilled = 0;
        timesPlayerPunched = 0;
        SecondsPlayerAlive = 0;
    }
}
