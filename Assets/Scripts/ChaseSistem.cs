using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSistem : MonoBehaviour
{
    //�Q��
    public GameObject player;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̈ʒu�����擾
        agent.destination = player.transform.position;
    }
}
