using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn {

    public static List<GameObject> enemies = new List<GameObject>();
    public static Vector3 respawnPosition;

    public static void StartRespwn() {
        Enemies();
        Player();
        Items();
    }

    static void Enemies() {
        foreach(GameObject e in enemies) {
            var ai = e.GetComponent<EnemyAI>();
            ai.playerTarget = null;
            ai.isClear = false;
            ai.isInAngle = false;
            ai.aiState = EnemyAI.AIState.idle;
        }
    }

    static void Player() {
        PlayerManager.player.transform.position = respawnPosition;
        PlayerManager.playerDeath.canRespawn = false;
        ResetEnemyAI();
    }

    static void Items() {
        Debug.Log("Reset Items");
    }

    static void ResetEnemyAI() {
        foreach (GameObject e in enemies) {
            e.GetComponent<EnemyAI>().SetPrefab();
        }
    }
}
