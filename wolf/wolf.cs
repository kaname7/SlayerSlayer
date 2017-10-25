using UnityEngine;
using System.Collections;

public class wolf : MonoBehaviour
{


    wolfMove wolfMove;
    Transform attackTarget;
    public GameObject EBite;
    private Transform player;

    // 待機時間は２秒とする
    public float waitBaseTime = 0.5f;
    // 残り待機時間
    float waitTime;
    // 移動範囲５メートル
    public float walkRange = 1.0f;
    // 初期位置を保存しておく変数
    public Vector3 basePosition;
    public float Distance = 10f;
    public float searchRange = 2.0f;

    // ステートの種類.
    enum State
    {
        Walking,    // 探索
        Chasing,    // 追跡
        Attacking,	// 攻撃
        Died,       // 死亡
    };

    State state = State.Walking;        // 現在のステート.
    State nextState = State.Walking;    // 次のステート.

    public AudioClip deathSeClip;
    AudioSource deathSeAudio;


    // Use this for initialization
    void Start()
    {
        StartCoroutine("search");
        wolfMove = GetComponent<wolfMove>();
        // 初期位置を保持
        basePosition = transform.position;
        // 待機時間
        waitTime = waitBaseTime;
    }
    IEnumerator search() {
        yield return new WaitForSeconds(0.2f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
        // Update is called once per frame
        void Update()
    {

        switch (state)
        {
            case State.Walking:
                Walking();
                break;
        }

        if (state != nextState)
        {
            state = nextState;
            switch (state)
            {
                case State.Walking:
                    WalkStart();
                    break;
            }
        }
    }


    // ステートを変更する.
    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }

    void WalkStart()
    {
        StateStartCommon();
    }

    void Walking()
    {
        // 待機時間がまだあったら
        if (waitTime > 0.0f)
        {
            // 待機時間を減らす
            waitTime -= Time.deltaTime;
            // 待機時間が無くなったら
            if (waitTime <= 0.0f)
            {
                // 範囲内の何処か
                Vector2 randomValue = Random.insideUnitCircle * walkRange;
                // 移動先の設定
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, 0.0f);
                //　目的地の指定.
                
                if (Vector3.Distance(player.position, transform.position) <= searchRange)
                {
                    SendMessage("SetDestination2", destinationPosition);
                }
                else
                {
                    SendMessage("SetDestination1", destinationPosition);
                }
            }
        }
        else
        {
            // 目的地へ到着
            if (wolfMove.Arrived())
            {

                // 待機状態へ
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }
            // ターゲットを発見したら追跡
            if (attackTarget)
            {
                ChangeState(State.Chasing);
            }
        }
    }


    // 追跡開始
    void ChaseStart()
    {
        StateStartCommon();
    }
    // 追跡中
    void Chasing()
    {
        // 移動先をプレイヤーに設定
        SendMessage("SetDestination", attackTarget.position);
        // 2m以内に近づいたら攻撃
        if (Vector3.Distance(attackTarget.position, transform.position) <= 2.0f)
        {
            ChangeState(State.Attacking);
        }
    }

    // ステートが始まる前にステータスを初期化する.
    void StateStartCommon()
    {

    }

    // 攻撃対象を設定する
    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }
}