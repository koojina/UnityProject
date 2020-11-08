using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 10.0f;
    float RotSpeed;
    float Mx;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        RotSpeed = GameObject.Find("Main Camera").GetComponent<CamRotate>().rotSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Mx= GameObject.Find("Main Camera").GetComponent<CamRotate>().mx;
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        tr.eulerAngles = new Vector3(0, Mx, 0);
    }
}
