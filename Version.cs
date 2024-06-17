using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_obfuscation
{
    internal class Version
    {
        private ModuleDef leModule;
        public Version(ModuleDef unModule)
        {
            this.leModule = unModule;
        }

        public bool logicVersion()
        {
            bool rep = false;

            foreach (TypeDef type in leModule.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    for (int x = 0; x < leModule.CustomAttributes.Count; x++)
                    {
                        CustomAttribute CA = leModule.CustomAttributes[x];
                        foreach (CAArgument constrArgs in CA.ConstructorArguments)
                        {
                            if (constrArgs.Value.ToString().Contains("Obfuscated with LoGiC.NET version 1.5."))
                            {
                                rep = true;
                            }
                        }
                    }
                }
            }

            return rep;
        }
    }
}
