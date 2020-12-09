    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 20f;
    public float mx = 0;
   public float my = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float m_X = Input.GetAxis("Mouse X");
        float m_Y = Input.GetAxis("Mouse Y"); //마우스 x,y 입력값

        mx += m_X * rotSpeed * Time.deltaTime;
        my += m_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -90f, 90f); //my의 값이 -90~90으로 유지되게 설정

        transform.eulerAngles = new Vector3(-my, mx, 0); //오일러 앵글의 각 축에 해당하는 값으로 물체를 회전
        
    }
}
