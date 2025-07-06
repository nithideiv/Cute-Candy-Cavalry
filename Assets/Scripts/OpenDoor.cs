using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 0.5f;
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            Debug.Log("Door Opened after level completed");
            isOpen = true;
            StartCoroutine(RotateDoor(Vector3.up * openAngle));
        }
    }

    private IEnumerator RotateDoor(Vector3 byAngles)
    {
        Quaternion fromRotation = transform.rotation;
        Quaternion toRotation = Quaternion.Euler(transform.eulerAngles + byAngles);
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, time);
            yield return null;
        }
    }
}
