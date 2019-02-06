namespace MosaicNumbers
{
    public class GameLogic
    {
        public int PlayerTries { get; private set; }
        public int TargetNumber { get; private set; }
        public int MaxNumberOnGrid { get; private set; }
        public double TimeElapsedFromStart { get; private set; }

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

            if (number == TargetNumber)
            {
                if (number == MaxNumberOnGrid)
                    FinishTheGame();
                else
                    ChangeTargetNumberByRule();
            }
        }
        private void FinishTheGame() => State = GameStates.Finished;
        private void ChangeTargetNumberByRule() => TargetNumber++;
        
        public void Tick(float deltaTime) => TimeElapsedFromStart += CanPlay() ? deltaTime : 0;
        private bool CanPlay() => State == GameStates.InPlay;
    }
}