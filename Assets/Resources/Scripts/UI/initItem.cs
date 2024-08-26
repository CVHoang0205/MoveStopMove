using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class initItem : MonoBehaviour
{
    public Image ItemImage;
    public GameObject price;
    public TextMeshProUGUI priceText;
    public Button actionButton;
    public TextMeshProUGUI buyText;

    public int state = 0;
    private GameItem thisItem;
    // Start is called before the first frame update
    void Start()
    {
        actionButton.onClick.AddListener(() => ButtonAction());
    }

    public void ButtonAction()
    {
        // Chua mua
        if (state == 1)
        {
            int currentGold = CreateBoss.Ins.gold;
            int priceItem = thisItem.item.Price;
            if (currentGold >= priceItem)
            {
                // Mua
                CreateBoss.Ins.ReduceGold(priceItem);
                itemJS.Ins.PurchaseItem(thisItem);
                ShopController.Ins.CreatItem(thisItem.item.Type);
            }
        }
        else if (state == 2) // Da mua, chua mac
        {
            itemJS.Ins.EquipItem(thisItem);
            ShopController.Ins.CreatItem(thisItem.item.Type);
            CreateBoss.Ins.InitPlayerItems();
        }
        else if (state == 3) // Da mua, da mac
        {
            itemJS.Ins.UnequipItem(thisItem);
            ShopController.Ins.CreatItem(thisItem.item.Type);
            CreateBoss.Ins.InitPlayerItems();
        }
    }

    public void InitItemUI(GameItem item)
    {
        thisItem = item;
        ItemImage.sprite = Resources.Load<Sprite>("UI/" + item.item.Type + "/" + item.item.Id);
        if (item.Purchased == false)
        {
            state = 1;
            price.SetActive(true);
            priceText.text = item.item.Price.ToString();
        }
        else
        {
            price.SetActive(false);
            if (item.IsEquip)
            {
                state = 3;
            }
            else
            {
                state = 2;
            }
        }
        InitButtonState();
    }

    private void InitButtonState()
    {
        if (state == 1)
        {
            buyText.text = "Buy";
            actionButton.GetComponent<Image>().color = UnityEngine.Color.white;
        }
        else if (state == 2)
        {
            actionButton.GetComponent<Image>().color = UnityEngine.Color.green;
            buyText.text = "Equip";
        }
        else if (state == 3)
        {
            buyText.text = "Used";
            actionButton.GetComponent<Image>().color = UnityEngine.Color.yellow;
        }
    }
}
