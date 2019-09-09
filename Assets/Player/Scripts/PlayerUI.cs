using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    
    [Header("Health:")]
    [SerializeField]
    RectTransform healthFill;
    [SerializeField]
    Image redScreen;

    [Header("Keys:")]
    [SerializeField]
    bool stop;//ignore this
    [SerializeField]
    Image 
        blueKey,
        redKey,
        yellowKey;
    [SerializeField]
    GameObject
        bluePrefab,
        redPrefab,
        yellowPrefab;

    [Header("Throw:")]
    [SerializeField]
    Image crosshair;

    [Header("Pause:")]
    [SerializeField]
    GameObject pauseCanvas;
    [SerializeField]
    GameObject pause;
    [SerializeField]
    GameObject options;
    
    [Header("Audio:")]
    [SerializeField]
    AudioMixer mixer;

    private void Update() {
        SetHealthAmount(PlayerManager.health/PlayerManager.maxHealth);
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerManager.pause) {
            pauseCanvas.SetActive(true);
            PlayerManager.pause = true;
            Time.timeScale = 0;
        }
        if (PlayerManager.throwing) {
            crosshair.enabled = true;
        } else {
            crosshair.enabled = false;
        }
    }

    void SetHealthAmount(float amount) {
        redScreen.color = new Color(1, 1, 1, -(amount - 1));
    }

    public void GetKeyImages() {
        if (PlayerManager.doorKeys.Contains(bluePrefab)) {
            blueKey.enabled = true;
        }

        if (PlayerManager.doorKeys.Contains(redPrefab)) {
            redKey.enabled = true;
        }

        if (PlayerManager.doorKeys.Contains(yellowPrefab)) {
            yellowKey.enabled = true;
        }
    }

    public void Back(GameObject currentScreen) {
        pause.SetActive(true);
        currentScreen.SetActive(false);
    }

    public void Options() {
        pause.SetActive(false);
        options.SetActive(true);
    }

    public void SetInvert(bool isInvert) {
        PlayerManager.invert = isInvert;
    }

    public void SetVolume(float vol, string group) {
        mixer.SetFloat(group, vol);
    }

    public void QuitToMenu() {
        //Load main menu here
    }
}