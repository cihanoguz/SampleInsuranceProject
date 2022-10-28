using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Base
{

    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            ValidationErrors = new List<ValidationError>();
        }

        public string Message { get; set; }
        public bool HasError { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }


        public void AddValidationError(string Key, string Value)
        {
            this.HasError = true;
            this.ValidationErrors.Add(new ValidationError { Key = Key, Value = Value });
        }

        /// <summary>
        /// It makes hasError area false, it should be used for erroneous operation.
        /// </summary>
        /// <param name="Message"></param>Base
        public void SetMessage(string Message)
        {
            this.HasError = true;
            this.Message = Message;
        }

        public T Data { get; set; }
    }

    public class ValidationError
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
