using UnityEngine;

public class ItemPlacementPreview : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject placementPrefab;
    public LayerMask groundLayer;
    public float maxDistance = 5f;
    public float gridSize = 1f;

    private GameObject previewInstance;

    void Start()
    {
        previewInstance = Instantiate(placementPrefab);
        previewInstance.GetComponent<Collider>().enabled = false;
        previewInstance.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.5f); // semi-transparent green
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundLayer))
        {
            Vector3 snappedPosition = SnapToGrid(hit.point);
            previewInstance.transform.position = snappedPosition;

            if (Input.GetMouseButtonDown(0)) // Left click to place
            {
                Instantiate(placementPrefab, snappedPosition, Quaternion.identity);
            }
        }
        else
        {
            previewInstance.transform.position = Vector3.one * -999f; // hide preview
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;
        return new Vector3(x, y, z);
    }
}
