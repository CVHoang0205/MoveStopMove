using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Character
{
    public Vector3 _destination;

    public NavMeshAgent agent;

    public IState<Boss> currentState;

    [SerializeField] GameObject poins;



    public bool isDestination => Vector3.Distance(_destination, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        Oninit();
        skin.RandomEquipItems();
        //ChangeAnimation("idle");
    }

    public override void Oninit()
    {
        ChangeState(new IdleState());
        base.Oninit();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) 
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeIsAttack() 
    {
        Invoke(nameof(ResetAttack), 1f);
    }

    public void ResetAttack() 
    {
        isAttack = false;
    }

    public void ChangeState(IState<Boss> newState) 
    {
        if (currentState != null) 
        {
            currentState.OnExit(this);
        }
        //Debug.Log(currentState + " " + newState);
        if (currentState != newState) 
        {
            currentState = newState;
            currentState.OnEnter(this);
        }
    }

    public void SetDestination(Vector3 targetVector)
    {
        agent.enabled = true;
        _destination = targetVector;
        agent.SetDestination(targetVector);
        _destination.y = 0f;
        
    }

    public override void OnDeath()
    {
        //Debug.Log("Boss chet");
        this.enabled = false;
        this.agent.enabled = false;
        StartCoroutine(isDeadDeastroyObjec());
        base.OnDeath();
    }

    public IEnumerator isDeadDeastroyObjec()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        Vector3 abc = new Vector3(0f, 1f, 0f);
        Instantiate(poins, transform.position + abc, Quaternion.LookRotation(Vector3.up));
        poins.GetComponent<Rigidbody>().AddTorque(Vector3.up * 500f);
        CreateBoss.Ins.bots.Remove(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coint"))
        {
            Debug.Log("Da An Coint");
            Destroy(other.gameObject);
            CreateBoss.Ins.GainGold(1);
            UIManager.Ins.InitGold();
        }
    }



}
