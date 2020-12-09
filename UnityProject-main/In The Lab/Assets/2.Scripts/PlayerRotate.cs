using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float RotSpeed = 0;
    float My = 0;
   // private Transform tr;
    // Start is called before the first frame update
    void start()
    {
        RotSpeed = GameObject.Find("Main Camera").GetComponent<CamRotate>().rotSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
        My = GameObject.Find("Main Camera").GetComponent<CamRotate>().my;

       
        transform.eulerAngles = new Vector3(-90, 0, -My);

    }
}
