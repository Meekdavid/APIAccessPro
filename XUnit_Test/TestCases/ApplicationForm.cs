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
using APIAccessProDependencies.Helpers.DTOs.Models.Preview;
using APIAccessProDependencies.Helpers.DTOs.Models;
using APIAccessProDependencies.Helpers.DTOs;
using APIAccessProDependencies.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using APIAccessProDependencies.Helpers.DTOs.Models.ApplicationForm;
using APIAccessProDependencies.Helpers.Common;
using APIAccessPro;

namespace XUnit_Test.TestCases
{
    public class ApplicationForm
    {
        private readonly HttpClient _client;
        private readonly IApplicationForm _mockCosmosDbService;
        private readonly IInputValidation _checkInputSafety;

        public ApplicationForm()
        {
            _mockCosmosDbService = A.Fake<IApplicationForm>();
            _checkInputSafety = A.Fake<IInputValidation>();
        }

        [Fact]
        public async void GetApplicationForm_ReturnsOkStatusCode()
        {
            // Arrange:
            //string sqlCosmosQuery = A.Fake<string>();
            var methodReturnResponse = A.Fake<MethodReturnResponse<List<ApplicationFormDTO>>>();
            A.CallTo(() => _mockCosmosDbService.RetrieveFormsAsync(Utils.SelectQuery)).Returns(methodReturnResponse);
            var controller = new ApplicationFormController(_mockCosmosDbService, _checkInputSafety);

            // Act:
            var actionResult = await controller.RetrieveForms();

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<ApplicationFormResponse>));

            var okResult = actionResult as ActionResult<ApplicationFormResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Fact]
        public async void UpdateApplicationForm_ReturnOkStatusCode()
        {
            //Arrange
            //var requestPayload = A.Fake<ApplicationFormDTO>();
            var requestPayload = RetreivePayload.applicationFormDTO;
            string phoneNo = RetreivePayload.applicationFormDTO.personal_Information.phone;
            var phoneValidationResponse = A.Fake<MethodReturnResponse<bool>>();
            var methodReturnResponse = A.Fake<MethodReturnResponse<ApplicationFormDTO>>();
            A.CallTo(() => _checkInputSafety.ValidatePhone(phoneNo)).Returns(phoneValidationResponse);
            A.CallTo(() => _mockCosmosDbService.UpdateFormAsync(requestPayload)).Returns(methodReturnResponse);

            var controller = new ApplicationFormController(_mockCosmosDbService, _checkInputSafety);

            // Act:
            var actionResult = await controller.UpdateForms(requestPayload);

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<ApplicationFormResponse>));

            var okResult = actionResult as ActionResult<ApplicationFormResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }

        [Fact]
        public async void AmendQuestionOnApplicationForm_ReturnsOkStatusCode()
        {
            //Arrange
            var requestPayload = A.Fake<AddQuestion>();
            var phoneValidationResponse = A.Fake<MethodReturnResponse<bool>>();
            var methodReturnResponse = A.Fake<MethodReturnResponse<ApplicationFormDTO>>();
            A.CallTo(() => _mockCosmosDbService.AmendFormAsync(requestPayload)).Returns(methodReturnResponse);

            var controller = new ApplicationFormController(_mockCosmosDbService, _checkInputSafety);

            // Act:
            var actionResult = await controller.AmendAddedQuestions(requestPayload);

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<ApplicationFormResponse>));

            var okResult = actionResult as ActionResult<ApplicationFormResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
