

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OperatingSystemInfos.Windows
{
  internal static class OSVersionInfo
  {
    private static string s_Edition;
    private static string s_Name;
    private const int PRODUCT_UNDEFINED = 0;
    private const int PRODUCT_ULTIMATE = 1;
    private const int PRODUCT_HOME_BASIC = 2;
    private const int PRODUCT_HOME_PREMIUM = 3;
    private const int PRODUCT_ENTERPRISE = 4;
    private const int PRODUCT_HOME_BASIC_N = 5;
    private const int PRODUCT_BUSINESS = 6;
    private const int PRODUCT_STANDARD_SERVER = 7;
    private const int PRODUCT_DATACENTER_SERVER = 8;
    private const int PRODUCT_SMALLBUSINESS_SERVER = 9;
    private const int PRODUCT_ENTERPRISE_SERVER = 10;
    private const int PRODUCT_STARTER = 11;
    private const int PRODUCT_DATACENTER_SERVER_CORE = 12;
    private const int PRODUCT_STANDARD_SERVER_CORE = 13;
    private const int PRODUCT_ENTERPRISE_SERVER_CORE = 14;
    private const int PRODUCT_ENTERPRISE_SERVER_IA64 = 15;
    private const int PRODUCT_BUSINESS_N = 16;
    private const int PRODUCT_WEB_SERVER = 17;
    private const int PRODUCT_CLUSTER_SERVER = 18;
    private const int PRODUCT_HOME_SERVER = 19;
    private const int PRODUCT_STORAGE_EXPRESS_SERVER = 20;
    private const int PRODUCT_STORAGE_STANDARD_SERVER = 21;
    private const int PRODUCT_STORAGE_WORKGROUP_SERVER = 22;
    private const int PRODUCT_STORAGE_ENTERPRISE_SERVER = 23;
    private const int PRODUCT_SERVER_FOR_SMALLBUSINESS = 24;
    private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 25;
    private const int PRODUCT_HOME_PREMIUM_N = 26;
    private const int PRODUCT_ENTERPRISE_N = 27;
    private const int PRODUCT_ULTIMATE_N = 28;
    private const int PRODUCT_WEB_SERVER_CORE = 29;
    private const int PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 30;
    private const int PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 31;
    private const int PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 32;
    private const int PRODUCT_SERVER_FOUNDATION = 33;
    private const int PRODUCT_HOME_PREMIUM_SERVER = 34;
    private const int PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 35;
    private const int PRODUCT_STANDARD_SERVER_V = 36;
    private const int PRODUCT_DATACENTER_SERVER_V = 37;
    private const int PRODUCT_ENTERPRISE_SERVER_V = 38;
    private const int PRODUCT_DATACENTER_SERVER_CORE_V = 39;
    private const int PRODUCT_STANDARD_SERVER_CORE_V = 40;
    private const int PRODUCT_ENTERPRISE_SERVER_CORE_V = 41;
    private const int PRODUCT_HYPERV = 42;
    private const int PRODUCT_STORAGE_EXPRESS_SERVER_CORE = 43;
    private const int PRODUCT_STORAGE_STANDARD_SERVER_CORE = 44;
    private const int PRODUCT_STORAGE_WORKGROUP_SERVER_CORE = 45;
    private const int PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE = 46;
    private const int PRODUCT_STARTER_N = 47;
    private const int PRODUCT_PROFESSIONAL = 48;
    private const int PRODUCT_PROFESSIONAL_N = 49;
    private const int PRODUCT_SB_SOLUTION_SERVER = 50;
    private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS = 51;
    private const int PRODUCT_STANDARD_SERVER_SOLUTIONS = 52;
    private const int PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE = 53;
    private const int PRODUCT_SB_SOLUTION_SERVER_EM = 54;
    private const int PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM = 55;
    private const int PRODUCT_SOLUTION_EMBEDDEDSERVER = 56;
    private const int PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE = 57;
    private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT = 59;
    private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL = 60;
    private const int PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC = 61;
    private const int PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC = 62;
    private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE = 63;
    private const int PRODUCT_CLUSTER_SERVER_V = 64;
    private const int PRODUCT_EMBEDDED = 65;
    private const int PRODUCT_STARTER_E = 66;
    private const int PRODUCT_HOME_BASIC_E = 67;
    private const int PRODUCT_HOME_PREMIUM_E = 68;
    private const int PRODUCT_PROFESSIONAL_E = 69;
    private const int PRODUCT_ENTERPRISE_E = 70;
    private const int PRODUCT_ULTIMATE_E = 71;
    private const int VER_NT_WORKSTATION = 1;
    private const int VER_NT_DOMAIN_CONTROLLER = 2;
    private const int VER_NT_SERVER = 3;
    private const int VER_SUITE_SMALLBUSINESS = 1;
    private const int VER_SUITE_ENTERPRISE = 2;
    private const int VER_SUITE_TERMINAL = 16;
    private const int VER_SUITE_DATACENTER = 128;
    private const int VER_SUITE_SINGLEUSERTS = 256;
    private const int VER_SUITE_PERSONAL = 512;
    private const int VER_SUITE_BLADE = 1024;

    public static OSVersionInfo.SoftwareArchitecture ProgramBits
    {
      get
      {
        Environment.GetEnvironmentVariables();
        OSVersionInfo.SoftwareArchitecture softwareArchitecture;
        switch (IntPtr.Size * 8)
        {
          case 32:
            softwareArchitecture = OSVersionInfo.SoftwareArchitecture.Bit32;
            break;
          case 64:
            softwareArchitecture = OSVersionInfo.SoftwareArchitecture.Bit64;
            break;
          default:
            softwareArchitecture = OSVersionInfo.SoftwareArchitecture.Unknown;
            break;
        }
        return softwareArchitecture;
      }
    }

    public static OSVersionInfo.SoftwareArchitecture OSBits
    {
      get
      {
        OSVersionInfo.SoftwareArchitecture softwareArchitecture;
        switch (IntPtr.Size * 8)
        {
          case 32:
            softwareArchitecture = !OSVersionInfo.Is32BitProcessOn64BitProcessor() ? OSVersionInfo.SoftwareArchitecture.Bit32 : OSVersionInfo.SoftwareArchitecture.Bit64;
            break;
          case 64:
            softwareArchitecture = OSVersionInfo.SoftwareArchitecture.Bit64;
            break;
          default:
            softwareArchitecture = OSVersionInfo.SoftwareArchitecture.Unknown;
            break;
        }
        return softwareArchitecture;
      }
    }

    public static OSVersionInfo.ProcessorArchitecture ProcessorBits
    {
      get
      {
        OSVersionInfo.ProcessorArchitecture processorArchitecture = OSVersionInfo.ProcessorArchitecture.Unknown;
        try
        {
          OSVersionInfo.SYSTEM_INFO lpSystemInfo = new OSVersionInfo.SYSTEM_INFO();
          OSVersionInfo.GetNativeSystemInfo(ref lpSystemInfo);
          switch (lpSystemInfo.uProcessorInfo.wProcessorArchitecture)
          {
            case 0:
              processorArchitecture = OSVersionInfo.ProcessorArchitecture.Bit32;
              break;
            case 6:
              processorArchitecture = OSVersionInfo.ProcessorArchitecture.Itanium64;
              break;
            case 9:
              processorArchitecture = OSVersionInfo.ProcessorArchitecture.Bit64;
              break;
            default:
              processorArchitecture = OSVersionInfo.ProcessorArchitecture.Unknown;
              break;
          }
        }
        catch
        {
        }
        return processorArchitecture;
      }
    }

    public static string Edition
    {
      get
      {
        if (OSVersionInfo.s_Edition != null)
          return OSVersionInfo.s_Edition;
        string str = string.Empty;
        System.OperatingSystem osVersion = Environment.OSVersion;
        OSVersionInfo.OSVERSIONINFOEX osVersionInfo = new OSVersionInfo.OSVERSIONINFOEX();
        osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof (OSVersionInfo.OSVERSIONINFOEX));
        if (OSVersionInfo.GetVersionEx(ref osVersionInfo))
        {
          int major = osVersion.Version.Major;
          int minor = osVersion.Version.Minor;
          byte wProductType = osVersionInfo.wProductType;
          short wSuiteMask = osVersionInfo.wSuiteMask;
          if (major == 4)
          {
            if ((int) wProductType == 1)
              str = "Workstation";
            else if ((int) wProductType == 3)
              str = ((uint) wSuiteMask & 2U) <= 0U ? "Standard Server" : "Enterprise Server";
          }
          else if (major == 5)
          {
            if ((int) wProductType == 1)
              str = ((uint) wSuiteMask & 512U) <= 0U ? (OSVersionInfo.GetSystemMetrics(86) != 0 ? "Tablet Edition" : "Professional") : "Home";
            else if ((int) wProductType == 3)
              str = minor != 0 ? (((uint) wSuiteMask & 128U) <= 0U ? (((uint) wSuiteMask & 2U) <= 0U ? (((uint) wSuiteMask & 1024U) <= 0U ? "Standard" : "Web Edition") : "Enterprise") : "Datacenter") : (((uint) wSuiteMask & 128U) <= 0U ? (((uint) wSuiteMask & 2U) <= 0U ? "Server" : "Advanced Server") : "Datacenter Server");
          }
          else
          {
            int edition;
            if (major == 6 && OSVersionInfo.GetProductInfo(major, minor, (int) osVersionInfo.wServicePackMajor, (int) osVersionInfo.wServicePackMinor, out edition))
            {
              switch (edition)
              {
                case 0:
                  str = "Unknown product";
                  break;
                case 1:
                  str = "Ultimate";
                  break;
                case 2:
                  str = "Home Basic";
                  break;
                case 3:
                  str = "Home Premium";
                  break;
                case 4:
                  str = "Enterprise";
                  break;
                case 5:
                  str = "Home Basic N";
                  break;
                case 6:
                  str = "Business";
                  break;
                case 7:
                  str = "Standard Server";
                  break;
                case 8:
                  str = "Datacenter Server";
                  break;
                case 9:
                  str = "Windows Small Business Server";
                  break;
                case 10:
                  str = "Enterprise Server";
                  break;
                case 11:
                  str = "Starter";
                  break;
                case 12:
                  str = "Datacenter Server (core installation)";
                  break;
                case 13:
                  str = "Standard Server (core installation)";
                  break;
                case 14:
                  str = "Enterprise Server (core installation)";
                  break;
                case 15:
                  str = "Enterprise Server for Itanium-based Systems";
                  break;
                case 16:
                  str = "Business N";
                  break;
                case 17:
                  str = "Web Server";
                  break;
                case 18:
                  str = "HPC Edition";
                  break;
                case 20:
                  str = "Express Storage Server";
                  break;
                case 21:
                  str = "Standard Storage Server";
                  break;
                case 22:
                  str = "Workgroup Storage Server";
                  break;
                case 23:
                  str = "Enterprise Storage Server";
                  break;
                case 24:
                  str = "Windows Essential Server Solutions";
                  break;
                case 25:
                  str = "Windows Small Business Server Premium";
                  break;
                case 26:
                  str = "Home Premium N";
                  break;
                case 27:
                  str = "Enterprise N";
                  break;
                case 28:
                  str = "Ultimate N";
                  break;
                case 29:
                  str = "Web Server (core installation)";
                  break;
                case 30:
                  str = "Windows Essential Business Management Server";
                  break;
                case 31:
                  str = "Windows Essential Business Security Server";
                  break;
                case 32:
                  str = "Windows Essential Business Messaging Server";
                  break;
                case 33:
                  str = "Server Foundation";
                  break;
                case 34:
                  str = "Home Premium Server";
                  break;
                case 35:
                  str = "Windows Essential Server Solutions without Hyper-V";
                  break;
                case 36:
                  str = "Standard Server without Hyper-V";
                  break;
                case 37:
                  str = "Datacenter Server without Hyper-V";
                  break;
                case 38:
                  str = "Enterprise Server without Hyper-V";
                  break;
                case 39:
                  str = "Datacenter Server without Hyper-V (core installation)";
                  break;
                case 40:
                  str = "Standard Server without Hyper-V (core installation)";
                  break;
                case 41:
                  str = "Enterprise Server without Hyper-V (core installation)";
                  break;
                case 42:
                  str = "Microsoft Hyper-V Server";
                  break;
                case 43:
                  str = "Express Storage Server (core installation)";
                  break;
                case 44:
                  str = "Standard Storage Server (core installation)";
                  break;
                case 45:
                  str = "Workgroup Storage Server (core installation)";
                  break;
                case 46:
                  str = "Enterprise Storage Server (core installation)";
                  break;
                case 47:
                  str = "Starter N";
                  break;
                case 48:
                  str = "Professional";
                  break;
                case 49:
                  str = "Professional N";
                  break;
                case 50:
                  str = "SB Solution Server";
                  break;
                case 51:
                  str = "Server for SB Solutions";
                  break;
                case 52:
                  str = "Standard Server Solutions";
                  break;
                case 53:
                  str = "Standard Server Solutions (core installation)";
                  break;
                case 54:
                  str = "SB Solution Server EM";
                  break;
                case 55:
                  str = "Server for SB Solutions EM";
                  break;
                case 56:
                  str = "Solution Embedded Server";
                  break;
                case 57:
                  str = "Solution Embedded Server (core installation)";
                  break;
                case 59:
                  str = "Essential Business Server MGMT";
                  break;
                case 60:
                  str = "Essential Business Server ADDL";
                  break;
                case 61:
                  str = "Essential Business Server MGMTSVC";
                  break;
                case 62:
                  str = "Essential Business Server ADDLSVC";
                  break;
                case 63:
                  str = "Windows Small Business Server Premium (core installation)";
                  break;
                case 64:
                  str = "HPC Edition without Hyper-V";
                  break;
                case 65:
                  str = "Embedded";
                  break;
                case 66:
                  str = "Starter E";
                  break;
                case 67:
                  str = "Home Basic E";
                  break;
                case 68:
                  str = "Home Premium E";
                  break;
                case 69:
                  str = "Professional E";
                  break;
                case 70:
                  str = "Enterprise E";
                  break;
                case 71:
                  str = "Ultimate E";
                  break;
              }
            }
          }
        }
        OSVersionInfo.s_Edition = str;
        return str;
      }
    }

    public static string Name
    {
      get
      {
        if (OSVersionInfo.s_Name != null)
          return OSVersionInfo.s_Name;
        string str1 = "unknown";
        System.OperatingSystem osVersion = Environment.OSVersion;
        OSVersionInfo.OSVERSIONINFOEX osVersionInfo = new OSVersionInfo.OSVERSIONINFOEX();
        osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof (OSVersionInfo.OSVERSIONINFOEX));
        if (OSVersionInfo.GetVersionEx(ref osVersionInfo))
        {
          int num1 = osVersion.Version.Major;
          int num2 = osVersion.Version.Minor;
          if (num1 == 6 && num2 == 2)
          {
            string str2 = OSVersionInfo.RegistryRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentVersion", "");
            if (!string.IsNullOrEmpty(str2))
            {
              string[] strArray = str2.Split('.');
              num1 = Convert.ToInt32(strArray[0]);
              num2 = Convert.ToInt32(strArray[1]);
            }
            if (OSVersionInfo.IsWindows10())
            {
              num1 = 10;
              num2 = 0;
            }
          }
          switch (osVersion.Platform)
          {
            case PlatformID.Win32S:
              str1 = "Windows 3.1";
              break;
            case PlatformID.Win32Windows:
              if (num1 == 4)
              {
                string szCsdVersion = osVersionInfo.szCSDVersion;
                switch (num2)
                {
                  case 0:
                    str1 = !(szCsdVersion == "B") && !(szCsdVersion == "C") ? "Windows 95" : "Windows 95 OSR2";
                    break;
                  case 10:
                    str1 = !(szCsdVersion == "A") ? "Windows 98" : "Windows 98 Second Edition";
                    break;
                  case 90:
                    str1 = "Windows Me";
                    break;
                }
                break;
              }
              break;
            case PlatformID.Win32NT:
              byte wProductType = osVersionInfo.wProductType;
              switch (num1)
              {
                case 3:
                  str1 = "Windows NT 3.51";
                  break;
                case 4:
                  switch (wProductType)
                  {
                    case 1:
                      str1 = "Windows NT 4.0";
                      break;
                    case 3:
                      str1 = "Windows NT 4.0 Server";
                      break;
                  }break;
                case 5:
                  switch (num2)
                  {
                    case 0:
                      str1 = "Windows 2000";
                      break;
                    case 1:
                      str1 = "Windows XP";
                      break;
                    case 2:
                      str1 = "Windows Server 2003";
                      break;
                  }break;
                case 6:
                  switch (num2)
                  {
                    case 0:
                      switch (wProductType)
                      {
                        case 1:
                          str1 = "Windows Vista";
                          break;
                        case 3:
                          str1 = "Windows Server 2008";
                          break;
                      }break;
                    case 1:
                      switch (wProductType)
                      {
                        case 1:
                          str1 = "Windows 7";
                          break;
                        case 3:
                          str1 = "Windows Server 2008 R2";
                          break;
                      }break;
                    case 2:
                      switch (wProductType)
                      {
                        case 1:
                          str1 = "Windows 8";
                          break;
                        case 3:
                          str1 = "Windows Server 2012";
                          break;
                      }break;
                    case 3:
                      switch (wProductType)
                      {
                        case 1:
                          str1 = "Windows 8.1";
                          break;
                        case 3:
                          str1 = "Windows Server 2012 R2";
                          break;
                      }break;
                  }break;
                case 10:
                  if (num2 == 0)
                  {
                    switch (wProductType)
                    {
                      case 1:
                        str1 = "Windows 10";
                        break;
                      case 3:
                        str1 = "Windows Server 2016";
                        break;
                    }
                  }
                  
                  break;
              }break;
            case PlatformID.WinCE:
              str1 = "Windows CE";
              break;
          }
        }
        OSVersionInfo.s_Name = str1;
        return str1;
      }
    }

    public static string ServicePack
    {
      get
      {
        string str = string.Empty;
        OSVersionInfo.OSVERSIONINFOEX osVersionInfo = new OSVersionInfo.OSVERSIONINFOEX();
        osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof (OSVersionInfo.OSVERSIONINFOEX));
        if (OSVersionInfo.GetVersionEx(ref osVersionInfo))
          str = osVersionInfo.szCSDVersion;
        return str;
      }
    }

    public static int BuildVersion
    {
      get
      {
        return int.Parse(OSVersionInfo.RegistryRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuildNumber", "0"));
      }
    }

    public static string VersionString
    {
      get
      {
        return OSVersionInfo.Version.ToString();
      }
    }

    public static Version Version
    {
      get
      {
        return new Version(OSVersionInfo.MajorVersion, OSVersionInfo.MinorVersion, OSVersionInfo.BuildVersion, OSVersionInfo.RevisionVersion);
      }
    }

    public static int MajorVersion
    {
      get
      {
        if (OSVersionInfo.IsWindows10())
          return 10;
        string str = OSVersionInfo.RegistryRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentVersion", "");
        if (string.IsNullOrEmpty(str))
          return Environment.OSVersion.Version.Major;
        return int.Parse(str.Split('.')[0]);
      }
    }

    public static int MinorVersion
    {
      get
      {
        if (OSVersionInfo.IsWindows10())
          return 0;
        string str = OSVersionInfo.RegistryRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentVersion", "");
        if (string.IsNullOrEmpty(str))
          return Environment.OSVersion.Version.Minor;
        return int.Parse(str.Split('.')[1]);
      }
    }

    public static int RevisionVersion
    {
      get
      {
        if (OSVersionInfo.IsWindows10())
          return 0;
        return Environment.OSVersion.Version.Revision;
      }
    }

    [DllImport("Kernel32.dll")]
    internal static extern bool GetProductInfo(int osMajorVersion, int osMinorVersion, int spMajorVersion, int spMinorVersion, out int edition);

    [DllImport("kernel32.dll")]
    private static extern bool GetVersionEx(ref OSVersionInfo.OSVERSIONINFOEX osVersionInfo);

    [DllImport("user32")]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("kernel32.dll")]
    public static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref OSVersionInfo.SYSTEM_INFO lpSystemInfo);

    [DllImport("kernel32.dll")]
    public static extern void GetNativeSystemInfo([MarshalAs(UnmanagedType.Struct)] ref OSVersionInfo.SYSTEM_INFO lpSystemInfo);

    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr LoadLibrary(string libraryName);

    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

    private static OSVersionInfo.IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
    {
      IntPtr hwnd = OSVersionInfo.LoadLibrary("kernel32");
      if (hwnd != IntPtr.Zero)
      {
        IntPtr procAddress = OSVersionInfo.GetProcAddress(hwnd, "IsWow64Process");
        if (procAddress != IntPtr.Zero)
          return (OSVersionInfo.IsWow64ProcessDelegate) Marshal.GetDelegateForFunctionPointer(procAddress, typeof (OSVersionInfo.IsWow64ProcessDelegate));
      }
      return (OSVersionInfo.IsWow64ProcessDelegate) null;
    }

    private static bool Is32BitProcessOn64BitProcessor()
    {
      OSVersionInfo.IsWow64ProcessDelegate wow64ProcessDelegate = OSVersionInfo.GetIsWow64ProcessDelegate();
      bool isWow64Process;
      if (wow64ProcessDelegate == null || !wow64ProcessDelegate(Process.GetCurrentProcess().Handle, out isWow64Process))
        return false;
      return isWow64Process;
    }

    private static bool IsWindows10()
    {
      return OSVersionInfo.RegistryRead("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", "").StartsWith("Windows 10", StringComparison.OrdinalIgnoreCase);
    }

    private static string RegistryRead(string RegistryPath, string Field, string DefaultValue)
    {
      string str1 = "";
      string str2 = "";
      string name = "";
      try
      {
        RegistryKey registryKey1 = (RegistryKey) null;
        string[] strArray = RegistryPath.Split('\\');
        if ((uint) strArray.Length > 0U)
        {
          strArray[0] = strArray[0].ToUpper();
          if (strArray[0] == "HKEY_CLASSES_ROOT")
            registryKey1 = Registry.ClassesRoot;
          else if (strArray[0] == "HKEY_CURRENT_USER")
            registryKey1 = Registry.CurrentUser;
          else if (strArray[0] == "HKEY_LOCAL_MACHINE")
            registryKey1 = Registry.LocalMachine;
          else if (strArray[0] == "HKEY_USERS")
            registryKey1 = Registry.Users;
          else if (strArray[0] == "HKEY_CURRENT_CONFIG")
            registryKey1 = Registry.CurrentConfig;
          if (registryKey1 != null)
          {
            for (int index = 1; index < strArray.Length; ++index)
            {
              name = name + str2 + strArray[index];
              str2 = "\\";
            }
            if (name != "")
            {
              RegistryKey registryKey2 = registryKey1.OpenSubKey(name);
              str1 = (string) registryKey2.GetValue(Field, (object) DefaultValue);
              registryKey2.Close();
            }
          }
        }
      }
      catch
      {
      }
      return str1;
    }

    public enum SoftwareArchitecture
    {
      Unknown,
      Bit32,
      Bit64,
    }

    public enum ProcessorArchitecture
    {
      Unknown,
      Bit32,
      Bit64,
      Itanium64,
    }

    private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, out bool isWow64Process);

    private struct OSVERSIONINFOEX
    {
      public int dwOSVersionInfoSize;
      public int dwMajorVersion;
      public int dwMinorVersion;
      public int dwBuildNumber;
      public int dwPlatformId;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public string szCSDVersion;
      public short wServicePackMajor;
      public short wServicePackMinor;
      public short wSuiteMask;
      public byte wProductType;
      public byte wReserved;
    }

    public struct SYSTEM_INFO
    {
      internal OSVersionInfo._PROCESSOR_INFO_UNION uProcessorInfo;
      public uint dwPageSize;
      public IntPtr lpMinimumApplicationAddress;
      public IntPtr lpMaximumApplicationAddress;
      public IntPtr dwActiveProcessorMask;
      public uint dwNumberOfProcessors;
      public uint dwProcessorType;
      public uint dwAllocationGranularity;
      public ushort dwProcessorLevel;
      public ushort dwProcessorRevision;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct _PROCESSOR_INFO_UNION
    {
      [FieldOffset(0)]
      internal uint dwOemId;
      [FieldOffset(0)]
      internal ushort wProcessorArchitecture;
      [FieldOffset(2)]
      internal ushort wReserved;
    }
  }
}
