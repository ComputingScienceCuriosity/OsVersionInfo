

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace JCS.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (JCS.Properties.Resources.resourceMan == null)
          JCS.Properties.Resources.resourceMan = new ResourceManager("JCS.Properties.Resources", typeof (JCS.Properties.Resources).Assembly);
        return JCS.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return JCS.Properties.Resources.resourceCulture;
      }
      set
      {
        JCS.Properties.Resources.resourceCulture = value;
      }
    }

    internal Resources()
    {
    }
  }
}
