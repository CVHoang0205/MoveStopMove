using UnityEngine;
using UnityEngine.AI;

public class RunningState : IState<Boss>
{
    public void OnEnter(Boss boss) 
    {
        //Debug.Log("running RunningState");
        boss.ChangeAnimation("run");
        boss.SetDestination(SeekTarget());
        //Debug.Log(SeekTarget());
    }
   public void OnExecute(Boss boss) 
    {
        if (boss.isDestination) 
        {
            boss.ChangeState(new IdleState());
        }
    }
   public void OnExit(Boss boss) 
    {

    }

    public Vector3 SeekTarget() 
    {
        for (int i = 0; i < 30; i++) 
        {
            Vector3 Newpoint = new Vector3(Random.Range(40, -40), 0f, Random.Range(35f, -35f));
            NavMeshHit hit;
            if (NavMesh.SamplePosition(Newpoint, out hit, 5f, NavMesh.AllAreas)) 
            {
                return hit.position;
            }
            else 
            {
                //Debug.Log("vị trí không hợp lệ");
            }
        }
        return Vector3.zero;
        //return new Vector3(Random.Range(40, -40), 0f, Random.Range(35f, -35f)); 
    }
}
