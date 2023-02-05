using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfIn : MonoBehaviour
{
    private MugManager manager;

    private void Awake()
    {
        manager = transform.parent.parent.GetComponent<MugManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Destroy(other.gameObject);
            manager.MugOut();
            transform.parent.gameObject.SetActive(false);
            
        }
    }
}
