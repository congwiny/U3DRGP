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

    //切换人物跑动的动画
    private void SwitchAnimation() 
    {
        //Vector3的数值转换成一个浮点的值通过sqrMagnitude
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }
}
