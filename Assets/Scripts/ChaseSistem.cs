using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSistem : MonoBehaviour
{
    //参照
    public GameObject player;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの位置情報を取得
        agent.destination = player.transform.position;
    }
}
