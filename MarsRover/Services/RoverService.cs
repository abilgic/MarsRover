using MarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public void GetAreaValues(ref int Width, ref int Height)
        {
            int tempInt;
            var SplittedAreaValues = new String[2];

            do
            {
                Console.WriteLine("Test Input:");
                var AreaValuesLine = Console.ReadLine();

                if (!string.IsNullOrEmpty(AreaValuesLine))
                {
                    SplittedAreaValues = AreaValuesLine.Split(' ');
                }
            } while (!int.TryParse(SplittedAreaValues.ElementAtOrDefault(0), out tempInt) || !int.TryParse(SplittedAreaValues.ElementAtOrDefault(1), out tempInt) || SplittedAreaValues.Length != 2);

            Width = Convert.ToInt32(SplittedAreaValues[0]);
            Height = Convert.ToInt32(SplittedAreaValues[1]);
        }

        public PDModel GetPositionAndDirection()
        {
            var pdmodel = new PDModel();
            int tempInt;
            var SplittedRoverValues = new String[3];
            bool isValidDirection;

            do
            {
                var RoverValuesLine = Console.ReadLine();

                if (!string.IsNullOrEmpty(RoverValuesLine))
                {
                    SplittedRoverValues = RoverValuesLine.Split(' ');
                }
                isValidDirection = CheckDirection(SplittedRoverValues[2]);

            } while (!int.TryParse(SplittedRoverValues[0], out tempInt) || !int.TryParse(SplittedRoverValues[1], out tempInt) || !onlyLetters(SplittedRoverValues[2]) || SplittedRoverValues.Length != 3 || !isValidDirection);

            pdmodel.PositionX = Convert.ToInt32(SplittedRoverValues[0]);
            pdmodel.PositionY = Convert.ToInt32(SplittedRoverValues[1]);
            pdmodel.Direction = SplittedRoverValues[2];

            return pdmodel;
        }

        public string? GetMovementAction()
        {
            bool isChar = false;
            bool isValidAction = false;

            string? RoverPathCommandLine = string.Empty;

            do
            {
                RoverPathCommandLine = Console.ReadLine();

                if (!string.IsNullOrEmpty(RoverPathCommandLine))
                {
                    isChar = onlyLetters(RoverPathCommandLine);
                    isValidAction = CheckMoveAction(RoverPathCommandLine);
                }

            } while (!isChar || !isValidAction);

            return RoverPathCommandLine;
        }

        public void PrintResult(RoverModel[] RoverModelList)
        {
            if (RoverModelList != null)
            {
                foreach (var roveritem in RoverModelList)
                {
                    Console.WriteLine(roveritem.PositionX + " " + roveritem.PositionY + " " + roveritem.Direction);
                }
            }
        }

        public bool CheckDirection(string direction)
        {
            if (direction == "E" || direction == "N" || direction == "W" || direction == "S")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckMoveAction(string action)
        {
            bool returnval = false;
            foreach (char item in action)
            {
                if (item.Equals('L') || item.Equals('R') || item.Equals('M'))
                {
                    returnval = true;
                }
                else
                {
                    returnval = false;
                    break;
                }
            }
            return returnval;
        }

        public bool onlyLetters(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

    }
}
