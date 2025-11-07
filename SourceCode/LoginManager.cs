using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public GameObject LoginPanel;
    public TMP_InputField idInput, pwInput;

    private void Awake()
    {
        LoginPanel.SetActive(true);
    }

    public void Button_StartGame()
    {
        if (UserData.userID == idInput.text && UserData.userPW == pwInput.text)
        {
            LoginPanel.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }

    public void Button_QuitGame()
    {
        Application.Quit();
    }
}