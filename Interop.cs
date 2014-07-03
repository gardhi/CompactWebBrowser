using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Compact13WebBrowser
{
    public static class Interop
    {
        [DllImport("coredll.dll", EntryPoint = "SipShowIM")]
        public static extern bool SipShowIM(int dwFlag);

    }
}
