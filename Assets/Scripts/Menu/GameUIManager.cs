using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    public bool hasSomeoneDied = false;

    public GameObject mainCanvas;
    public Text endScreenText;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ChangeEndScreenText(string text)
    {
        endScreenText.text = text;
    }

    public void ChangeEndScreenTextColor(Color color)
    {
        endScreenText.color = color;
    }

    public void ShowEndScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        mainCanvas.GetComponent<Animator>().SetTrigger("Open");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }





}
