using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public AudioSource changeSceneSound;
    private bool changed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !changed) {
            changed = true;
            changeSceneSound.PlayOneShot(changeSceneSound.clip);
            StartCoroutine(LoadYourAsyncScene("MarioLevel2"));
        }
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        yield return new WaitUntil(() => !changeSceneSound.isPlaying);
        CentralManager.centralManagerInstance.changeScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
