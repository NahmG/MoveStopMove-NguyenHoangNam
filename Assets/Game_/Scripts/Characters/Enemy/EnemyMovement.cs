using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : CharacterMovement
{
    [SerializeField] Enemy enemy;

    public override void StopMoving()
    {
        enemy.agent.isStopped = true;
        enemy.agent.ResetPath();
    }

    public override void Move()
    {
        if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            if (RandomPoint(enemy.Tf.position, 10f, out Vector3 point))
            {
                enemy.agent.SetDestination(point);
            }
        }
    }

    public bool IsReachDestination()
    {
        float dis = enemy.agent.remainingDistance;
        return dis > 0 && dis < 0.1f;
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
