using UnityEngine;

public class InitSkin : Singleton<InitSkin>
{

    [SerializeField] Transform weapons;
    [SerializeField] Transform head;
    [SerializeField] SkinnedMeshRenderer pants;

    public int weaponsId = 0;
    public GameObject weaponItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerEquipItems() 
    {
        // xoa do` cu~ di
        foreach (Transform obj in weapons)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in head)
        {
            Destroy(obj.gameObject);
        }
      
        int idWeapons = itemJS.Ins.GetIdOfItemsEquiped("Weapons");
        if (idWeapons > 0)
        {
            InitWeapon(idWeapons - 1);
        }

        int idHat = itemJS.Ins.GetIdOfItemsEquiped("Hat");
        if (idHat > 0)
        {
            InitHats(idHat - 1);
        }
        
        int idPants = itemJS.Ins.GetIdOfItemsEquiped("Pants");
        if (idPants > 0)
        {
            InitPants(idPants - 1);
        }
    }

    public void RandomEquipItems() 
    {
        InitWeapon(Random.Range(0, itermDtabase.Ins.weapons.Count));
        InitPants(Random.Range(0, itermDtabase.Ins.pantsMaterial.Count));
        InitHats(Random.Range(0, itermDtabase.Ins.hats.Count));
    }

    public void InitWeapon(int id)
    {
        GameObject weapon = itermDtabase.Ins.GetWeapon(id);
        Instantiate(weapon, weapons);
    }
    public void InitPants(int id)
    {
        Material pantMaterial = itermDtabase.Ins.GetPantsMaterialsById(id);
        pants.material = pantMaterial;
    }

    public void InitHats(int id) 
    {
        GameObject hat = itermDtabase.Ins.GetHatsById(id);
        Instantiate(hat, head);
    }
}
