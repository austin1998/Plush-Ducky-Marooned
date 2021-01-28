using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour {
    [SerializeField] AudioSource footstep;
    [SerializeField] float MAX_SOUND_DELAY = 0.5f;
    [SerializeField] float MIN_SOUND_DELAY = 0f;
    [SerializeField] float MIN_PITCH = 0.5f;
    [SerializeField] float MAX_PITCH = 1.2f;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(DelayFootstep());
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator DelayFootstep() {
        yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_SOUND_DELAY, MAX_SOUND_DELAY));
        footstep.pitch = UnityEngine.Random.Range(MIN_PITCH, MAX_PITCH);
        Debug.Log("play skeleton footstep with volume:" + footstep.volume);
        footstep.Play();
    }
}
