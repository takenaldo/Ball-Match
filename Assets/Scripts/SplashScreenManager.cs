using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreenManager : MonoBehaviour
{
    public string nextScene= "Main";

    private float startTime;
    public float waitingSeconds = 5;

    public GameObject progressBar;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

//        moveUp();
    
    }

    // Update is called once per frame
    void Update()
    {
        float now = Time.time;
        
        if (now - startTime > waitingSeconds)
            SceneManager.LoadScene(nextScene);

//            rotateProgressBar();
        //    moveUp();

    }

    void rotateProgressBar()
    {
        progressBar.gameObject.transform.Rotate(0, 0, 1 * 100);

    }

    private void moveUp()
    {
        progressBar.gameObject.transform.Translate(Vector2.up * 2);
    }

}
