using UnityEngine;

public class CrankHandle : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        Debug.Log("Crank pulled!");
        GetComponent<CrankPull>().StartCrankMotion(); // Custom method
    }
}