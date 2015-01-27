using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiReader.Core
{
    public class Controler
    {
        public Controler(FileInfo fichier)
        {
            FileInfo = fichier;
            FileContent = File.ReadAllText(FileInfo.FullName, new UTF8Encoding());
            Methods = new List<Method>();
            ExtractMethods();
        }

        public string ClassName { get; set; }
        public FileInfo FileInfo { get; set; }
        public string FileContent { get; set; }
        public List<Method> Methods { get; set; }

        private static readonly Regex MethodRegex = new Regex(@"^(public|internal) (async)? +([\S<>]+) +([\S<>]+)\((.+)?\)$");
        private static readonly Regex AttributRegex = new Regex(@"^\[([\w])+ ?(.+)?\]$");

        public void ExtractMethods()
        {
            var lignes = FileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var attribute = new List<string>();


            for (int i = 0; i < lignes.Length; i++)
            {
                try
                {


                    lignes[i] = lignes[i].Trim();
                    if (lignes[i].Contains(" class "))
                    {
                        ClassName = lignes[i].Substring(lignes[i].IndexOf(" class ", StringComparison.Ordinal));
                    }

                    Match m = MethodRegex.Match(lignes[i]);
                    if (m.Success)
                    {
                        Methods.Add(new Method(m.Groups[4].Value, attribute, m.Groups[5].Value, m.Groups[3].Value));
                        attribute.Clear();

                    }

                    m = AttributRegex.Match(lignes[i]);
                    if (m.Success)
                    {
                        attribute.Add(lignes[i]);

                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("ExtractMethods "+ex.Message);
                }
            }

        }
    }

}