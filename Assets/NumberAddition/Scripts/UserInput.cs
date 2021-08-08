using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumbersAddition {
    public class UserInput : MonoBehaviour
    {
        [SerializeField] private Box box;
        [HideInInspector] public BoxSpawer BoxSpawner;


        private void Update()
        {

            if (Input.GetMouseButtonDown(0)) SetBox();
        }

        private void SetBox()
        {
            BoxSpawner.OnBoxSetted();
            box.SetBox();
            Destroy(this);
        }
    }
}