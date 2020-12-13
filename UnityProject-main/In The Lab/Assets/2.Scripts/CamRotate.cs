    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 70f;
    public float mx = 0;
   public float my = 0;
    private AudioSource audioSource;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        // 오디오 소스 생성해서 추가
        audioSource = gameObject.GetComponent<AudioSource>();
        // 뮤트: true일 경우 소리가 나지 않음
        audioSource.mute = false;
        // 루핑: true일 경우 반복 재생
        audioSource.loop = true;
        // 자동 재생: true일 경우 자동 재생
        audioSource.playOnAwake = false;
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
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.gState == GameManager.GameState.Run && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)))//플레이어 달리는 소리
        {
            audioSource.Play();
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            audioSource.Stop();
        }
    }
}
