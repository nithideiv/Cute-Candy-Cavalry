using UnityEngine;

public class HealthBarFacePlayer : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the tag is set correctly.");
        }
    }

    void Update()
    {
        if (player != null) 
        {
            transform.LookAt(player);
            transform.Rotate(0, 180, 0); 
        }
    }
}
