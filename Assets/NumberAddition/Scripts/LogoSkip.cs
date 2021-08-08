using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NumbersAddition
{
    public class LogoSkip : MonoBehaviour
    {

        byte alfa = 0;
        public Image panel;
        void Start()
        {
            StartCoroutine("skip");
        }



        IEnumerator skip()
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine("todark");
        }

        IEnumerator todark()
        {

            alfa += 5;
            if (panel.color.a < 1)
            {
                panel.color = new Color32(0, 0, 0, alfa);
                yield return new WaitForSeconds(0.002f);
                StartCoroutine("todark");
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }


    }
}