using UnityEngine;


public class Character : AbstracCharacter
{
    public Animator animator;
    private string currentAnimation = "idle";
    public CircleBuffet circle;
    public Bullet BulletPrefabs;
    //public Transform bulletWeapon;
    public bool isAttack = false;
    public Transform RightHand;
    public TargetIndicator indicator;
    public int level = 0;
    public InitSkin skin;
    public itermDtabase ic;

    public override void Oninit() 
    {
        level= 0;
    }

    public override void OnAttack()
    {
        AttackTarget();
    }

    public override void OnDeath()
    {
        ChangeAnimation("dead");
        indicator.gameObject.SetActive(false);
    }

    public void UpdateLevel() 
    {
        level++;
        indicator.InitTarget(level);
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        //Debug.Log("tang diem");
    }

    public void AttackTarget() 
    {
        circle.RemoveNullTarget();
        if (circle.listBoss.Count > 0) 
        {

            Bullet bullet = Instantiate(BulletPrefabs);
            bullet.transform.position = transform.position + Vector3.up * 1;
            Vector3 direction = (circle.GetNearTarget().position - transform.position).normalized;
            bullet.Shooter = this.gameObject; 
            bullet.transform.forward = direction;
            transform.forward = bullet.transform.position;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
            bullet.GetComponent<Rigidbody>().AddTorque(Vector3.up * 500f);
            transform.forward = direction;
        }
    }

    public void ChangeAnimation(string animName) 
    {
        if (currentAnimation != animName) 
        {
            animator.ResetTrigger(animName);
            currentAnimation = animName;
            animator.SetTrigger(currentAnimation);
        }

    }
}
