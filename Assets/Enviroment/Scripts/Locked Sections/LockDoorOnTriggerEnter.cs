using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject doorToLock;
    [SerializeField] GameObject enemy;
    [SerializeField] List<Transform> spawners = new List<Transform>();
    int enemiesToSpawn = 11;
    int enemiesSpawned = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER) {
            doorToLock.SetActive(true);
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies() {
        enemiesSpawned++;
        yield return new WaitForSeconds(0.5f);
        if(enemiesSpawned < enemiesToSpawn) {
            int i = Random.Range(0, spawners.Count);
            GameObject tmp = Instantiate(enemy, spawners[i]);
            tmp.transform.parent = null;
            tmp.GetComponent<SpawnAI>().AISpawn();
            Debug.Log(enemiesSpawned);
            StartCoroutine(SpawnEnemies());
        }
    }
}
