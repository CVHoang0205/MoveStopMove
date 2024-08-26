using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateBoss : Singleton<CreateBoss>
{
    public List<Transform> ListBossTransform = new List<Transform>(); //vị trí khởi tạo
    public int totalBoss = 20; // tổng số lượng tạo
    public int spawnedBoss = 0; // đếm số lần tạo
    public Canvas indicatorCanvas;
    public GameObject BossPrefabs; // đối tượng tạo
    public TargetIndicator indicator;
    public MovePlayer player;
    public List<Boss> bots = new List<Boss>();
    public int gold;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CreateNewBoss());
        GainGold(1000);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame() 
    {

        for (int i = 0; i < bots.Count; i++) 
        {
            bots[i].Oninit();
           
        }

        StartCoroutine(CreateNewBoss());
        InitGold();
       
    }

    public void InitGold() 
    {
        if (!PlayerPrefs.HasKey("Gold")) 
        {
            gold = 0;
            PlayerPrefs.SetInt("Gold", 0);
        }
        else 
        {
            gold = PlayerPrefs.GetInt("Gold");
        }
    }

    public void InitPlayerItems()
    {
        player.skin.PlayerEquipItems();
    }

    public void GainGold(int num) 
    {
        gold += num;
        PlayerPrefs.SetInt("Gold", gold);
        UIManager.Ins.InitGold();
    }

    public void ReduceGold(int num) 
    {
        gold -= num;
        PlayerPrefs.SetInt("Gold", gold);
        UIManager.Ins.InitGold();
    }

    public void RepPlayGame() 
    {
        for (int i = 0; i < bots.Count; i++) 
        {
            spawnedBoss = 0;
            Destroy(bots[i].indicator.gameObject);
            Destroy(bots[i].gameObject);
        }
        bots.Clear();
        player.Oninit();
        //StartCoroutine(CreateNewBoss());
    }


    IEnumerator CreateNewBoss() 
    {
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        playerIndicator.InitTarget(UnityEngine.Color.black, 0 , "Player");

        while (spawnedBoss < totalBoss) 
        {
            for (int i = 0; i < ListBossTransform.Count; i++)
            {
                Boss bossCreate = Instantiate(BossPrefabs, ListBossTransform[i].transform.position, Quaternion.identity).GetComponent<Boss>();
                spawnedBoss++;
                TargetIndicator bossIndicator = Instantiate(indicator, indicatorCanvas.transform);
                bossIndicator.character = bossCreate;
                bossCreate.indicator = bossIndicator;

                bots.Add(bossCreate);
                UnityEngine.Color color = Random.ColorHSV();
                string bossName = Constant.PlayerName[Random.Range(0, Constant.PlayerName.Length)] + Random.Range(0, 10000);
                bossIndicator.InitTarget(color, 0, bossName);
                yield return new WaitForSeconds(5f);
            }
        }       
    }

    public void EndGame() 
    {
        JoystickControl.direct = Vector3.zero;
    }
}
