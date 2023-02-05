using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Play()
    {
        turnManager.ChangeTurn(turnManager.startPlayer);
        gameObject.SetActive(false);
    }

    public void Sober()
    {
        foreach(ThrowBall player in turnManager.players)
        {
            player.promilles = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
