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
    [SerializeField] float startHealthDelay = 0;
    [SerializeField] float healDelayTimer = 5;
    [SerializeField] float regenSpeed = 1;
    bool damaged = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerManager.alive = true;
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(startHealthDelay + healDelayTimer <= Time.time && damaged && PlayerManager.alive) {
            PlayerManager.health += regenSpeed * Time.deltaTime;
            if(PlayerManager.health > PlayerManager.maxHealth) {
                PlayerManager.health = PlayerManager.maxHealth;
                damaged = false;
            }
        }
    }

    public void DamagePlayer(float damage) {
        PlayerManager.health = PlayerManager.health - damage;
        Debug.Log(PlayerManager.health);
        damaged = true;
        CheckHealth();
    }

    void CheckHealth() { 
        if(PlayerManager.health <= 0) {
            killPlayer();
            SetStats();
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
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        PlayerManager.ResetStats();
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
}
