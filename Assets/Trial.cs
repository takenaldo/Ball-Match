using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trial : MonoBehaviour
{
    public GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody2D>().AddForce(destination.transform.position * 20);   


    
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > destination.transform.position.y)
            transform.Translate(Vector3.down * 5f * Time.deltaTime);
    }
}
