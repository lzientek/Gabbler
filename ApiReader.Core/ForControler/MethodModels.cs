using System.Collections.Generic;
using ApiReader.Core.ForModel;

namespace ApiReader.Core
{
    public class MethodModels
    {
        public MethodModels()
        {
            Arguments = new List<IModel>();
        }
        public List<IModel> Arguments { get; set; }
        public IModel ReturnModel{ get; set; }
    }
}