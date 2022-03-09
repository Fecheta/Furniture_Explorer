using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int elementIndex;

    private GameObject furnitureListManager;
    void Start()
    {
        furnitureListManager = GameObject.FindGameObjectWithTag("furniture_list");
    }

    public void SetAsSelected()
    {
        furnitureListManager
            .GetComponent<FurnitureListManager>()
            .SetCurrentFurnitureObject(elementIndex);
    }
}
