using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // 발사 위치
    public GameObject firePosition;

    // 피격 이펙트 오브젝트
    public GameObject bulletEffect;

    public GameObject[] eff_Flash;
    // 피격 이펙트 파티클 시스템
    ParticleSystem ps;
    GunCtrl gc;
    // 발사 무기 공격력
    public int weaponPower = 5;

    Animator anim;
    void Start()
    {
        // 피격 이펙트 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        gc = GameObject.Find("FirePosition").GetComponent<GunCtrl>();
        // 게임 상태가 "게임 중" 상태일 때에만 조작 가능하게 한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        // 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다.

        // 마우스 왼쪽 버튼 입력을 받는다.
        if (Input.GetMouseButton(0) && gc.bullet > 0)
        {
            // 만일 이동 블렌드 트리 파라미터의 값이 0이라면, 공격 애니메이션을 실시한다.
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("attack");
            }
            // 레이를 생성하고 발사될 위치와 진행 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // 레이가 부딪힌 대상의 정보를 저장할 변수를 생성한다.
            RaycastHit hitInfo = new RaycastHit();
            
            // 레이를 발사하고, 만일 부딪힌 물체가 있으면...
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 만일 레이에 부딪힌 대상의 레이어가 "Enemy"라면 데미지 함수를 실행한다.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                // 그렇지 않다면, 레이에 부딪힌 지점에 피격 이펙트를 플레이한다.
                else
                {
                    // 피격 이펙트의 위치를 레이가 부딪힌 지점으로 이동시킨다.
                    bulletEffect.transform.position = hitInfo.point;

                    // 피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                    bulletEffect.transform.forward = hitInfo.normal;

                    // 피격 이펙트를 플레이한다.
                    ps.Play();
                }
            }
          StartCoroutine(ShootEffectOn(0.05f));
        }
    }
    IEnumerator ShootEffectOn(float d)
    {
        int num = Random.Range(0, eff_Flash.Length - 1);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(d);
        eff_Flash[num].SetActive(false);
    }
}
