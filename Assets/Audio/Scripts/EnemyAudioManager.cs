﻿using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class EnemyAudioManager : MonoBehaviour {

    public Sound[] sounds;
    
	void Awake () {
		
        foreach(Sound s in sounds) {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
            s.source.outputAudioMixerGroup = s.output;
            s.source.playOnAwake = s.playOnAwake;
            s.source.spatialBlend = s.spatialBlend;

        }

	}

    void Start () {
        //Play("Theme");
        //Play("SoundEffect");

    }

    public void Play (string name)  {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }
}
