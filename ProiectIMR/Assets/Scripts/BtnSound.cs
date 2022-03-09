using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSound : MonoBehaviour
{
    private AudioSource soundObject;
    private Button thisButton;
    void Start()
    {
        soundObject = GameObject.FindGameObjectWithTag("click_sound").GetComponent<AudioSource>();
        thisButton = this.GetComponent<Button>();
        
        thisButton.onClick.AddListener(PlayOnClick);
    }

    void PlayOnClick()
    {
        soundObject.Play();
    }
}
