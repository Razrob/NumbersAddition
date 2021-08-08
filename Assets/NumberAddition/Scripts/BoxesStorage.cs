using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

namespace NumbersAddition
{
    public class BoxesStorage : MonoBehaviour
    {

        public GameObject[][] Boxes = new GameObject[5][];

        void Start()
        {
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < 5; i++)
            {
                Boxes[i] = new GameObject[8];
                for (int x = 0; x < 6; x++)
                {
                    Boxes[i][x] = null;
                }
            }
        }

    }
}