// Decompiled with JetBrains decompiler
// Type: JCS.Properties.Settings
// Assembly: OSVersionInfo, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C3E12D11-4029-4DB1-BB3C-2BC4EEE7B71C
// Assembly location: P:\icare\SHARE\Debug\OSVersionInfo.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace JCS.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
