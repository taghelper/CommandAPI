using System;
using CommandAPI.Models;
using Xunit;

namespace test.CommandAPI.Tests
{
    //because of having three identical Arrange components inside the test methods, we commented out this class

    //but it would be useful to review them later
    // public class CommandTests
    // {
    //     //be aware not to forget about decorating endpoints with [Fact] attribute!
    //     [Fact]
    //     public void CanChangeHowTo()
    //     {
    //         //Arrange
    //         var testCommand =
    //             new Command()
    //             {
    //                 HowTo = "Do something awsome",
    //                 Platform = "xUnit",
    //                 CommandLine = "dotnet test"
    //             };
    //         //Act
    //         testCommand.HowTo = $"Execute Unit Tests";
    //         //testCommand.Platform=$"Unit";
    //         //testCommand.CommandLine=$"dotnet core";
    //         //Assert
    //         Assert.Equal("Execute Unit Tests", testCommand.HowTo);
    //         //Assert.Equal("nit", testCommand.Platform);//causees the test method to fail!
    //         // Assert.Equal("Unit", testCommand.Platform);
    //         // Assert.Equal("dotnet core", testCommand.CommandLine);
    //         //point: I've commented out the thre prevoius lines above! because
    //         //we should test only one thing each time!
    //     }
    //     //be aware not to forget about decorating endpoints with [Fact] attribute!
    //     [Fact]
    //     public void ChangePlatform()
    //     {
    //         //Arrange
    //         var testCommand =
    //             new Command()
    //             {
    //                 HowTo = "Do something awsome",
    //                 Platform = "xUnit",
    //                 CommandLine = "dotnet test"
    //             };
    //         //Act
    //         testCommand.Platform = "newUnit";
    //         //Assert
    //         Assert.Equal("newUnit", testCommand.Platform);
    //     }
    //     //be aware not to forget about decorating endpoints with [Fact] attribute!
    //     [Fact]
    //     public void ChangeCommandLine()
    //     {
    //         //Arrange
    //         var testCommand =
    //             new Command {
    //                 HowTo = "Do something awsome",
    //                 Platform = "xUnit",
    //                 CommandLine = "dotnet test"
    //             };
    //         //Act
    //         testCommand.CommandLine = "new Command";
    //         //Assert
    //         Assert.Equal("new Command", testCommand.CommandLine);
    //     }
    //}

    //
    //because we have three Arrange components in all our test methods, we created the new class below:
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand=new Command{
                HowTo = "Do something awsome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };              ;
        }

        public void Dispose(){
            testCommand=null;
        }

//the arrange component will be left empty in the three following test methods.
        [Fact]
        public void CanChangeHowTo(){
            //Arrange

            //Act
            testCommand.HowTo="Change How To";

            //Asset
            Assert.Equal("Change How To",testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform(){
            //Arrange

            //Act
            testCommand.Platform="Change Platform";

            //Assert
            Assert.Equal("Change Platform",testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine(){
        //Arrange

        //Act
        testCommand.CommandLine="Change Command Line";

        //Assert
        Assert.Equal("Change Command Line",testCommand.CommandLine);
        }
    }
}
