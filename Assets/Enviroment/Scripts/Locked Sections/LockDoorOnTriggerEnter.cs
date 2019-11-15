using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject doorToLock;
    [SerializeField] GameObject enemy;
    [SerializeField] List<Transform> spawners = new List<Transform>();
    int enemiesToSpawn = 20;
    int enemiesSpawned = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            doorToLock.SetActive(true);
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(0.5f);
        int i = Random.Range(0, spawners.Count);
        GameObject tmp = Instantiate(enemy, spawners[i]);
        Debug.Log(enemiesSpawned);
        enemiesSpawned++;
        tmp.GetComponent<EnemyAI>().AISpawn();
        if(enemiesSpawned <= enemiesToSpawn) {
            StartCoroutine(SpawnEnemies());
        }
    }
}
