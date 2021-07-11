using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Text score;
    private int playerScore = 0;
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnScoreIncrease;

    // Singleton Pattern
    private static GameManager _instance;
    // Getter
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public override void Awake()
    {
        base.Awake();
        Debug.Log("awake called");
        // other instructions
    }

    // // Member variables can be referred to as fields.
    // private int _healthPoints;

    // // healthPoints is a basic property
    // public int healthPoints {
    //     get {
    //         // Some other code
    //         // ...
    //         return _healthPoints;
    //     }
    //     set {
    //         // Some other code, check etcd
    //         // ...
    //         _healthPoints = value;  // value is the amount passed by the setter
    //     }
    // }
    // // usage
    // Debug.Log(player.healthPoints);  // this will call instructions under get{}
    // player.healthPoints += 20;   // this will call instructions under set{}, where value is 20

    public void increaseScore()
    {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();

        OnScoreIncrease();
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = "SCORE: " + playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
