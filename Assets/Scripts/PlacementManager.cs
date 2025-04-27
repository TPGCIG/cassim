using System.Drawing;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject ghostPrefab;
    public GameObject prefab;
    public float gridSize = 1f;
    public KeyCode placementModeKey = KeyCode.B;

    private GameObject currentGhost;
    private bool placementMode = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(placementModeKey)) {
            placementMode = !placementMode;

            if (placementMode) {
                currentGhost = Instantiate(ghostPrefab);
            } else {
                if (currentGhost != null) {
                    Destroy(currentGhost);
                }
            }
        }

        if (placementMode && currentGhost != null) {
            UpdateGhostPosition();
        }
    }

    void UpdateGhostPosition() {

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 100f))
        {
            Vector3 point = hitInfo.point;

            

            // Snap point to nearest grid point
            Vector3 snappedPoint = new Vector3(
                Mathf.Round(point.x / gridSize) * gridSize,
                Mathf.Round(point.y / gridSize) * gridSize, // Keep actual height (usually 0 if ground)
                Mathf.Round(point.z / gridSize) * gridSize
            );

            if (Input.GetMouseButtonDown(0)) {
                PlaceObject(snappedPoint);
            }

            currentGhost.transform.position = snappedPoint;
        }
    }

    private void PlaceObject(Vector3 position) {
        // Instantiate a new cube prefab at the hit position
        Instantiate(prefab, position, Quaternion.identity);
    }
}
