using System.Collections.Generic;
using UnityEngine;

public class AddToPath : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> pathList = new List<GameObject>();
    private GameObject selectedPathObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.y = 0;
            GameObject newObject = Instantiate(prefab, mousePosition, Quaternion.identity);

            // Add the new object to the path list
            pathList.Add(newObject);

            // Update the path for the PathFollower script
            PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
            foreach (PathFollower pathFollower in pathFollowers)
            {
                pathFollower.myMoveType.path = pathList;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                selectedPathObject = hit.transform.gameObject;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (selectedPathObject != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.y = 0;
                selectedPathObject.transform.position = mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectedPathObject = null;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Get the first PathFollower object
            PathFollower pathFollower = FindObjectOfType<PathFollower>();

            if (pathFollower != null)
            {
                // Instantiate a new object
                PathFollower newPathFollower = Instantiate(pathFollower);

                // Set the new object's speed to a random value between 1 and 15
                newPathFollower.maxSpeed = Random.Range(1f, 15f);

                // Update the path for the new object
                newPathFollower.myMoveType.path = pathList;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Get all the PathFollower objects
            PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();

            // Destroy all the PathFollower objects except for the first one
            for (int i = 0; i < pathFollowers.Length-1; i++)
            {
                Destroy(pathFollowers[i].gameObject);
            }

            for (int i = 1; i < pathList.Count; i++)
            {
                Destroy(pathList[i].gameObject);
            }
            var a = pathList[0];
            pathList.Clear();
            pathList.Add(a);
            pathFollowers[0].myMoveType.path = pathList;
        }
    }
}
