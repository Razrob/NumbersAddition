using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace NumbersAddition
{
    public class GameLooper : MonoBehaviour
    {

        [SerializeField] private GameObject _pauseWindow;
        [SerializeField] private string _menuSceneName;

        void Start()
        {

        }

        public void ToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_menuSceneName);
        }
        public void Pause()
        {
            _pauseWindow.SetActive(!_pauseWindow.activeSelf);
            Time.timeScale = Convert.ToInt32(_pauseWindow.activeSelf);
        }

    }
}