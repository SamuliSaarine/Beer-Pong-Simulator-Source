using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public ThrowBall[] players;
    public GameObject menu;
    public int startPlayer;
    public MugManager[] mugManagers;

    // Start is called before the first frame update
    void Start()
    {
        GameOver();
    }

    public void ChangeTurn(int player)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        players[player].Active = false;
        player = player == 1 ? 0 : 1;
        players[player].Active = true;
    }

    public void GameOver()
    {
        players[0].CancelInvoke();
        players[1].CancelInvoke();
        players[0].Active = false;
        players[1].Active = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menu.SetActive(true);
        foreach (var mugs in mugManagers)
        {
            mugs.mugs = 10;
            foreach (Transform mug in mugs.transform)
            {
                mug.gameObject.SetActive(true);
            }
        }
    }
}
