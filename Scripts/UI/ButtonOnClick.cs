using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{
    public Button RestartButton;

    void Start()
    {
        Button btn = RestartButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button.");
    }
}

