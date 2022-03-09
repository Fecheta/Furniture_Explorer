using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]

public class PlaneViewToggle : MonoBehaviour
{
    private ARPlaneManager planeManager;
    [SerializeField]
    private Text toggleButtonText;

   private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        toggleButtonText.text = "Disable Planes";
        SetAllPlanesActive(false);
    }

    public void TogglePlaneView()
    {
        planeManager.enabled = !planeManager.enabled;
        string toggleButtonMessage = "";
        if (planeManager.enabled)
        {
            toggleButtonMessage = "Disable Planes";
            SetAllPlanesActive(true);
        }
        else
        {
            toggleButtonMessage = "Enable Planes";
            SetAllPlanesActive(false);
        }
        toggleButtonText.text = toggleButtonMessage;
    }
    public void SetAllPlanesActive(bool value)
    {
        foreach(var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
