using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    //攻击的敌人
    private GameObject attackTarget;
    //上次攻击的时间点（用于攻击时间的冷却）
    private float lastAttackTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        MouseManager.Instance.onMouseClicked += MoveToTarget;
        MouseManager.Instance.OnEnemyClicked += EventAttack;
    }

    void Update()
    {
        SwitchAnimation();
        //衰减攻击时间
        lastAttackTime -= Time.deltaTime;
    }

    //切换人物跑动的动画
    private void SwitchAnimation() 
    {
        //Vector3的数值转换成一个浮点的值通过sqrMagnitude
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        //停止所有的协程
        StopAllCoroutines();
        //还原agent可移动的状态
        agent.isStopped = false;
        agent.destination = target;
    }

    private void EventAttack(GameObject target)
    {
        //判断攻击的目标是存在的（敌人有可能死亡了，就不存在了）
        if (target != null) 
        {
            //给attackTarget 赋值
            attackTarget = target;
            //启动协程
            StartCoroutine(MoveToAttackTarget());
        }


    }

    //不断的循环去判断攻击的目标和当前Player的距离
    //如果两者的距离大于攻击距离的话，就让人物不断移动到目标位置
    //使用协程，while循环去判断
    IEnumerator MoveToAttackTarget()
    {
        //确保人物是可以移动的
        agent.isStopped = false;
        //让人物转向攻击的目标
        transform.LookAt(attackTarget.transform);
        //判断两者之间的距离（没有使用带有攻击距离的武器，暂时先用1）
        while (Vector3.Distance(attackTarget.transform.position,transform.position)>1)
        {
            agent.destination = attackTarget.transform.position;
            //return null 表示下一帧再次执行上面的命令
            yield return null;
        }
        //让人物停下来
        agent.isStopped = true;
        //判断上次攻击的时间
        if (lastAttackTime<0)
        {
            anim.SetTrigger("Attack");
            //重置冷却时间
            lastAttackTime = 0.5f;
        }
      
    }
}
