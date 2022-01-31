using MarsRover.Models;
using MarsRover.Services;
using System.Text.RegularExpressions;

const int RoverNumber = 2;//Number of Rovers
var SplittedAreaValues = new String[2];
RoverModel[] RoverModelList = new RoverModel[RoverNumber];
int tempInt;

do
{
    Console.WriteLine("Test Input:");
    var AreaValuesLine = Console.ReadLine();

    if (!string.IsNullOrEmpty(AreaValuesLine))
    {
        SplittedAreaValues = AreaValuesLine.Split(' ');
    }
} while (!int.TryParse(SplittedAreaValues.ElementAtOrDefault(0), out tempInt) || !int.TryParse(SplittedAreaValues.ElementAtOrDefault(1), out tempInt));

var Width = Convert.ToInt32(SplittedAreaValues[0]);
var Height = Convert.ToInt32(SplittedAreaValues[1]);
var _service = new RoverService();

for (int i = 0; i < RoverNumber; i++)
{
    var RoverValuesLine = Console.ReadLine();
    var SplittedRoverValues = new String[3];

    if (!string.IsNullOrEmpty(RoverValuesLine))
    {
        SplittedRoverValues = RoverValuesLine.Split(' ');   

        if (!string.IsNullOrEmpty(SplittedRoverValues.ElementAtOrDefault(0)) && !string.IsNullOrEmpty(SplittedRoverValues.ElementAtOrDefault(1)) && !string.IsNullOrEmpty(SplittedRoverValues.ElementAtOrDefault(2)))
        {
            if (int.TryParse(SplittedRoverValues.ElementAtOrDefault(0), out tempInt) && int.TryParse(SplittedRoverValues.ElementAtOrDefault(1), out tempInt)&& onlyLetters(SplittedRoverValues[2]))
            {
               
                var PositionX = Convert.ToInt32(SplittedRoverValues[0]);
                var PositionY = Convert.ToInt32(SplittedRoverValues[1]);
                var Direction = SplittedRoverValues[2];

                var RoverPathCommandLine = Console.ReadLine();

                if (!string.IsNullOrEmpty(RoverPathCommandLine))
                {
                    if (onlyLetters(RoverPathCommandLine))
                    {
                        if (RoverPathCommandLine.Contains('L')|| RoverPathCommandLine.Contains('M')|| RoverPathCommandLine.Contains('R'))
                        {
                            if (Direction.Equals('N')|| Direction.Equals('S')|| Direction.Equals('E')|| Direction.Equals('W'))
                            {
                                RoverModelList[i] = _service.CalculateRoverCoordinates(Width, Height, RoverPathCommandLine, PositionX, PositionY, Direction);

                            }
                            
                        }                        

                    }
                }

            }
           
        }
        else
        {
            i--;
            continue;
        }
    }

}
Console.WriteLine("Output:");
if (RoverModelList != null &&RoverModelList.Any() && RoverModelList[0] !=null)
{
    for (int i = 0; i < RoverModelList.Length; i++)
    {
        
        Console.WriteLine(RoverModelList[i].PositionX + " " + RoverModelList[i].PositionY + " " + RoverModelList[i].Direction);

    }
}

static bool onlyLetters(string str)
{    
    return Regex.IsMatch(str, @"^[a-zA-Z]+$");
}