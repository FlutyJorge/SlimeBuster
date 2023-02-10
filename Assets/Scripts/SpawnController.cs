using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("エリア別のスライム")]
    public GameObject enemy0;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    [Space(20)]

    public AreaManager aM;
    //エリアとスポーン番号の二次元配列
    public GameObject[ , ] Spawner;

    private GameObject[] tagObject;
    private int spawnerIndex;
    private float worldTimer;
    private int[] SpawnInterval;
    private bool[ , ] ReadyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //書くスポーンに必要な要素を配列に格納

        //スポナー
        Spawner = new GameObject[4, 6];
        for (spawnerIndex = 0; spawnerIndex <= 5; spawnerIndex++)
        {
            Spawner[0, spawnerIndex] = GameObject.Find("A0Spawner" + spawnerIndex);
            Spawner[1, spawnerIndex] = GameObject.Find("A1Spawner" + spawnerIndex);
            Spawner[2, spawnerIndex] = GameObject.Find("A2Spawner" + spawnerIndex);
            Spawner[3, spawnerIndex] = GameObject.Find("A3Spawner" + spawnerIndex);
        }

        //スポーンフラグ。　順に15秒、30秒、39秒
        ReadyToSpawn = new bool[4, 3] {{true, true, true}, {true, true, true}, {true, true, true}, {true, true, true}};

        //スポーンのインターバル3種類
        SpawnInterval = new int[] {5, 10, 15};

        //ワールドタイマーの初期化
        worldTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //ワールドタイマーを加算
        worldTimer = worldTimer + 1f * Time.deltaTime;

        //エリア別にスポーンを切り替え
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

    //引数:sIFromからisToまでのスポーン地点のインデックス, エリア番号, インターバル時間
    private IEnumerator StartSpawn(int sIFrom, int sITo, int areaNum, int intervalNum, GameObject enemy, string tagName)
    {
        //1.フラグがtrueならスポーンを開始
        if (ReadyToSpawn[areaNum, intervalNum])
        {
            StartCoroutine(StopSpawn(sIFrom, sITo, areaNum, intervalNum, enemy, tagName));
            //4.インターバルの間にフラグをfalseに
            ReadyToSpawn[areaNum, intervalNum] = false;
        }
        yield return null;
    }

    private IEnumerator StopSpawn(int sIFrom, int sITo, int areaNum, int intervalNum, GameObject enemy, string tagName)
    {
        //2.スポーン地点をインデックスにより複数指定
        for (spawnerIndex = sIFrom; spawnerIndex <= sITo; spawnerIndex ++)
        {
            //3.エネミーをスポーン
            tagObject = GameObject.FindGameObjectsWithTag(tagName);
            if (tagObject.Length < 8)
            {
                Instantiate(enemy, Spawner[areaNum, spawnerIndex].transform.position, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(SpawnInterval[intervalNum]);
        //5.インターバル後、フラグをtrueにすることでスポーン再開
        ReadyToSpawn[areaNum, intervalNum] = true;
    }
}
