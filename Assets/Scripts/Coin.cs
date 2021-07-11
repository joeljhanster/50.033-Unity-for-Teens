using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D coinBody;
    private AudioSource coinAudio;
    public GameConstants gameConstants;
    private Vector3 upScaler;
    private Vector3 downScaler;

    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        upScaler = transform.localScale / (float) gameConstants.coinTimeStep;
        downScaler = upScaler / (float) gameConstants.coinTimeStep;
        coinBody = GetComponent<Rigidbody2D>();
        coinAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !collected) {
            coinAudio.Play();
            StartCoroutine("ScaleAndDestroy");

            // Increase score and spawn new enemy
            CentralManager.centralManagerInstance.increaseScore();
        }
    }

    IEnumerator ScaleAndDestroy()
    {
        // Become big
        for (int step = 0; step < gameConstants.coinTimeStep; step++) {
            this.transform.localScale = this.transform.localScale + upScaler;
            // wait for next frame
            yield return null;
        }

        // Become small
        for (int step = 0; step < gameConstants.coinTimeStep; step++) {
            this.transform.localScale = this.transform.localScale - downScaler;
            // wait for next frame
            yield return null;
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * gameConstants.rotatorRotateSpeed * Time.deltaTime);
    }
}
