using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MosaicNumbers
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private GameCell _cellPrefab;

        private List<GameCell> _gameCells = new List<GameCell>();
        private bool[] _numberIsOnBoard;

        public int MaxNumberOfCells { get; private set; }

        public void Construct(int rows, int columns)
        {
            MaxNumberOfCells = rows * columns;
            PrepareGridLayout(rows, columns);
            FillLayoutWithElements(MaxNumberOfCells);            
        }
                       
        private void PrepareGridLayout(int rows, int columns)
        {
            _gridLayout.constraintCount = columns;
            _gridLayout.cellSize = new Vector2(Screen.width / (float) columns, Screen.height / (float) rows);
        }
        private void FillLayoutWithElements(int amount)
        {
            for (var i = 0; i < amount; i++)
                _gameCells.Add(Instantiate(_cellPrefab, _gridLayout.transform));
        }

        public void SetNewCellNumbers()
        {
            _numberIsOnBoard = new bool[MaxNumberOfCells];
            foreach (var cell in _gameCells)            
                cell.SetNumber(FindFreeNumber());            
        }
        private int FindFreeNumber()
        {
            var num = Random.Range(0, MaxNumberOfCells);
            while (_numberIsOnBoard[num])
                num = Random.Range(0, MaxNumberOfCells);
            
            _numberIsOnBoard[num] = true;
            return num + 1;
        }                
    }
}