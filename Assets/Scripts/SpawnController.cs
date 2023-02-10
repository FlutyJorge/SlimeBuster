using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("�G���A�ʂ̃X���C��")]
    public GameObject enemy0;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    [Space(20)]

    public AreaManager aM;
    //�G���A�ƃX�|�[���ԍ��̓񎟌��z��
    public GameObject[ , ] Spawner;

    private GameObject[] tagObject;
    private int spawnerIndex;
    private float worldTimer;
    private int[] SpawnInterval;
    private bool[ , ] ReadyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //�����X�|�[���ɕK�v�ȗv�f��z��Ɋi�[

        //�X�|�i�[
        Spawner = new GameObject[4, 6];
        for (spawnerIndex = 0; spawnerIndex <= 5; spawnerIndex++)
        {
            Spawner[0, spawnerIndex] = GameObject.Find("A0Spawner" + spawnerIndex);
            Spawner[1, spawnerIndex] = GameObject.Find("A1Spawner" + spawnerIndex);
            Spawner[2, spawnerIndex] = GameObject.Find("A2Spawner" + spawnerIndex);
            Spawner[3, spawnerIndex] = GameObject.Find("A3Spawner" + spawnerIndex);
        }

        //�X�|�[���t���O�B�@����15�b�A30�b�A39�b
        ReadyToSpawn = new bool[4, 3] {{true, true, true}, {true, true, true}, {true, true, true}, {true, true, true}};

        //�X�|�[���̃C���^�[�o��3���
        SpawnInterval = new int[] {5, 10, 15};

        //���[���h�^�C�}�[�̏�����
        worldTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //���[���h�^�C�}�[�����Z
        worldTimer = worldTimer + 1f * Time.deltaTime;

        //�G���A�ʂɃX�|�[����؂�ւ�
        if (aM.onArea[0])
        {
            StartCoroutine(StartSpawn(0, 1, 0, 0, enemy0, "Enemy0"));
            StartCoroutine(StartSpawn(2, 3, 0, 1, enemy0, "Enemy0"));
            StartCoroutine(StartSpawn(4, 5, 0, 2, enemy0, "Enemy0"));
        }
        else if (aM.onArea[1])
        {
            StartCoroutine(StartSpawn(0, 1, 1, 0, enemy1, "Enemy1"));
            StartCoroutine(StartSpawn(2, 3, 1, 1, enemy1, "Enemy1"));
            StartCoroutine(StartSpawn(4, 5, 1, 2, enemy1, "Enemy1"));
        }
        else if (aM.onArea[2])
        {
            StartCoroutine(StartSpawn(0, 1, 2, 0, enemy2, "Enemy2"));
            StartCoroutine(StartSpawn(2, 3, 2, 1, enemy2, "Enemy2"));
            StartCoroutine(StartSpawn(4, 5, 2, 2, enemy2, "Enemy2"));
        }
        else if (aM.onArea[3])
        {
            StartCoroutine(StartSpawn(0, 1, 3, 0, enemy3, "Enemy3"));
            StartCoroutine(StartSpawn(2, 3, 3, 1, enemy3, "Enemy3"));
            StartCoroutine(StartSpawn(4, 5, 3, 2, enemy3, "Enemy3"));
        }
    }

    //����:sIFrom����isTo�܂ł̃X�|�[���n�_�̃C���f�b�N�X, �G���A�ԍ�, �C���^�[�o������
    private IEnumerator StartSpawn(int sIFrom, int sITo, int areaNum, int intervalNum, GameObject enemy, string tagName)
    {
        //1.�t���O��true�Ȃ�X�|�[�����J�n
        if (ReadyToSpawn[areaNum, intervalNum])
        {
            StartCoroutine(StopSpawn(sIFrom, sITo, areaNum, intervalNum, enemy, tagName));
            //4.�C���^�[�o���̊ԂɃt���O��false��
            ReadyToSpawn[areaNum, intervalNum] = false;
        }
        yield return null;
    }

    private IEnumerator StopSpawn(int sIFrom, int sITo, int areaNum, int intervalNum, GameObject enemy, string tagName)
    {
        //2.�X�|�[���n�_���C���f�b�N�X�ɂ�蕡���w��
        for (spawnerIndex = sIFrom; spawnerIndex <= sITo; spawnerIndex ++)
        {
            //3.�G�l�~�[���X�|�[��
            tagObject = GameObject.FindGameObjectsWithTag(tagName);
            if (tagObject.Length < 8)
            {
                Instantiate(enemy, Spawner[areaNum, spawnerIndex].transform.position, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(SpawnInterval[intervalNum]);
        //5.�C���^�[�o����A�t���O��true�ɂ��邱�ƂŃX�|�[���ĊJ
        ReadyToSpawn[areaNum, intervalNum] = true;
    }
}
