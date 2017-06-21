using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster : MonoBehaviour {

    public static List<int> ScoreCumulative (List<int> bowls)
    {
        List<int> cummulativeScore = new List<int>();
        List<int> framesScore = ScoreFrames(bowls);
        int runningTotal = 0;
        foreach (int score in framesScore)
        {
            runningTotal += score;
            cummulativeScore.Add(runningTotal);
        }

        return cummulativeScore;
    }

    public static List<int> ScoreFrames (List<int> bowls)
    {
        int[] scores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        int[] bonusesGiven = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] bonusesAwarded = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
        List<int> frameList = new List<int>();
        int bowlsInCurrentFrame = 0;
        int currentFrame = 0;
        int lastScoredFrame = 0;
        for (int i=0; i < bowls.Count; i++)
        {
            scores[currentFrame] += bowls[i];
            for (int j = lastScoredFrame; j < currentFrame; j++)
            {
               if (bonusesGiven[j] < bonusesAwarded[j])
                {
                    scores[j] += bowls[i];
                    bonusesGiven[j]++;
                    if (bonusesGiven[j] == bonusesAwarded[j])
                    {
                        frameList.Add(scores[j]);
                        lastScoredFrame = j;
                    }
                }
            }
            bowlsInCurrentFrame++;

            if (bowlsInCurrentFrame >= 2 || scores[currentFrame] >= 10)
            {
                if (scores[currentFrame] >= 10 && bowlsInCurrentFrame == 2)
                {
                    bonusesAwarded[currentFrame] = 1;
                }
                else if (scores[currentFrame] >= 10 && bowlsInCurrentFrame == 1)
                {
                    bonusesAwarded[currentFrame] = 2;
                }
                else
                {
                    if (currentFrame < 10)
                    {
                        frameList.Add(scores[currentFrame]);
                    }
                }
                bowlsInCurrentFrame = 0;
                currentFrame++;
            }
        }
        return frameList;
    }
}
