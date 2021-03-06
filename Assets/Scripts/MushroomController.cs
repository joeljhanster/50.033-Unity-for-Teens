using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D mushroomBody;
    private SpriteRenderer mushroomSprite;
    private float speed = 5;
    private Vector2 velocity;
    private int moveRight = 1;
    private AudioSource mushroomAudio;
    private bool hit = false;
    // private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomSprite = GetComponent<SpriteRenderer>();
        mushroomAudio = GetComponent<AudioSource>();
        mushroomBody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);    // Add an impulse force upwards
    }

    // void OnBecameInvisible()
    // {
    //     Destroy(gameObject);
    // }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipes")) {
            // Change direction
            // If pipe is on the right of mushroom, move left; else, move right
            if (col.gameObject.transform.position.x > mushroomBody.position.x)
            {
                moveRight = -1;
            } else
            {
                moveRight = 1;
            }
        }
        
        if (col.gameObject.CompareTag("Player") && !hit) {
            // Stop moving when it collides with Mario
            speed = 0;
            mushroomAudio.Play();
            hit = true;

            // Add visual feedback upon collision (mushroom slightly enlarged and then scaled in)
            // collected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = mushroomBody.velocity;
        velocity = new Vector2((moveRight) * speed, velocity.y);
        mushroomBody.velocity = velocity;
    }
}
