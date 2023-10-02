using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Parser
    {
        private static Parser parser = new Parser();

        public static Parser GetParser() { return parser; }
    }
}
