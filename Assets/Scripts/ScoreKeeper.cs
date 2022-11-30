using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
  public int CorrectAnwers { get; private set; } = 0;
  public int QuestionsSeen { get; private set; } = 0;

  public void IncrementCorrectAnswers()
  {
    CorrectAnwers++;
  }

  public void IncrementQuestionsSeen()
  {
    QuestionsSeen++;
  }

  public int CalculateScore()
  {
    return Mathf.RoundToInt((float)CorrectAnwers / (float)QuestionsSeen * 100);
  }
}
