using UnityEngine;

public class EnemyHealthBarFollow : MonoBehaviour
{
    public Transform enemy; 
    public Vector3 offset = new Vector3(0, 10, 0); 

    void Update()
    {
        if (enemy == null) 
        {
            Destroy(gameObject); 
            return;
        }

        transform.position = enemy.position + offset;
        transform.LookAt(Camera.main.transform);
    }

}
