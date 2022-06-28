using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    public Button startButton;
    public Button optionButton;
    public Button exitButton;

    private UnityAction action;

    void Start()
    {
        action = () => OnStartClick();
        // UnityAction�� �Ἥ �̺�Ʈ ����
        startButton.onClick.AddListener(action);

        optionButton.onClick.AddListener(delegate { OnButtonClick(optionButton.name); } );
        // ���� �޼��� ����

        exitButton.onClick.AddListener(() => OnButtonClick(exitButton.name));
    }
    
    void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitClick()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    void OnButtonClick(string str)
    {
        Debug.Log($"Click Button : {str}");
    }
}
