using MarsRover.Models;
using MarsRover.Services;
using Xunit;

namespace MarsRoverTest
{
    public class MarsRoverTest
    {
         //Test Input:
         //5 5
         //1 2 N
         //LMLMLMLMM
         //3 3 E
         //MMRMMRMRRM
         //Expected Output:
         //1 3 N
         //5 1 E
        [Fact]
        public void MarsRoverTest1()
        {   
            int Width = 5;
            int Height = 5;
            int PositionX = 1;
            int PositionY = 2;
            string Direction = "N";
            var RoverPathCommandLine = "LMLMLMLMM";
            var _service = new RoverService();
            
            var roverresult = _service.CalculateRoverCoordinates(Width, Height, RoverPathCommandLine, PositionX, PositionY, Direction);
            
            // assert
            var okResult = Assert.IsType<RoverModel>(roverresult);
            Assert.NotNull(roverresult);
            Assert.Equal(1, okResult.PositionX);
            Assert.Equal(3, okResult.PositionY);
            Assert.Equal("N", okResult.Direction);
        }

        [Fact]
        public void MarsRoverTest2()
        {
            int Width = 5;
            int Height = 5;
            int PositionX = 3;
            int PositionY = 3;
            string Direction = "E";
            var RoverPathCommandLine = "MMRMMRMRRM";
            var _service = new RoverService();

            var roverresult = _service.CalculateRoverCoordinates(Width, Height, RoverPathCommandLine, PositionX, PositionY, Direction);

            // assert
            var okResult = Assert.IsType<RoverModel>(roverresult);
            Assert.NotNull(roverresult);
            Assert.Equal(5, okResult.PositionX);
            Assert.Equal(1, okResult.PositionY);
            Assert.Equal("E", okResult.Direction);
        }
    }
}