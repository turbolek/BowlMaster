using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster : MonoBehaviour
{

    public enum Action { Tidy, Reset, EndTurn, EndGame };

    private int bowlNumber = 1;
    private int[] bowlResults = new int[21];

    public Action Bowl(int pins)
    {

        bool bowlIsOdd = (bowlNumber % 2) == 1;
        bowlResults[bowlNumber-1] = pins;
        if (bowlNumber >= 21)
        {
            return Action.EndGame;
        }

        if (ExtraBowlAwarded())
        {
            bowlNumber += (bowlNumber == 19 ? 2 : 1);
            return Action.Reset;
        }
        else if (bowlNumber == 20 && !ExtraBowlAwarded())
        {
            return Action.EndGame;
        }

        if (pins == 10)
        {
            bowlNumber += 2;
            return Action.EndTurn;
        }
        else if (pins >= 0 && pins < 10)
        {
            bowlNumber += 1;
            if (bowlIsOdd)
            {
                return Action.Tidy;
            }
            else
            {
                return Action.EndTurn;
            }
        }
        else
        {
            throw new UnityException("Invalid pins");
        }
    }

    private bool ExtraBowlAwarded()
    {
        return (bowlResults[18] + bowlResults[19] >= 10);
    }
}