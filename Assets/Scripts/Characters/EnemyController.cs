using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyStates;

    private Animator animator;

    //������Ѳ�ߵĵ��˻���վ׮�ĵ���
    public bool isGuard;

    [Header("Basic Settings")]
    public float sightRadius;

    //�������ʼ���ƶ��ٶȣ��������׷��״̬������˵��ٶ��Ǵ�ֵ��һ��
    private float originSpeed;

    private GameObject attackTarget;

    //boolֵ��϶�����ת��
    private bool isWalk;
    private bool isChase;
    private bool isFollow;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        originSpeed = agent.speed;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SwitchStates();
        SwitchAnimation();
    }

    void SwitchAnimation()
    {
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Chase", isChase);
        animator.SetBool("Follow", isFollow);
    }

    void SwitchStates()
    {
        //�������Player���л���׷����״̬
        if (FoundPlayer())
        {
            enemyStates = EnemyStates.CHASE;
        }
        
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                break;
            case EnemyStates.CHASE:
                //TODO:׷Player

                //TODO:�ڹ�����Χ���򹥻�
                //TODO:��϶���
                isWalk = false;
                isChase = true;
                agent.speed = originSpeed;
                if (!FoundPlayer())
                {
                    //TODO:���ѻص���һ��״̬
                    isFollow = false;
                    //����������ӳٵ����⣬����agent.destinationΪ��ǰ������Ϳ�����
                    agent.destination = transform.position;
                }
                else
                {
                    isFollow = true;
                    agent.destination = attackTarget.transform.position;
                }
                break;
            case EnemyStates.DEAD:
                break;
        }
    }


    //������Χ��Player
    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }
}
