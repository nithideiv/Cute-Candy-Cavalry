using UnityEngine;
using Unity.Cinemachine;

public class CinemachineCam : MonoBehaviour
{

    public CinemachineCamera vcam;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vcam.Target.TrackingTarget = player.transform;
        vcam.Target.LookAtTarget = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
