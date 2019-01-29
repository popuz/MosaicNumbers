using System;
using NUnit.Framework;
using UnityEngine;

namespace MosaicNumbers.Tests
{
    public class GameLogicTests
    {
        private GameLogic _game;

        private int GetRandomGridNumber() => UnityEngine.Random.Range(1, _game.MaxNumberOnGrid);

        private int GetNotTargetNumber()
        {
            var num = GetRandomGridNumber();
            while (num == _game.TargetNumber)
                num = GetRandomGridNumber();
            return num;
        }
        
        private void HitAllNumbersInOrder()
        {
            for (var i = 1; i <= _game.MaxNumberOnGrid; i++)
                _game.HitGridNumber(_game.TargetNumber);
        }

        [SetUp]
        public void Initialize()
        {
            _game = new GameLogic();
            _game.Start();
        }

        [Test]
        public void WhenGameStarts_PlayerTriesEqualsToZero() => Assert.AreEqual(0, _game.PlayerTries);
        
        [Test]
        public void WhenGameStarts_TargetNumberEqualToOne() => Assert.AreEqual(1, _game.TargetNumber);


        [Test]
        public void WhenHitNumber_TriesIncreased()
        {
            var startTries = _game.PlayerTries;
            _game.HitGridNumber(GetRandomGridNumber());
            Assert.AreEqual(startTries + 1, _game.PlayerTries);
        }

        [Test]
        public void WhenHitRightNumber_TargetNumberChanged()
        {
            var startTargetNumber = _game.TargetNumber;
            _game.HitGridNumber(_game.TargetNumber);
            Assert.AreNotEqual(startTargetNumber, _game.TargetNumber);
        }

        [Test]
        public void WhenHitWrongNumber_TargetNumberUnchanged()
        {
            var startTargetNumber = _game.TargetNumber;
            _game.HitGridNumber(GetNotTargetNumber());
            Assert.AreEqual(startTargetNumber, _game.TargetNumber);
        }

        [Test]
        public void WhenHitAllNumbers_TargetNumberEqualMaxNumber()
        {
            HitAllNumbersInOrder();
            Assert.AreEqual(_game.MaxNumberOnGrid, _game.TargetNumber);
        }

        [Test]
        public void WhenRoundStarts_TimerIsEqualZero() => Assert.AreEqual(0f, _game.TimeElapsedFromStart);

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
            Assert.AreEqual(deltaTime * ticks, _game.TimeElapsedFromStart);
        }

        [Test]
        public void WhenHitAllNumbers_TimerNoMoreIncreased()
        {
            HitAllNumbersInOrder();
            var gameOverTime = _game.TimeElapsedFromStart;
            _game.Tick(10f);

            Assert.AreEqual(gameOverTime, _game.TimeElapsedFromStart);
        }

        [Test]
        public void WhenHitAllNumbers_GameStateChangedToFinished()
        {
            HitAllNumbersInOrder();
            Assert.AreEqual(_game.State,GameStates.Finished);
        }
    }    
}