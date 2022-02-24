using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if(UNITY_EDITOR)
using UnityEditor;
#endif

public class MainManuUiController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen = null;
    [SerializeField] private GameObject recordsMenuScreen = null;
    [SerializeField] private TextMeshProUGUI[] recordsText = null;
    [SerializeField] private Button newGameBtn = null;
    [SerializeField] private TMP_InputField nameField = null;
    [SerializeField] private TextMeshProUGUI playerName = null;

    private void Awake()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowRecordsMenu()
    {
        mainMenuScreen.SetActive(false);
        for(int i = 0; i < GameManager.records.Length; i++)
        {
            recordsText[i].text = $"{i + 1}.  Score: {GameManager.records[i]} - {GameManager.recordsNames[i]}";
        }

        recordsMenuScreen.SetActive(true);
    }

    public void ExitRecordsMenu()
    {
        recordsMenuScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void ExitGame()
    {
        #if (UNITY_EDITOR)
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void enterPlayerName()
    {
        GameManager.playerName = nameField.text;

        if (!playerName.gameObject.activeSelf)
        {
            playerName.text = "With " + GameManager.playerName;
            playerName.gameObject.SetActive(true);
            newGameBtn.gameObject.SetActive(true);
            nameField.gameObject.SetActive(false);
        }        
    }
}
