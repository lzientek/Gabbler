using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiReader.Core
{
    public class RegexHelper
    {
        public static readonly Regex MethodRegex = new Regex(@"^(public|internal) (async)? +([\S<>]+) +([\S<>]+)\((.+)?\)$");
        public static readonly Regex AttributRegex = new Regex(@"^\[([\w])+ ?(.+)?\]$");
        public static readonly Regex PropRegex = new Regex(@"^public +([\S<>]+) +([a-zA-Z]+) +({|{.+})?$");
        public static readonly Regex ClassNameRegex = new Regex(@"^public class +([a-zA-Z]+)");
    }
}
