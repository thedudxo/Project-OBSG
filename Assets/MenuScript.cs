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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerManager.pause) {
            pauseCanvas.SetActive(true);
            PlayerManager.pause = true;
            Time.timeScale = 0;
        }
    }
    // ----------------------------------------------Pause Menus---------------------------------------------- //
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
    // ----------------------------------------------Main Menus---------------------------------------------- //
}
