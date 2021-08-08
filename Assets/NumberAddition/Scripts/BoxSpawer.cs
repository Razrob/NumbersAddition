using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumbersAddition
{
    public class BoxSpawer : MonoBehaviour
    {
        [SerializeField] private Vector3 _boxInstantiatePosition;
        [SerializeField] private GameObject _boxPrefab;

        [SerializeField] private StatsDisplay _statsDisplay;
        [SerializeField] private GameLooper _gameLooper;
        [SerializeField] private BoxesStorage _boxesStorage;


        void Start()
        {

            StartCoroutine(SpawnBox());
        }


        public void OnBoxSetted()
        {
            StartCoroutine(SpawnBox());
        }

        private IEnumerator SpawnBox()
        {
            yield return new WaitForSeconds(0.75f);

            int _boxNumber = 2;
            for (int i = 0; i < Random.Range(0, 4); i++) _boxNumber *= 2;

            _statsDisplay.OnBoxSpawned(_boxNumber);
            GameObject _box = Instantiate(_boxPrefab, _boxInstantiatePosition, Quaternion.identity);
            _box.GetComponent<Box>().SetParams(_boxesStorage, _gameLooper, _boxNumber);
            _box.GetComponent<UserInput>().BoxSpawner = this;

        }
    }
}