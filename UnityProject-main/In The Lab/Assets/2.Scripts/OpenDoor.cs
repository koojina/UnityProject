using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    PlayerCtrl player;
    float timer;
    GameObject sp;
    enum stage{
        one,
        two,
        three,
        four
    }
    stage sg;
    void Start()
    {
        sp= GameObject.Find("SpawnPoint");
        timer = 0;
        sg = stage.one;
    }
    // Start is called before the first frame update
    void open()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 20.0f);
        for (int i = 0; i < colls.Length; ++i) {
            if (colls[i].gameObject.CompareTag("Door") && sg == stage.one&&timer>4&&player.kill>=5)
            { 
                Destroy(colls[i].gameObject);
                sg = stage.two;
                gameObject.transform.position = new Vector3(-64.29f, 0.03f, -48.85f);
                timer = 0;
                sp.transform.position = new Vector3(-104, 2.792517f, 71.5f);
            }
           else if (colls[i].gameObject.CompareTag("Door2") && sg == stage.two && timer > 4 && player.kill >= 10)
            {
                Destroy(colls[i].gameObject);
                sg = stage.three;
                gameObject.transform.position = new Vector3(33.84f, 0.05f, -41.14f);
                gameObject.transform.localScale = new Vector3(2, 2.25f, 2);
                timer = 0;
                sp.transform.position = new Vector3(-19.1f, 2.792517f, -17.1f);
            }
           else if (colls[i].gameObject.CompareTag("Door3") && sg == stage.three && timer > 4 && player.kill >= 20)
            {
                Destroy(colls[i].gameObject);
                sg = stage.four;
                gameObject.transform.position = new Vector3(-95.6f, 0.05f, -167.5f);
                timer = 0;
                sp.transform.position = new Vector3(-96.1f, 2.792517f, -127.4f);
            }
            else if (colls[i].gameObject.CompareTag("Door4") && sg == stage.four && timer > 4 && player.kill >= 40)
            {
                Destroy(colls[i].gameObject);
                timer = 0;
            }
    }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        print("스테이지 :"+ sg);
        if (player.step >= player.step_)
            open();
    }
}

