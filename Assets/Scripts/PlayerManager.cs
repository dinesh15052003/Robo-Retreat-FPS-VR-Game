using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechXR.Core.Sense;

public class PlayerManager : MonoBehaviour
{
    public Shooting shooting;

    public SenseController senseController;
    public GameObject laserPointer, MainMenu, startGameAssets;

    public Transform enemyContainer;

    private Health playerHealth;
   
    public Transform startPosition;

    public ScoreManager scoreManager;


    bool inGame;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth= GetComponent<Health>();
        startPosition.position = transform.position;

        scoreManager.SetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (SenseInput.GetButton(ButtonName.L) && inGame)
            shooting.Shoot();

        if (playerHealth.health<=0)
            GameOver();
    }

    public void GameStart()
    {
        inGame = true;
        MainMenu.SetActive(false);

        senseController.ToggleJoystickMovement(true);
        shooting.gameObject.SetActive(true); 

        laserPointer.gameObject.SetActive(false);
        startGameAssets.SetActive(true);

        scoreManager.ResetScore();
    }

    public void GameOver()
    {
        inGame= false; 
        MainMenu.SetActive(true);

        senseController.ToggleJoystickMovement(false);
        shooting.gameObject.SetActive(false);

        laserPointer.gameObject.SetActive(true);
        startGameAssets.SetActive(false);
        laserPointer.GetComponent<LaserPointer>().ButtonState= false;

        playerHealth.ResetHealth();

        transform.position = startPosition.position;

        foreach (Transform child in enemyContainer)
            child.gameObject.GetComponent<EnemyAI>().EnemyDeath();

        scoreManager.SetHighScore();
    }
}
