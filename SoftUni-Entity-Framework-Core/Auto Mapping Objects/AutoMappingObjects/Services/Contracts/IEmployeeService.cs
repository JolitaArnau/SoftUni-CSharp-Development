namespace AutoMappingObjects.Services.Contracts
{
    using System;
    
    using Models;
    using ModelsDto;


    public interface IEmployeeService
    {
        Employee ById(int id);

        void Add(string firstName, string lastName, decimal salary);

        void SetBirthday(int id, DateTime birthday);

        void SetAddress(int id, string address);

        void SetManager(int emloyeeId, int managerId);

        EmployeeDto GetEmployeeInfo(int id);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int id);

        ManagerDto GetManagerInfo(int id);

        string OlderThan(int age);
    }
}