using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI finalScoretext;
  ScoreKeeper scoreKeeper;
  // Start is called before the first frame update
  void Awake()
  {
    scoreKeeper = FindObjectOfType<ScoreKeeper>();
  }

  public void ShowFinalScore()
  {
    finalScoretext.text = $"Congratulations!\n Your Final score was {scoreKeeper.CalculateScore()}%";
  }
}
