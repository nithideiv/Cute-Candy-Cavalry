using UnityEngine;
using UnityEngine.Rendering;

public class Tutorial_Scene : MonoBehaviour
{

    public GameObject ui_canvas;
    public GameObject tutorial_ui_1;
    public GameObject tutorial_ui_2;

    private Vector3[] origs;

    private GameObject player;
    public GameObject tutorial_canvas;
    public GameObject main_script;

    private float tut1_time;
    private float tut2_time;
    private float last_time;

    private Player_Stats player_stats;

    private CharacterController controller;

    private Fighting_Script fighting_script;

    public bool running = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origs = disable_ui(ui_canvas);

        Main_PlayerSelect script = main_script.GetComponent<Main_PlayerSelect>();
        player = script.returnPlayer();
        player_stats = player.GetComponent<Player_Stats>();
        controller = player.GetComponent<CharacterController>();
        fighting_script = player.GetComponent<Fighting_Script>();
        fighting_script.canMove = false;
        tut1_time = Find_End_Time(3.6f);

    }

    private float Find_End_Time(float wait)
    {
        last_time = Time.time + wait;
        return last_time;
    }

    void FixedUpdate()
    {
        if ((tutorial_ui_1.activeSelf == true) && (Time.time >= tut1_time))
        {
            tutorial_ui_1.SetActive(false);
            tutorial_ui_2.SetActive(true);

            tut2_time = Find_End_Time(4.4f);
        }
        else if ((tutorial_ui_2.activeSelf == true) && (Time.time >= tut2_time))
        {
            tutorial_ui_2.SetActive(false);

            fighting_script.canMove = true;
            controller.gameObject.SetActive(true);

            enable_ui(ui_canvas, origs);

            StartCoroutine(player_stats.InitializeHealthBarUI());
            StartCoroutine(player_stats.InitializeWeaponIconUI());
        }

    }

    private Vector3[] disable_ui(GameObject parent)
    {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>(true);
        Vector3[] original = new Vector3[allChildren.Length];
        int i = 0;
        foreach (Transform child in allChildren)
        {
            original[i] = child.localScale;
            i++;
            child.localScale = new Vector3(0, 0, 0);
        }

        return original;
    }

    private Vector3[] enable_ui(GameObject parent, Vector3[] original)
    {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>(true);
        int i = 0;
        foreach (Transform child in allChildren)
        {
            child.localScale = original[i];
            i++;
        }

        return original;
    }
}
