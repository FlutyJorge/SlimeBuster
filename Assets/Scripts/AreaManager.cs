using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

public class AreaManager : MonoBehaviour
{
    //�C���f�b�N�X��0�̎��AArea0������
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

        //�G���A����p�t���O��z��Ɋi�[
        HoldAreaFrag();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�G���A�̔���
    private void OnTriggerEnter(Collider other)
    {
        //�G���A0�`1��
        if (other.name == "B/w0and1")
        {
            if (onArea[0])
            {
                onArea[0] = false;
                onArea[1] = true;
                Debug.Log("�G���A1�ł�");
            }
            else if (!onArea[0])
            {
                onArea[1] = false;
                onArea[0] = true;
                Debug.Log("�G���A0�ł�");
            }
        }

        //�G���A0~2��
        if (other.name == "B/w0and2(0)" || other.name == "B/w0and2(1)" || other.name == "B/w0and2(2)")
        {
            if (onArea[0])
            {
                onArea[0] = false;
                onArea[2] = true;
                Debug.Log("�G���A2�ł�");
            }
            else if (!onArea[0])
            {
                onArea[2] = false;
                onArea[0] = true;
                Debug.Log("�G���A0�ł�");
            }
        }

        //�G���A0�`3��
        if (other.name == "B/w0and3")
        {
            if (onArea[0])
            {
                onArea[0] = false;
                onArea[3] = true;
                Debug.Log("�G���A3�ł�");
            }
            else if (!onArea[0])
            {
                onArea[3] = false;
                onArea[0] = true;
                Debug.Log("�G���A0�ł�");
            }
        }

        //�G���A1�`3��
        if (other.name == "B/w1and3")
        {
            if (onArea[1])
            {
                onArea[1] = false;
                onArea[3] = true;
                Debug.Log("�G���A3�ł�");
            }
            else if (!onArea[1])
            {
                onArea[3] = false;
                onArea[1] = true;
                Debug.Log("�G���A1�ł�");
            }
        }

        //�G���A2�`3��
        if (other.name == "B/w2and3")
        {
            if (onArea[2])
            {
                onArea[2] = false;
                onArea[3] = true;
                Debug.Log("�G���A3�ł�");
            }
            else if (!onArea[1])
            {
                onArea[3] = false;
                onArea[2] = true;
                Debug.Log("�G���A2�ł�");
            }
        }
    }

    //�G���A����e�L�X�g�̕\�����G���A���
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

    //�G���A�J���e�L�X�g�\���͈͂���o���Ƃ�
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

        //�G���A�J�������̊m�F�����
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

        //�����i�K�̓G���A0�ɂ���
        onArea[0] = true;
    }
}
