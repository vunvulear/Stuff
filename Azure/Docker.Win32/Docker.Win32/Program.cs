using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using NetFwTypeLib;
using System.Diagnostics;

namespace Docker.Win32
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Application");

            #region Win32 Sample
            Console.WriteLine("Start Win32 Sample");

            SYSTEM_INFO pSI = new SYSTEM_INFO();
            GetSystemInfo(ref pSI);

            Console.WriteLine($"dwProcessorLevel: {pSI.dwProcessorLevel}");

            Console.WriteLine("End Win32 Sample");
            #endregion

            Console.WriteLine();

            #region COM Sample
            Console.WriteLine("Start COM Sample");

            // COM Registration
            Console.WriteLine("COM Registation");
            RegistarDlls("Interop.NetFwTypeLib.dll");

            const string guidFWPolicy2 = "{E2B3C97F-6AE1-41AC-817A-F6F92166D7DD}";
            const string guidRWRule = "{2C5BC43E-3369-4C33-AB0C-BE9469677AF4}";
            Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));
            Type typeFWRule = Type.GetTypeFromCLSID(new Guid(guidRWRule));
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);
            INetFwRule newRule = (INetFwRule)Activator.CreateInstance(typeFWRule);
            newRule.Name = "MabuAsTcpLocker_OutBound_Rule";
            newRule.Description = "Block outbound traffic  over TCP port 80";
            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            newRule.RemotePorts = "8888";
            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            newRule.Enabled = true;
            newRule.Profiles = 6;
            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            Console.WriteLine("End COM Sample");
            #endregion

            Console.WriteLine("End Application");
        }

        [DllImport("kernel32")]
        static extern void GetSystemInfo(ref SYSTEM_INFO pSI);

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        public static void RegistarDlls(string filePath)
        {
                //'/s' : Specifies regsvr32 to run silently and to not display any message boxes.
                string fileinfo = "/s" + " " + "\"" + filePath + "\"";
                Process reg = new Process();
                //This file registers .dll files as command components in the registry.
                reg.StartInfo.FileName = "regsvr32.exe";
                reg.StartInfo.Arguments = fileinfo;
                reg.StartInfo.UseShellExecute = false;
                reg.StartInfo.CreateNoWindow = true;
                reg.StartInfo.RedirectStandardOutput = true;
                reg.Start();
                reg.WaitForExit();
                reg.Close();
        }
    }




}
