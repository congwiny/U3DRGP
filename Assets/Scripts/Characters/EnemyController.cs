using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyStates;

    //设置是巡逻的敌人还是站桩的敌人
    public bool isGuard;

    [Header("Basic Settings")]
    public float sightRadius;

    private GameObject attackTarget;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        //如果发现Player，切换到追击的状态
        if (FoundPlayer())
        { 
            enemyStates = EnemyStates.CHASE;
            Debug.Log("找到Player...");
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
                if(!FoundPlayer())
                {
                    //TODO:拉脱回到上一个状态
                }
                else
                {
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
