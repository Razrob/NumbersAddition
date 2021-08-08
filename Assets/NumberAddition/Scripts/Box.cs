using System.Collections.Generic;
using UnityEngine; 
using System;
using System.Collections;

namespace NumbersAddition
{
    public class Box : MonoBehaviour
    {

        [SerializeField] private Color32[] _colorSprite;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private TextMesh _numberText;

        private BoxesStorage _boxesStorage;
        private GameLooper _gameLooper;

        private int _number;
        private void CheckAroundBoxes()
        {
            List<(int x, int y)> _indexes = new List<(int, int)>();

            _indexes = GetAroundBoxes(transform.position);

            for (int i = 0; i < _indexes.Count; i++)
            {
                Box numberController = _boxesStorage.Boxes[_indexes[i].x][_indexes[i].y].GetComponent<Box>();
                numberController.transform.position += new Vector3(0, 0, 0.5f);
                numberController.Moving(transform.position - numberController.transform.position);
                _indexes[i] = (_indexes[i].x, _indexes[i].y + 1);
            }
            if (_indexes.Count > 0)
            {
                NumberSwitch(_number *  (int)Math.Pow(2, _indexes.Count));
                Invoke(nameof(CheckAroundBoxes), 0.5f);
            }
        }

        private void ZeroingBoxesArrayCell(Vector2 _index)
        {
            if (_index.x <= 5 && _index.y <= 6) _boxesStorage.Boxes[Convert.ToInt32(_index.x)][Convert.ToInt32(_index.y)] = null;
        }

        private void AddCellToBoxesArray(Vector2 index)
        {
            _boxesStorage.Boxes[Convert.ToInt32(index.x)][Convert.ToInt32(index.y)] = gameObject;
        }

        private void NumberSwitch(int _boxNumber)
        {
            _number = _boxNumber;
            _numberText.text = _number.ToString();

            int nm = 2;
            for (int i = 0; i < _colorSprite.Length; i++)
            {
                if (nm == _boxNumber)
                {
                    _sprite.color = _colorSprite[i];
                }
                nm *= 2;
            }
        }

        private void Falling()
        {
            ZeroingBoxesArrayCell(transform.position);

            if ((int)transform.position.y > 0 && _boxesStorage.Boxes[Convert.ToInt32(transform.position.x)][Convert.ToInt32(transform.position.y - 1)] == null)
            {
                StartCoroutine(SmoothFalling(transform.position));
            }
            else
            {
                if (transform.position.y > 5.5f)
                {
                    _gameLooper.ToMenu();
                    return;
                }
                AddCellToBoxesArray(transform.position);
                CheckAroundBoxes();
            }
        }
        private List<(int, int)> GetAroundBoxes(Vector2 _position)
        {
            List<(int x, int y)> _indexes = new List<(int, int)>();

            for (int x = (int)_position.x - 1; x <= (int)_position.x + 1; x += 2)
            {
                int y = (int)_position.y;
                if (y < 0 || x < 0 || x > 4 || y > 5) continue;
                GameObject tempGameObject = _boxesStorage.Boxes[x][y];
                if (tempGameObject != null) if (tempGameObject.GetComponent<Box>()._number == _number) _indexes.Add((x, y));
            }
            for (int y = (int)_position.y - 1; y <= (int)_position.y + 1; y += 2)
            {
                int x = (int)_position.x;
                if (y < 0 || x < 0 || x > 4 || y > 5) continue;
                GameObject tempGameObject = _boxesStorage.Boxes[x][y];
                if (tempGameObject != null) if (tempGameObject.GetComponent<Box>()._number == _number) _indexes.Add((x, y));
            }
            return _indexes;
        }

        private void Moving(Vector2 _direction)
        {
            _boxesStorage.Boxes[Convert.ToInt32(transform.position.x)][Convert.ToInt32(transform.position.y)] = null;

            StartCoroutine(SmoothMove(transform.position, _direction));
        }

        private IEnumerator SmoothFalling(Vector3 _startPosition)
        {

            if (transform.position != _startPosition + Vector3.down)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPosition + Vector3.down, 0.05f);
                yield return new WaitForSeconds(0.005f);
                StartCoroutine(SmoothFalling(_startPosition));
            }
            else
            {
                Falling();
                StopCoroutine(SmoothFalling(_startPosition));
            }
        }
        private IEnumerator SmoothMove(Vector3 _startPosition, Vector2 _direction)
        {
            if (transform.position != _startPosition + (Vector3)_direction)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPosition + (Vector3)_direction, 0.05f);
                yield return new WaitForSeconds(0.005f);
                StartCoroutine(SmoothMove(_startPosition, _direction));
            }
            else
            {
                GameObject _box = _boxesStorage.Boxes[(int)_startPosition.x][(int)_startPosition.y + 1];
                if (_box != null) _box.GetComponent<Box>().Falling();

                Destroy(gameObject);
            }

        }
        public void SetParams(BoxesStorage boxesStorage, GameLooper gameLooper, int boxNumber)
        {
            _boxesStorage = boxesStorage;
            _gameLooper = gameLooper;
            NumberSwitch(boxNumber);
        }


        public void SetBox()
        {
            transform.position = new Vector3((float)Math.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x), transform.position.y, transform.position.z);
            Falling();
        }
    }
}