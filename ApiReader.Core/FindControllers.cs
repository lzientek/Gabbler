using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiReader.Core
{
    public class FindControllers
    {
        private string _controlerPath;
        private string[] _modelsPath;

        public List<Controler> Controlers { get; set; }
        public List<Model> Models { get; set; }

        public FindControllers(string controlerPath, params string[] modelPaths)
        {
            _controlerPath = controlerPath;
            _modelsPath = modelPaths;
            Controlers = new List<Controler>();
            Models = new List<Model>();
            GetControllerAndMethods();
        }

        public void GetControllerAndMethods()
        {
            var dir = new DirectoryInfo(_controlerPath);
            var fichiers = dir.GetFiles("*Controller.cs", SearchOption.AllDirectories);

            foreach (FileInfo fichier in fichiers)
            {
                Controlers.Add(new Controler(fichier));
            }
            foreach (var mp in _modelsPath)
            {
                dir = new DirectoryInfo(mp);
                fichiers = dir.GetFiles("*.cs", SearchOption.AllDirectories);

                foreach (FileInfo fichier in fichiers)
                {
                    Models.Add(new Model(fichier));
                }
            }

        }

    }
}
