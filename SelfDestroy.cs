using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float time = 10f;
    public ThrowBall ball;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), 5f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
