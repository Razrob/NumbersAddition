using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NumbersAddition
{
    public class MenuDisplay : MonoBehaviour
    {
        [SerializeField] private string _gameSceneName;

        void Start()
        {
            SavableValues.Highscore = PlayerPrefs.GetInt("Highscore");
        }

        public void StartFreeGame()
        {
            SceneManager.LoadScene(_gameSceneName);
        }

    }
}