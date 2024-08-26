using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : Singleton<ShopController>
{
    public GameObject ItemPrefabs;
    public GameObject parent;
    

    public Button[] listButton;

    private static string[] listType = { "Weapons", "Hat", "Pants" };
    // Start is called before the first frame update
    void Start()
    {
        ClickButtonType(0);
        listButton[0].onClick.AddListener(() => ClickButtonType(0));
        listButton[1].onClick.AddListener(() => ClickButtonType(1));
        listButton[2].onClick.AddListener(() => ClickButtonType(2));
    }

    private void CloseShop()
    {
        Debug.Log("press");
        UIManager.Ins.InitGameState(1);
    }

    private void ClickButtonType(int type)
    {
        for (int i = 0; i < 3; i++)
        {
            listButton[i].GetComponent<Image>().color = UnityEngine.Color.white;
        }
        listButton[type].GetComponent<Image>().color = UnityEngine.Color.yellow;
        CreatItem(listType[type]);
    }


    public void CreatItem(string type)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        List<GameItem> itemIngame = itemJS.Ins.GetAllItemOfType(type);
        for (int i = 0; i < itemIngame.Count; i++)
        {
            GameObject item = Instantiate(ItemPrefabs, parent.transform);
            item.GetComponent<initItem>().InitItemUI(itemIngame[i]);
        }
    }
}
