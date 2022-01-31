using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Services
{
    public class RoverService 
    {
        private const int MAX_ANGLE = 360;
        private const int MIN_ANGLE = 0;
        private const int ANGLE_INTERVAL = 90;

        public RoverModel CalculateRoverCoordinates(int Width, int Height, string RoverPathCommandLine, int PositionX, int PositionY, string Direction)
        {
            var RoverModel = new RoverModel();

            var angle = MIN_ANGLE;

            if (PositionX < Width && PositionY < Height)
            {
                angle = GetDirectionAngle(Direction);

                foreach (char c in RoverPathCommandLine)
                {
                    switch (c)
                    {
                        case 'L':
                            angle = SetLeftAngle(angle);
                            break;
                        case 'R':
                            angle = SetRightAngle(angle);
                            break;
                        case 'M':
                            MoveRover(angle, ref PositionX, ref PositionY);
                            break;
                    }
                }//foreach loop
            }//if statement

            RoverModel.PositionX = PositionX;
            RoverModel.PositionY = PositionY;
            RoverModel.Direction = GetDirection(angle);

            return RoverModel;

        }//CalculateRoverCoordinates method

        public int SetLeftAngle(int angle)
        {
            angle += ANGLE_INTERVAL;

            if (angle >= MAX_ANGLE)
            {
                angle = MIN_ANGLE;
            }
            return angle;
        }
        public int SetRightAngle(int angle)
        {
            angle -= ANGLE_INTERVAL;

            if (angle < MIN_ANGLE)
            {
                angle = MAX_ANGLE - ANGLE_INTERVAL; // 270;// 360-90// Reverse angle orbit calculation  
            }
            return angle;
        }
        public void MoveRover(int angle, ref int PositionX, ref int PositionY)
        {
            switch (angle)
            {
                case (int)Angels.EastAngle://East
                    PositionX++;
                    break;
                case (int)Angels.NorthAngle://North
                    PositionY++;
                    break;
                case (int)Angels.WestAngle://West
                    PositionX--;
                    break;
                case (int)Angels.SouthAngle://South
                    PositionY--;
                    break;
            }

        }
        public int GetDirectionAngle(string Direction)
        {
            int angle = MIN_ANGLE;

            switch (Direction)
            {
                case "E"://East
                    angle = (int)Angels.EastAngle;
                    break;
                case "N"://North
                    angle = (int)Angels.NorthAngle;
                    break;
                case "W"://West
                    angle = (int)Angels.WestAngle;
                    break;
                case "S"://South
                    angle = (int)Angels.SouthAngle;
                    break;
            }
            return angle;
        }
        public string GetDirection(int angle)
        {
            string? Direction = "";

            switch (angle)
            {
                case (int)Angels.EastAngle://East
                    Direction = "E";
                    break;
                case (int)Angels.NorthAngle://North
                    Direction = "N";
                    break;
                case (int)Angels.WestAngle://West
                    Direction = "W";
                    break;
                case (int)Angels.SouthAngle://South
                    Direction = "S";
                    break;
            }
            return Direction;
        }

    }
}
