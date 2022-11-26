using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyStates;

    private Animator animator;

    //设置是巡逻的敌人还是站桩的敌人
    public bool isGuard;

    [Header("Basic Settings")]
    public float sightRadius;

    //敌人最初始的移动速度，如果不是追击状态，则敌人的速度是此值的一半
    private float originSpeed;

    private GameObject attackTarget;

    //bool值配合动画的转换
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
        //如果发现Player，切换到追击的状态
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
                //TODO:追Player

                //TODO:在攻击范围内则攻击
                //TODO:配合动画
                isWalk = false;
                isChase = true;
                agent.speed = originSpeed;
                if (!FoundPlayer())
                {
                    //TODO:拉脱回到上一个状态
                    isFollow = false;
                    //解决拉脱有延迟的问题，设置agent.destination为当前的坐标就可以了
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


    //查找周围的Player
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
