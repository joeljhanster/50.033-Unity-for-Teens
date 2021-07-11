using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;

    public void consumedBy(GameObject player)
    {
        // give player jump boost
        player.GetComponent<PlayerController>().maxSpeed *= 2;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().maxSpeed /= 2;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")) {
            // update UI
            CentralManager.centralManagerInstance.addPowerup(t, 0, this);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

// public class OrangeMushroom : MonoBehaviour, ConsumableInterface
// {
//     private Rigidbody2D mushroomBody;
//     private SpriteRenderer mushroomSprite;
//     private float speed = 5;
//     private Vector2 velocity;
//     private int moveRight = 1;
//     private AudioSource mushroomAudio;
//     private bool hit = false;
//     private bool collected = false;
//     public Texture t;

//     // Start is called before the first frame update
//     void Start()
//     {
//         mushroomBody = GetComponent<Rigidbody2D>();
//         mushroomSprite = GetComponent<SpriteRenderer>();
//         mushroomAudio = GetComponent<AudioSource>();
//         mushroomBody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);    // Add an impulse force upwards
//     }

//     public void consumedBy(GameObject player)
//     {
//         // give player jump boost
//         player.GetComponent<PlayerController>().maxSpeed *= 2;
//         StartCoroutine(removeEffect(player));
//     }

//     IEnumerator removeEffect(GameObject player)
//     {
//         yield return new WaitForSeconds(5.0f);
//         player.GetComponent<PlayerController>().maxSpeed /= 2;
//     }


//     void OnCollisionEnter2D(Collision2D col)
//     {
//         if (col.gameObject.CompareTag("Pipes")) {
//             // Change direction
//             // If pipe is on the right of mushroom, move left; else, move right
//             if (col.gameObject.transform.position.x > mushroomBody.position.x)
//             {
//                 moveRight = -1;
//             } else
//             {
//                 moveRight = 1;
//             }
//         }
        
//         if (col.gameObject.CompareTag("Player") && !hit && !collected) {
//             // Stop moving when it collides with Mario
//             speed = 0;
//             mushroomAudio.Play();
//             hit = true;

//             // Add visual feedback upon collision (mushroom slightly enlarged and then scaled in)
//             collected = true;

//             // update UI
//             CentralManager.centralManagerInstance.addPowerup(t, 0, this);
//             GetComponent<Collider2D>().enabled = false;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         var velocity = mushroomBody.velocity;
//         velocity = new Vector2((moveRight) * speed, velocity.y);
//         mushroomBody.velocity = velocity;
//     }
// }
