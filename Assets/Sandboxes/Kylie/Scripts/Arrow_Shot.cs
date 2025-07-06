using UnityEngine;

public class Arrow_Shot : MonoBehaviour
{

    public bool shot;
    public Main_PlayerSelect script;
    private GameObject player;

    public GameObject arrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = script.returnPlayer();
        arrow = this.gameObject;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (shot == true)
        {

            if (other.gameObject.layer == 9)
            {

                if (player.GetComponent<Player_Stats>().attacked == false)
                {

                    player.GetComponent<Player_Stats>().attacked = true;
                    other.gameObject.GetComponent<EnemyScript>().TakeDamage(2);
                    Debug.Log(other.gameObject.GetComponent<EnemyScript>().ShowHealth());
                    player.GetComponent<Player_Stats>().ResetAttack();
                    shot = false;

                }
            }

            if (other.gameObject.layer == 7)
            {
                shot = false;
            }
            if (shot == false)
            {
                Destroy(arrow);
            }

            Destroy(arrow, 5);
        
    
        }
    }
}
