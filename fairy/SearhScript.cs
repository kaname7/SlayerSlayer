using UnityEngine;
using System.Collections;

public class SearhScript : MonoBehaviour {

    public float WaitForSeconds;
    public GameObject Ice; //Iceを入れる
    Coroutine coroutine;
    private bool attack;   //whileのon/off
    public GameObject doragonObject; //playerを入れる
    public GameObject doragonObject2; //リスタートプレイヤーを入れる
    public float waitattackspeed = 1f; //Iceプレハブの発射間隔

    int dir = -1; // 左

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 doragonPos = doragonObject.transform.position;            //プレイヤーの位置
        Vector3 doragonPos2 = doragonObject2.transform.position;            //プレイヤーの位置
        float direction = doragonPos.x - transform.position.x;           //方向
        float direction2 = doragonPos.x - transform.position.x;           //方向2
        int nowdir = direction > 0 ? 1 : -1;
        int nowdir2 = direction2 > 0 ? 1 : -1;
        if (dir != nowdir) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            dir = nowdir;
        }
        if (dir != nowdir2)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            dir = nowdir2;
        }
}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Doragon")
        {
            Debug.Log("ドラゴン侵入");
            StartCoroutine(Attack());
        }
    }
 
    IEnumerator Attack()
    {

        attack = true;  //whileを動かす

        while (attack)
        {
            //Debug.Log("発射");
            yield return new WaitForSeconds(waitattackspeed); 
            GameObject obj = Instantiate(Ice, new Vector3(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;//Iceプレハブを出す
                              //1秒待って
            Ice a = obj.GetComponent<Ice>();
            a.Shoot((MonoBehaviour)this);
        }
}

    
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Doragon") {
            //Debug.Log("Stay");
            //animator.SetBool("isAttacking", true);
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Doragon")
        {
            attack = false;    //whileを止める
           // animator.SetBool("isAttacking", false);
            //Debug.Log("Exit");
        }
    }
}
