using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Button restartButton;
    [SerializeField] Button sprintButton;
    [SerializeField] Button jumpButton;
    // private ObstacleMovement ObstacleMovementScriptO;

    public float jumpForce = 10f;
    public float gravityModifier = 1f;
    public Vector3 startingGravity = new Vector3(0f, -9.81f, 0f);
    public bool isOnGround = true;
    public bool gameOver = false;
    public int doubleJump = 0;
    [SerializeField] Vector3 startingPosition;

    void Start()
    {
        Physics.gravity = startingGravity;
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        ObstacleMovementScriptB = GameObject.Find("Background").GetComponent<ObstacleMovement>();
        transform.position = startingPosition;
    }


    void Update()
    {
        MakePlayerJump();
        MakePlayerRunFast();
    }



    public void MakePlayerRunFast()
    {

        if (Input.GetKey(KeyCode.LeftShift) && isOnGround && !gameOver)
            if (isOnGround && !gameOver)
            {
                playerAnimator.SetBool("Run_fast", true);
                ObstacleMovementScriptB.obstacleSpeed = 80f;
                // ObstacleMovementScriptO.obstacleSpeed = 80f;
            }
            // }

            // public void MakePlayerRunSlow()
            // {
            else if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround && !gameOver)
                if (isOnGround && !gameOver)
                {
                    playerAnimator.SetBool("Run_fast", false);
                    ObstacleMovementScriptB.obstacleSpeed = 40f;
                    // ObstacleMovementScriptO.obstacleSpeed = 40f;
                }

    }

    public void MakePlayerJump()
    {
        // if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJump != 2) && !gameOver)
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {

            if ((isOnGround || doubleJump != 2) && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJump++;
                isOnGround = false;
                playerAnimator.SetTrigger("Jump_trig");
                dirtParticles.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
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
            restartButton.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(false);
            sprintButton.gameObject.SetActive(false);
        }
    }
}
