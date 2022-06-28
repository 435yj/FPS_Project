using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameOverMgr : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button exitButton;

    private UnityAction action;

    void Start()
    {
        action = () => Restart();
        // UnityAction을 써서 이벤트 연결

        restartButton.onClick.AddListener(action);
        exitButton.onClick.AddListener(() => Exit());
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
