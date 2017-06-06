using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster actionMaster;

    [SetUp]
    public void SetUp()
    {
        actionMaster = new ActionMaster();
    }

    private void Perform18Bowls()
    {
        for (int i = 1; i < 10; i++)
        {
            actionMaster.Bowl(0);
            actionMaster.Bowl(0);
        }
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl10ReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10) );
    }

    [Test]
       public void T02LessThan0PinsThrowsInvalidPinsException()
    {
        
        try
        {
            actionMaster.Bowl(-1);
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
            actionMaster.Bowl(11);
        }
        catch (UnityException exception)
        {
            Assert.AreEqual(exception.Message, "Invalid pins");
        }
    }

    [Test]
    public void T04Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T05Bowl2Bowl8ReturnsEndTurn()
    {
        actionMaster.Bowl(2);
        Assert.AreEqual(endTurn, actionMaster.Bowl(8));
    }

    [Test]
    public void T06Bowl0ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T07Bowl0Bowl0ReturnsEndTurn()
    {
        actionMaster.Bowl(0);
        Assert.AreEqual(endTurn, actionMaster.Bowl(0));
    }

    [Test]
    public void T13Bowl0Bowl0InTurn10ReturnsEndGame()
    {
        Perform18Bowls();
        actionMaster.Bowl(0);
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T14Bowl10InTurn10ReturnsEndTurn()
    {
        Perform18Bowls();
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T15Bowl5Bowl5InTurn10ReturnsEndTurn()
    {
        Perform18Bowls();
        actionMaster.Bowl(5);
        Assert.AreEqual(reset, actionMaster.Bowl(5));
    }

    [Test]
    public void T16Bowl10Bowl5InTurn10ReturnsEndGame()
    {
        Perform18Bowls();
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(5));
    }

    [Test]
    public void T17Bowl10Bowl10InTurn10ReturnsEndGame()
    {
        Perform18Bowls();
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
}
