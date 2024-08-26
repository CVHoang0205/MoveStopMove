using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Shooter;
    public Transform weapons;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void InitBullet(int id) 
    {
        GameObject weapon = itermDtabase.Ins.GetWeapon(id);
        Instantiate(weapon, weapons);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1.5f + 0.05f * Shooter.GetComponent<Character>().level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Shooter)
            return;

        if (other.tag == "boss") 
        {
            //Debug.Log("Bắn trúng boss");
            other.GetComponent<Character>().OnDeath();
            Shooter.GetComponent<Character>().UpdateLevel();
            Destroy(gameObject);
        }
    }


}
