using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Helper : MonoBehaviour
{
    public static string SETTING_VIBRATION = "setting_vibration";
    public static string SETTING_MUSIC     = "setting_music";
    public static string BG_MUSIC = "bg_music";



    public static string STATUS_ON = "on";
    public static string STATUS_OFF = "off";

    public static string USER_LEVEL = "user_level";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool Paused()
    {
        return Time.timeScale == 0;
    }

    public void LoadScene(string name)
    {
        if (Paused())
            resume();
        SceneManager.LoadScene(name);
    }

    public static void pause()
    {
        Time.timeScale = 0;
    }

    public static void resume()
    {
        Time.timeScale = 1;
    }

    public static void enable(GameObject go)
    {
        go.SetActive(true);
    }

    public static void disable(GameObject go)
    {
        go.SetActive(false);
    }




    public void triggMusicSetting()
    {
        AudioSource[] allAudioSources = getAllAudioSources();
         if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_ON)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_OFF);
            GameObject.FindGameObjectWithTag("BG_MUSIC").GetComponent<AudioSource>().Stop();

/*            foreach (AudioSource audioSource in allAudioSources)
                audioSource.Stop();*/
        }
        else if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_OFF)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_ON);
            GameObject.FindGameObjectWithTag("BG_MUSIC").GetComponent<AudioSource>().Play();
/*            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);*/
        }
    }



    public void triggMusicSettingWithBG(GameObject audioManager)
    {
        AudioSource[] allAudioSources = getAllAudioSources();
        if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_ON)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_OFF);
            foreach (AudioSource audioSource in allAudioSources)
                audioSource.Stop();
        }
        else if (PlayerPrefs.GetString(SETTING_MUSIC, STATUS_ON) == STATUS_OFF)
        {
            PlayerPrefs.SetString(SETTING_MUSIC, STATUS_ON);
            audioManager.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(audioManager);
        }
    }




    public AudioSource[] getAllAudioSources()
    {
      return  GameObject.FindObjectsOfType<AudioSource>();
    }


    public void changeVibrationSetting()
    {
        if(PlayerPrefs.GetString(SETTING_VIBRATION, STATUS_ON) == STATUS_ON)
            PlayerPrefs.SetString(SETTING_VIBRATION, STATUS_OFF);
        else
            PlayerPrefs.SetString(SETTING_VIBRATION, STATUS_ON);
    }


    public static void rotateAroundMyself(GameObject go)
    {
        go.transform.Rotate(new Vector3(0, 0, -1));
    }

    public void LoadLevel(int lvl)
    {
        if ((lvl) <= PlayerPrefs.GetInt(Helper.USER_LEVEL, 1))
        {
            LoadScene("Level" + lvl);
        }

    }

    public static int getUserLevel()
    {
        return PlayerPrefs.GetInt(Helper.USER_LEVEL, 1);
    }

    public void PlayCurrentLevel()
    {
        LoadScene("Level" + PlayerPrefs.GetInt(Helper.USER_LEVEL, 1));
    }

    public void ReloadLevel()
    {
        LoadScene("Level" + GameManager.instance.current_level);
    }

    public void LoadNextLevel()
    {
        
        if (GameManager.instance.current_level + 1 <= getUserLevel())
        {
            
            LoadScene("Level" + (GameManager.instance.current_level + 1));
        }
        else
        {
            

        }
    }


    public static void upgradeUserLevel()
    {
        int previous_level = PlayerPrefs.GetInt(Helper.USER_LEVEL, 1);
        int new_level = previous_level + 1;
        PlayerPrefs.SetInt(Helper.USER_LEVEL, new_level);
    }



}
