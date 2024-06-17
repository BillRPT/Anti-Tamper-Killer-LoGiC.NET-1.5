using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using System;

namespace String_obfuscation
{
    internal class Antitamperkiller
    {
        private ModuleDef leModule;
        public Antitamperkiller(ModuleDef unModule)
        {
            this.leModule = unModule;
        }

        public bool antitampKill()
        {
            bool rep = false;
            // Chercher la méthode qui contient l'antitamper
            foreach (TypeDef type in leModule.Types)
            {
                // Parcourir toutes les méthodes du type
                foreach (MethodDef method in type.Methods)
                {
                    // Si la méthode n'a pas de corps, l'ignorer
                    if (!method.HasBody) continue;

                    for (int i = 0; i < method.Body.Instructions.Count; i++)
                    {
                        Instruction instr = method.Body.Instructions[i];

                        // Vérifier si l'instruction est un appel à .ctor de EntryPointNotFoundException
                        if (instr.OpCode == OpCodes.Newobj && instr.Operand is MemberRef memberRef && memberRef.FullName == "System.Void System.EntryPointNotFoundException::.ctor()")
                        {
                            // Remplacer l'instruction newobj par nop
                            method.Body.Instructions[i].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i].Operand = null;
                            //Remplacer l'instruction throw par nop
                            method.Body.Instructions[i + 1].OpCode = OpCodes.Nop;
                            method.Body.Instructions[i + 1].Operand = null;

                            // Conserver l'ancienne valeur de MaxStack
                            method.Body.KeepOldMaxStack = true;

                            rep = true;

                            //Réecrire le fichier avec les options que j'ai mit

                            var writerOptions = new ModuleWriterOptions(leModule)
                            {
                                Logger = DummyLogger.NoThrowInstance,
                                MetadataOptions = { Flags = MetadataFlags.KeepOldMaxStack }
                            };
                            //Save sur le bureau
                            leModule.Write(@"C:\Users\" + Environment.UserName + "\\Desktop\\AntitampRemoved.exe", writerOptions);

                            break;
                        }
                    }
                }
            }
            return rep;
        }
    }
}
