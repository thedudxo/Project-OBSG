using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour {
    
    [Header("Pause:")]
    [SerializeField]
    bool menu;
    [SerializeField]
    GameObject 
        pauseCanvas,
        playerCanvas,
        pause,
        options,
        GPOptions,
        AudioOptions,
        quit,
        sceneSettings;
    GameObject currentOption;
    

    private void Start() {
        currentOption = GPOptions;
    }

    private void Update() {
        if (menu) { return; }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!PlayerManager.pause) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerCanvas.SetActive(false);
                pauseCanvas.SetActive(true);
                PlayerManager.pause = true;
                Time.timeScale = 0;
                sceneSettings.SetActive(false);
            } else if (PlayerManager.pause)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseCanvas.SetActive(false);
                playerCanvas.SetActive(true);
                PlayerManager.pause = false;
                Time.timeScale = 1;
                sceneSettings.SetActive(true);
            }
        }
    }
    // ----------------------------------------------Pause Menus---------------------------------------------- //
    public void BackPause(GameObject currentScreen) {
        pause.SetActive(true);
        currentScreen.SetActive(false);
    }

    public void PauseTo(GameObject newScreen) {
        pause.SetActive(false);
        newScreen.SetActive(true);
    }

    public void QuitToMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    // --------------------Options Menus---------------------- //
    public void OptionsMenu(GameObject to) {
        currentOption.SetActive(false);
        to.SetActive(true);
        currentOption = to;
    }

    public void SetInvert(bool isInvert) {
        PlayerManager.invert = isInvert;
    }

    public void SetSens(float sens) {
        PlayerManager.sensitivity = (int)sens;
        Debug.Log(PlayerManager.sensitivity);
    }
    // --------------------Main Menus------------------------- //
    public void StartGame() {
        PlayerManager.pause = false;
        LoadManager.loadScene(LoadManager.TUTORIAL);
    }

    public void TransitionTo(string to) {
        Camera.main.GetComponent<Animator>().SetTrigger(to);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
