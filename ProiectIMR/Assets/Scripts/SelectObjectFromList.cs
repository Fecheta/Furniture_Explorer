using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectFromList : MonoBehaviour
{
    [SerializeField] int objNumber;
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
    }
   public void SelectObject()
    {
        PlayerPrefs.SetInt("selectedObj", objNumber);
    }
}
