using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using dnlib.DotNet;
using dnlib.DotNet.MD;
using System.Net.Configuration;
using System.Drawing;
using Console = Colorful.Console;

namespace String_obfuscation
{
    internal class Program
    {
        public const int MaxValue = 2147483647;
        static void Main(string[] args)
        {

            Console.Title = "LoGiC.NET version 1.5 Antitamper remover";
            Console.Write("Drag and drop your files : ", Color.Purple);

            try
            {
                ModuleContext modCtx = ModuleDef.CreateModuleContext();
                string input = Console.ReadLine();
                ModuleDefMD module = ModuleDefMD.Load(input, modCtx);

                Version uneVersion = new Version(module);

                if (uneVersion.logicVersion() == true)
                {
                    Console.WriteLine("LoGiC.NET version 1.5 detected !", Color.Purple);

                    Antitamperkiller kill = new Antitamperkiller(module);
                    if (kill.antitampKill() == true)
                    {
                        Console.WriteLine("Antitamper has been removed !", Color.Green);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Antitamper don't found", Color.Green);
                    }
                }
                else
                {
                    Console.WriteLine("Dont supporte this version", Color.Yellow);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("This is not .Net assembly", Color.Green);
            }
        }
    }
}
