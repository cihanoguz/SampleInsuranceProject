using API.Code;
using Entity.Policies;
using Model;
using Model.Base;
using Model.Request.Policies;
using Model.Response.Policies;
using Repository.Customers.Interface;
using Repository.Policies.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace API.Controllers
{
    public class PolicyController : BaseController<PolicyController>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOptions<ApplicationSettings> _settings;

        public PolicyController(IPolicyRepository policyRepository
            , IOptions<ApplicationSettings> settings)
        {
            _settings = settings;
            _policyRepository = policyRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Agent")]
        public ActionResult<BaseResponse<PolicyAddResponse>> AddPolicy([FromBody] PolicyAddRequest request)
        {

            var response = new BaseResponse<PolicyAddResponse>();
            response.Data = new PolicyAddResponse();

            try
            {

                var customer = _customerRepository.GetBy(x => x.IdentityNo == request.TCKN).FirstOrDefault();

                if (customer == null)
                {
                    response.Data = new PolicyAddResponse();
                    response.Message = "No such customer found";
                }
                var policy = new Policy
                {
                    PolicyNo = request.PolicyNo,
                    CustomerId = customer.Id,
                    AgentCode = request.AgentCode,
                    EndorsementNo = 0
                };

                _policyRepository.Add(policy);

                response.Data.PolicyNo = policy.PolicyNo;
                response.Data.CustomerFullName = $"{customer.Name} {customer.Surname}";
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Agent")]
        public ActionResult<BaseResponse<PolicyUpdateResponse>> UpdatePolicy([FromBody] PolicyUpdateRequest request)
        {

            var response = new BaseResponse<PolicyUpdateResponse>();
            response.Data = new PolicyUpdateResponse();

            try
            {

                var policy = _policyRepository.GetById(request.PolicyId);
                if (policy == null)
                {
                    response.Message = "No such policy found";
                    response.HasError = true;
                    return response;
                }

                policy.StartDate = request.StartDate;
                policy.EndDate = request.EndDate;
                policy.IssueDate = request.IssueDate;
                policy.EndorsementNo += policy.EndorsementNo;
                _policyRepository.Update(policy);

                response.Data.PolicyId = policy.Id;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<BaseResponse<PolicyUpdateResponse>> DeletePolicy([FromBody] IDRequest request)
        {

            var response = new BaseResponse<PolicyUpdateResponse>();
            response.Data = new PolicyUpdateResponse();

            try
            {

                var policy = _policyRepository.GetById(request.Id);

                _policyRepository.Delete(policy);

                response.Data.PolicyId = policy.Id;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
