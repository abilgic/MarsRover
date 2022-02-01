using MarsRover.Models;
using MarsRover.Services;
using System.Text.RegularExpressions;

const int RoverNumber = 2;//Number of Rovers
int Width=0;
int Height=0;
var _service = new RoverService();
RoverModel[] rovermodel = new RoverModel[2];

_service.GetAreaValues(ref  Width, ref  Height);

for (int i = 0; i < RoverNumber; i++)
{
    var pdmodel= _service.GetPositionAndDirection();
    string? RoverPathCommandLine = _service.GetMovementAction();

    if (!string.IsNullOrEmpty(RoverPathCommandLine))
    {
       rovermodel[i] = _service.CalculateRoverCoordinates(Width, Height, RoverPathCommandLine, pdmodel.PositionX, pdmodel.PositionY, pdmodel.Direction);

    }    
}
Console.WriteLine("Output:");
_service.PrintResult(rovermodel);

