using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    public GameObject hitEffect;
    private Transform tr;
    public float moveSpeed = 7f; //이동 속도 변수
    public int hp = 100;
    int mHP = 100;
    public Slider Sliderhp;
    public Slider Sliderstep;
    CharacterController CC; //캐릭터 컨트롤러 변수
    float gravity = -20f; //중력 변수
    float yVelocity = 0; // 수직 속력 변수
    public float jump =4f;
    public bool isJumping = false; //점프 상태 변수
    Animator anim; //애니메이터 변수
    private AudioSource audioSource;//점프 오디오 소리
    GameManager gm;
    float step;
    float step_;
    bool stepon;
    public void Start()
    {
        tr = GetComponent<Transform>();
       CC = GetComponent<CharacterController>();//시작과 동시에 캐릭터 컨트롤러 컴포넌트 할당 받기 위해 
        anim = GetComponentInChildren<Animator>();
        // 오디오 소스 생성해서 추가
        audioSource = gameObject.GetComponent<AudioSource>();
        step = 0;
        step_ = 20;
    }
    
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir = Camera.main.transform.TransformDirection(moveDir);  //메인 카메라 기준으로 방향 변환
        tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        if(anim!=null)
        anim.SetFloat("MoveMotion", moveDir.magnitude);
        if (isJumping && CC.collisionFlags == CollisionFlags.Below)//만약 점프 중 & 다시 바닥(Below) 착지
        {
            isJumping = false; //점프 전 상태로 초기화
            yVelocity = 0;
        }
        if (gm.gState == GameManager.GameState.Run && Input.GetButtonDown("Jump") && !isJumping&&audioSource!=null)//만약 점프키를 누름 & 점프 X
        {
            audioSource.Play();
            yVelocity = jump;
            isJumping = true; //점프 상태로 변경
        }
        yVelocity += gravity * Time.deltaTime;
        moveDir.y = yVelocity; //캐릭터 수직 속도에 중력 적용
        CC.Move(moveDir * moveSpeed * Time.deltaTime); //이동
        if(Sliderhp!=null)
        Sliderhp.value = (float)hp / (float)mHP;
        if(Sliderstep!=null)
        Sliderstep.value = (float)step / (float)step_;
    }
    IEnumerator PlayEffect()
    {
        hitEffect.SetActive(true); //피격 UI 활성화
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);

    }
    public void DamageAction(int damage)
    {
        hp -= damage;
        if (0 < hp)
        {
            StartCoroutine(PlayEffect());
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("step"))
            stepon = true;
        {
            StartCoroutine(StartStep());
        }   
    }
    IEnumerator StartStep()
    {
        while (stepon)
        {
            step += 0.1f;
            yield return null;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        StopCoroutine(StartStep());
        stepon = false;
        step = 0;
    }
}
