using Microsoft.Azure.SpatialAnchors.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CredentialHelper : MonoBehaviour
{
    [SerializeField]
    private SpatialAnchorManager SpatialAnchorManager;

    [SerializeField]
    private SpatialAnchorConfig SpatialAnchorConfig;

    private void Start() {
        SpatialAnchorManager.SpatialAnchorsAccountId = SpatialAnchorConfig.SpatialAnchorsAccountId;
        SpatialAnchorManager.SpatialAnchorsAccountKey = SpatialAnchorConfig.SpatialAnchorsAccountKey;
    }

}
