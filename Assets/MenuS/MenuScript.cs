using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour {
    
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

    [SerializeField]
    bool menu;

    private void Start() {
    }

    private void Update()
    {
        if (menu) { return; }
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerManager.pause) {
            pauseCanvas.SetActive(true);
            PlayerManager.pause = true;
            Time.timeScale = 0;
        }
    }
    // ----------------------------------------------Pause Menus---------------------------------------------- //
    public void BackPause(GameObject currentScreen) {
        pause.SetActive(true);
        currentScreen.SetActive(false);
    }

    public void OptionsPause() {
        pause.SetActive(false);
        options.SetActive(true);
    }

    public void SetInvert(bool isInvert) {
        PlayerManager.invert = isInvert;
    }

    public void QuitToMenu() {
        //Load main menu here
    }
    // ----------------------------------------------Main Menus---------------------------------------------- //
    public void StartGame() {
        //start the game
    }

    public void TransitionTo(string to) {
        Camera.main.GetComponent<Animator>().SetTrigger(to);
    }

    public void QuitGame() {

    }
}
