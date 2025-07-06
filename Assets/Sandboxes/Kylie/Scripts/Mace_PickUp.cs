
using UnityEngine;

public class Mace_PickUp : MonoBehaviour
{

    //public bool collide;

    public Main_PlayerSelect main_script;
    public Player_Stats player_script;
    public GameObject player;
    private bool attacked;
    public GameObject mace;

    public LayerMask enemyLayer;

    public float sightRange, attackRange;
    public bool enemyInSightRange, enemyInAttackRange;

    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = main_script.returnPlayer();
        player_script = player.GetComponent<Player_Stats>();
        mace = this.gameObject;
        //collide = false;
        //mace.GetComponent<Animator>().Play(
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on Mace PickUp GameObject!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (player_script.collide == true) {
            if (player.GetComponent<Player_Stats>().curr_weapon == mace) {
                if (Input.GetMouseButtonDown(0)) {
                    player.GetComponent<Player_Stats>().MaceAttack_1(mace);
                }
            }
        }



    }

    void OnTriggerEnter(Collider other)
    {

        if (player_script.collide == true) {
            if (player_script.mace_attack == true) {
                if (other.gameObject.layer == 9) {
    
                    if (player_script.attacked == false) {
                        player_script.attacked = true;

                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(4);
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
                player.GetComponent<Player_Stats>().curr_weapon = mace;
                // change icon
                if (player_script.weaponIconUI != null)
                {
                    Debug.Log("weapon change");
                    player_script.weaponIconUI.UpdateWeaponIcon(mace);
                }

                if (audioSource != null)
                {
                    audioSource.Play(); 
                }
                     


                mace.gameObject.transform.position = mace.gameObject.transform.position - new Vector3(0, mace.transform.position.y, 0);
                mace.gameObject.transform.parent = other.gameObject.transform;
                mace.gameObject.GetComponent<Light>().enabled= false;
                mace.gameObject.transform.rotation = Quaternion.Euler(80,0,0);
                mace.GetComponent<Animator>().applyRootMotion = false;
                //mace.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                player_script.collide = true;
            }
        }

    }

    void OnTriggerStay(Collider other)
    {

        if (player_script.collide == true) {
            if (player_script.mace_attack == true) {
                if (other.gameObject.layer == 9) {
                    if (player.GetComponent<Player_Stats>().attacked == false) {
                        player.GetComponent<Player_Stats>().attacked = true;
    
                        other.gameObject.GetComponent<EnemyScript>().TakeDamage(4);
                        player.GetComponent<Player_Stats>().ResetAttack();
                    }
                }
            }
        }
    }
}
