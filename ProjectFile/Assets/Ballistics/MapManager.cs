using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Canvas mapCanvas; // assign the map canvas in the inspector
    public GameObject mapObject; // assign the map object in the inspector
    public GameObject objectToSpawn; // assign the object to spawn in the inspector
    public float spawnOffset = 0f; // distance above the surface to spawn the object
    public Camera mapCamera; // assign the map camera in the inspector
    public Camera mainCamera;
    public float raycastHeight = 40.0f; // height above the map to perform the raycast
    public Vector3 offset;
    public float offset2;
    bool mapOpen = false;
    public GameObject canon;
    GameObject lastSpawnedObject = null;
    // Start is called before the first frame update
    void Start()
    {
        // disable the map canvas and mouse control
        mapCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapOpen = !mapOpen;
           // mapCanvas.enabled = mapOpen;
            if (mapOpen)
            {
                // enable mouse control for map
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                mapCamera.enabled = true;
                mainCamera.enabled = false;
            }
            else
            {
                // disable mouse control for map
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                mapCamera.enabled = false;
                mainCamera.enabled = true;
            }
        }

        if (mapOpen && Input.GetMouseButtonDown(0))
        {
            if (lastSpawnedObject != null)
            {
                Destroy(lastSpawnedObject);
            }

            // raycast from map to spawn object on surface
            Vector3 mouseP = Input.mousePosition;
            Ray ray = mapCamera.ScreenPointToRay(mouseP);
            //Debug.Log(Input.mousePosition);
           // Debug.Log(Screen.width);
            //Debug.Log(Screen.height);
            //Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(ray.origin.x, raycastHeight, ray.origin.z), Vector3.down, out hit))
            {
                Vector3 spawnPosition = hit.point + Vector3.up * spawnOffset;
                lastSpawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                //float a = 1f;
                canon.GetComponent<Canon>().target = lastSpawnedObject.transform;
                canon.GetComponent<Canon>().CalculateForce();
                canon.GetComponent<Canon>().CalculateAngle(canon.GetComponent<Canon>().CurrentForce);
            }
        }
    }
}
