using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Controllers;
using EmployeesApp.Web.Views.Employees;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeApp.Web.Tests
{
    public class EmployeeControllerTests
    {
        [Fact]
        public void Index_Returns_View_Model()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(e => e.GetAll())
            .Returns([
                new Employee { Id = 10, Name = "test", Email = "test@asdasd"},
                new Employee { Id = 20, Name = "test2", Email = "test2@asdasd" }
                ]);
            var controller = new EmployeesController(employeeService.Object);
            // Act
            var result = controller.Index();

            // Assert

            Assert.IsType<ViewResult>(result);


        }
        [Fact]
        public void Create_ValidModel_ShouldAddEmployeeAndRedirect()
        {
            // Arrange
            var mockService = new Mock<IEmployeeService>();

            var controller = new EmployeesController(mockService.Object);

            var viewModel = new CreateVM {
                Name = "Test",
                Email = "test@example.com",
                BotCheck = 4 
            };

            // Act
            var result = controller.Create(viewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            mockService.Verify(s => s.Add(It.Is<Employee>(
                e => e.Name == viewModel.Name && e.Email == viewModel.Email)), Times.Once);
        }

        [Fact]

        public void Details_Return_View_Model()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(e => e.GetById(10))
                .Returns(new Employee { Id = 10, Name = "test", Email = "test@asdasd" });
            var controller = new EmployeesController(employeeService.Object);
            // Act
            var result = controller.Details(10);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<DetailsVM>(viewResult.Model);
            var model = (DetailsVM)viewResult.Model;
            Assert.Equal(10, model.Id);
        }
    } 
}

