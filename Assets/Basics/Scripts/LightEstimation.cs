﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// A component that can be used to access the most
/// recently received light estimation information
/// for the physical environment as observed by an
/// AR device.
/// </summary>
[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour {
    [SerializeField]
    [Tooltip("The ARCameraManager which will produce frame events containing light estimation information.")]
    ARCameraManager m_CameraManager;

    [SerializeField]
    private float LightAdjustment = 0.3f;

    /// <summary>
    /// Get or set the <c>ARCameraManager</c>.
    /// </summary>
    public ARCameraManager cameraManager {
        get { return m_CameraManager; }
        set {
            if (m_CameraManager == value)
                return;

            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;

            m_CameraManager = value;

            if (m_CameraManager != null & enabled)
                m_CameraManager.frameReceived += FrameChanged;
        }
    }

    /// <summary>
    /// The estimated brightness of the physical environment, if available.
    /// </summary>
    public float? brightness { get; private set; }

    /// <summary>
    /// The estimated color temperature of the physical environment, if available.
    /// </summary>
    public float? colorTemperature { get; private set; }

    /// <summary>
    /// The estimated color correction value of the physical environment, if available.
    /// </summary>
    public Color? colorCorrection { get; private set; }

    /// <summary>
    /// The estimated direction of the main light of the physical environment, if available.
    /// </summary>
    public Vector3? mainLightDirection { get; private set; }

    /// <summary>
    /// The estimated color of the main light of the physical environment, if available.
    /// </summary>
    public Color? mainLightColor { get; private set; }

    /// <summary>
    /// The estimated intensity in lumens of main light of the physical environment, if available.
    /// </summary>
    public float? mainLightIntensityLumens { get; private set; }

    /// <summary>
    /// The estimated spherical harmonics coefficients of the physical environment, if available.
    /// </summary>
    public SphericalHarmonicsL2? sphericalHarmonics { get; private set; }

    void Awake() {
        m_Light = GetComponent<Light>();
    }

    void OnEnable() {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived += FrameChanged;
    }

    void OnDisable() {

        if (m_CameraManager != null)
            m_CameraManager.frameReceived -= FrameChanged;
    }

    void FrameChanged(ARCameraFrameEventArgs args) {
        if (args.lightEstimation.averageBrightness.HasValue) {
            brightness = args.lightEstimation.averageBrightness.Value;
            m_Light.intensity = brightness.Value + LightAdjustment;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue) {
            colorTemperature = args.lightEstimation.averageColorTemperature.Value;
            m_Light.colorTemperature = colorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue) {
            colorCorrection = args.lightEstimation.colorCorrection.Value;
            m_Light.color = colorCorrection.Value;
        }
    }

    Light m_Light;
}
