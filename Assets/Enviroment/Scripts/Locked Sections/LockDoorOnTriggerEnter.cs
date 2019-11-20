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

    [SerializeField] GameObject disableProbes;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== Tags.PLAYER)
        doorToLock.SetActive(true);
        disableProbes.SetActive(false);
            StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        enemiesSpawned++;
        yield return new WaitForSeconds(0.5f);
        if(enemiesSpawned < enemiesToSpawn) {
            int i = Random.Range(0, spawners.Count);
            GameObject tmp = Instantiate(enemy, spawners[i]);
            tmp.transform.parent = null;
            tmp.GetComponent<EnemyAI>().AISpawn();
            Debug.Log(enemiesSpawned);
            StartCoroutine(SpawnEnemies());
        }
    }
}
