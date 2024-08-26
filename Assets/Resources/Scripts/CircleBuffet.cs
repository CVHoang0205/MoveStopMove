using System.Collections.Generic;
using UnityEngine;

public class CircleBuffet : MonoBehaviour
{
    public List<Character> listBoss = new List<Character>();
    public GameObject CirtcleTarget;
    // Start is called before the first frame update
    void Start()
    {
        //CirtcleTarget.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RemoveNullTarget() 
    {
        for (int i = 0; i < listBoss.Count; i++) 
        {
            if (listBoss[i] == null) 
            {
                listBoss.Remove(listBoss[i]);
            }
        }
    }

    public Transform GetNearTarget() 
    {
        float minDistance = float.MaxValue;
        int intdex = 0;
        for (int i = 0; i < listBoss.Count; i++) 
        {
            float distance = (transform.position - listBoss[i].transform.position).magnitude;
            if(minDistance > distance) 
            {
                minDistance = distance;
                intdex = i;
            }
        }
        return listBoss[intdex].transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("boss")) 
        {
            Debug.Log("Đã Phát Hiện có vật trong tầm ngắm");
            CirtcleTarget.SetActive(true);
            listBoss.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        listBoss.Remove(other.GetComponent<Character>());
        CirtcleTarget.SetActive(false);
    }
}
