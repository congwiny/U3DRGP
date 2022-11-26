using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyStates;

    //������Ѳ�ߵĵ��˻���վ׮�ĵ���
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
        //�������Player���л���׷����״̬
        if (FoundPlayer())
        { 
            enemyStates = EnemyStates.CHASE;
            Debug.Log("�ҵ�Player...");
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
                if(!FoundPlayer())
                {
                    //TODO:���ѻص���һ��״̬
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
