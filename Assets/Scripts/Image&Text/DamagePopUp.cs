using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public float lifeTime = 0.6f;
    public float minDist = 0f;
    public float maxDist = 0f;

    [HideInInspector] public TextMeshPro tmPro;
    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    private void Awake()
    {
        tmPro = GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //テキストを飛ばす方向
        float direction = Random.Range(-30, 120);
        //テキストが飛ぶ距離
        float dist = Random.Range(minDist, maxDist);

        //テキストの目的地を決定
        iniPos = transform.position;
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //テキストをカメラの向きに固定
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        timer += Time.deltaTime;
        float fraction = lifeTime / 2f;

        //テキストの破壊、フェードイン
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else if (timer > fraction)
        {
            tmPro.color = Color.Lerp(tmPro.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
        }

        //テキストの位置と大きさを時間経過により変更
        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));

        //SetDamageText();
    }

    //スライムが被ダメしたら呼ばれる
    public void SetDamageText(int damage)
    {
        tmPro.SetText(damage.ToString());
    }
}
