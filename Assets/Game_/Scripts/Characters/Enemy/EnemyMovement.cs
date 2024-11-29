using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : CharacterMovement
{
    [SerializeField] Enemy enemy;
    Vector3 des;


    private void Update()
    {
        if(enemy.IsDead) return;

        if (enemy.agent.velocity.magnitude < .3f)
        {
            Moving = false;
        }
        else
        {
            Moving = true;
        }
    }

    public override void StopMoving()
    {
        Moving = false;

        enemy.agent.isStopped = true;
        enemy.agent.ResetPath();

    }

    public override void Move()
    {
        if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            if (RandomPoint(transform.position, 10f, out Vector3 point))
            {
                enemy.agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
