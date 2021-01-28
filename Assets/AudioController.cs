using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jungleAmbient;
    [SerializeField] AudioClip caveAmbient;
    /*[SerializeField] float FADETIME = 5.0f;
    [SerializeField] float DURATION = 5.0f;
    [SerializeField] float TARGET_VOLUME = 0f;

    public void PlayJungleAmbient() {
        Debug.Log("play jungle ambient");
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(jungleAmbient, 0.4f);
        }
    }
    //public void StopJungleAmbient() {//cole fix later
    //    if (audioSource.isPlaying) {
    //        float startVolume = audioSource.volume;
    //        while (audioSource.volume > 0) {
    //            audioSource.volume -= startVolume * Time.deltaTime / FADETIME;
    //        }
    //        audioSource.Stop();
    //        audioSource.volume = startVolume;
    //    }
    //}
    public void PlayCaveAmbient() {
        Debug.Log("play cave ambient");
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(caveAmbient, 0.4f);
        }
    }
    public void StopSound(AudioSource audioSource) {
        Debug.Log("stop sound");
        //StartFade(audioSource, DURATION, TARGET_VOLUME);
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
        float currentTime = 0;
        float start = audioSource.volume;
        
        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }*/
}
