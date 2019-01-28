using NUnit.Framework;

namespace MosaicNumbers.Tests
{
    public class GameLogicTests
    {
        private GameLogic _game;
        
        private void HitAllNumbers()
        {
            for (var i = _game.Step; i <= _game.MaxNumber; i++)
                _game.HitNumber(_game.Step);
        }

        [SetUp]
        public void Initialize()
        {
            _game = new GameLogic();
            _game.Start();
        }

        [Test]
        public void WhenGameRoundStarts_StepEqualToOne() => Assert.AreEqual(1, _game.Step);

        [Test]
        public void WhenHitRightNumber_IncreaseStep()
        {
            var startStep = _game.Step;
            _game.HitNumber(_game.Step);
            Assert.AreEqual(startStep + 1, _game.Step);
        }

        [Test]
        public void WhenHitWrongNumber_StepUnchanged()
        {
            var startStep = _game.Step;
            var number = UnityEngine.Random.Range(1, _game.MaxNumber);

            _game.HitNumber(number == _game.Step ? number - 1 : number);

            Assert.AreEqual(startStep, _game.Step);
        }

        [Test]
        public void WhenHitAllNumbers_StepsEqualToMaxNumberPlusOne()
        {
            HitAllNumbers();
            Assert.AreEqual(_game.MaxNumber + 1, _game.Step);
        }

        [Test]
        public void WhenRoundStarts_TimerIsEqualZero() => Assert.AreEqual(0f, _game.RoundTime);

        [TestCase(0, 1f)]
        [TestCase(1, 10f)]
        [TestCase(1, 0f)]
        [TestCase(1, 2f)]
        [TestCase(5, 7f)]
        [TestCase(10, 3f)]
        public void AfterSeveralTicks_TimerSummedDeltaTimes(int ticks, float deltaTime)
        {
            for (var i = 0; i < ticks; i++)
                _game.Tick(deltaTime);
            Assert.AreEqual(deltaTime * ticks, _game.RoundTime);
        }

        [Test]
        public void WhenHitAllNumbers_TimerNoMoreIncreased()
        {
            HitAllNumbers();
            var gameOverTime = _game.RoundTime;
            _game.Tick(10f);
            
            Assert.AreEqual(gameOverTime, _game.RoundTime);
        }
    }
}