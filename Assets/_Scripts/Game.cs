using UnityEngine;
using UnityEngine.UI;

namespace MosaicNumbers
{
    [RequireComponent(typeof(ResultsUI))]
    public class Game : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _GameBoardGridLayout;
        [SerializeField] private GameCell _cellPrefab;
        [SerializeField] private int _rows, _columns;
        [SerializeField, Tooltip("If should shuffle game board after each mistaken touch")]
        private bool NeedShuffleAfterMistake = false;

        private GameBoard _board;
        private GameLogic _logic;

        private ResultsUI _resultsUI;

        private void Awake()
        {
            _resultsUI = GetComponent<ResultsUI>();

            _board = new GameBoard(_GameBoardGridLayout, CellFactory);
            _logic = new GameLogic();
        }

        private GameCell CellFactory()
        {
            return Instantiate(_cellPrefab, _GameBoardGridLayout.transform);
        }

        private void Start()
        {
            _board.Construct(_rows, _columns);
            _board.SetNewCellNumbers();

            foreach (var cell in _board.GameCells)
                cell.OnCellPressed += CheckPressedWithTarget;

            _logic.OnRightAnswer += _board.SetNewCellNumbers;
            _logic.OnGameFinish += HandleGameFinish;
            _logic.StartGame(1, _board.MaxNumberOfCells);
        }

        private void CheckPressedWithTarget(int pressedNumber)
        {
            _logic.HitGridNumber(pressedNumber);
            if (NeedShuffleAfterMistake)
                _board.SetNewCellNumbers();
        }

        private void HandleGameFinish()
        {
            _board.Hide();
            _resultsUI.Show(_logic.PlayerTries, _logic.TimeElapsedFromStart);
        }

        private void Update()
        {
            _logic.Tick(Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            
            if (Input.GetKeyDown(KeyCode.R))
                Restart();
        }

        private void Restart()
        {
            _resultsUI.Hide();
            _board.Show();
            _logic.StartGame(1, _board.MaxNumberOfCells);
        }
    }
}