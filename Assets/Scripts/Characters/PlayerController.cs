using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        MouseManager.Instance.onMouseClicked += MoveToTarget;
    }

    void Update()
    {
        SwitchAnimation();
    }

    //�л������ܶ��Ķ���
    private void SwitchAnimation() 
    {
        //Vector3����ֵת����һ�������ֵͨ��sqrMagnitude
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }
}
