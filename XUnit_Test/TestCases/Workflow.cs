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
using APIAccessProDependencies.Helpers.DTOs.Models.Program;
using APIAccessProDependencies.Helpers.DTOs.Models;
using APIAccessProDependencies.Helpers.DTOs;
using APIAccessProDependencies.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using APIAccessProDependencies.Helpers.DTOs.Models.WorkFlow;
using APIAccessProDependencies.Helpers.DTOs.Models.Preview;
using APIAccessProDependencies.Helpers.Common;
using APIAccessProDependencies.Helpers.DTOs.Global;
//using Serilog;

namespace XUnit_Test.TestCases
{
    public class Workflow
    {
        private readonly HttpClient _client;
        private readonly IWorkflow _mockCosmosDbService;
        private readonly IInputValidation _checkInputSafety;

        public Workflow()
        {
            _mockCosmosDbService = A.Fake<IWorkflow>();
            _checkInputSafety = A.Fake<IInputValidation>();
        }

        [Fact]
        public async void GetWorkflows_ReturnsOkStatusCode()
        {
            // Arrange:            
            var mockService = new Mock<IWorkflow>();

            var successResponse = new MethodReturnResponse<List<WorkflowDTO>>
            {
                _message = Utils.StatusMessage_Success,
                objectValue = new List<WorkflowDTO>(),
                Logs = new List<Log>()
            };
            mockService.Setup(service => service.RetrieveWorkflowsAsync(Utils.SelectQuery)).ReturnsAsync(successResponse);
            
            var controller = new WorkflowController(mockService.Object);

            // Act:
            var actionResult = await controller.RetrieveFlows();

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<WorkflowResponse>));

            var okResult = actionResult as ActionResult<WorkflowResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);


            var responseData = okObjectResult.Value as WorkflowResponse;
            responseData.ResponseMessage.Should().NotBeNull();
            responseData.ResponseMessage.Should().Be(Utils.StatusMessage_Success);
            responseData.Workflow.Should().NotBeNull();
            responseData.Workflow.Should().BeOfType<List<WorkflowDTO>>();

        }

        [Fact]
        public async void UpdateWorkflow_ReturnOkStatusCode()
        {
            //Arrange
            var requestPayload = A.Fake<WorkflowDTO>();
            var methodReturnResponse = A.Fake<MethodReturnResponse<WorkflowDTO>>();
            A.CallTo(() => _mockCosmosDbService.UpdateWorkflowsAsync(requestPayload, "stage")).Returns(methodReturnResponse);

            var controller = new WorkflowController(_mockCosmosDbService);

            // Act:
            var actionResult = await controller.UpdateWorkflow(RetreivePayload.workflowDTOHolder);

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<WorkflowResponse>));

            var okResult = actionResult as ActionResult<WorkflowResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
    }
}
