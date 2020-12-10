using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; //목표

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position; //카메라 위치와 목표 위치 일치시킴
    }
}
