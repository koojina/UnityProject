using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float RotSpeed = 0;
    float Mx = 0;
   // private Transform tr;
    // Start is called before the first frame update
    void start()
    {
        RotSpeed = GameObject.Find("Main Camera").GetComponent<CamRotate>().rotSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
        Mx = GameObject.Find("Main Camera").GetComponent<CamRotate>().mx;

       
        transform.eulerAngles = new Vector3(-90, 0, Mx);

    }
}
