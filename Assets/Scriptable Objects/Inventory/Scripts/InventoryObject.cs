using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver {
    public List<InventorySlot> Container = new List<InventorySlot>();
    private ItemDatabaseObject database;
    public string savePath;

    private void OnEnable() {
#if UNITY_EDITOR
        database = (ItemDatabaseObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }

    public void AddItem(ItemObject _item, int _amount) {
        for (int i = 0; i < Container.Count; i++) {
            if (Container[i].item == _item) {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(_item, _amount, database.GetId[_item]));
    }

    public void Save() {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load() {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(String.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
    
    public void OnBeforeSerialize() {
    }

    public void OnAfterDeserialize() {
        for (int i = 0; i < Container.Count; i++) 
            Container[i].item = database.GetItem[Container[i].ID];
    }
}

[System.Serializable]
public class InventorySlot {
    public ItemObject item;
    public int amount;
    public int ID;

    public InventorySlot(ItemObject _item, int _amount, int _id) {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value) {
        amount += value;
    }
}