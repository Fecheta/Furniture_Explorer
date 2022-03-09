using System.Collections.Generic;
using UnityEngine;

public class FurnitureListManager : MonoBehaviour
{
    [SerializeField] private int currentFurnitureIndex = 0;
    [SerializeField] private List<GameObject> furnitureList = new List<GameObject>();
    [SerializeField] private List<GameObject> furnitureListElements = new List<GameObject>();
    
    private static FurnitureListManager _furnitureListManager;

    private void Awake()
    {
        if (_furnitureListManager == null)
        {
            _furnitureListManager = this;
            DontDestroyOnLoad(_furnitureListManager);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GETCurrentFurnitureObject()
    {
        return currentFurnitureIndex;
    }

    public void SetCurrentFurnitureObject(int value)
    {
        int listLength = furnitureList.Count - 1;
        
        if (listLength < value || value < 0)
        {
            currentFurnitureIndex = -1;
        }

        currentFurnitureIndex = value;
    }

    public GameObject GetCurrentObject()
    {
        return furnitureList[currentFurnitureIndex];
    }

    public GameObject GetCurrentListElement()
    {
        return furnitureListElements[currentFurnitureIndex];
    }
}
