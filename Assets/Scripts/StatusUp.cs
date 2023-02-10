using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusUp : MonoBehaviour
{
    [Header("objects&scriupts")]
    public GameObject succeSlider;
    public GameObject completeTx;
    public Slider slider;
    public PlayerStatus playerSta;
    public SlimeController slimeCtrl;
    public GunController gunCtrl;
    public PointText pointTxScript;

    [Header("Text")]
    public GameObject defenceText;
    public GameObject attackText;
    public GameObject magazineText;

    [Header("else")]
    [HideInInspector] public bool isUpdating = false;

    private BoxCollider boxCol;
    private TextMeshPro txt;
    private int defenceLimit = 0;
    private float timer = 0f;
    private float completeTime = 5f;
    private bool complete = false;
    private bool isRunning = false;
    private bool defenceArea = false, attackArea = false, magazineArea = false;

    // Update is called once per frame
    void Update()
    {
        if (isUpdating)
        {
            //時間経過でスライダーを満たす
            timer += Time.deltaTime;
            slider.value = Mathf.Lerp(0, 100, timer / completeTime);
        }

        if (slider.value == 100)
        {
            //スライダーが100%になったらフラグを立てる
            slider.value = 0;
            complete = true;
        }
    }

    //ステータスアップアイテムに近づいたらテキストを表示
    private void OnTriggerStay(Collider other)
    {
        //強化項目別に判定
        if (other.gameObject.name == "Defence")
        {
            if (defenceLimit < 5)
            {
                Strengthen(defenceText, 100f);
                defenceArea = true;
            }
            else if (defenceLimit == 5)
            {
                TextMeshProUGUI leveMaxTx = defenceText.GetComponent<TextMeshProUGUI>();
                leveMaxTx.SetText("Max Level!");
                Strengthen(defenceText, Mathf.Infinity);
                defenceArea = true;
            }
        }
        else if (other.gameObject.name == "Attack")
        {
            Strengthen(attackText, 100f);
            attackArea = true;
        }
        else if (other.gameObject.name == "Magazine")
        {
            Strengthen(magazineText, 100f);
            magazineArea = true;
        }

        //コンプリートフラグが立ったらコルーチンスタート
        if (complete == true)
        {
            Debug.Log("complete");
            StartCoroutine("CompleteUpdate");
            complete = false;
        }
    }

    //テキスト表示エリアから出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Defence")
        {
            defenceText.SetActive(false);
            succeSlider.SetActive(false);
            defenceArea = false;
        }
        else if (other.gameObject.name == "Attack")
        {
            attackText.SetActive(false);
            succeSlider.SetActive(false);
            attackArea = false;
        }
        else if (other.gameObject.name == "Magazine")
        {
            magazineText.SetActive(false);
            succeSlider.SetActive(false);
            magazineArea = false;
        }
    }

    private void Strengthen(GameObject itemText, float neccesPoint)
    {
        //もし強化中でなければテキストを表示
        if (!isRunning)
        {
            itemText.SetActive(true);
        }

        //もし強化中ならスライダーを表示
        if (Input.GetKey(KeyCode.E) && !isRunning && playerSta.point >= neccesPoint)
        {
            succeSlider.SetActive(true);
            isUpdating = true;
        }
        else
        {
            isUpdating = false;
            slider.value = 0;
            succeSlider.SetActive(false);
            timer = 0f;
        }
    }

    IEnumerator CompleteUpdate()
    {
        //コルーチンの重複を防ぐ
        if (isRunning)
        {
            yield break;
        }
        isRunning = true;

        //強化項目ごとに処理を分ける
        if (defenceArea && defenceLimit < 5)
        {
            slimeCtrl.attackDamage -= 0.2f;
            playerSta.point -= 100;
            defenceLimit++;
            pointTxScript.pointTx.SetText("Point " + playerSta.point.ToString());
            defenceText.SetActive(false);
        }
        else if (attackArea)
        {
            playerSta.damage += 1;
            playerSta.point -= 100;
            pointTxScript.pointTx.SetText("Point " + playerSta.point.ToString());
            attackText.SetActive(false);
        }
        else if (magazineArea)
        {
            gunCtrl.magazineSize += 5;
            playerSta.point -= 100;
            pointTxScript.pointTx.SetText("Point " + playerSta.point.ToString());
            magazineText.SetActive(false);
        }

        succeSlider.SetActive(false);
        completeTx.SetActive(true);

        yield return new WaitForSeconds(2f);

        //2秒後成功テキスト表示
        completeTx.SetActive(false);
        isRunning = false;
        yield break;
    }
}
