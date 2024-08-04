using HealthInsuranceAPI.DTOs;
using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.Customers;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInsuranceAPI.Services
{
    public class CustomerService : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(CreateCustomerDTO customerDto)
        {
            var customer = new Customer
            {
                CustomerID = Guid.NewGuid(),
                UserID = customerDto.UserID,
                Name = customerDto.Name,
                Address = customerDto.Address,
                Phone = customerDto.Phone,
                DateOfBirth = customerDto.DateOfBirth
            };

            _customerRepository.AddCustomer(customer);
        }

        public CustomerDTO GetCustomerById(Guid customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null) throw new Exception("Customer not found.");

            return new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                UserID = customer.UserID,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                DateOfBirth = customer.DateOfBirth
            };
        }

        public void UpdateCustomer(UpdateCustomerDTO customerDto)
        {
            var customer = _customerRepository.GetCustomerById(customerDto.CustomerID);
            if (customer == null) throw new Exception("Customer not found.");

            customer.Name = customerDto.Name;
            customer.Address = customerDto.Address;
            customer.Phone = customerDto.Phone;
            customer.DateOfBirth = customerDto.DateOfBirth;

            _customerRepository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(Guid customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null) throw new Exception("Customer not found.");

            _customerRepository.DeleteCustomer(customerId);
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            return customers.Select(customer => new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                UserID = customer.UserID,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                DateOfBirth = customer.DateOfBirth
            }).ToList();
        }
    }
}
