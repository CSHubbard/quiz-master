using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
  [Header("Questions")]
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] List<QuestionSO> questions;
  QuestionSO currentQuestion;

  [Header("Answers")]
  [SerializeField] GameObject[] answerButtons;
  private int correctAnswerIndex;
  private bool hasAnsweredEarly;

  [Header("Buttons")]
  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;

  [Header("Timer")]
  [SerializeField] Image timerImage;
  Timer timer;

  [Header("Scoring")]
  [SerializeField] TextMeshProUGUI scoreText;
  ScoreKeeper scoreKeeper;

  [Header("Progress Bar")]
  [SerializeField] Slider progressBar;

  [SerializeField] public bool isComplete;

  

  // Start is called before the first frame update
  void Awake()
  {
    timer = FindObjectOfType<Timer>();
    scoreKeeper = FindObjectOfType<ScoreKeeper>();
    progressBar.maxValue = questions.Count;
    progressBar.value = 0;
  }

  void Update()
  {
    timerImage.fillAmount = timer.fillFraction;
    if (timer.loadNextQuestion)
    {
      if (progressBar.value == progressBar.maxValue)
      {
        isComplete = true;
        return;
      }
      hasAnsweredEarly = false;
      GetNextQuestion();
      timer.loadNextQuestion = false;
    }
    else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
    {
      DisplayAnswer(-1);
      SetButtonState(false);
    }
  }

  private void DisplayQuestion()
  {
    if (currentQuestion != null)
    {
      questionText.text = currentQuestion.GetQuestion();

      for (int i = 0; i < answerButtons.Length; i++)
      {
        answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
      }
    }
  }

  private void GetNextQuestion()
  {
    if (questions.Count > 0)
    {
      SetButtonState(true);
      SetDefaultButtonSprites();
      GetRandomQuestion();
      DisplayQuestion();
      scoreKeeper.IncrementQuestionsSeen();
      progressBar.value++;
    }
  }

  private void GetRandomQuestion()
  {
    int index = Random.Range(0, questions.Count);
    currentQuestion = questions[index];
    if (questions.Contains(currentQuestion))
    {
      questions.Remove(currentQuestion);
    }
  }

  // Hooked up to answer buttons in the UI
  public void OnAnswerSelected(int index)
  {
    hasAnsweredEarly = true;
    DisplayAnswer(index);
    SetButtonState(false);
    timer.CancelTimer();
    scoreText.text = $"Score: {scoreKeeper.CalculateScore()}%";
  }

  private void DisplayAnswer(int index)
  {
    if (currentQuestion != null)
    {
      if (index == currentQuestion.GetCorrectAnswerIndex())
      {
        scoreKeeper.IncrementCorrectAnswers();
        questionText.text = "Correct!";
        answerButtons[index].GetComponent<Image>().sprite = correctAnswerSprite;
      }
      else
      {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        questionText.text = "Wrong. The correct answer was: \n" + currentQuestion.GetAnswer(correctAnswerIndex);
        answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = correctAnswerSprite;
      }
    }
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
