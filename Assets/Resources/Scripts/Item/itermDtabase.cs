using System.Collections.Generic;
using UnityEngine;

public class itermDtabase : Singleton<itermDtabase>
{
    public List<GameObject> weapons;
    public List<Material> pantsMaterial;
    public List<GameObject> hats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetHatsById(int id) 
    {
        return hats[id];
    }

    public GameObject GetWeapon(int id) 
    {
        return weapons[id];
    }

    public Material GetPantsMaterialsById(int id) 
    {
        return pantsMaterial[id];
    }

    

}

 