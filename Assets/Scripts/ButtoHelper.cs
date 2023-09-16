using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtoHelper : MonoBehaviour
{

    public Sprite spriteON, spriteOFF;

    private void Start()
    {
        if(PlayerPrefs.GetString(Helper. SETTING_MUSIC, Helper. STATUS_ON) == Helper.STATUS_OFF)
        {
            Image image = gameObject.GetComponent<Image>();
            image.sprite = spriteOFF;

        }
    }

    // for buttons that has only two options like on and off
    public void SwitchButtonONOFF()    {
        Image image = gameObject.GetComponent<Image>();
        if (image.sprite == spriteON)
            image.sprite = spriteOFF;
        else
            image.sprite = spriteON;
    }
}
