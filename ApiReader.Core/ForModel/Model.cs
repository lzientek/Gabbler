using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ApiReader.Core.ForModel;
using Newtonsoft.Json;

namespace ApiReader.Core
{
    public class Model : IModel
    {
        private string _json;

        public Model(FileInfo fichier)
        {
            FileInfo = fichier;
            FileContent = File.ReadAllText(FileInfo.FullName, new UTF8Encoding());
            Proprietes = new List<Propriete>();
            ExtractProperties();
        }


        private void ExtractProperties()
        {
            foreach (var ligne in FileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var l = ligne.Trim();
                var m = RegexHelper.PropRegex.Match(l);
                if (m.Success)
                {
                    Proprietes.Add(new Propriete()
                    {
                        Name = m.Groups[2].Value,
                        Type = m.Groups[1].Value
                    });
                }
                m = RegexHelper.ClassNameRegex.Match(l);
                if (m.Success)
                {
                    ClassName = m.Groups[1].Value;
                }
            }
        }

        public string Json
        {
            get
            {
                ToJson();
                return _json;
            }

        }

        public string PropName { get; set; }


        private void ToJson()
        {
            using (var writer = new StringWriter(new StringBuilder()))
            {
                var js = new JsonTextWriter(writer);
                js.Formatting = Formatting.Indented;
                if (ClassName.StartsWith("IEnumerable")) { js.WriteStartArray(); }

                js.WriteStartObject();

                foreach (var propriete in Proprietes)
                {
                    js.WritePropertyName(propriete.Name);
                    js.WriteValue(propriete.Type);
                }
                js.WriteEndObject();
                if (ClassName.StartsWith("IEnumerable")) { js.WriteEndArray(); }

                _json = writer.GetStringBuilder().ToString();
            }

        }

        public FileInfo FileInfo { get; set; }
        public string FileContent { get; set; }
        public List<Propriete> Proprietes { get; set; }
        public string ClassName { get; set; }
    }
}