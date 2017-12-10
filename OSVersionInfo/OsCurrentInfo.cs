
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using OSInfo = OperatingSystemInfos.Windows;

namespace OSVersionInfo
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retourne la valeur de l'attribut [Description]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }

    public static class OsCurrentInfo
    {
        public enum Description
        {
            [Description("Name")]         
            Name,
            [Description("Edition" )]     
            Edition,
            [Description("Service Pack")] 
            ServicePack,
            [Description("Version")]      
            Version,
            [Description("ProcessorBits")]
            ProcessorBits,
            [Description("OSBits")]      
            OsBits,
            [Description("ProgramBits")]
            ProgramBits,
        }

        #region Properties


        /// <summary>
        /// Determines if our current OS is Win10.
        /// </summary>
        public static bool? IsWin10
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Name.GetDescription()))
                    return System.String.Equals(OsVersionInfosDictionnary[Description.Name.GetDescription()], "Windows 10", StringComparison.OrdinalIgnoreCase);

                return null;
            }
        }

        /// <summary>
        /// Determines if our current OS is Win8.1.
        /// </summary>
        public static bool? IsWin8dot1
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Name.GetDescription()))
                    return System.String.Equals(OsVersionInfosDictionnary[Description.Name.GetDescription()], "Windows 8.1", StringComparison.OrdinalIgnoreCase);

                return null;
            }
        }

        /// <summary>
        /// Determines if our current OS is Win8.
        /// </summary>
        public static bool? IsWin8
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Name.GetDescription()))
                    return System.String.Equals(OsVersionInfosDictionnary[Description.Name.GetDescription()], "Windows 8", StringComparison.OrdinalIgnoreCase);

                return null;
            }
        }


        /// <summary>
        /// Determines if our current OS is Win7.
        /// </summary>
        public static bool? IsWin7
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Name.GetDescription()))
                    return System.String.Equals(OsVersionInfosDictionnary[Description.Name.GetDescription()], "Windows 7", StringComparison.OrdinalIgnoreCase);

                return null;
            }
        }

        /// <summary>
        /// Determines if our current OS is WinXP.
        /// </summary>
        public static bool? IsWinXP
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Name.GetDescription()))
                    return System.String.Equals(OsVersionInfosDictionnary[Description.Name.GetDescription()], "Windows XP", StringComparison.OrdinalIgnoreCase);

                return null;
            }
        }

        /// <summary>
        /// Determines if our current OS is WinVista.
        /// </summary>
        public static bool? IsWinVista
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Name.GetDescription()))
                    return System.String.Equals(OsVersionInfosDictionnary[Description.Name.GetDescription()], "Windows Vista", StringComparison.OrdinalIgnoreCase);

                return null;
            }
        }

        /// <summary>
        /// Retrieves the current version of the os.
        /// </summary>
        public static string Version
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Version.GetDescription()))
                    return OsVersionInfosDictionnary[Description.Version.GetDescription()];

                return null;
            } 
        }

        /// <summary>
        /// Retrieves the current service pack of the os.
        /// </summary>
        public static string ServicePack
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.ServicePack.GetDescription()))
                    return OsVersionInfosDictionnary[Description.ServicePack.GetDescription()];

                return null;
            }
        }

        /// <summary>
        /// Retrieves the current edition of the os.
        /// </summary>
        public static string Edition
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.Edition.GetDescription()))
                    return OsVersionInfosDictionnary[Description.Edition.GetDescription()];

                return null;
            }
        }

        /// <summary>
        /// Retrieves the bitness of the processor.
        /// </summary>
        public static string ProcessorBits
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.ProcessorBits.GetDescription()))
                    return OsVersionInfosDictionnary[Description.ProcessorBits.GetDescription()];

                return null;
            }
        }

        /// <summary>
        /// Retrieves the bitness of the current process.
        /// </summary>
        public static string ProgramBits
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.ProgramBits.GetDescription()))
                    return OsVersionInfosDictionnary[Description.ProgramBits.GetDescription()];

                return null;
            }
        }

        /// <summary>
        /// Retrieves the bitness of the os.
        /// </summary>
        public static string OsBits
        {
            get
            {
                if (OsVersionInfosDictionnary.ContainsKey(Description.OsBits.GetDescription()))
                    return OsVersionInfosDictionnary[Description.OsBits.GetDescription()];

                return null;
            }
        }

        #endregion 

        #region Variables
        /// <summary>
        /// This dictionnary contains the current os infos.
        /// </summary>
        public static IDictionary<string, string> OsVersionInfosDictionnary = new Dictionary<string, string>();
        #endregion

        #region Ctor

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static OsCurrentInfo()
        {
            Init();
        }

        #endregion

        #region Methods


        /// <summary>
        /// Loading os informations on cache.
        /// </summary>
        private static void Init()
        {
            try
            {
                if (OsVersionInfosDictionnary.Any()) return;

                OsVersionInfosDictionnary.Add(Description.Name.GetDescription()         , OSInfo.OSVersionInfo.Name);

                OsVersionInfosDictionnary.Add(Description.Edition.GetDescription()      , OSInfo.OSVersionInfo.Edition);

                OsVersionInfosDictionnary.Add(Description.ServicePack.GetDescription()  , OSInfo.OSVersionInfo.ServicePack);

                OsVersionInfosDictionnary.Add(Description.Version.GetDescription()      , OSInfo.OSVersionInfo.VersionString);

                OsVersionInfosDictionnary.Add(Description.ProcessorBits.GetDescription(), OSInfo.OSVersionInfo.ProcessorBits.ToString());

                OsVersionInfosDictionnary.Add(Description.OsBits.GetDescription()       , OSInfo.OSVersionInfo.OSBits.ToString());

                OsVersionInfosDictionnary.Add(Description.ProgramBits.GetDescription()  , OSInfo.OSVersionInfo.ProgramBits.ToString());
            }
            catch
            {
            }

        }
        #endregion

    }
}
