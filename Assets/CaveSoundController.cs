using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoundController : MonoBehaviour
{
    public bool inside;
    private string lastTouched = "";
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip caveAmbient;
    [SerializeField] float DURATION = 5.0f;

    public void InnerWallTriggered(Collider other) {
        if (other.tag == "Enemy")
        {
            Debug.Log("skeleton hit inner\n");
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy.lastTouched == "outer")
            {
                enemy.inside = true;
                Debug.Log("skeleton entering cave\n");
                other.gameObject.transform.Find("caveMini").gameObject.SetActive(true);
                other.gameObject.transform.Find("surfaceMini").gameObject.SetActive(false);
            }
            enemy.lastTouched = "inner";
        }
        if (other.tag == "Player")
        {
            if (lastTouched == "outer")
            {
                inside = true;
                UpdateState();
            }
            lastTouched = "inner";
        }
    }

    public void OuterWallTriggered(Collider other)
    {

        if (other.tag == "Enemy") 
        {
            Debug.Log("skeleton hit outer\n");
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>(); 
            if (enemy.lastTouched == "inner") 
            {
                enemy.inside = false;
                Debug.Log("skeleton leaving cave\n");
                other.gameObject.transform.Find("caveMini").gameObject.SetActive(false);
                other.gameObject.transform.Find("surfaceMini").gameObject.SetActive(true);
            }
            enemy.lastTouched = "outer"; 
        }
        if (other.tag == "Player")
        {
            if (lastTouched == "inner")
            {
                inside = false;
                UpdateState();
            }
            lastTouched = "outer";
        }
    }

    public void UpdateState() {
        if (inside && !audioSource.isPlaying) {
            FindObjectOfType<minimapManager>().setToCave();
            PlayMusic();
        }
        else if (!inside && audioSource.isPlaying) {
            FindObjectOfType<minimapManager>().setToSurface();
            StopMusic();
        }
    }
    void PlayMusic() {
        Debug.Log("play music");
            audioSource.PlayOneShot(caveAmbient, 0.4f);
    }

    void StopMusic() {//cole fix later
        Debug.Log("stop music");
        /*if (audioSource.isPlaying) {
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / FADETIME;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;
        }*/
        StartCoroutine(StartFade(audioSource, DURATION, 0.0f));
    }
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
        //Debug.Log("start fade");
        float currentTime = 0;
        float start = audioSource.volume;

        while ((currentTime < duration) && (!inside)) {
           // Debug.Log("current time" + currentTime);
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
