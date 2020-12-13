using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private Transform cam;
    Vector3 s_p;
    GameManager gm;
    public Text tx;
    public int bullet;
    public int bullettotal;
    float timer;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();

        s_p = cam.localPosition;

        // 오디오 소스 생성해서 추가
        audioSource = gameObject.GetComponent<AudioSource>();

        // 뮤트: true일 경우 소리가 나지 않음
        audioSource.mute = false;

        // 루핑: true일 경우 반복 재생
       // audioSource.loop = false;

        // 자동 재생: true일 경우 자동 재생
        audioSource.playOnAwake = false;

        bullet = 30;
        bullettotal = 30;
    }

    // Update is called once per frame
    void Update()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        tx.text = string.Format("{0}", bullet);
        tx.text += " / " + bullettotal;
        if (bullet>0&&gm.gState==GameManager.GameState.Run&&Input.GetMouseButtonDown(0))
        { 
            StartCoroutine(Shake());
          
        }
        else if (Input.GetMouseButtonUp(0)||bullet<1)
        {
            StopCoroutine(Shake());
            //audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bullet == bullettotal)
                { }
            else bullet = 30;
        }

    }
    public IEnumerator Shake()
    {
        while (Input.GetMouseButton(0)&&bullet>0)
        {
            int y, x;
            y = Random.Range(0, 2);
            x = Random.Range(0, 2);
            timer += Time.deltaTime;
            if (timer>0.2f&&bullet>0)
            {
                audioSource.Play();
                cam.eulerAngles = new Vector3(x == 0 ? cam.eulerAngles.x + 3.5f : cam.eulerAngles.x - 5.5f, y == 0 ? cam.eulerAngles.y + 1.5f : cam.eulerAngles.y - 1.5f, y == 0 ? cam.eulerAngles.z + 1.5f : cam.eulerAngles.z - 1.5f);
               bullet--;
               timer = 0;
            }
            yield return null;
        }
    }
}
