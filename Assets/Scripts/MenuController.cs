using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnPlayerDeath += RestartUI;
        foreach (Transform eachChild in transform) {
            if (eachChild.name != "Score" && eachChild.name != "Powerups") {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
            }
        }
    }

    void RestartUI()
    {
        Debug.Log("Restart mechanism");
        foreach (Transform eachChild in transform) {
            if (eachChild.name != "Score" && eachChild.name != "Powerups") {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(true);
            }
        }
    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("MarioLevel1");
        foreach (Transform eachChild in transform) {
            if (eachChild.name != "Score" && eachChild.name != "Powerups") {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
