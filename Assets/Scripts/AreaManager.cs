using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class AreaManager : MonoBehaviour
{
    //インデックスが0の時、Area0を示す
    [HideInInspector] public bool[] onArea = new bool[4];
    public float vfxDeadTime = 4f;
    public PlayerStatus playerSta;
    public PointText pointTxScript;
    public GameObject areaTx;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;

        //エリア判定用フラグを配列に格納
        HoldAreaFrag();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //エリアの判定
    private void OnTriggerEnter(Collider other)
    {
        //エリア0～1間
        if (other.name == "B/w0and1")
        {
            if (onArea[0])
            {
                onArea[0] = false;
                onArea[1] = true;
                Debug.Log("エリア1です");
            }
            else if (!onArea[0])
            {
                onArea[1] = false;
                onArea[0] = true;
                Debug.Log("エリア0です");
            }
        }

        //エリア0~2間
        if (other.name == "B/w0and2(0)" || other.name == "B/w0and2(1)" || other.name == "B/w0and2(2)")
        {
            if (onArea[0])
            {
                onArea[0] = false;
                onArea[2] = true;
                Debug.Log("エリア2です");
            }
            else if (!onArea[0])
            {
                onArea[2] = false;
                onArea[0] = true;
                Debug.Log("エリア0です");
            }
        }

        //エリア0～3間
        if (other.name == "B/w0and3")
        {
            if (onArea[0])
            {
                onArea[0] = false;
                onArea[3] = true;
                Debug.Log("エリア3です");
            }
            else if (!onArea[0])
            {
                onArea[3] = false;
                onArea[0] = true;
                Debug.Log("エリア0です");
            }
        }

        //エリア1～3間
        if (other.name == "B/w1and3")
        {
            if (onArea[1])
            {
                onArea[1] = false;
                onArea[3] = true;
                Debug.Log("エリア3です");
            }
            else if (!onArea[1])
            {
                onArea[3] = false;
                onArea[1] = true;
                Debug.Log("エリア1です");
            }
        }

        //エリア2～3間
        if (other.name == "B/w2and3")
        {
            if (onArea[2])
            {
                onArea[2] = false;
                onArea[3] = true;
                Debug.Log("エリア3です");
            }
            else if (!onArea[1])
            {
                onArea[3] = false;
                onArea[2] = true;
                Debug.Log("エリア2です");
            }
        }
    }

    //エリア解放テキストの表示＆エリア解放
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "AW0and1")
        {
            OpenArea(200, other);
        }
        else if (other.name == "AW0and2(0)")
        {
            OpenArea(200, other);
        }
        else if (other.name == "AW0and2(1)")
        {
            OpenArea(100, other);
        }
        else if (other.name == "AW0and2(2)")
        {
            OpenArea(100, other);
        }
        else if (other.name == "AW0and3")
        {
            OpenArea(200, other);
        }
        else if (other.name == "AW1and3")
        {
            OpenArea(200, other);
        }
        else if (other.name == "AW2and3")
        {
            OpenArea(200, other);
        }
    }

    //エリア開放テキスト表示範囲から出たとき
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "AreaWall")
        {
            text.enabled = false;
        }
    }

    //
    public void OpenArea(int necespoint, Collider other)
    {
        text.enabled = true;
        text.SetText(necespoint.ToString() + "point\nPress E");

        //エリア開放条件の確認＆解放
        if (playerSta.point >= necespoint && Input.GetKeyDown(KeyCode.E))
        {
            Collider[] boxCol;
            boxCol = other.GetComponents<Collider>();

            playerSta.point -= necespoint;
            pointTxScript.pointTx.SetText("Point " + playerSta.point.ToString());

            boxCol[0].enabled = false;
            boxCol[1].enabled = false;
            text.enabled = false;
        }
    }

    private void HoldAreaFrag()
    {
        for (int i = 0; i < onArea.Length; i++)
        {
            onArea[i] = false;
        }

        //初期段階はエリア0にいる
        onArea[0] = true;
    }
}
