using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleMusicCounter : MonoBehaviour
{
    public bool inside;
    public int colliderCount = 0;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jungleAmbient;
    [SerializeField] float DURATION = 5.0f;

    public void Enter(Collider other) {
            colliderCount += 1;
            UpdateState();
    }

    public void Exit(Collider other) {
            colliderCount -= 1;
            UpdateState();
    }

    void UpdateState() {
        Debug.Log("colliderCount: " + colliderCount);
        if (colliderCount == 0 && audioSource.isPlaying) {
            inside = false;
            StopMusic();
        }
        else if (colliderCount > 0 && !audioSource.isPlaying) { 
            inside = true;
            PlayMusic();
        }
    }

    void PlayMusic() {
            Debug.Log("play jungle ambient");
            audioSource.PlayOneShot(jungleAmbient, 0.4f);
    }

    void StopMusic() {//cole fix later
            Debug.Log("stop jungle ambient");
            /*float startVolume = audioSource.volume;
            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / FADETIME;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;*/
            StartCoroutine(StartFade(audioSource, DURATION, 0.0f));
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
        //Debug.Log("start fade");
        float currentTime = 0;
        float start = audioSource.volume;

        while ((currentTime < duration ) && (!inside) ) {
            //Debug.Log("current time" + currentTime);
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        //Debug.Log("StartFade ended");
        audioSource.Stop();
        audioSource.volume = start;
        UpdateState();
        yield break;
    }
}
