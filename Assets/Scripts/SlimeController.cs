using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SlimeController : MonoBehaviour
{
    [Header("reference")]
    public GameObject damageText;
    public GameObject slimeBody;
    public GameObject heart;
    public Face faces;
    public PointText pointTxScript;

    [Header("else")]
    public float attackDamage;
    public float slimeHP = 50f;

    private GameObject target;
    private Animator anim;
    private NavMeshAgent agent;
    private Material faceMaterial;
    private Vector3 instantiatePos;
    private Vector3 prefabPos;
    private PlayerStatus playerSta;
    private AudioSource audioSource;
    private bool plusPoint;
    private bool dropHeart = false;

    //列挙型
    enum State {Idle, Wander, Attack, Chase, Dead};
    State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        playerSta = target.GetComponent<PlayerStatus>();
        pointTxScript = GameObject.Find("Point").GetComponent<PointText>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        faceMaterial = slimeBody.GetComponent<Renderer>().materials[1];
        audioSource = GetComponent<AudioSource>();
        plusPoint = true;
    }

    // Update is called once per frame
    void Update()
    {

        instantiatePos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        prefabPos = this.transform.position + new Vector3(0f, 0.5f, 0f);

        switch (state)
        {
            case State.Idle:

                TurnOffTrigger();
                SetFace(faces.Idleface);

                if (CanSeePlayer())
                {
                    state = State.Chase;
                }
                else if (Random.Range(0, 5000) < 5)
                {
                    //一定の確率でWanderに移行
                    state = State.Wander;
                }

                break;

            case State.Wander:

                if (!agent.hasPath)
                {
                    float newX = transform.position.x + Random.Range(-5, 5);
                    float newZ = transform.position.z + Random.Range(-5, 5);
                    Vector3 NextPos = new Vector3(newX, transform.position.y, newZ);

                    agent.SetDestination(NextPos);
                    agent.stoppingDistance = 0;

                    TurnOffTrigger();
                    SetFace(faces.WalkFace);

                    anim.SetBool("walk", true);
                }

                if (Random.Range(0, 5000) < 5)
                {
                    state = State.Idle;
                    agent.ResetPath();
                }

                if (CanSeePlayer())
                {
                    state = State.Chase;
                }

                break;

            case State.Chase:

                if (GManager.instance.gameOver)
                {
                    TurnOffTrigger();
                    agent.ResetPath();
                    state = State.Wander;
                    return;
                }

                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 1;

                TurnOffTrigger();
                SetFace(faces.WalkFace);

                anim.SetBool("walk", true);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = State.Attack;
                }

                if (ForGetPlayer())
                {
                    agent.ResetPath();
                    state = State.Wander;
                }

                break;

            case State.Attack:

                if (GManager.instance.gameOver)
                {
                    TurnOffTrigger();
                    agent.ResetPath();
                    state = State.Wander;
                    return;
                }
                //ゲームオーバーではない場合
                TurnOffTrigger();
                SetFace(faces.attackFace);
                anim.SetBool("attack", true);
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

                if (DistanceToPlayer() > agent.stoppingDistance + 2)
                {
                    state = State.Chase;
                }

                break;

            case State.Dead:

                Invoke("SlimeDestroy", 1.4f);

                break;
        }
    }

    //アニメーションフラグの無効化
    public void TurnOffTrigger()
    {
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
        anim.SetBool("damage1", false);
        anim.SetBool("damage2", false);
    }

    //プレイヤーとの距離を測定
    private float DistanceToPlayer()
    {
        if (GManager.instance.gameOver)
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(target.transform.position, transform.position);
    }

    //プレイヤーが追跡範囲内にいるか
    private bool CanSeePlayer()
    {
        if (DistanceToPlayer() < 15)
        {
            return true;
        }
        return false;
    }

    //プレイヤーが追跡範囲外にいるか
    private bool ForGetPlayer()
    {
        if (DistanceToPlayer() > 20)
        {
            return true;
        }
        return false;
    }

    //ダメージ付与
    public void DamagePlayer()
    {
        if (target != null)
        {
            target.GetComponent<PlayerMovement>().TakeHit(attackDamage);
        }
    }

    //スライム被ダメージ処理
    public void SlimeDamage(int damage)
    {
        slimeHP -= damage;
        audioSource.Play();

        //ダメージテキストを生成
        DamagePopUp damagePU = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamagePopUp>();
        damagePU.SetDamageText(damage);

        //もし敵HPが0になったら
        if (slimeHP <= 0f)
        {
            //敵の死亡
            SlimeDead();
        }
    }

    private void SlimeDead()
    {
        TurnOffTrigger();
        SetFace(faces.damageFace);
        anim.SetBool("damage2", true);

        //ポイントの加算
        if (plusPoint)
        {
            playerSta.point += 10;
            pointTxScript.pointTx.SetText("Point " + playerSta.point.ToString());
            plusPoint = false;
        }

        //回復アイテムを20%で落とす
        if (Probability(20) && dropHeart)
        {
            Debug.Log("実行");
            Instantiate(heart, prefabPos, Quaternion.identity);
        }
        state = State.Dead;
    }

    private void SlimeDestroy()
    {
        Destroy(this.gameObject);
    }

    //スキン(EnemyAi参考)
    private void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    //アニメーション挙動の修正
    void OnAnimatorMove()
    {
        // apply root motion to AI
        Vector3 position = anim.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }

    //回復アイテムドロップ用
    private bool Probability(float percent)
    {
        int number = UnityEngine.Random.Range(0, 100);

        if (percent >= number && !dropHeart)
        {
            Debug.Log("true");
            dropHeart = true;
            return true;
        }
        else if (!dropHeart)
        {
            dropHeart = true;
            return false;
        }
        else
        {
            dropHeart = true;
            return false;
        }
    }
}
