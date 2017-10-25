using UnityEngine;
using System.Collections;

public class searchScript2 : MonoBehaviour {

    public Animator animator;
    public Transform player;
    private Transform wolf;
    private float rotationSmooth = 1f;
    public float speed = 5f;
    public float limitDistance = 10f; //敵キャラクターがどの程度近づいてくるか設定(この値以下には近づかない）



    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    
    void Update () {
        /*
        Vector3 playerPos = player.position;                 //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向と距離を求める。
        float distance = direction.sqrMagnitude;            //directionから距離要素だけを取り出す。
        direction = direction.normalized;                   //単位化（距離要素を取り除く）
        direction.y = 0f;                                   //後に敵の回転制御に使うためY軸情報を消去。これにより敵上下を向かなくなる。

        //プレイヤーの距離が一定以上でなければ、敵キャラクターはプレイヤーへ近寄ろうとしない
        if (distance >= limitDistance)
        {

            //プレイヤーとの距離が制限値以上なので普通に近づく
            transform.position = transform.position + (direction * speed * Time.deltaTime);

        }
        else if (distance < limitDistance)
        {
            //プレイヤーの方を向く
            // transform.rotation = Quaternion.LookRotation(direction);
            //transform.localScale = new Vector3(Mathf.Sign(direction.x),1,1);
        }

        //地面衝突判定処理。（今回は直接座標を操作しているため実装したが、直接座標操作はあまり好ましくないため
        //後でUnityのキャラクター操作機能を用いた敵キャラクターの実装を紹介する。）
        */
    }  

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(player.transform.position.x < wolf.transform.position.x){
                transform.localScale = new Vector3(-1, 1, 1);
            }else{
                    transform.localScale = new Vector3(1, 1, 1);
                }
            
            //Debug.Log("Stay");
            //animator.SetBool("isAttacking", true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //animator.SetBool("isAttacking", false);
            //Debug.Log("Exit");
        }
    }
}
