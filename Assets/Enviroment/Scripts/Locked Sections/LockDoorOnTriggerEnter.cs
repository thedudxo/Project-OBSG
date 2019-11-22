using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnTriggerEnter : MonoBehaviour {

    [SerializeField] GameObject doorToLock;
    [SerializeField] List<Transform> spawners = new List<Transform>();
    [SerializeField] int enemiesToSpawn = 11;
    public List<SpawnAI> enemies = new List<SpawnAI>();
    public GameObject boss;
    [SerializeField] bool bossFight = false;
    bool waiting = true;
    [HideInInspector] public int enemiesSpawned = 0;

    private void Update() {
        if (bossFight && !waiting) {
            if (!boss.GetComponent<BossScript>().dead) {
                if (enemiesSpawned < enemiesToSpawn)
                    StartCoroutine(WaitSpawn());
            }
        }
    }

    public void CheckEnemies() {
        if (enemies.TrueForAll(AllDead)) {
            Debug.Log("All dead");
            enemies.Clear();
            doorToLock.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            doorToLock.SetActive(true);
            StartCoroutine(SpawnEnemies());
            if(boss != null) {
                boss.GetComponent<Animator>().SetTrigger("Entry");
                bossFight = true;
            }
            GetComponent<Collider>().enabled = false;
        }
    }

    static bool AllDead(SpawnAI enemy) {
        return enemy.dead;
    }

    IEnumerator WaitSpawn() {
        waiting = true;
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies() {
        Debug.Log(enemiesSpawned);
        yield return new WaitForSeconds(0.5f);
        if(enemiesSpawned != enemiesToSpawn) {
            enemiesSpawned++;
            int i = Random.Range(0, spawners.Count);
            EnemeyManager.Instance.SpawnEnemy(spawners[i], GetComponent<LockDoorOnTriggerEnter>());
            StartCoroutine(SpawnEnemies());
        } else if(enemiesSpawned >= enemiesToSpawn) {
            waiting = false;
            enemiesSpawned = enemiesToSpawn;
        }
    }
}
