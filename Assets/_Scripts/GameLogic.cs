namespace MosaicNumbers
{
    public class GameLogic
    {
        public int Step { get; private set; }
        public int MaxNumber { get; private set; }
        public double RoundTime { get; private set; }

        public void Start()
        {
            Step = 1;
            MaxNumber = 25;
            RoundTime = 0f;
        }
        
        public void Tick(float deltaTime) => RoundTime += (Step > MaxNumber)? 0 : deltaTime;
        
        public void HitNumber(int i) => Step += (i == Step) ? 1 : 0;             
    }
}