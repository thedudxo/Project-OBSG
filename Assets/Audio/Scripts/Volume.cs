using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour {

    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    string group;

    public void SetVolume(float vol) {
        mixer.SetFloat(group, vol);
    }
}
