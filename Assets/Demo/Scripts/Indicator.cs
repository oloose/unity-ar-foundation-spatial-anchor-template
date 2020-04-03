using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Indicator : MonoBehaviour {
    [SerializeField]
    private GameObject Cursor;

    private ARRaycastManager rayManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public bool OnSurface { get; private set; } = false;

    void Start() {
        rayManager = FindObjectOfType<ARRaycastManager>();

        if (Cursor == null) {
            Cursor = transform.GetChild(0).gameObject;
        }
    }

    void Update() {
        if(rayManager.Raycast(
            new Vector2(Screen.width / 2, Screen.height / 2), 
            hits,
            TrackableType.PlaneWithinPolygon)) {

            transform.SetPositionAndRotation(hits[0].pose.position, hits[0].pose.rotation);
            OnSurface = true;
            Cursor.SetActive(true);

        } else {
            OnSurface = false;
            Cursor.SetActive(false);
        }


    }
}
