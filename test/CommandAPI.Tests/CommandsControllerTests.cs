using System;
using xUnit;
using src.CommandAPI.Models;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Profiles;

namespace test.CommandAPI.Tests
{
    public class CommandsControllerTests
    {
        [Fact]
        public void GetCommandItems_Returns200Ok_WhenDBIsEmpty(){
            //Arrange
            ver mockRepo=new Mock<ICommandAPIRepo>();

            mockRepo.Setup(repo=>
                repo.GetAllCommands()).Returns(GetComands(0));


            //
            var realProfile=new CommandsProfile();   
            var configuration=new MapperConfiguration(cfg=>
                cfg.AddProfile(realProfile);
            ); 
            IMapper mapper=new Mapper(configuration);

            //var controller= new CommandsController(mockRepo.Object,/*AutoMapper*/);  
            var controller= new CommandsController(mockRepo.Object,mapper);  
        }

        private List<Command> GetComands(int num){
            var commands=new List<Command>();

            if (num>0)
            {
                commands.Add(new Command{
                    Id=0,
                    HowTo="How to generate a migration",
                    CommandLine="dotnet ef migrations add <name of Migration>",
                    Platform=".NET Core ef"
                });
            }

            return commands;
        }
        
    }
}