using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    //�����ĵ���
    private GameObject attackTarget;
    //�ϴι�����ʱ��㣨���ڹ���ʱ�����ȴ��
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
        //˥������ʱ��
        lastAttackTime -= Time.deltaTime;
    }

    //�л������ܶ��Ķ���
    private void SwitchAnimation() 
    {
        //Vector3����ֵת����һ�������ֵͨ��sqrMagnitude
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void MoveToTarget(Vector3 target)
    {
        //ֹͣ���е�Э��
        StopAllCoroutines();
        //��ԭagent���ƶ���״̬
        agent.isStopped = false;
        agent.destination = target;
    }

    private void EventAttack(GameObject target)
    {
        //�жϹ�����Ŀ���Ǵ��ڵģ������п��������ˣ��Ͳ������ˣ�
        if (target != null) 
        {
            //��attackTarget ��ֵ
            attackTarget = target;
            //����Э��
            StartCoroutine(MoveToAttackTarget());
        }


    }

    //���ϵ�ѭ��ȥ�жϹ�����Ŀ��͵�ǰPlayer�ľ���
    //������ߵľ�����ڹ�������Ļ����������ﲻ���ƶ���Ŀ��λ��
    //ʹ��Э�̣�whileѭ��ȥ�ж�
    IEnumerator MoveToAttackTarget()
    {
        //ȷ�������ǿ����ƶ���
        agent.isStopped = false;
        //������ת�򹥻���Ŀ��
        transform.LookAt(attackTarget.transform);
        //�ж�����֮��ľ��루û��ʹ�ô��й����������������ʱ����1��
        while (Vector3.Distance(attackTarget.transform.position,transform.position)>1)
        {
            agent.destination = attackTarget.transform.position;
            //return null ��ʾ��һ֡�ٴ�ִ�����������
            yield return null;
        }
        //������ͣ����
        agent.isStopped = true;
        //�ж��ϴι�����ʱ��
        if (lastAttackTime<0)
        {
            anim.SetTrigger("Attack");
            //������ȴʱ��
            lastAttackTime = 0.5f;
        }
      
    }
}
