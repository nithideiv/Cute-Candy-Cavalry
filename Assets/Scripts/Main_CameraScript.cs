using UnityEngine;

public class Main_CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player;
    public GameObject mainScript;
    public Camera main;
    public Transform wall;
    float zoomspeed = 2f;
    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
