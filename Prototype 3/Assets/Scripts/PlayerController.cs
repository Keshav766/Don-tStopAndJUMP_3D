using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    private Animator playerAnimator;
    public ParticleSystem collisionExplosion;
    public ParticleSystem dirtParticles;
    public AudioSource cameraAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private ObstacleMovement ObstacleMovementScriptB;
    // private ObstacleMovement ObstacleMovementScriptO;

    public float jumpForce = 10f;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public bool gameOver = false;
    public int doubleJump = 0;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        ObstacleMovementScriptB = GameObject.Find("Background").GetComponent<ObstacleMovement>();
        // ObstacleMovementScriptO = GameObject.Find("Obstacle").GetComponent<ObstacleMovement>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump != 2) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            doubleJump++;
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            dirtParticles.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            playerAnimator.SetBool("Run_fast", true);
            ObstacleMovementScriptB.obstacleSpeed = 80f;
            // ObstacleMovementScriptO.obstacleSpeed = 80f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            playerAnimator.SetBool("Run_fast", false);
            ObstacleMovementScriptB.obstacleSpeed = 40f;
            // ObstacleMovementScriptO.obstacleSpeed = 40f;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticles.Play();
            doubleJump = 0;
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            collisionExplosion.Play();
            dirtParticles.Stop();
            cameraAudio.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
