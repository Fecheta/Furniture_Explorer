using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectOnTap : MonoBehaviour
{
    [SerializeField] private GameObject gameObj;
    private GameObject spawnedObj;
    private ARRaycastManager _arRaycastManager;
    private GameObject furnitureListManager;
    private Vector2 touchPosition;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector3 scaleChange;
    private Vector3 scaleBase;
    private bool lastUp= false;


    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        scaleChange = new  Vector3(0.1f, 0.1f, 0.1f);
    }

    private void Start()
    {
        furnitureListManager = GameObject.FindGameObjectWithTag("furniture_list");
        gameObj = furnitureListManager
            .GetComponent<FurnitureListManager>()
            .GetCurrentObject();
        spawnedObj = null;
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;

        }
        touchPosition = default;
        return false;
    }

  
    void Update()
    {
       if(!TryGetTouchPosition(out Vector2 touchPosition))
       {
           return;
       } 
       if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
       {
           var hitPose = hits[0].pose;
           if (spawnedObj == null)
           {
               spawnedObj = Instantiate(gameObj, hitPose.position, hitPose.rotation);
               scaleBase = spawnedObj.transform.localScale;
           }
           else
           {
               spawnedObj.transform.position = hitPose.position;
           }

       }
    }

    public void ChangeRotation()
    {
        spawnedObj.transform.Rotate(0, 35, 0);
    }

    public void ChangeScale()
    {
        if (spawnedObj.transform.localScale.y < 1.3 * scaleBase.y && spawnedObj.transform.localScale.y >=scaleBase.y)
        {
            spawnedObj.transform.localScale += scaleChange;
            lastUp = true;
        }
        else 
            if(spawnedObj.transform.localScale.y > 0.5 * scaleBase.y && spawnedObj.transform.localScale.y < scaleBase.y)
            {
                spawnedObj.transform.localScale -= scaleChange ;
                lastUp = false;
            }
            else
            {
                if (lastUp)
                {
                    spawnedObj.transform.localScale = scaleBase - scaleChange;
                }
                else
                {
                    spawnedObj.transform.localScale = scaleBase;
                }
            }
    }
}
