using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSetter : MonoBehaviour
{

    public Button[] btnLevels;
    public Sprite[] spriteLevels;

    // Start is called before the first frame update
    void Start()
    {

//        PlayerPrefs.SetInt(Helper.USER_LEVEL, 1);
//        
        for (int i = 0; i < Mathf.Min(PlayerPrefs.GetInt(Helper.USER_LEVEL, 1)) && i < btnLevels.Length; i++)
        {
            btnLevels[i].GetComponent<Image>().sprite = spriteLevels[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
