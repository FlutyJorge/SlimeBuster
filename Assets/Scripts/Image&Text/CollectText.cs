using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectText : MonoBehaviour
{
    [Header("参照")]
    public PlayerMovement playerMov;
    public MouseLook mouseLook;
    public GunController gunCtrl;
    public SpawnController spawnCtrl;

    private float timer = 0f;
    private TextMeshProUGUI tmPro;

    // Start is called before the first frame update
    void Start()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //開始3秒でテキストを完全表示、操作可能状態にした後5秒かけてテキストをフェードアウト
        if (timer < 3)
        {
            tmPro.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timer / 3f));
        }
        else if (timer == 3)
        {
            CanMove();
            tmPro.color = new Color(1, 1, 1, 1);
        }
        else if (timer > 3 && timer < 8)
        {
            CanMove();
            tmPro.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, (timer - 3) / 5f));
        }
        else
        {
            CanMove();
            tmPro.color = new Color(0, 0, 0, 0);
            Destroy(this.gameObject);
        }
    }

    private void CanMove()
    {
        //操作可能化
        playerMov.enabled = true;
        mouseLook.enabled = true;
        gunCtrl.enabled = true;

        //スライムのスポーン開始
        spawnCtrl.enabled = true;
    }
}
