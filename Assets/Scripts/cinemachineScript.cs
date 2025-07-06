using UnityEngine;
using Unity.Cinemachine;

public class cinemachineScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player;
    public GameObject mainScript;
    public CinemachineCamera vcam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Main_PlayerSelect script = mainScript.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
        vcam.Target.TrackingTarget = player.transform;
        vcam.Target.LookAtTarget = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
