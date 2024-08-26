using UnityEngine;

public class AttackState : IState<Boss>
{
    float timer = 0;
    public void OnEnter(Boss boss) 
    {
        boss.ChangeAnimation("attack");
        boss.isAttack = true;
        boss.ChangeIsAttack();
    }

    public void OnExecute(Boss boss)
    {
        timer+= Time.deltaTime;
        if(timer > 0.5f) 
        {
            boss.circle.RemoveNullTarget();
            if(boss.circle.listBoss.Count > 0) 
            {
                boss.AttackTarget();
            }
            boss.ChangeState(new IdleState());
        }
    }

    public void OnExit(Boss boss)
    {

    }
}
