using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 7f; //이동 속도 변수
    
    CharacterController CC; //캐릭터 컨트롤러 변수
    float gravity = -20f; //중력 변수
    float yVelocity = 0; // 수직 속력 변수
    public float jump =4f;
    public bool isJumping = false; //점프 상태 변수

     public void Start()
    {
        tr = GetComponent<Transform>();
       CC = GetComponent<CharacterController>();//시작과 동시에 캐릭터 컨트롤러 컴포넌트 할당 받기 위해 
    }
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        
        moveDir = Camera.main.transform.TransformDirection(moveDir);  //메인 카메라 기준으로 방향 변환
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
       
       
        
        if(isJumping && CC.collisionFlags == CollisionFlags.Below)//만약 점프 중 & 다시 바닥(Below) 착지
        {
            isJumping = false; //점프 전 상태로 초기화
            yVelocity = 0;
        }
        if (Input.GetButtonDown("Jump") && !isJumping)//만약 점프키를 누름 & 점프 X
        {
            yVelocity = jump;
            isJumping = true; //점프 상태로 변경
        }
        yVelocity += gravity * Time.deltaTime;
        moveDir.y = yVelocity; //캐릭터 수직 속도에 중력 적용
        CC.Move(moveDir * moveSpeed * Time.deltaTime); //이동
    }
}
