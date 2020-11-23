using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    float RotSpeed;
    float Mx;
    private Transform tr;
    // Start is called before the first frame update
    void start()
    {
        RotSpeed = GameObject.Find("Main Camera").GetComponent<CamRotate>().rotSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
        Mx = GameObject.Find("Main Camera").GetComponent<CamRotate>().mx;

       
        tr.eulerAngles = new Vector3(0, Mx, 0);

    }
}
