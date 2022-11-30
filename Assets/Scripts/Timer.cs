using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
  public bool loadNextQuestion;
  public bool isAnsweringQuestion;
  public float fillFraction;
  float timerValue;
  [SerializeField] float timeToCompleteQuestion = 30f;
  [SerializeField] float timeToShowCorrectAnswer = 10f;


  // Update is called once per frame
  void Update()
  {
    UpdateTimer();
  }

  public void CancelTimer()
  {
    timerValue = 0;
  }

  private void UpdateTimer()
  {
    timerValue -= Time.deltaTime;
    if (timerValue > 0)
    {
      fillFraction = timerValue / (isAnsweringQuestion ? timeToCompleteQuestion : timeToShowCorrectAnswer);
    }
    else
    {
      if (isAnsweringQuestion)
      {
        timerValue = timeToShowCorrectAnswer;
      }
      else
      {
        timerValue = timeToCompleteQuestion;
        loadNextQuestion = true;
      }
      isAnsweringQuestion = !isAnsweringQuestion;
    }
  }
}
