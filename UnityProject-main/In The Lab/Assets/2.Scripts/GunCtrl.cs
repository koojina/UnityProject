using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private Transform cam;
    Vector3 s_p;
    GameManager gm;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();

        s_p = cam.localPosition;

        // 오디오 소스 생성해서 추가
        audioSource = gameObject.GetComponent<AudioSource>();

        // 뮤트: true일 경우 소리가 나지 않음
        audioSource.mute = false;

        // 루핑: true일 경우 반복 재생
        audioSource.loop = false;

        // 자동 재생: true일 경우 자동 재생
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.gState==GameManager.GameState.Run&&Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shake());
            audioSource.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(Shake());
            audioSource.Stop();
        }
    }
    public IEnumerator Shake()
    {
        float timer = 0;
        while (Input.GetMouseButton(0))
        {
            int y, x;
            y = Random.Range(0, 2);
            x = Random.Range(0, 2);
            if (timer %2  == 0)
            cam.eulerAngles = new Vector3(x == 0 ? cam.eulerAngles.x + 0.2f : cam.eulerAngles.x - 2.5f, y == 0 ? cam.eulerAngles.y + 1.5f : cam.eulerAngles.y - 1.5f, y == 0 ? cam.eulerAngles.z + 1.5f : cam.eulerAngles.z - 1.5f);
            timer++;
            yield return null;
        }
    }
}
