using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneEV : MonoBehaviour
{
    public AudioSource changeSceneSound;

    private bool changed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !changed) {
            Debug.Log("Player entering castle");
            changed = true;
            changeSceneSound.PlayOneShot(changeSceneSound.clip);
            StartCoroutine(WaitSoundClip("MarioEVLevel2"));
        }
    }

    IEnumerator WaitSoundClip(string sceneName)
    {
        yield return new WaitUntil(() => !changeSceneSound.isPlaying);
        StartCoroutine(ChangeScene("MarioEVLevel2"));
    }

    IEnumerator ChangeScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
