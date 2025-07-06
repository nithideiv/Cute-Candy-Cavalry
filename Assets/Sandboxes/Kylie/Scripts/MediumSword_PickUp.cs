
using System;
using UnityEngine;

public class MediumSword_PickUp : MonoBehaviour
{

    //public bool collide;

    public Main_PlayerSelect main_script;
    public Player_Stats player_script;
    public GameObject player;
    private bool attacked;
    public GameObject sword;

    public LayerMask enemyLayer;

    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;

    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = main_script.returnPlayer();
        player_script = player.GetComponent<Player_Stats>();
        sword = this.gameObject;
        //collide = false;
        //mace.GetComponent<Animator>().Play(
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on Sword PickUp GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (player_script.collide == true) {
            if (player.GetComponent<Player_Stats>().curr_weapon == sword) {
                if (Input.GetMouseButtonDown(0)) {
                    player.GetComponent<Player_Stats>().Sword_Attack(sword);
                }
            }
        }



    }

    void OnTriggerEnter(Collider other)
    {

        if (player_script.collide == true) {
            Debug.Log("collide");
            if (player_script.sword_attack == true)
            {
                Debug.Log("swing");
                if (other.gameObject.layer == 9)
                {

                    if (player_script.attacked == false)
                    {
                        player_script.attacked = true;
                        Debug.Log("attack");

                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(3);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
                }
            }
        }
        // 8 = Player layer
        if (player_script.collide == false) {
            if (other.gameObject.layer == 8) {
                player = other.gameObject;
                player.GetComponent<Player_Stats>().HasWeapon = true;
                player.GetComponent<Player_Stats>().curr_weapon = sword;
                // change icon
                if (player_script.weaponIconUI != null)
                {
                    Debug.Log("weapon change");
                    player_script.weaponIconUI.UpdateWeaponIcon(sword);
                }

                if (audioSource != null)
                {
                    audioSource.Play(); 
                }

                Vector3 sword_rotate = new Vector3(
                    90, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z
                );
                sword.gameObject.transform.position = player.transform.position;
                sword.gameObject.transform.position += new Vector3(0, 0.4f, 0);
                sword.gameObject.transform.rotation = Quaternion.Euler(sword_rotate);

                sword.gameObject.transform.parent = other.gameObject.transform;
                sword.GetComponent<Animator>().SetTrigger("hold_sword");

                sword.gameObject.GetComponent<Light>().enabled= false;
    
                player_script.collide = true;
            }
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (player_script.collide == true) {
            //if (sword.gameObject.GetComponent<Animator>().GetBool("sword_swing")) {
            if (player_script.sword_attack == true) {

                if (other.gameObject.layer == 9) {
                    if (player_script.attacked == false)
                    {
                        Debug.Log("HIT");
                        player_script.attacked = true;

                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(3);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
            }
            }
        }
    }
}
