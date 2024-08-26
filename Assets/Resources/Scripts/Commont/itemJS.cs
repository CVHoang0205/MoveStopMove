﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;
using System.Text;
using System;
using UnityEngine.SocialPlatforms;

public class itemJS : Singleton<itemJS>
{
    private JsonData itemData;
    public JsonData inGameItemData;
    public List<GameItem> ListItemInGame= new List<GameItem>();
    private List<Item> listItem = new List<Item>();
    private string filePath = "MyItem.txt";
    // Start is called before the first frame update
    void Start()
    {
        LoadResourcesFromTxt();
        ConstructDatabase();
        LoadDataFromLocalDb();
        //Debug.Log(filePath);
    }

    private void LoadDataFromLocalDb()
    {
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        Debug.Log(filePathFull);
        if (!File.Exists(filePathFull))
        {
            // Chua ton tai
            AddNewItemFirstTime();
            Save();
        }
        else
        {
            // Da ton tai
            byte[] jsonByte = null;
            try
            {
                jsonByte = File.ReadAllBytes(filePathFull);
            }
            catch
            {
            }
            string jsonData = Encoding.ASCII.GetString(jsonByte);
            inGameItemData = JsonMapper.ToObject(jsonData);
            ConstructMyItemDb();
        }
    }

    public void EquipItem(GameItem item)
    {
        UnequipItem(item);
        for (int i = 0; i < ListItemInGame.Count; i++)
        {
            if (item.item.Type == ListItemInGame[i].item.Type && item.item.Id == ListItemInGame[i].item.Id)
            {
                ListItemInGame[i].IsEquip = true;
                break;
            }
        }
        Save();
    }

    public void UnequipItem(GameItem item)
    {
        for (int i = 0; i < ListItemInGame.Count; i++)
        {
            if (item.item.Type == ListItemInGame[i].item.Type)
            {
                ListItemInGame[i].IsEquip = false;
            }
        }
        Save();
    }

    public void PurchaseItem(GameItem item)
    {
        for (int i = 0; i < ListItemInGame.Count; i++)
        {
            if (item.item.Type == ListItemInGame[i].item.Type && item.item.Id == ListItemInGame[i].item.Id)
            {
                ListItemInGame[i].Purchased = true;
                break;
            }
        }
        Save();
    }

    private void LoadResourcesFromTxt()
    {
        string filePath = "StreamingAsset/item";
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        itemData = JsonMapper.ToObject(targetFile.text);
    }

    private void AddNewItemFirstTime()
    {
        for (int i = 0; i < listItem.Count; i++)
        {
            GameItem newGameItem = new GameItem();
            newGameItem.item = listItem[i];
            newGameItem.Purchased = false;
            newGameItem.IsEquip = false;
            ListItemInGame.Add(newGameItem);
        }
    }

    public List<GameItem> GetAllItemOfType(string type)
    {
        List<GameItem> listItem = new List<GameItem>();
        for (int i = 0; i < ListItemInGame.Count; i++)
        {
            if (type == ListItemInGame[i].item.Type)
            {
                listItem.Add(ListItemInGame[i]);
            }
        }
        return listItem;
    }

    public int GetIdOfItemsEquiped(string type)
    {
        for (int i = 0; i < ListItemInGame.Count; i++)
        {
            if (type == ListItemInGame[i].item.Type && ListItemInGame[i].IsEquip == true)
            {
                return ListItemInGame[i].item.Id;
            }
        }
        return 0;
    }

    private void Save()
    {
        //InitUserStats();
        string jsonData = JsonConvert.SerializeObject(ListItemInGame.ToArray(), Formatting.Indented);
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);
        if (!Directory.Exists(Path.GetDirectoryName(filePathFull)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePathFull));
        }
        if (!File.Exists(filePathFull))
        {
            File.Create(filePathFull).Close();
        }
        try
        {
            File.WriteAllBytes(filePathFull, jsonByte);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cannot save" + e.Message);
        }
    }


    private void ConstructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item item = new Item();
            item.Id = (int)itemData[i]["Id"];
            item.Type = (string)itemData[i]["Type"];
            item.Price = (int)itemData[i]["Price"];
            item.Atk = (int)itemData[i]["Atk"];
            item.Def = (int)itemData[i]["Def"];
            item.Spd = (int)itemData[i]["Spd"];
            listItem.Add(item);
        }
    }

    private void ConstructMyItemDb()
    {
        for (int i = 0; i < inGameItemData.Count; i++)
        {
            GameItem gameItem = new GameItem();
            gameItem.item = new Item();
            gameItem.item.Id = (int)inGameItemData[i]["item"]["Id"];
            gameItem.item.Type = (string)inGameItemData[i]["item"]["Type"];
            gameItem.item.Price = (int)inGameItemData[i]["item"]["Price"];
            gameItem.item.Atk = (int)inGameItemData[i]["item"]["Atk"];
            gameItem.item.Def = (int)inGameItemData[i]["item"]["Def"];
            gameItem.item.Spd = (int)inGameItemData[i]["item"]["Spd"];
            gameItem.IsEquip = (bool)inGameItemData[i]["IsEquip"];
            gameItem.Purchased = (bool)inGameItemData[i]["Purchased"];
            ListItemInGame.Add(gameItem);
        }
    }
}

public class Item 
{
    public int Id { get; set; }
    public string Type { get; set; }

    public int Price { get; set; }

    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
}

public class GameItem 
{
    public bool Purchased { get; set; }
    public bool IsEquip { get; set; }

    public Item item { get; set; }
}
