using System.Collections.Generic;
using NUnit.Framework;

namespace KataBowling
{
    [TestFixture]
    public class BowlingTests
    {
        [Test]
        public void When_Bowl_Rolls_In_Gutter_Then_No_Points_Are_Scored()
        {
            BowlingScoreCalculator bowlingScoreCalculator = new BowlingScoreCalculator();
            
            int score = bowlingScoreCalculator.GetScore(
                new Game(
                new List<Frame>
                    {
                        new Frame {FirstRoll = 0}
                    }));
            Assert.AreEqual(0, score);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(7)]
        public void When_Bowl_Hits_Several_Pins_Then_Score_Is_Number_Of_Pins(int pins)
        {
            BowlingScoreCalculator bowlingScoreCalculator = new BowlingScoreCalculator();

            int score = bowlingScoreCalculator.GetScore(new Game(
                new List<Frame>
                    {
                        new Frame {FirstRoll = pins}
                    }));
            Assert.AreEqual(pins, score);
        }

        [Test]
        public void When_Two_Rolls_With_Two_And_Five_Pins_Are_Rolled_Then_FrameScore_Is_Seven()
        {
            BowlingScoreCalculator bowlingScoreCalculator = new BowlingScoreCalculator();

            int score = bowlingScoreCalculator.GetScore(new Game(
                new List<Frame>
                    {
                        new Frame {FirstRoll = 2, SecondRoll = 5}
                    }));

            Assert.AreEqual(7, score);
        }

        [Test]
        public void The_Score_Of_Game_With_No_Spares_And_Strikes_Is_The_Sum_OF_Rols()
        {
            var game = new Game();
            game.AddFrame(new Frame()
                              {
                                  FirstRoll = 1,
                                  SecondRoll = 2
                              });
             game.AddFrame(new Frame()
                              {
                                  FirstRoll = 2,
                                  SecondRoll = 3
                              });
            var bowling = new BowlingScoreCalculator();
            var score = bowling.GetScore(game);
            Assert.AreEqual(8, score);
        }

        [Test]
        public  void When_First_Roll_Of_First_Frame_Equals_10_Then_The_Score_Of_The_Second_Frame_is_added_Twice()
        {
            var game = new Game();
            game.AddFrame(new Frame()
            {
                FirstRoll = 10,
                SecondRoll = 0
            });
            game.AddFrame(new Frame()
            {
                FirstRoll = 2,
                SecondRoll = 3
            });
            var bowling = new BowlingScoreCalculator();
            var score = bowling.GetScore(game);
            Assert.AreEqual(20, score);
        }

        [Test]
        public void When_First_Frame_Was_Strike_And_Second_Wasnt_Then_Third_Frame_Is_Counted_Only_Once()
        {
            var game = new Game();
            game.AddFrame(new Frame()
            {
                FirstRoll = 10,
                SecondRoll = 0
            });
            game.AddFrame(new Frame()
            {
                FirstRoll = 3,
                SecondRoll = 5
            }); 
            game.AddFrame(new Frame()
            {
                FirstRoll = 1,
                SecondRoll = 0
            });
            var bowling = new BowlingScoreCalculator();
            var score = bowling.GetScore(game);
            Assert.AreEqual(27, score);
        }

        [Test]
        public void When_Two_Frames_In_Row_Are_Strike_Then_Third_Frame_Is_Counted_For_Second_Frame()
        {
            var game = new Game();
            game.AddFrame(new Frame() // == 10+10+1 (?)
            {
                FirstRoll = 10,
                SecondRoll = 0
            });
            game.AddFrame(new Frame() // == 10 + 1+1
            {
                FirstRoll = 10,
                SecondRoll = 0
            });
            game.AddFrame(new Frame() // == 1+1
            {
                FirstRoll = 1,
                SecondRoll = 1
            });
            var bowling = new BowlingScoreCalculator();
            var score = bowling.GetScore(game);
            Assert.AreEqual(35, score);
        }
    }

    public class Frame
    {
        public int FirstRoll { get; set; }
        public int SecondRoll { get; set; }
    }
}
