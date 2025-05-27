using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using Moq;

namespace EmployeeApp.Application.Tests;

public class EmployeeTests
{
    [Fact]
    public void GetById_ValidId_ReturnEmployee()
    {

        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        employeeRepository
                .Setup(x => x.GetById(10))
                .Returns(new Employee { Id = 10, Name = "John Doe", Email = "John.Doe@lakdj" });

        var employeeService = new EmployeeService(employeeRepository.Object);

        // Act
        var employee = employeeService.GetById(10);

        // Assert
        Assert.NotNull(employee);
        Assert.Equal(10, employee.Id);
        employeeRepository.Verify(x => x.GetById(10), Times.Exactly(1));
    }

    [Fact]
    public void GetAllEmployees_ReturnsListOfEmployees()
    {
        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        employeeRepository
            .Setup(e => e.GetAll())
            .Returns([
                new Employee { Id = 10, Name = "test", Email = "test@asdasd"},
                new Employee { Id = 20, Name = "test2", Email = "test2@asdasd" }
                ]);
        var employeeService = new EmployeeService(employeeRepository.Object);
        // Act
        var employees = employeeService.GetAll();

        // Assert
        Assert.NotNull(employees);
        Assert.Equal(2, employees.Length);
        employeeRepository.Verify(e => e.GetAll(), Times.Exactly(1));

    }

    [Fact]
    public void Add_ShouldAddNewEmployeeToListOfEmployees()
    {
        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        var employeeService = new EmployeeService(employeeRepository.Object);

        // Act
        var newEmployee = new Employee { Id = 30, Name = "New Employee", Email = "new.employee@test.fake.se" };
        employeeService.Add(newEmployee);

        //Assert
        employeeRepository.Verify(e => e.Add(It.IsAny<Employee>()), Times.Exactly(1));
    }

    [Fact]
    public void CheckIsVIP_EmailStartsWithAnders_ReturnsTrue()
    {
        // Arrange
        var employeeRepository = new Mock<IEmployeeRepository>();
        var employeeService = new EmployeeService(employeeRepository.Object);
        var employee = new Employee { Id = 1, Name = "VIP Employee", Email = "anders.fake.employee@mail.fake.se" };

        // Act
        var isVIP = employeeService.CheckIsVIP(employee);

        // Assert
        Assert.True(isVIP);
    }
}
