using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{
    [Header("Management")]
    public int playerIndex;
    public TurnManager manager;
    public GameObject[] EnableOnTurn;
    private bool active;

    [Header("Throwing")]
    public Transform hand;
    private Vector2 mousePos;
    public Camera cam;
    public GameObject ball;
    public float maxForce;
    public float forceGrowthSpeed;
    private int throws = 2;
    private bool coolDown;
    private GameObject current;

    [Header("Swaying")]
    public float promilles;
    public float shakeSpeed;
    public float shakeForce;

    [Header("UI")]
    public TMP_Text text;
    public Slider powerSlider;

    // Start is called before the first frame update
    void Start()
    {
        Power = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if(active)
        {
            float mouseSens;
            if(Input.GetMouseButton(1))
            {
                cam.fieldOfView = 25f;
                mouseSens = 0.008f;
            }
            else
            {
                cam.fieldOfView = 35f;
                mouseSens = 0.012f;
            }

            mousePos = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            float shaking = Mathf.Sin(Time.time * shakeSpeed) * promilles * promilles * shakeForce;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x - mousePos.y - shaking * mouseSens, transform.eulerAngles.y + mousePos.x + shaking * mouseSens, shaking);

            if (throws > 0 && !coolDown)
            {
                if (Input.GetMouseButton(0))
                {
                    Power += forceGrowthSpeed;
                }
                else if (Power > 0f)
                {
                    hand.DetachChildren();
                    Rigidbody rb = current.GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    rb.AddForce(transform.forward * Power, ForceMode.Impulse);
                    current.GetComponent<SelfDestroy>().enabled = true;
                    throws--;
                    Debug.Log($"{playerIndex} has {throws} throws left, Power was {Power}");
                    text.text = throws + " throws left!";
                    coolDown = true;
                    Invoke(nameof(CoolDown), 2f);
                    Power = 0;
                }
            }
        }
    }

    private void CoolDown()
    {
        coolDown = false;
        if (throws == 0)
        {
            manager.ChangeTurn(playerIndex);
        }
        else
        {
            current = Instantiate(ball, hand.position, transform.rotation);
            current.transform.SetParent(hand);
        }
    }

    public bool Active
    {
        set 
        {
            Debug.Log(playerIndex + ": " + value);
            active = value;
            cam.enabled = value;
            throws = value ? 2 : 0;
            text.text = throws + " throws left!";
            foreach (var go in EnableOnTurn)
            {
                go.SetActive(value);
            }
            if(value == true)
            {
                current = Instantiate(ball, hand.position, transform.rotation);
                current.transform.SetParent(hand);
            }
        }
    }


    private float _power;
    public float Power
    {

        get { return _power; }
        set
        {
            if(value<maxForce)
            {
                _power = value;
                powerSlider.value = value / maxForce;
            }
        }
    }
}
