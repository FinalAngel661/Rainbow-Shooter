using UnityEngine;

public class Switch_Player_Color : MonoBehaviour
{

    public int selectColor = 0;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchPlayerColor();

    }

    void SwitchPlayerColor()
    {
        if (Input.GetKeyDown("x"))
        {
            selectColor = (selectColor + 1) % 3;
            switch (selectColor)
            {
                case 0:
                    GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 1:
                    GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 2:
                    GetComponent<Renderer>().material.color = Color.blue;
                    break;
                default:
                    Debug.Log("Meh");
                    break;
            }
        }
    }
}