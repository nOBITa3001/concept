using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concept.Tools
{
    public class ValidationItem
    {
        public int errorID { get; set; }
        public string errorTxt { get; set; }

        public ValidationItem(int Id, String Txt)
        {
            this.errorID = Id;
            this.errorTxt = Txt;
        }

        public int geterrorID()
        {
            return this.errorID;
        }
        public string geterrorTxt()
        {
            return this.errorTxt;
        }

    }
}
