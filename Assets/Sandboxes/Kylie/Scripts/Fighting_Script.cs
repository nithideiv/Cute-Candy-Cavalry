using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fighting_Script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CharacterController controller;
    public GameObject mainScript;

    public bool canMove = true;
    public Transform playerBody;
    
    public Vector3 inputDirection;
    public GameObject player;
    private float moveSpeed = 10.0f;
    private float sprintSpeed = 20.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = player.GetComponent<CharacterController>();
        playerBody = player.transform;
        canMove = true;
        //Cursor.SetCursor(null, new Vector2(0,0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

        float playerSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = sprintSpeed;
        }
        else
        {
            playerSpeed = moveSpeed;
        }

        if (canMove == true)
        {
            controller.gameObject.SetActive(true);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //float x = Input.GetAxis("Mouse X");
            //float z = Input.GetAxis("Mouse Y");

            playerBody.Rotate(0, Input.GetAxis("Mouse X") * (playerSpeed / 4f), 0);
            inputDirection = new Vector3(x, 0, z).normalized;
            Vector3 worldInputDirection = playerBody.TransformDirection(inputDirection);

            controller.Move(worldInputDirection * Time.deltaTime * playerSpeed);

        }
        else
        {
            controller.gameObject.SetActive(false);
        }
    }
}
