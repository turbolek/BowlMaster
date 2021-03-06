﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static List<Pin> standingPins = new List<Pin>();

    private List<int> bowls = new List<int>();
    private PinSetter pinSetter;
    private Ball ball;
    private GutterBall gutterBall;
    private PinCounter pinCounter;
    private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<PinSetter>();
        ball = FindObjectOfType<Ball>();
        gutterBall = FindObjectOfType<GutterBall>();
        pinCounter = FindObjectOfType<PinCounter>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Bowl (int pinFall)
    {
        bowls.Add(pinFall);
        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.PerformAction(nextAction, GameManager.standingPins);
        scoreDisplay.FillRollCard(bowls);
        ball.Reset();
        gutterBall.ballLeftBox = false;
    }
}
