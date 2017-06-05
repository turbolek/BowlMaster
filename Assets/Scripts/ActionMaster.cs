using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster : MonoBehaviour
{

    public enum Action { Tidy, Reset, EndTurn, EndGame };

    private int bowlNumber = 1;
    private int turn = 1;
    private int score = 0;
    //private List<int> turnsScore;
    //private bool isSpare = false;
    private int pinsDownInTurn = 0;
    private bool isSpare = false;

    public Action Bowl(int pins)
    {

        bool bowlIsOdd = (bowlNumber % 2) == 1;

        pinsDownInTurn += pins;
        Score(pins);
        if (pins == 10)
        {
            bowlNumber += 2;
            
            if (IsEndGame(turn, pinsDownInTurn))
            {
                return Action.EndGame;
            } else
            {
                NextTurn();
                return Action.EndTurn;
            }
            
        }
        else if (pins >= 0 && pins < 10)
        {
            bowlNumber += 1;
            if (bowlIsOdd)
            {
                if (IsEndGame(turn, pinsDownInTurn))
                {
                    return Action.EndGame;
                } else
                {
                    return Action.Tidy;
                }                
            } else
            {
                if (IsEndGame(turn, pinsDownInTurn))
                {
                    return Action.EndGame;
                }
                else
                {
                    NextTurn();
                    return Action.EndTurn;
                }
            }
            
        }
        else
        {
            throw new UnityException("Invalid pins");
        }

    }

    public int GetTurn()
    {
        return turn;
    }

    private bool IsEndGame(int turn, int pinsDown)
    {
        if ((turn >= 10 && pinsDown < 10) || turn >= 11)
        {
            return true;
        }
        else
            return false;
    }

    private void NextTurn()
    {
        pinsDownInTurn = 0;
        turn++;
    }

    public int GetScore()
    {
        return score;
    }

    private void Score(int pinsDown)
    {
        if (isSpare)
        {
            score += pinsDown * 2;
            isSpare = false;
            return;
        } else
        {
            score += pinsDown;
        }

        isSpare = (pinsDownInTurn == 10);
    }
}