using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.SharePoint.Administration;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Provides logging capabilities for the Unified Logging System (ULS) in SharePoint
    /// </summary>
    public static class TraceProvider
    {
        static UInt64 hTraceLog;
        static UInt64 hTraceReg;

        static class NativeMethods
        {
            internal const int TRACE_VERSION_CURRENT = 1;
            internal const int ERROR_SUCCESS = 0;
            internal const int ERROR_INVALID_PARAMETER = 87;
            internal const int WNODE_FLAG_TRACED_GUID = 0x00020000;

            internal enum TraceFlags
            {
                TRACE_FLAG_START = 1,
                TRACE_FLAG_END = 2,
                TRACE_FLAG_MIDDLE = 3,
                TRACE_FLAG_ID_AS_ASCII = 4
            }

            // Copied from Win32 APIs
            [StructLayout(LayoutKind.Sequential)]
            internal struct EVENT_TRACE_HEADER_CLASS
            {
                internal byte Type;
                internal byte Level;
                internal ushort Version;
            }

            // Copied from Win32 APIs
            [StructLayout(LayoutKind.Sequential)]
            internal struct EVENT_TRACE_HEADER
            {
                internal ushort Size;
                internal ushort FieldTypeFlags;
                internal EVENT_TRACE_HEADER_CLASS Class;
                internal uint ThreadId;
                internal uint ProcessId;
                internal Int64 TimeStamp;
                internal Guid Guid;
                internal uint ClientContext;
                internal uint Flags;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            internal struct ULSTraceHeader
            {
                internal ushort Size;
                internal uint dwVersion;
                internal uint Id;
                internal Guid correlationID;
                internal TraceFlags dwFlags;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                internal string wzExeName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                internal string wzProduct;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                internal string wzCategory;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 800)]
                internal string wzMessage;
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct ULSTrace
            {
                internal EVENT_TRACE_HEADER Header;
                internal ULSTraceHeader ULSHeader;
            }

            // Copied from Win32 APIs
            internal enum WMIDPREQUESTCODE
            {
                WMI_GET_ALL_DATA = 0,
                WMI_GET_SINGLE_INSTANCE = 1,
                WMI_SET_SINGLE_INSTANCE = 2,
                WMI_SET_SINGLE_ITEM = 3,
                WMI_ENABLE_EVENTS = 4,
                WMI_DISABLE_EVENTS = 5,
                WMI_ENABLE_COLLECTION = 6,
                WMI_DISABLE_COLLECTION = 7,
                WMI_REGINFO = 8,
                WMI_EXECUTE_METHOD = 9
            }

            // Copied from Win32 APIs
            internal unsafe delegate uint EtwProc(NativeMethods.WMIDPREQUESTCODE requestCode, IntPtr requestContext, uint* bufferSize, IntPtr buffer);

            // Copied from Win32 APIs
            [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
            internal static extern unsafe uint RegisterTraceGuids([In] EtwProc cbFunc, [In] void* context, [In] ref Guid controlGuid, [In] uint guidCount, IntPtr guidReg, [In] string mofImagePath, [In] string mofResourceName, out ulong regHandle);

            // Copied from Win32 APIs
            [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
            internal static extern uint UnregisterTraceGuids([In]ulong regHandle);

            // Copied from Win32 APIs
            [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
            internal static extern UInt64 GetTraceLoggerHandle([In]IntPtr Buffer);

            // Copied from Win32 APIs
            [DllImport("advapi32.dll", SetLastError = true)]
            internal static extern uint TraceEvent([In]UInt64 traceHandle, [In]ref ULSTrace evnt);
        }

        public enum TraceSeverity
        {
            Unassigned = 0,
            CriticalEvent = 1,
            WarningEvent = 2,
            InformationEvent = 3,
            Exception = 4,
            Assert = 7,
            Unexpected = 10,
            Monitorable = 15,
            High = 20,
            Medium = 50,
            Verbose = 100,
        }

        public static void WriteTrace(uint tag, TraceSeverity level, Guid correlationGuid, string exeName, string productName, string categoryName, string message)
        {
            const ushort sizeOfWCHAR = 2;
            NativeMethods.ULSTrace ulsTrace = new NativeMethods.ULSTrace();

            // Pretty standard code needed to make things work
            ulsTrace.Header.Size = (ushort)Marshal.SizeOf(typeof(NativeMethods.ULSTrace));
            ulsTrace.Header.Flags = NativeMethods.WNODE_FLAG_TRACED_GUID;
            ulsTrace.ULSHeader.dwVersion = NativeMethods.TRACE_VERSION_CURRENT;
            ulsTrace.ULSHeader.dwFlags = NativeMethods.TraceFlags.TRACE_FLAG_ID_AS_ASCII;
            ulsTrace.ULSHeader.Size = (ushort)Marshal.SizeOf(typeof(NativeMethods.ULSTraceHeader));

            // Variables communicated to SPTrace
            ulsTrace.ULSHeader.Id = tag;
            ulsTrace.Header.Class.Level = (byte)level;
            ulsTrace.ULSHeader.wzExeName = exeName;
            ulsTrace.ULSHeader.wzProduct = productName;
            ulsTrace.ULSHeader.wzCategory = categoryName;
            ulsTrace.ULSHeader.wzMessage = message;
            ulsTrace.ULSHeader.correlationID = correlationGuid;

            // Optionally, to improve performance by reducing the amount of data copied around,
            // the Size parameters can be reduced by the amount of unused buffer in the Message
            if (message.Length < 800)
            {
                ushort unusedBuffer = (ushort)((800 - (message.Length + 1)) * sizeOfWCHAR);
                ulsTrace.Header.Size -= unusedBuffer;
                ulsTrace.ULSHeader.Size -= unusedBuffer;
            }

            // register trace provider of not already registered
            if (hTraceLog == 0)            
                RegisterTraceProvider();

            NativeMethods.TraceEvent(hTraceLog, ref ulsTrace);
        }

        public static unsafe void RegisterTraceProvider()
        {
            SPFarm farm = SPFarm.Local;
            Guid traceGuid = farm.TraceSessionGuid;
            uint result = NativeMethods.RegisterTraceGuids(ControlCallback, null, ref traceGuid, 0, IntPtr.Zero, null, null, out hTraceReg);
            System.Diagnostics.Debug.Assert(result == NativeMethods.ERROR_SUCCESS);
        }

        public static void UnregisterTraceProvider()
        {
            if (hTraceReg != 0)
            {
                uint result = NativeMethods.UnregisterTraceGuids(hTraceReg);
                System.Diagnostics.Debug.Assert(result == NativeMethods.ERROR_SUCCESS);
            }
        }

        public static uint TagFromString(string wzTag)
        {
            System.Diagnostics.Debug.Assert(wzTag.Length == 4);
            return (uint)(wzTag[0] << 24 | wzTag[1] << 16 | wzTag[2] << 8 | wzTag[3]);
        }

        static unsafe uint ControlCallback(NativeMethods.WMIDPREQUESTCODE RequestCode, IntPtr Context, uint* InOutBufferSize, IntPtr Buffer)
        {
            uint Status;
            switch (RequestCode)
            {
                case NativeMethods.WMIDPREQUESTCODE.WMI_ENABLE_EVENTS:
                    hTraceLog = NativeMethods.GetTraceLoggerHandle(Buffer);
                    Status = NativeMethods.ERROR_SUCCESS;
                    break;
                case NativeMethods.WMIDPREQUESTCODE.WMI_DISABLE_EVENTS:
                    hTraceLog = 0;
                    Status = NativeMethods.ERROR_SUCCESS;
                    break;
                default:
                    Status = NativeMethods.ERROR_INVALID_PARAMETER;
                    break;
            }

            *InOutBufferSize = 0;
            return Status;
        }
    }
}