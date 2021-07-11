using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{
    private float speed;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    public CustomCastEvent castPowerup;


    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        speed = gameConstants.playerDefaultSpeed;

        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    public void PlayerDiesSequence()
    {
        // Mario dies
        Debug.Log("Mario dies");
        // do whatever you want here, animate etc
        // ...
        marioAnimator.SetTrigger("onDeath");
        speed = 0;
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                marioBody.AddForce(movement * speed);
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState) {
            marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (
            col.gameObject.CompareTag("Ground") ||
            col.gameObject.CompareTag("Obstacles") ||
            col.gameObject.CompareTag("Pipes") ||
            col.gameObject.CompareTag("Spawned")
        ) {
            onGroundState = true;   // back on ground
            Debug.Log("Play Dust");
            dustCloud.Play();
        }
    }
    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState) {
            faceRightState = false;
            marioSprite.flipX = true;

            if (Mathf.Abs(marioBody.velocity.x) > 0.5)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState) {
            faceRightState = true;
            marioSprite.flipX = false;

            if (Mathf.Abs(marioBody.velocity.x) > 0.5)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("z")) {
            castPowerup.Invoke(KeyCode.Z);
        }

        if (Input.GetKeyDown("x")) {
            castPowerup.Invoke(KeyCode.X);
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
    }
}
