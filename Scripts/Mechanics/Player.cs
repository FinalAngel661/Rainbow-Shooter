using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int selectColor = 0;
    public int StartingHealth = 3;
    public int CurrentHealth;
    public Image FullPie, TwoSlice, OneSlice, Nolife, RedCircle, YellowCircle, BlueCircle;

    Mouse_Movement playerMov;

    private bool isDead;
    private bool damaged;
    private bool invincible;

    // Use this for initialization
    void Awake () {
        CurrentHealth = StartingHealth;
	}

    void Start() {
        GetComponent<Renderer>().material.color = Color.red;
        RedCircle.enabled = true;
        YellowCircle.enabled = false;
        BlueCircle.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        SwitchPlayerColor();
        SetHealthPie();
        
        if (transform.position.y < -30) {
            Death();
        }
    }

    void SwitchPlayerColor() {
        if (Input.GetKeyDown("x")) {
            selectColor = (selectColor + 1) % 3;
            switch (selectColor) {
                case 0:
                    GetComponent<Renderer>().material.color = Color.red;
                    RedCircle.enabled = true;
                    YellowCircle.enabled = false;
                    BlueCircle.enabled = false;
                    break;
                case 1:
                    GetComponent<Renderer>().material.color = Color.yellow;
                    RedCircle.enabled = false;
                    YellowCircle.enabled = true;
                    BlueCircle.enabled = false;
                    break;
                case 2:
                    GetComponent<Renderer>().material.color = Color.blue;
                    RedCircle.enabled = false;
                    YellowCircle.enabled = false;
                    BlueCircle.enabled = true;
                    break;
                default:
                    Debug.Log("Meh");
                    break;
            }
        }
    }

    void SetHealthPie() {
        if (CurrentHealth == 3) {
            FullPie.enabled = true;
            TwoSlice.enabled = false;
            OneSlice.enabled = false;
            Nolife.enabled = false;
        } else if (CurrentHealth == 2) {
            FullPie.enabled = false;
            TwoSlice.enabled = true;
            OneSlice.enabled = false;
            Nolife.enabled = false;
        } else if (CurrentHealth == 1) {
            FullPie.enabled = false;
            TwoSlice.enabled = false;
            OneSlice.enabled = true;
            Nolife.enabled = false;
        } else if (CurrentHealth == 0) {
            FullPie.enabled = false;
            TwoSlice.enabled = false;
            OneSlice.enabled = false;
            Nolife.enabled = true;
        }
    }

    public void TakeDamage(int amount) {
        if (!invincible) {
            damaged = true;
            CurrentHealth -= amount;

            if (CurrentHealth <= 0 && !isDead) {
                Death();
            } else {
                StartCoroutine(InvincibilityFlash());
            }
        }
    }

    IEnumerator InvincibilityFlash() {
        invincible = true;
        for (int i = 0; i < 3; i++) {
            GetComponent<Renderer>().material.color = Color.gray;
            yield return new WaitForSeconds(0.17f);

            switch (selectColor) {
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
            yield return new WaitForSeconds(0.17f);
        }

        GetComponent<Renderer>().material.color = Color.gray;
        yield return new WaitForSeconds(0.17f);

        switch (selectColor) {
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
        invincible = false;
    }

    void Death() {
        isDead = true;
        //playerMov.enabled = false;
        SceneManager.LoadScene("GameOver");
    }
}
