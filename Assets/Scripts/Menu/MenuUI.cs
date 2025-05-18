using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public GameObject difficultySelection;

    [SerializeField] private float buttonWidth = 200;
    [SerializeField] private float buttonHeight = 60;

    [SerializeField] private Texture logo;

    public enum MenuType { Canvas, OnGUI, None}

    [SerializeField] MenuType menuType;
    [SerializeField] GameObject canvasMenuUI;
    private bool isShowingGUI;

    private void Start()
    {
        switch (menuType)
        {
            case MenuType.Canvas:
                canvasMenuUI.SetActive(true);
                isShowingGUI = false;
                break;
            case MenuType.OnGUI:
                canvasMenuUI.SetActive(false);
                isShowingGUI = true;
                break;
            default:
                canvasMenuUI.SetActive(false);
                isShowingGUI = false;
                break;
        }
    }

    private void OnGUI()
    {
        if (isShowingGUI)
        {
            GUI.DrawTexture(new Rect(0, 0, 300, 230), logo);

            if (GUI.Button(new Rect(330, 30, buttonWidth, buttonHeight), "New game"))
                SceneManager.LoadScene("GameScene");

            if (GUI.Button(new Rect(330, 110, buttonWidth, buttonHeight), "Exit"))
                Application.Quit();
        }
    }

    public void OpenDifficultySelection()
    {
        difficultySelection.SetActive(true);
    }

    public void SetDifficultyAndStart(int damageOnPlayer)
    {
        PlayerPrefs.SetInt("DamageOnPlayer", damageOnPlayer);
        LoadSceneByName("GameScene");
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
