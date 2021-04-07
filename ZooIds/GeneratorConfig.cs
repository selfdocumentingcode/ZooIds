using System;
using System.Collections.Generic;
using System.Text;

namespace ZooIds
{
    public class GeneratorConfig
    {
        public uint NumAdjectives { get; set; } = 2;
        public string Delimiter { get; set; } = "-";
        public CaseStyle CaseStyle { get; set; } = CaseStyle.LowerCase;

        public GeneratorConfig(uint numAdjectives, string delimiter, CaseStyle caseStyle)
        {
            NumAdjectives = numAdjectives;
            Delimiter = delimiter;
            CaseStyle = caseStyle;
        }

        private GeneratorConfig() { }

        public static GeneratorConfig Default => new GeneratorConfig();
        public static GeneratorConfig Gfycat => new GeneratorConfig(3, string.Empty, CaseStyle.LowerCase);
    }
}
