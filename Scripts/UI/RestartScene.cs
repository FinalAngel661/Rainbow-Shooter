using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScene : MonoBehaviour {

    public Button RestartButton;

    void Start()
    {
        Button btn = RestartButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

