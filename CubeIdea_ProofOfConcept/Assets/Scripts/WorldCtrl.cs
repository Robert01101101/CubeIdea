using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldCtrl : MonoBehaviour
{
    bool inputDelay = false;
    bool curTimeTravel = false;
    public PlayerCtrl playerCtrl;
    public HUD hud;
    private List<AbstractEnemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<AbstractEnemy>(FindObjectsOfType<AbstractEnemy>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!inputDelay)
        {
            if (Input.anyKey)
            {
                inputDelay = true;
                StartCoroutine(InputDelay());
                bool validInput = false;
                bool timeTravel = false;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    timeTravel = playerCtrl.initiateMove(Vector3.forward);
                    validInput = true;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    timeTravel = playerCtrl.initiateMove(Vector3.back);
                    validInput = true;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    timeTravel = playerCtrl.initiateMove(Vector3.right);
                    validInput = true;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    timeTravel = playerCtrl.initiateMove(Vector3.left);
                    validInput = true;
                }
                if (validInput) {
                    if (curTimeTravel != timeTravel)
                    {
                        if (timeTravel == true)
                        { //Starting Time Travel
                            hud.TimeTravelStart();
                        } else
                        { //Ending Time Travel
                            hud.TimeTravelEnd();
                        }
                    }
                    curTimeTravel = timeTravel;
                    WorldTick(timeTravel); 
                }
            }
        }
    }

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(1);
        playerCtrl.TickEnd();
        inputDelay = false;
    }

    public void WorldTick(bool timeTravel)
    {
        foreach(AbstractEnemy ae in enemies)
        {
            ae.EnemyTick(timeTravel);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public bool GetCurTimeTravel()
    {
        return curTimeTravel;
    }
}
