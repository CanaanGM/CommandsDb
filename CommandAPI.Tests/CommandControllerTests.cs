using AutoMapper;

using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Profiles;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandAPI.Tests
{
    public class CommandControllerTests : IDisposable
    {

        Mock<ICommandAPIRepo> mockRepo;
        CommandProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();


            realProfile = new CommandProfile();
            configuration = new MapperConfiguration(
                cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }
        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);

        }


        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandController(mockRepo.Object, mapper);

            var result = controller.GetAll();

            var okRes = result.Result as OkObjectResult;
            var commands = okRes.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }



        [Fact]
        public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandController(mockRepo.Object, mapper);

            var result = controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.GetAll();

            Assert.IsType<Task<ActionResult>>(result);
        }

        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistentIdProvided()
        {
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.GetCommandById(1).Result;

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_Returns200Ok_WhenValidIDProvided()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.GetCommandById(1).Result;

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandID_Returns200Ok_WhenValidProvided()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.GetCommandById(1);

            Assert.IsType<ActionResult<CommandReadDto>>(result.Result);
        }



        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.CreateCommand(new CommandCreateDto { }).Result;

            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }


        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.CreateCommand(new CommandCreateDto { }).Result;

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }


        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.UpdateCommandById(1,new CommandUpdateDto { }).Result;
           
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenExistentResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.UpdateCommandById(0, new CommandUpdateDto { }).Result;

            Assert.IsType<NotFoundResult>(result);
        }



        [Fact]
        public void PartialCommandupdate_Return404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null );

            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.Partialupdate(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto> { }).Result;

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(
                new Command()
                {
                    Id = 1,
                    HowTo = "MOCK",
                    Platform = "MOCK",
                    CommandLine = "MOCK"
                });
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.DeleteCommand(1).Result;

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public void DeleteCommand_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandController(mockRepo.Object, mapper);
            var result = controller.DeleteCommand(0).Result;
            Assert.IsType<NotFoundResult>(result);

        }


        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command()
                {
                    Id = 0,
                    CommandLine = "dotnet ef migrations add <NAME>",
                    HowTo = "Generate new migration",
                    Platform = "dotnet"
                });
            }
            return commands;
        }
    }
}
