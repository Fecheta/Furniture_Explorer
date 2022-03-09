using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private GameObject planesButton;

    private GameObject currentlyPlacedObject;
    private GameObject furnitureListManager;
    
    private ARSessionOrigin arOrigin;
    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private Vector3 scaleChange;
    private Vector3 scaleBase;
    private bool lastUp= false;

    [SerializeField] private int rotationUpdate = 45;
    [SerializeField] private double maxScaleValue = 1.5;
    [SerializeField] private double minScaleValue = 0.5;

    public GameObject rotateLeftBtn;
    private int rotateLeftTouched = 0;
    
    public GameObject rotateRightBtn;
    private int rotateRightTouched = 0;
    
    public GameObject scalePlusBtn;
    private int scalePlusTouched = 0;
    
    public GameObject scaleMinBtn;
    private int scaleMinTouched = 0;
    
    void Start()
    {
        furnitureListManager = GameObject.FindGameObjectWithTag("furniture_list");
        objectToPlace = furnitureListManager
            .GetComponent<FurnitureListManager>()
            .GetCurrentObject();
        scaleChange = new  Vector3(0.1f, 0.1f, 0.1f);
        
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = arOrigin.GetComponent<ARRaycastManager>();
    }
    
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid
            && Input.touchCount > 0
            && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject() ||
                EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            if (currentlyPlacedObject)
            {
                Destroy(currentlyPlacedObject);
                currentlyPlacedObject = null;
                
                placementIndicator.SetActive(true);
            }
            else
            {
                currentlyPlacedObject = Instantiate(
                    objectToPlace,
                    placementPose.position,
                    placementPose.rotation);
                scaleBase = currentlyPlacedObject.transform.localScale;
                placementIndicator.SetActive(false);
            }
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (currentlyPlacedObject)
        {
            return;
        }
        
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var camForward = Camera.current.transform.forward;
            var camBearing = new Vector3(camForward.x, 0, camForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(camBearing);
        }
    }

    // private void UpdateScaleAndRotationButtons()
    // {
    //     rotateLeftBtn.GetComponent<Button>().interactable = rotateLeftTouched != 8;
    //     rotateRightBtn.GetComponent<Button>().interactable = rotateRightTouched != 8;
    //
    //     scalePlusBtn.GetComponent<Button>().interactable = scalePlusTouched != 5;
    //     scaleMinBtn.GetComponent<Button>().interactable = scaleMinTouched != 5;
    // }

    // private void ResetScaleAndRotationTouchedCount()
    // {
    //     rotateLeftTouched = 0;
    //     rotateRightTouched = 0;
    //     
    //     scalePlusTouched = 0;
    //     scaleMinTouched = 0;
    // }
    
    public void ChangeRotationLeft()
    {
        currentlyPlacedObject.transform.Rotate(0, +rotationUpdate, 0);
        
        // rotateLeftTouched++;
        // rotateRightTouched--;
        // UpdateScaleAndRotationButtons();
    }
    
    public void ChangeRotationRight()
    {
        currentlyPlacedObject.transform.Rotate(0, -rotationUpdate, 0);
        
        // rotateLeftTouched--;
        // rotateRightTouched++;
        // UpdateScaleAndRotationButtons();
    }
    
    public void ChangeScalePlus()
    {
        if (currentlyPlacedObject.transform.localScale.y <= maxScaleValue)
        {
            currentlyPlacedObject.transform.localScale += scaleChange;
        }

        // scalePlusTouched++;
        // scaleMinTouched--;
        // UpdateScaleAndRotationButtons();
    }
    
    public void ChangeScaleMinus()
    {
        if(currentlyPlacedObject.transform.localScale.y >= minScaleValue)
        {
            currentlyPlacedObject.transform.localScale -= scaleChange;
        }
        
        // scalePlusTouched--;
        // scaleMinTouched++;
        // UpdateScaleAndRotationButtons();
    }
}
