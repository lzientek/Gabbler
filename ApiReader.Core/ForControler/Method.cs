using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using ApiReader.Core.ForModel;

namespace ApiReader.Core
{
    public class Method
    {
        public Method(string methodName, List<string> attribute, string args, string returnType)
        {
            MethodName = methodName;
            ReturnType = returnType;
            Arguments = new List<MethodArgs>();
            Authorize = false;
            TransformArgs(args);
            TransformAttribute(attribute);
        }

        private void TransformArgs(string args)
        {
            foreach (var arg in args.Split(new[] { "," }, StringSplitOptions.None))
            {
                try
                {
                    var arguments = new MethodArgs();
                    var argt = arg.Trim();
                    if (argt.StartsWith("["))
                    {
                        if (argt.StartsWith("[FromBody]"))
                        {
                            arguments.IsFromBody = true;
                        }
                        int start = argt.IndexOf('[');
                        argt = argt.Remove(start, (argt.IndexOf(']') - start) + 1).Trim();
                    }
                    var split = argt.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    arguments.Type = split[0];
                    arguments.Name = split[1];
                    Arguments.Add(arguments);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("TransformArgs :" + ex.Message);
                }
            }
        }

        public void TransformAttribute(List<string> attribute)
        {
            foreach (var attr in attribute)
            {
                var att = attr.Trim(' ', '[', ']');
                if (!att.Contains("("))
                {
                    switch (att)
                    {
                        case "HttpPost":
                            HttpMethod = HttpMethod.Post;
                            break;
                        case "HttpDelete":
                            HttpMethod = HttpMethod.Delete;
                            break;
                        case "HttpPut":

                            HttpMethod = HttpMethod.Put;
                            break;
                        case "HttpGet":
                            HttpMethod = HttpMethod.Get;
                            break;
                        case "Authorize":
                            Authorize = true;
                            break;
                        case "AllowAnonimous":
                            Authorize = false;
                            break;

                    }
                }
                else
                {
                    var args = att.Substring(att.IndexOf('('));
                    att = att.Remove(att.IndexOf('('));
                    var values = args.Split(',');

                    switch (att)
                    {
                        case "Route":
                            Route = values[0].Trim('"', '(', ')');
                            break;
                        case "Authorize":
                            AuthorizeDetails = string.Join("; ", values);
                            break;
                        case "ResponseType":
                            ReturnType = values[0].Remove(0, ("(typeof(").Length).Trim(')');
                            break;
                    }

                }
            }

        }

        private string RemoveList(string arg)
        {
            return arg.Replace("IEnumerable", "").Trim('<', '>');
        }

        public MethodModels FindMethodsModels(IEnumerable<Model> models)
        {
            var returnObject = new MethodModels();

            foreach (var arg in Arguments)
            {
                var realType = RemoveList(arg.Type);
                if (models.Any(m => m.ClassName == realType))
                {
                    var mod = models.First(m => m.ClassName == realType);
                    mod.PropName = arg.Name;
                    mod.ClassName = arg.Type;
                    returnObject.Arguments.Add(mod);
                }
                else
                {
                    returnObject.Arguments.Add(new SimpleModel() { ClassName = arg.Type,PropName = arg.Name});
                }


            }

            //type de retour
            returnObject.ReturnModel = models.FirstOrDefault(m => m.ClassName == RemoveList(ReturnType));

            if (returnObject.ReturnModel != null)
            {
                returnObject.ReturnModel.ClassName = ReturnType;
            }
            else
            {
                returnObject.ReturnModel = new SimpleModel()
                {
                    ClassName = ReturnType,
                };
            }
            return returnObject;
        }


        public string Route { get; set; }

        public string MethodName { get; set; }
        public List<MethodArgs> Arguments { get; set; }

        public HttpMethod HttpMethod { get; set; }
        public string ReturnType { get; set; }
        public bool Authorize { get; set; }
        public string AuthorizeDetails { get; set; }
    }
}