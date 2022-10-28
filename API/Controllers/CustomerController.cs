using API.Code;
using Entity.Customers;
using Model;
using Model.Base;
using Model.Request.Customers;
using Model.Response.Customers;
using Repository.Customers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class CustomerController : BaseController<CustomerController>
    {
        private readonly ICustomerRepository _customerRepository; 
        private readonly IOptions<ApplicationSettings> _settings;

        public CustomerController(ICustomerRepository customerRepository
            , IOptions<ApplicationSettings> settings)
        {
            _settings = settings;
            _customerRepository = customerRepository; 
        }


        [HttpPost]
        [Authorize(Roles ="Admin,Agent")]
        public ActionResult<BaseResponse<CustomerAddResponse>> AddCustomer([FromBody] CustomerAddRequest request)
        {
           
            var response = new BaseResponse<CustomerAddResponse>();
            response.Data = new CustomerAddResponse();
            
            try
            {

                var checkIdentity = _customerRepository.Any(x => x.IdentityNo == request.IdentityNo);
                if (checkIdentity)
                {
                    response.Message = "More than one customer with the same Identity No cannot be created.";
                    response.HasError = true;
                    return response;
                }
                    
                var customer = new Customer
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    IdentityNo = request.IdentityNo,
                };

                _customerRepository.Add(customer);
                 
                response.Data.Name = customer.Name;
                response.Data.SurName = customer.Surname;
                response.Data.Id = customer.Id;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Agent")]
        public ActionResult<BaseResponse<CustomerUpdateResponse>> UpdateCustomer([FromBody] CustomerUpdateRequest request)
        {

            var response = new BaseResponse<CustomerUpdateResponse>();
            response.Data = new CustomerUpdateResponse();

            try
            {

                var checkIdentity = _customerRepository.Any(x => x.IdentityNo == request.IdentityNo && x.Id != request.CustomerId);
                if (checkIdentity)
                {
                    response.Message = "More than one customer with the same Identity No cannot be created.";
                    response.HasError = true;
                    return response;
                }

                var customer = new Customer
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    IdentityNo = request.IdentityNo,
                    Id = request.CustomerId
                };

                _customerRepository.Update(customer);

                response.Data.Name = customer.Name;
                response.Data.SurName = customer.Surname;
                response.Data.Id = customer.Id;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<BaseResponse<CustomerUpdateResponse>> DeleteCustomer([FromBody] IDRequest request)
        {

            var response = new BaseResponse<CustomerUpdateResponse>();
            response.Data = new CustomerUpdateResponse();

            try
            {

                var customer = _customerRepository.GetById(request.Id);
               
                _customerRepository.Delete(customer);

                response.Data.Name = customer.Name;
                response.Data.SurName = customer.Surname;
                response.Data.Id = customer.Id;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Agent")]
        public ActionResult<BaseResponse<CustomerResponse>> GetAllCustomerById([FromBody] IDRequest request)
        {

            var response = new BaseResponse<CustomerResponse>();
            response.Data = new CustomerResponse();

            try
            {
                var customer = _customerRepository.GetById(request.Id);
                
                response.Data = new CustomerResponse(customer);
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Agent")]
        public ActionResult<BaseResponse<List<CustomerResponse>>> GetAllCustomer()
        {

            var response = new BaseResponse<List<CustomerResponse>>();
            response.Data = new List<CustomerResponse>();

            try
            {
                var customer = _customerRepository.GetAll();

                response.Data = customer.Select(x => new CustomerResponse(x)).ToList();
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
