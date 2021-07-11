using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerEV : MonoBehaviour
{
    // events to subscribe
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;

    public GameConstants gameConstants;
    private float originalX;
    private int moveRight;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private Animator enemyAnimator;
    private AudioSource enemyAudio;
    private bool outside = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();

        enemyAnimator.SetBool("rejoice", false);

        // get the starting position
        originalX = transform.position.x;

        // randomise initial direction
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;

        // compute initial velocity
        ComputeVelocity();
    }

    // callbacks must be PUBLIC
    public void PlayerDeathResponse()
    {
        Debug.Log("Enemy killed Mario");
        // do whatever you want here, animate etc
        // ...
        enemyAnimator.SetBool("rejoice", true);
        moveRight = 0;
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }
    void MoveEnemy()
    {
        ComputeVelocity();
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.CompareTag("Player"))
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
                Debug.Log("Invoking onEnemyDeath");
                onEnemyDeath.Invoke();
            }
            else
            {
                // hurt player implement later
                // CentralManager.centralManagerInstance.damagePlayer();
                enemyAudio.Play();
                Debug.Log("Invoking onPlayerDeath");
                onPlayerDeath.Invoke();
            }
        }
    }

    void KillSelf()
    {
        // enemy dies
        // CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        Debug.Log("Kill sequence ends");
    }

    IEnumerator flatten()
    {
        Debug.Log("Flatten starts");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + enemySprite.bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flatten ends");
        this.gameObject.SetActive(false);

        Debug.Log("Enemy returned to pool");
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {
            // move enemy
            MoveEnemy();
            outside = false;
        }
        else if (!outside)
        {
            // change direction
            moveRight *= -1;
            // ComputeVelocity();
            MoveEnemy();
            outside = true;
        }
        enemySprite.flipX = moveRight == 1 ? false : true;
    }
}
