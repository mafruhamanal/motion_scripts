using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    public GameObject SpawnableObject;
    public XROrigin XROrigin;
    public ARRaycastManager RaycastManager;
    public ARPlaneManager PlaneManager;


    // list that stores Raycast hits
    private List<ARRaycastHit> RaycastHits = new List<ARRaycastHit>();

    private void Update()
    {
        // check if there is a touch that occured
        if (Input.touchCount > 0)
        {
            // get the first touch and if the touch has started then shoot raycast
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = RaycastManager.Raycast(Input.GetTouch(0).position, RaycastHits, TrackableType.PlaneWithinPolygon); // once the raycast has hit the ground, returns if the raycast hit the generated plane or not
                if (collision && isButtonPressed() == false)
                { // if the ui button is pressed, then it won't instantiate object
                    {
                        // instantiate object first
                        GameObject _object = Instantiate(SpawnableObject);
                        // then set the position of the object to the position and rotation of the raycast hit
                        _object.transform.position = RaycastHits[0].pose.position;
                        _object.transform.rotation = RaycastHits[0].pose.rotation;
                    }

                    // once we place our first object we want to disable the planes (only because we dont want the planes to be visible even after placing object)
                    foreach (var planes in PlaneManager.trackables)
                    {
                        planes.gameObject.SetActive(false);
                    }
                    PlaneManager.enabled = false;
                }
            }
        }
    }

    // check if UI button is being clicked
        public bool isButtonPressed()
        {
            if (EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // to switch between 3d objects to view
        public void switchObject(GameObject plant)
        {
            SpawnableObject = plant;

        }
    
}