using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
[RequireComponent(typeof(ARRaycastManager))]
public class placer : MonoBehaviour
{
    public GameObject prefab;
    private GameObject spawned;
    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetPosition(out Vector2 touchPosition) {
        if (Input.touchCount > 0) {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetPosition(out Vector2 touchPosition)) {
            return;
        }
        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
            var hitPose = hits[0].pose;
            if (spawned == null)
            {
                //spawned = Instantiate(prefab, hitPose.position, hitPose.rotation);
                spawned = prefab;
                prefab.transform.position = hitPose.position;
                //prefab.transform.rotation=(-90,0,hitPose);
                prefab.SetActive(true);
                spawned = prefab;

            }
            else
            {
                spawned.transform.position = hitPose.position;
             //   spawned.transform.LookAt(Camera.main.transform);
            }
            


        }

    }
}
