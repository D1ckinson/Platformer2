using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private string _scoreText = "Количество очков: ";
    private int _score;

    private void Awake() =>
        SetScore();
    private void SetScore() =>
        _text.text = _scoreText + _score;

    public void AddScore(int score)
    {
        _score += score;

        SetScore();
    }
}
