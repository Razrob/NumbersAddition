using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumbersAddition
{
    public class StatsDisplay : MonoBehaviour
    {

        private int _highscore;
        private int _score;


        [SerializeField] private Text _highscoreUI;
        [SerializeField] private Text _scoreUI;


        private void Start()
        {
            Init();
        }
        private void Init()
        {
            _highscore = SavableValues.Highscore;
            _highscoreUI.text = $"Highscore: {_highscore}";
        }
        private void ScoreIncrease(int _value)
        {
            _score += _value;
            _scoreUI.text = $"Score: {_score}";
            if (_score > _highscore) ChangeHighscore(_score);
        }

        public void OnBoxSpawned(int _boxNumber)
        {
            ScoreIncrease(_boxNumber);
        }

        public void ChangeHighscore(int _score)
        {
            SavableValues.Highscore = _score;
            _highscore = _score;
            _highscoreUI.text = "Highscore: " + SavableValues.Highscore.ToString();
            PlayerPrefs.SetInt("Highscore", _highscore);
        }
    }
}