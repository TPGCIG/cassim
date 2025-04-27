using UnityEngine;

public class LookAtPoint : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 100f;


    void Update() 
    {

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, maxDistance)) {
            Debug.DrawLine(playerCamera.transform.position, hitInfo.point, Color.red);

            Vector3 hitPoint = hitInfo.point;
        }

    }

}