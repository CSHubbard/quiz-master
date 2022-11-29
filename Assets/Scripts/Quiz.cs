using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] QuestionSO question;
  [SerializeField] GameObject[] answerButtons;
  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;
  private int correctAnswerIndex;


  // Start is called before the first frame update
  void Start()
  {
    DisplayQuestion();
  }

  private void DisplayQuestion()
  {
    questionText.text = question.GetQuestion();

    for (int i = 0; i < answerButtons.Length; i++)
    {
      answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetAnswer(i);
    }
  }

  private void GetNextQuestion()
  {
    DisplayQuestion();
    SetDefaultButtonSprites();
    SetButtonState(true);
  }

  public void OnAnswerSelected(int index)
  {
    if (index == question.GetCorrectAnswerIndex())
    {
      questionText.text = "Correct!";
      answerButtons[index].GetComponent<Image>().sprite = correctAnswerSprite;
    }
    else
    {
      correctAnswerIndex = question.GetCorrectAnswerIndex();
      questionText.text = "Wrong. The correct answer was: \n" + question.GetAnswer(correctAnswerIndex);
      answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = correctAnswerSprite;
    }
    SetButtonState(false);
  }

  private void SetDefaultButtonSprites()
  {
    for (int i = 0; i < answerButtons.Length; i++)
    {
      answerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
    }
  }

  private void SetButtonState(bool state)
  {
    for (int i = 0; i < answerButtons.Length; i++)
    {
      answerButtons[i].GetComponent<Button>().interactable = state;

    }
  }
}
