using System.Collections;
using UnityEngine;

public class MovePlayer : Character
{
    public float Speed = 5f; // tốc độ di chuyển người chơi
    [SerializeField] Transform Player;
    public LayerMask Plane; // layerMask để di chuyển bằng raycast
    public int colorIndex = 0; // màu của nhân vật
    public CircleBuffet circler; // vòng tròng phát hiện đối thủ
    [SerializeField] SkinnedMeshRenderer body; //thêm màu cho nhân vật người chơi

    //attack-->
    [SerializeField] GameObject Bullet; // Vũ khí
    public GameObject taggetBoss; //boss hiện tại

    private CouterTime couter = new CouterTime(); // ngắt khi không đánh nữa

    // Start is called before the first frame update
    void Start()
    {
        colorIndex = Random.Range(0, 10);
        body.material = Color.Ins.SetMaterialInGame(colorIndex);    
    }

    public override void Oninit()
    {
        this.enabled = true;
        skin.PlayerEquipItems();
        indicator.InitTarget(UnityEngine.Color.black, 0, "Player");
        transform.localScale = new Vector3(1f, 1f, 1f);
        base.Oninit();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;

        if (direction.magnitude > 0)
        {
            couter.Cance();
            Vector3 nextPoint = transform.position + direction * Time.deltaTime * Speed;
            if (Moving(nextPoint))
            {
                transform.position = nextPoint;
            }
            Player.forward = direction;
            ChangeAnimation("run");
        }
        else if (!isAttack)
        {
            ChangeAnimation("idle");
            circle.RemoveNullTarget();
            if (circle.listBoss.Count > 0)
            {
                AttackOfPlayer();
            }
        }
        else
        {
            couter.Execute();
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
                AttackOfPlayer();
        }

    }

    public void AttackOfPlayer() 
    {
        isAttack = true;
        Invoke(nameof(ChangeIsAttack), 1.5f);
        ChangeAnimation("attack");
        couter.Start(AttackTarget, 0.5f);
    }

    public void ChangeIsAttack() 
    {
        isAttack = false;
    }

    private bool Moving(Vector3 nextPoint) 
    {
        RaycastHit hit;
        return Physics.Raycast(nextPoint + Vector3.up, Vector3.down, out hit, 4f, Plane);
    }

 

    public override void OnDeath()
    {
        couter.Cance();
        //Debug.Log("Player dead");
        this.enabled = false;
        StartCoroutine(WaitforActiveObjec());
        base.OnDeath();
    }

    IEnumerator WaitforActiveObjec() 
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
        this.indicator.gameObject.SetActive(false);
        //GetComponent<CreateBoss>().EndGame();
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
