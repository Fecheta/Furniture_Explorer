using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundSingletone : MonoBehaviour
{
    private static ButtonSoundSingletone _buttonClickSound;

    void Awake()
    {
        if(_buttonClickSound == null)
        {
            _buttonClickSound = this;
            DontDestroyOnLoad(_buttonClickSound);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
