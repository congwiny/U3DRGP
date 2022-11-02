using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyStates enemyStates;

    [Header("Basic Settings")]
    public float sightRadius;

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
                return true;
            }
        }
        return false;
    }
}
