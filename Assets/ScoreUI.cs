using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _recordText;

    public void SetScoreText(int scoreNumber) => _scoreText.text = "Score: " + scoreNumber;
    public void SetScoreRecordText(int scoreRecordNumber) => _recordText.text = "Score Record: " + scoreRecordNumber;
}
