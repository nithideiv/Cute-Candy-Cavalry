using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject mainScript;
    public Transform playerBody;
    
    public Vector3 inputDirection;
    public GameObject player;
    private float playerSpeed = 6.0f;

    private AudioSource walkAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
        controller = player.GetComponent<CharacterController>();
        playerBody = player.transform;

        // Get the AudioSource from the player
        walkAudio = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.gameObject.activeSelf == true)
        {

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            inputDirection = new Vector3(x, 0, z);
            Vector3 worldInputDirection = playerBody.TransformDirection(inputDirection);

            controller.Move(worldInputDirection * Time.deltaTime * playerSpeed);

            /* if (Input.GetAxisRaw("Horizontal") > 0) {
                 RotatePlayer(1);
             } else if (Input.GetAxisRaw("Horizontal") < 0) {
                 RotatePlayer(-1);
             }*/

            if (inputDirection.magnitude > 0.01f)
            {
                // Play the walking sound if not already playing
                if (!walkAudio.isPlaying)
                {
                    walkAudio.Play();
                }
            }
            else
            {
                // Stop the walking sound if the player is not moving
                if (walkAudio.isPlaying)
                {
                    walkAudio.Stop();
                }
            }

            if (inputDirection.magnitude > 0.01f)
            {
                inputDirection.Normalize();
                if (z >= 0)
                {
                    if (x > 0)
                    {
                        //playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.LookRotation(inputDirection), 10f * Time.deltaTime);
                        playerBody.Rotate(Vector3.up * 80f * Time.deltaTime);
                    }
                    else if (x < 0)
                    {
                        playerBody.Rotate(-1 * Vector3.up * 80f * Time.deltaTime);
                    }
                }
                //playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.LookRotation(inputDirection), 10f * Time.deltaTime);
            }
        }
        else
        {
            
        }
    }  
}
