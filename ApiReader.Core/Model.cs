using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApiReader.Core
{
    public class Model
    {
        public Model(FileInfo fichier)
        {
            FileInfo = fichier;
            FileContent = File.ReadAllText(FileInfo.FullName, new UTF8Encoding());
            Proprietes = new List<Propriete>();
        }

        public FileInfo FileInfo { get; set; }
        public string FileContent { get; set; }
        public List<Propriete> Proprietes { get; set; } 
    }
}