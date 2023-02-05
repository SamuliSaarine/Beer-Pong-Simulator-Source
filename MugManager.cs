using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugManager : MonoBehaviour
{
    public int mugs;
    public ThrowBall player;
    public TurnManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MugOut()
    {
        mugs--;
        player.promilles += 0.2f;
        if (mugs == 0)
        {
            manager.GameOver();
        }
    }
}
