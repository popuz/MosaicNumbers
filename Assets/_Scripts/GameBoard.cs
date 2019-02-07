using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MosaicNumbers
{
    public class GameBoard 
    {        
        public List<GameCell> GameCells { get; } = new List<GameCell>();        
        public int MaxNumberOfCells { get; private set; }
                
        private readonly GridLayoutGroup _gridLayout;
        private bool[] _numberIsOnBoard;
        
        private readonly Func<GameCell> CreateCell;

        public GameBoard(GridLayoutGroup gridLayout, Func<GameCell> CellFactory)
        {
            CreateCell = CellFactory;
            _gridLayout = gridLayout;
        }

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
                GameCells.Add(CreateCell());            
        }

        public void SetNewCellNumbers()
        {
            _numberIsOnBoard = new bool[MaxNumberOfCells];
            foreach (var cell in GameCells)            
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

        public void Hide()
        {
            _gridLayout.gameObject.SetActive(false);
        }

        public void Show()
        {
            _gridLayout.gameObject.SetActive(true);
        }
    }
}