using UnityEngine;

public class IdleState : IState<Boss>
{
    private float timer = 0;
    public void OnEnter(Boss boss) 
    {
        //Debug.Log("idle");
        boss.ChangeAnimation("idle");
    }

    public void OnExecute(Boss boss) 
    {
        timer += Time.deltaTime;
        if (timer > 1f) 
        {
            //Debug.Log("run IdleState");
            boss.ChangeState(new RunningState());
        }

        boss.circle.RemoveNullTarget();
        if (boss.circle.listBoss.Count > 0 && !boss.isAttack) 
        {
            boss.ChangeState(new AttackState());
        }

    }

    public void OnExit(Boss boss) 
    {

    }
}
