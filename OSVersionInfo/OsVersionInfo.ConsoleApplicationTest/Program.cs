

using System;
using OSVersionInfo;

namespace OsVersionInfo.ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Concat(OsCurrentInfo.Description.Edition.GetDescription()      , ": ", OsCurrentInfo.Edition));
            Console.WriteLine(string.Concat(OsCurrentInfo.Description.OsBits.GetDescription()       , ": ", OsCurrentInfo.OsBits));
            Console.WriteLine(string.Concat(OsCurrentInfo.Description.ProcessorBits.GetDescription(), ": ", OsCurrentInfo.ProcessorBits));
            Console.WriteLine(string.Concat(OsCurrentInfo.Description.ProgramBits.GetDescription()  , ": ", OsCurrentInfo.ProgramBits));
            Console.WriteLine(string.Concat(OsCurrentInfo.Description.ServicePack.GetDescription()  , ": ", OsCurrentInfo.ServicePack));
            Console.WriteLine(string.Concat(OsCurrentInfo.Description.Version.GetDescription()      , ": ", OsCurrentInfo.Version));

            Console.ReadKey();
        }
    }
}
