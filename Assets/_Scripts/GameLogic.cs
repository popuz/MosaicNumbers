using System;

namespace MosaicNumbers
{
    public class GameLogic
    {
        public event Action OnRightAnswer;
        public event Action OnGameFinish;
        
        public int PlayerTries { get; private set; }
        public int TargetNumber { get; private set; }
        public int MaxNumberOnGrid { get; private set; }
        public float TimeElapsedFromStart { get; private set; }

        public GameStates State;

        public void StartGame(int startTargetNumber,int maxNumberOnBoard)
        {                        
            TargetNumber = startTargetNumber;
            MaxNumberOnGrid = maxNumberOnBoard;
            
            PlayerTries = 0;
            TimeElapsedFromStart = 0f;
            
            State = GameStates.InPlay;
        }

        public void HitGridNumber(int number)
        {
            PlayerTries++;
            if (number != TargetNumber) return;

            if (number == MaxNumberOnGrid)
            {
                FinishTheGame();
                OnGameFinish?.Invoke();
            }
            else 
            {
                ChangeTargetNumberByRule();
                OnRightAnswer?.Invoke();
            }
        }
        private void FinishTheGame() => State = GameStates.Finished;
        private void ChangeTargetNumberByRule() => TargetNumber++;
        
        public void Tick(float deltaTime) => TimeElapsedFromStart += CanPlay() ? deltaTime : 0;
        private bool CanPlay() => State == GameStates.InPlay;        
    }
}