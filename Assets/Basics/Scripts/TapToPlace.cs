using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour {
    [SerializeField]
    private GameObject Prefab;

    private List<GameObject> objects = new List<GameObject>();

    private ARRaycastManager rayManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start() {
        rayManager = FindObjectOfType<ARRaycastManager>();
    }

    private void OnDestroy() {
        foreach(GameObject g in objects) {
            Destroy(g);
        }
        objects.Clear();
    }

    private void Update() {
#if !UNITY_EDITOR
        if (!(Input.touchCount > 0)) {
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended &&
            rayManager.Raycast(
                new Vector2(Screen.width / 2, Screen.height / 2),
                hits,
                TrackableType.PlaneWithinPolygon)) {

            GameObject g = Instantiate(Prefab, transform.parent);
            objects.Add(g);
            Vector3 hitPos = hits[0].pose.position;
            Vector3 position = new Vector3(hitPos.x, hitPos.y + g.GetComponent<Renderer>().bounds.size.y / 2, hitPos.z);
            g.transform.position = position;
        }
#endif
    }


}
