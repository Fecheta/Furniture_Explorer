using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSelected : MonoBehaviour
{
    private GameObject listManager;
    private GameObject listElement;
    public GameObject currentElement;
    void Start()
    {
        // currentElement = GetComponent<>
        
        listManager = GameObject.FindGameObjectWithTag("furniture_list");

        listElement = listManager.GetComponent<FurnitureListManager>().GetCurrentListElement();
        
        Image currentSelectedImage = listElement.
            transform.GetChild(0)
            .gameObject
            .GetComponent<Image>();
        
        string currentSelectedLabel = listElement
            .transform
            .GetChild(1)
            .gameObject
            .GetComponent<Image>()
            .transform
            .GetChild(0)
            .gameObject
            .GetComponent<Text>()
            .text;

        currentElement.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = currentSelectedImage.sprite;
        currentElement.transform.GetChild(1).gameObject.GetComponent<Image>().transform.GetChild(0).gameObject
            .GetComponent<Text>().text = currentSelectedLabel;
    }
}
