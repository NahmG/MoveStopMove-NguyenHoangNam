public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.movement.StopMoving();
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.Attack();

        if(enemy.attacker.Target == null)
        {
            enemy.ChangeState(new MoveState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }

}
