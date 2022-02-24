using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public static string playerName;
    public static string[] recordsNames;
    public static int[] records;

    private void Awake()
    {
        if(gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;

        if(recordsNames == null && records == null)
        {
            if (!LoadRecords())
            {
                recordsNames = new string[5];
                records = new int[5];
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable] public class SaveData
    {
        public string[] names;
        public int[] records;
    }

    public bool LoadRecords()
    {
        string pathRecords = Application.persistentDataPath + "/saveRecords.json";

        if(File.Exists(pathRecords))
        {
            string json = File.ReadAllText(pathRecords);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            records = data.records;
            recordsNames = data.names;
            return true;
        }
        else
        {
            Debug.Log("Not found saves records.");
            return false;
        }
    }

    public void SaveRecords()
    {
        SaveData data = new SaveData();
        data.records = records;
        data.names = recordsNames;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveRecords.json", json);
    }
     
}
