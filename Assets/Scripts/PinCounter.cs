using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    public int lastStandingCount = -1;
    public Text standingPinsDisplay;

    private float lastChangeTime;
    private Pin[] standingPins;
    private GutterBall gutterBall;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gutterBall = FindObjectOfType<GutterBall>();
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        standingPinsDisplay.text = CountStanding().ToString();
        if (gutterBall.ballLeftBox)
        {
            standingPinsDisplay.color = Color.red;
            CheckStanding();
        }
    }

    int CountStanding()
    {
        GetStandingPins();
        return GameManager.standingPins.Count;
    }

    void PinsHaveSettled()
    {
        Debug.Log("Green");
        standingPinsDisplay.color = Color.green;
        gameManager.Bowl(lastStandingCount - CountStanding());
        lastStandingCount = -1;
    }

    void CheckStanding()
    {
        if (CountStanding() != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = CountStanding();
            return;
        }

        float settleTimeLimit = 3f;
        if (Time.time - lastChangeTime > settleTimeLimit)
        {
            PinsHaveSettled();
        }
    }

    public void GetStandingPins()
    {
        List<Pin> standingPins = new List<Pin>();
        Pin[] Pins = GameObject.FindObjectsOfType<Pin>();
        foreach (Pin pin in Pins)
        {
            if (pin.IsStanding())
            {
                standingPins.Add(pin);
            }
        }
        GameManager.standingPins = standingPins;
    }
}
