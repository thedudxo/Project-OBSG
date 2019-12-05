using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject stats;

    [Header("Texts Stats: ")]
    [SerializeField] Text timeAlive;
    [SerializeField] Text kills;
    [SerializeField] Text punches;
    [SerializeField] Text punchEfficancy;

    [Header("Health:")]
    [SerializeField] GameObject hitIndicator;
    [SerializeField] float startHealthDelay = 0;
    [SerializeField] float healDelayTimer = 5;
    [SerializeField] float regenSpeed = 1;
    bool damaged = false;

    [Header("Respawn:")]
    [SerializeField] GameObject currentRespawn;
    [SerializeField] int respawnObjectsCount = 0;
    public bool canRespawn = false;

    [Header("Death:")]
    [SerializeField] GameObject[] deaths;
    [SerializeField] Camera main;
    [SerializeField] Camera fps;

    private void Start() {
        Respawn.respawnPosition = transform.position;
        Respawn.respawnRotation = transform.rotation;
    }

//          if (canRespawn) {
//              Respawn.StartRespwn();
//          } else {
//              Debug.Log("Can't Respawn");
//          }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerManager.alive = true;
            PlayerManager.health = PlayerManager.maxHealth;
            PlayerManager.keys = 0;
            LoadManager.loadScene(LoadManager.LoadedScene);

        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (respawnObjectsCount != 0 && !canRespawn) {
                SetRespawn();
            } else {
                Debug.Log("You have no respawn objects");
            }
        }

        if (startHealthDelay + healDelayTimer <= Time.time && damaged && PlayerManager.alive) {
            PlayerManager.health += regenSpeed * Time.deltaTime;
            if(PlayerManager.health > PlayerManager.maxHealth) {
                PlayerManager.health = PlayerManager.maxHealth;
                damaged = false;
            }
        }
    }

    void SetRespawn() {
        currentRespawn.SetActive(true);
        currentRespawn.transform.position = transform.position;
        canRespawn = true;
        respawnObjectsCount--;
    }

    public void DamagePlayer(float damage, Transform enemy) {
        DamageManager.Instance.SpawnIndicator(enemy);
        //hitIndicator.GetComponent<HitIndicator>().target = enemy;
        PlayerManager.health = PlayerManager.health - damage;
        damaged = true;
        CheckHealth();
        int i = Random.Range(0, 3);
        AudioManager.instance.Play("Grunt" + i);
    }

    void CheckHealth() { 
        if(PlayerManager.health <= 0) {
            killPlayer();
            //SetStats();
        } else {
            startHealthDelay = Time.time;
        }
    }

    void PlayerWin() {
        PlayerManager.win = true;
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        PlayerManager.ResetStats();
    }

    void SetStats() {
        stats.SetActive(true);
        timeAlive.text = "Seconds Survived: " + PlayerManager.SecondsPlayerAlive;
        kills.text = "Things you punched: " + PlayerManager.enemiesPlayerKilled;
        //punches.text = "Times you punched: " + PlayerManager.timesPlayerPunched;
        //punchEfficancy.text = "Punch Efficancy Rating: " + (float)  PlayerManager.enemiesPlayerKilled / PlayerManager.timesPlayerPunched * 100 + "%";
    }

    void killPlayer() {
        PlayerManager.health = 0;
        PlayerManager.alive = false;
        deaths[GetComponent<WeaponManager>().currentWeaponIndex].gameObject.SetActive(true);
        GetComponent<WeaponManager>().weapons[GetComponent<WeaponManager>().currentWeaponIndex].gameObject.SetActive(false);
        main.enabled = false;
        fps.enabled = false;
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        PlayerManager.ResetStats();
        
        AudioManager.instance.Play("Death");

    }

    public void respawnPlayer()
    {
        PlayerManager.alive = true;
        deathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerManager.player.transform.position = PlayerManager.respawnPoint;
        PlayerManager.player.GetComponent<Rigidbody>().velocity = new Vector3();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == Tags.FINISH) {
            SetStats();
            PlayerWin();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.RESPAWN) {
            respawnObjectsCount++;
            currentRespawn = other.gameObject;
            currentRespawn.GetComponent<Collider>().enabled = false;
            currentRespawn.SetActive(false);
        }
    }
}
