using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //발사 위치
    public GameObject fireposition;


    // Update is called once per frame
    void Update()
    {
        //마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총 발사
        //마우스 왼쪽 버튼을 입력받음.
        if (Input.GetMouseButtonDown(0))
        {
            //발사 위치, 진행 방향 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transfrom.forward);

            //레이가 부딪힌 대상의 정보를 저장할 변수 생성
            RaycastHit hitInfo = new RaycastHit();

            //레이 발사 후 피격 이벤트 표시
            if (Physics.Raycast(ray, out hitInfo))
            {
                //피격 이벤트 위치를 레이가 부딪힌 시점으로 이동
                bulletEffect.transform.position = hitInfo.point;

                //피격 이벤트 플레이
                ps.Play();
            }
        }
    }
    public GameObject bulletEffect;

    ParticleSystem ps;

    void Start()
    {
        //파티클 시스템 오브젝트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }
}
