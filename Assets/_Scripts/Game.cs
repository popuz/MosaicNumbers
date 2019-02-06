using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MosaicNumbers
{
    [RequireComponent(typeof(GameBoard))]
    public class Game : MonoBehaviour
    {
        private GameBoard _board;
        private GameLogic _logic;
        private readonly List<GameCell> _gameCells = new List<GameCell>();
        private void Awake()
        {
            _board = GetComponent<GameBoard>();
            _logic = new GameLogic();
        }

        private void Start()
        {
            _board.Construct(2,5);
            _board.SetNewCellNumbers();
            _logic.StartGame(1, _board.MaxNumberOfCells);
        }
        
        private void Update()
        {
            _logic.Tick(Time.deltaTime);
            
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            
            if(Input.GetKeyDown(KeyCode.Space))
                _board.SetNewCellNumbers();
        }                
    }
}