using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        ScoreText.text = $"{GameManager.playerName} Score : 0";

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{GameManager.playerName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        WriteRecords();
    }

    private void WriteRecords()
    {
        bool empty = true;

        //write empty slots;
        for(int i = 0;i < GameManager.recordsNames.Length;i++)
        {            
            if(GameManager.recordsNames[i] == null)
            {
                GameManager.recordsNames[i] = GameManager.playerName;
                GameManager.records[i] = m_Points;
                break;
            }

            if(i == 4) empty = false;
        }

        sortRecords();
        Debug.Log(empty);
        if (!empty)
        {
            AddNewRecords();
        }        
    }

    private void sortRecords()
    {
        int[] r = GameManager.records;
        string[] name = GameManager.recordsNames;
        for (int i = 0; i < r.Length; i++)
        {
            int num = r[i];  // the biggest number
            int index = i;   // index this number

            for (int j = index; j < r.Length; j++)
            {
                if (r[j] > num)
                {
                    num = r[j];
                    index = j;
                }
            }

            r[index] = r[i];
            r[i] = num;

            string m = name[index]; // the neme with biggest number
            name[index] = name[i];
            name[i] = m;
        }

        GameManager.records = r;
        GameManager.recordsNames = name;
    }

    private void AddNewRecords()
    {
        int index = 0;
        int num = 0;
        string name = "";
        string name2 = "";
        bool change = true;
        foreach(int n in GameManager.records)
        {
            if (m_Points > n && change)
            {
                num = GameManager.records[index];
                GameManager.records[index] = m_Points;

                name = GameManager.recordsNames[index];
                GameManager.recordsNames[index] = GameManager.playerName;

                change = false;
            }
            else if(!change)
            {
                GameManager.records[index] = num;                
                num = n;

                name2 = GameManager.recordsNames[index];
                GameManager.recordsNames[index] = name;
                name = name2;
            }

            index++;
        }
    }
}
