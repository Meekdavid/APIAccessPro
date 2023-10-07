using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using Xunit;
using APIAccessPro;
using APIAccessPro.Controllers;
using APIAccessProDependencies.Helpers.DTOs.Models.ApplicationForm;
using APIAccessProDependencies.Helpers.DTOs.Models;
using APIAccessProDependencies.Helpers.DTOs;
using APIAccessProDependencies.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using APIAccessProDependencies.Helpers.DTOs.Models.Program;
using APIAccessProDependencies.Helpers.Common;

namespace XUnit_Test.TestCases
{
    public class Program
    {
        private readonly HttpClient _client;
        private readonly IProgram _mockCosmosDbService;
        private readonly IInputValidation _checkInputSafety;

        public Program()
        {
            _mockCosmosDbService = A.Fake<IProgram>();
            _checkInputSafety = A.Fake<IInputValidation>();
        }

        [Fact]
        public async void GetPrograms_ReturnsOkStatusCode()
        {
            // Arrange:
            var methodReturnResponse = A.Fake<MethodReturnResponse<List<ProgramDTO>>>();
            A.CallTo(() => _mockCosmosDbService.RetrieveProgramsAsync(Utils.SelectQuery)).Returns(methodReturnResponse);
            var controller = new ProgramController(_checkInputSafety, _mockCosmosDbService);

            // Act:
            var actionResult = await controller.RetrievePrograms();

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<ProgramResponse>));

            var okResult = actionResult as ActionResult<ProgramResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Fact]
        public async void UpdatePrograms_ReturnOkStatusCode()
        {
            //Arrange
            //var requestPayload = A.Fake<ProgramDTO>();
            var requestPayload = RetreivePayload.ProgramDTO;
            var methodReturnResponse = A.Fake<MethodReturnResponse<ProgramDTO>>();
            A.CallTo(() => _mockCosmosDbService.UpdateProgramAsync(requestPayload)).Returns(methodReturnResponse);

            var controller = new ProgramController(_checkInputSafety, _mockCosmosDbService);

            // Act:
            var actionResult = await controller.UpdateProgram(requestPayload);

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<ProgramResponse>));

            var okResult = actionResult as ActionResult<ProgramResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Fact]
        public async void AddNewProgram_ReturnsOkStatusCode()
        {
            //Arrange
            //var requestPayload = A.Fake<ProgramDTO>();
            var requestPayload = RetreivePayload.ProgramDTO;
            var methodReturnResponse = A.Fake<MethodReturnResponse<ProgramDTO>>();
            A.CallTo(() => _mockCosmosDbService.AddProgramAsync(requestPayload)).Returns(methodReturnResponse);

            var controller = new ProgramController(_checkInputSafety, _mockCosmosDbService);

            // Act:
            var actionResult = await controller.AddPrograms(requestPayload);

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<ProgramResponse>));

            var okResult = actionResult as ActionResult<ProgramResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
