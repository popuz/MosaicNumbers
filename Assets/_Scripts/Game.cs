using UnityEngine;
using UnityEngine.UI;

namespace MosaicNumbers
{
    [RequireComponent(typeof(GameBoard))]
    public class Game : MonoBehaviour
    {
        private GameBoard _board;
        private GameLogic _logic;
        
        private void Awake()
        {
            _board = GetComponent<GameBoard>();
            _logic = new GameLogic();
        }

        private void Start()
        {
            _board.Construct(2,2);
            _logic.StartGame(1,4);
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}