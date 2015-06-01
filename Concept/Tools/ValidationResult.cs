using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Concept.Tools
{
    public class ValidationResult
    {
        public List<ValidationItem> ValidationErrors;

        public ValidationResult()
        {
            this.ValidationErrors = new List<ValidationItem>();
        }
        public void Add(int ErrorId, string ErrorTxt)
        {
            this.ValidationErrors.Add(new ValidationItem(ErrorId, ErrorTxt));
        }
        public bool isSuccess()
        {
            return (this.ValidationErrors.Count == 0);
        }
        public String ToJson()
        {
            return (JsonConvert.SerializeObject(this));
        }
    }
}
