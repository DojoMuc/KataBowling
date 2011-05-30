using System;
using System.Collections.Generic;

namespace KataBowling
{
    public class Game
    {
        private List<Frame> frames;

        public Game(List<Frame> frames)
        {
            this.frames = frames;
        }

        public Game()
        {
            this.frames = new List<Frame>();
        }

        public List<Frame> Frames
        {
            get { return this.frames; }
        }

        public void AddFrame(Frame frame)
        {
            this.frames.Add(frame);
        }
    }

    public class BowlingScoreCalculator
    {




        public int GetScore(Game game)
        {
            var frames = game.Frames;

            int result = 0;
            bool lastWasStrike = false;
            int nextRolsToAdd = 0;

            foreach (var frame in frames)
            {
                result += AddFrameRolls(frame);
                switch (nextRolsToAdd)
                {
                    case 1:
                        result += frame.FirstRoll;
                        nextRolsToAdd = 0;
                        break;
                    case 2:
                        result += frame.FirstRoll;
                        if (frame.SecondRoll > 0)
                        {
                            result += frame.SecondRoll;
                            nextRolsToAdd = 0;
                        
                        }
                        else // second role in next frame
                        {
                            nextRolsToAdd = 1;
                        }
                        break;
                }

                lastWasStrike = frame.FirstRoll == 10;
                if (frame.FirstRoll==10)
                {
                    nextRolsToAdd += 2;
                }
            }
            return result;
        }

        private int AddFrameRolls(Frame frame)
        {
            return frame.FirstRoll + frame.SecondRoll;
        }
    }
}