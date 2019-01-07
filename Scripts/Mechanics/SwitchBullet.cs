using UnityEngine;

public class SwitchBullet : MonoBehaviour {

    public int SelectedColor = 0;


	// Use this for initialization
	void Start ()
    {

        SelectBullet();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        int PrevColor = SelectedColor;

        if (Input.GetKeyDown("z"))
        {
            if (SelectedColor >= transform.childCount - 1)
            {
                SelectedColor = 0;
            }
            else
            {
                SelectedColor++;
            }
                

        }

        if(PrevColor != SelectedColor)
        {
            SelectBullet();

        }
	    	
	}


    void SelectBullet()
    {
        int indx = 0;

        foreach (Transform bullet in transform)
        {
            if (indx == SelectedColor)
            {
                bullet.gameObject.SetActive(true);
               
            }
            else
            {
                bullet.gameObject.SetActive(false);
            }

            indx++;


        }

    }
}
