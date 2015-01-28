using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReader.Core.ForModel
{
    public class SimpleModel:IModel
    {
        public SimpleModel()
        {
        }
        public string ClassName { get; set; }
        public string Json { get { return null; } }
        public string PropName { get; set; }
    }
}
