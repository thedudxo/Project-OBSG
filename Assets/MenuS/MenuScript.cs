using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour {
    
    [Header("Pause:")]
    [SerializeField]
    GameObject pauseCanvas;
    [SerializeField]
    GameObject playerCanvas;
    [SerializeField]
    GameObject pause;
    [SerializeField]
    GameObject options;
    [SerializeField]
    GameObject quit;
    
    [SerializeField]
    bool menu;

    private void Start() {
    }

    private void Update() {
        if (menu) { return; }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!PlayerManager.pause) {
                playerCanvas.SetActive(false);
                pauseCanvas.SetActive(true);
                PlayerManager.pause = true;
                Time.timeScale = 0;
            } else if (PlayerManager.pause) {
                pauseCanvas.SetActive(false);
                playerCanvas.SetActive(true);
                PlayerManager.pause = false;
                Time.timeScale = 1;
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

    public void SetInvert(bool isInvert) {
        PlayerManager.invert = isInvert;
    }

    public void QuitToMenu() {
        SceneManager.LoadScene(0);
    }
    // ----------------------------------------------Main Menus---------------------------------------------- //
    public void StartGame() {
        PlayerManager.pause = false;
        SceneManager.LoadScene(1);
    }

    public void TransitionTo(string to) {
        Camera.main.GetComponent<Animator>().SetTrigger(to);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
