using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private List<int> pinFalls;

    [SetUp]
    public void Setup()
    {
        pinFalls = new List<int>();
    } 

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl10ReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02LessThan0PinsThrowsInvalidPinsException()
    {

        try
        {
            pinFalls.Add(-1);
            ActionMaster.NextAction(pinFalls);
        }
        catch (UnityException exception)
        {
            Assert.AreEqual(exception.Message, "Invalid pins");
        }
    }

    [Test]
    public void T03MoreThan10PinsThrowsInvalidPinsException()
    {
        try
        {
            pinFalls.Add(11);
            ActionMaster.NextAction(pinFalls);
        }
        catch (UnityException exception)
        {
            Assert.AreEqual(exception.Message, "Invalid pins");
        }
    }

    [Test]
    public void T04Bowl8ReturnsTidy()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T05Bowl2Bowl8ReturnsEndTurn()
    {
        int[] bowls = { 1, 2 };
        pinFalls.AddRange(bowls) ;
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T06Bowl0ReturnsTidy()
    {
        pinFalls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T07Bowl0Bowl0ReturnsEndTurn()
    {
        int[] bowls = { 0, 0 };
        pinFalls.AddRange(bowls);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T08Bowl0Bowl0InTurn10ReturnsEndGame()
    {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        pinFalls.AddRange(bowls);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T09Bowl10InTurn10ReturnsReset()
    {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10 };
        pinFalls.AddRange(bowls);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T10Bowl5Bowl5InTurn10ReturnsReset()
    {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5 };
        pinFalls.AddRange(bowls);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T11Bowl10Bowl10InTurn10ReturnsReset()
    {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10};
        pinFalls.AddRange(bowls);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T12Bowl10Bowl5InTurn10ReturnsTidy()
    {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 5};
        pinFalls.AddRange(bowls);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T13Bowl10Bowl10Bowl10InTurn10ReturnsEndGame()
    {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10, 10};
        pinFalls.AddRange(bowls);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T14Bowl0Bowl10ReturnsEndTurn()
    {
        int[] bowls = {0, 10};
        pinFalls.AddRange(bowls);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T15Bowl0Bowl10Bowl3ReturnsTidy()
    {
        int[] bowls = { 0, 0, 3};
        pinFalls.AddRange(bowls);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }
}
