using System.Collections;
using UnityEngine;

public class Banana_Attack : MonoBehaviour
{

    public bool shot;
    public Main_PlayerSelect script;
    private GameObject player;

    public GameObject heart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = script.returnPlayer();
        heart = this.gameObject;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8)
        {
            player.gameObject.GetComponent<Player_Stats>().TakeDamage(3);
        }
    }
}