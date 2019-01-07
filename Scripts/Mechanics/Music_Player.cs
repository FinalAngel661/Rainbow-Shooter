using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Player : MonoBehaviour {

	public AudioClip otherClip;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying) {
            audioSource.clip = otherClip;
            audioSource.Play();
            audioSource.loop = true;
        }
	}
}
