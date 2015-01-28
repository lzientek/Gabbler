using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReader.Core.ForModel
{
    public interface IModel
    {
        string ClassName { get; set; }
        string Json { get; }

        string PropName { get; set; }
    }
}
