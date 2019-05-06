using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PS3_TMAPI
{
    private static PS3_TMAPI.EnumerateTargetsExCallbackPriv ms_enumTargetsExCallbackPriv = new PS3_TMAPI.EnumerateTargetsExCallbackPriv(PS3_TMAPI.EnumTargetsExPriv);
    private static PS3_TMAPI.EnumerateTargetsExCallback ms_enumTargetsExCallback = (PS3_TMAPI.EnumerateTargetsExCallback)null;
    private static object ms_enumTargetsExUserData = (object)null;
    private static Dictionary<PS3_TMAPI.TTYChannel, PS3_TMAPI.TTYCallbackAndUserData> ms_userTtyCallbacks = new Dictionary<PS3_TMAPI.TTYChannel, PS3_TMAPI.TTYCallbackAndUserData>(1);
    private static Dictionary<int, PS3_TMAPI.PadPlaybackCallbackAndUserData> ms_userPadPlaybackCallbacks = new Dictionary<int, PS3_TMAPI.PadPlaybackCallbackAndUserData>(1);
    private static Dictionary<int, PS3_TMAPI.PadCaptureCallbackAndUserData> ms_userPadCaptureCallbacks = new Dictionary<int, PS3_TMAPI.PadCaptureCallbackAndUserData>(1);
    private static PS3_TMAPI.CustomProtocolCallbackPriv ms_customProtoCallbackPriv = new PS3_TMAPI.CustomProtocolCallbackPriv(PS3_TMAPI.CustomProtocolHandler);
    private static Dictionary<PS3_TMAPI.CustomProtocolId, PS3_TMAPI.CusProtoCallbackAndUserData> ms_userCustomProtoCallbacks = new Dictionary<PS3_TMAPI.CustomProtocolId, PS3_TMAPI.CusProtoCallbackAndUserData>(1);
    private static Dictionary<int, PS3_TMAPI.FtpCallbackAndUserData> ms_userFtpCallbacks = new Dictionary<int, PS3_TMAPI.FtpCallbackAndUserData>(1);
    private static Dictionary<int, PS3_TMAPI.FileTraceCallbackAndUserData> ms_userFileTraceCallbacks = new Dictionary<int, PS3_TMAPI.FileTraceCallbackAndUserData>(1);
    private static Dictionary<int, PS3_TMAPI.TargetCallbackAndUserData> ms_userTargetCallbacks = new Dictionary<int, PS3_TMAPI.TargetCallbackAndUserData>(1);
    private static PS3_TMAPI.HandleEventCallbackPriv ms_eventHandlerWrapper = new PS3_TMAPI.HandleEventCallbackPriv(PS3_TMAPI.EventHandlerWrapper);
    public const uint AllTTYStreams = 4294967295U;
    public const uint DefaultProcessPriority = 999U;
    public const uint DefaultProtocolPriority = 128U;

    static PS3_TMAPI()
    {
    }

    public static bool FAILED(PS3_TMAPI.SNRESULT res)
    {
        return !PS3_TMAPI.SUCCEEDED(res);
    }

    public static bool SUCCEEDED(PS3_TMAPI.SNRESULT res)
    {
        return res >= PS3_TMAPI.SNRESULT.SN_S_OK;
    }

    private static bool Is32Bit()
    {
        return IntPtr.Size == 4;
    }

    private static byte VersionMajor(ulong version)
    {
        return (byte)(version >> 16);
    }

    private static byte VersionMinor(ulong version)
    {
        return (byte)(version >> 8);
    }

    private static byte VersionFix(ulong version)
    {
        return (byte)version;
    }

    private static void VersionComponents(ulong version, out byte major, out byte minor, out byte fix)
    {
        major = PS3_TMAPI.VersionMajor(version);
        minor = PS3_TMAPI.VersionMinor(version);
        fix = PS3_TMAPI.VersionFix(version);
    }

    public static byte SDKVersionMajor(ulong sdkVersion)
    {
        return PS3_TMAPI.VersionMajor(sdkVersion);
    }

    public static byte SDKVersionMinor(ulong sdkVersion)
    {
        return PS3_TMAPI.VersionMinor(sdkVersion);
    }

    public static byte SDKVersionFix(ulong sdkVersion)
    {
        return PS3_TMAPI.VersionFix(sdkVersion);
    }

    public static void SDKVersionComponents(ulong sdkVersion, out byte major, out byte minor, out byte fix)
    {
        major = PS3_TMAPI.SDKVersionMajor(sdkVersion);
        minor = PS3_TMAPI.SDKVersionMinor(sdkVersion);
        fix = PS3_TMAPI.SDKVersionFix(sdkVersion);
    }

    public static byte CPVersionMajor(ulong cpVersion)
    {
        return PS3_TMAPI.VersionMajor(cpVersion);
    }

    public static byte CPVersionMinor(ulong cpVersion)
    {
        return PS3_TMAPI.VersionMinor(cpVersion);
    }

    public static byte CPVersionFix(ulong cpVersion)
    {
        return PS3_TMAPI.VersionFix(cpVersion);
    }

    public static void CPVersionComponents(ulong cpVersion, out byte major, out byte minor, out byte fix)
    {
        major = PS3_TMAPI.CPVersionMajor(cpVersion);
        minor = PS3_TMAPI.CPVersionMinor(cpVersion);
        fix = PS3_TMAPI.CPVersionFix(cpVersion);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTMVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTMVersionX86(out IntPtr version);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTMVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTMVersionX64(out IntPtr version);

    public static PS3_TMAPI.SNRESULT GetTMVersion(out string version)
    {
        IntPtr version1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetTMVersionX86(out version1) : PS3_TMAPI.GetTMVersionX64(out version1);
        version = Marshal.PtrToStringAnsi(version1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetAPIVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetAPIVersionX86(out IntPtr version);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetAPIVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetAPIVersionX64(out IntPtr version);

    public static PS3_TMAPI.SNRESULT GetAPIVersion(out string version)
    {
        IntPtr version1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetAPIVersionX86(out version1) : PS3_TMAPI.GetAPIVersionX64(out version1);
        version = Marshal.PtrToStringAnsi(version1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3TranslateError", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT TranslateErrorX86(PS3_TMAPI.SNRESULT res, out IntPtr message);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3TranslateError", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT TranslateErrorX64(PS3_TMAPI.SNRESULT res, out IntPtr message);

    public static PS3_TMAPI.SNRESULT TranslateError(PS3_TMAPI.SNRESULT errorCode, out string message)
    {
        IntPtr message1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.TranslateErrorX86(errorCode, out message1) : PS3_TMAPI.TranslateErrorX64(errorCode, out message1);
        message = Marshal.PtrToStringAnsi(message1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetErrorQualifier", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetErrorQualifierX86(out uint qualifier, out IntPtr message);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetErrorQualifier", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetErrorQualifierX64(out uint qualifier, out IntPtr message);

    public static PS3_TMAPI.SNRESULT GetErrorQualifier(out uint qualifier, out string message)
    {
        IntPtr message1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetErrorQualifierX86(out qualifier, out message1) : PS3_TMAPI.GetErrorQualifierX64(out qualifier, out message1);
        message = Marshal.PtrToStringAnsi(message1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConnectStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConnectStatusX86(int target, out uint status, out IntPtr usage);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConnectStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConnectStatusX64(int target, out uint status, out IntPtr usage);

    public static PS3_TMAPI.SNRESULT GetConnectStatus(int target, out PS3_TMAPI.ConnectStatus status, out string usage)
    {
        uint status1;
        IntPtr usage1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetConnectStatusX86(target, out status1, out usage1) : PS3_TMAPI.GetConnectStatusX64(target, out status1, out usage1);
        status = (PS3_TMAPI.ConnectStatus)status1;
        usage = Marshal.PtrToStringAnsi(usage1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT InitTargetCommsX86();

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT InitTargetCommsX64();

    public static PS3_TMAPI.SNRESULT InitTargetComms()
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.InitTargetCommsX64();
        else
            return PS3_TMAPI.InitTargetCommsX86();
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CloseTargetComms", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CloseTargetCommsX86();

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3CloseTargetComms", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CloseTargetCommsX64();

    public static PS3_TMAPI.SNRESULT CloseTargetComms()
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.CloseTargetCommsX64();
        else
            return PS3_TMAPI.CloseTargetCommsX86();
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3EnumerateTargets", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnumerateTargetsX86(PS3_TMAPI.EnumerateTargetsCallback callback);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3EnumerateTargets", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnumerateTargetsX64(PS3_TMAPI.EnumerateTargetsCallback callback);

    public static PS3_TMAPI.SNRESULT EnumerateTargets(PS3_TMAPI.EnumerateTargetsCallback callback)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.EnumerateTargetsX64(callback);
        else
            return PS3_TMAPI.EnumerateTargetsX86(callback);
    }

    private static int EnumTargetsExPriv(int target, IntPtr unused)
    {
        return PS3_TMAPI.ms_enumTargetsExCallback(target, PS3_TMAPI.ms_enumTargetsExUserData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3EnumerateTargetsEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnumerateTargetsExX86(PS3_TMAPI.EnumerateTargetsExCallbackPriv callback, IntPtr unused);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3EnumerateTargetsEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnumerateTargetsExX64(PS3_TMAPI.EnumerateTargetsExCallbackPriv callback, IntPtr unused);

    public static PS3_TMAPI.SNRESULT EnumerateTargetsEx(PS3_TMAPI.EnumerateTargetsExCallback callback, ref object userData)
    {
        PS3_TMAPI.ms_enumTargetsExCallback = callback;
        PS3_TMAPI.ms_enumTargetsExUserData = userData;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.EnumerateTargetsExX64(PS3_TMAPI.ms_enumTargetsExCallbackPriv, IntPtr.Zero);
        else
            return PS3_TMAPI.EnumerateTargetsExX86(PS3_TMAPI.ms_enumTargetsExCallbackPriv, IntPtr.Zero);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetNumTargets", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetNumTargetsX86(out uint numTargets);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetNumTargets", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetNumTargetsX64(out uint numTargets);

    public static PS3_TMAPI.SNRESULT GetNumTargets(out uint numTargets)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetNumTargetsX64(out numTargets);
        else
            return PS3_TMAPI.GetNumTargetsX86(out numTargets);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTargetFromName", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTargetFromNameX86(string name, out int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTargetFromName", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTargetFromNameX64(string name, out int target);

    public static PS3_TMAPI.SNRESULT GetTargetFromName(string name, out int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetTargetFromNameX64(name, out target);
        else
            return PS3_TMAPI.GetTargetFromNameX86(name, out target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Reset", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ResetX86(int target, ulong resetParameter);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Reset", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ResetX64(int target, ulong resetParameter);

    public static PS3_TMAPI.SNRESULT Reset(int target, PS3_TMAPI.ResetParameter resetParameter)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ResetX64(target, (ulong)resetParameter);
        else
            return PS3_TMAPI.ResetX86(target, (ulong)resetParameter);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ResetEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ResetExX86(int target, ulong boot, ulong bootMask, ulong reset, ulong resetMask, ulong system, ulong systemMask);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ResetEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ResetExX64(int target, ulong boot, ulong bootMask, ulong reset, ulong resetMask, ulong system, ulong systemMask);

    public static PS3_TMAPI.SNRESULT ResetEx(int target, PS3_TMAPI.BootParameter bootParameter, PS3_TMAPI.BootParameterMask bootMask, PS3_TMAPI.ResetParameter resetParameter, PS3_TMAPI.ResetParameterMask resetMask, PS3_TMAPI.SystemParameter systemParameter, PS3_TMAPI.SystemParameterMask systemMask)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ResetExX64(target, (ulong)bootParameter, (ulong)bootMask, (ulong)resetParameter, (ulong)resetMask, (ulong)systemParameter, (ulong)systemMask);
        else
            return PS3_TMAPI.ResetExX86(target, (ulong)bootParameter, (ulong)bootMask, (ulong)resetParameter, (ulong)resetMask, (ulong)systemParameter, (ulong)systemMask);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetResetParameters", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetResetParametersX86(int target, out ulong boot, out ulong bootMask, out ulong reset, out ulong resetMask, out ulong system, out ulong systemMask);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetResetParameters", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetResetParametersX64(int target, out ulong boot, out ulong bootMask, out ulong reset, out ulong resetMask, out ulong system, out ulong systemMask);

    public static PS3_TMAPI.SNRESULT GetResetParameters(int target, out PS3_TMAPI.BootParameter bootParameter, out PS3_TMAPI.BootParameterMask bootMask, out PS3_TMAPI.ResetParameter resetParameter, out PS3_TMAPI.ResetParameterMask resetMask, out PS3_TMAPI.SystemParameter systemParameter, out PS3_TMAPI.SystemParameterMask systemMask)
    {
        ulong boot;
        ulong bootMask1;
        ulong reset;
        ulong resetMask1;
        ulong system;
        ulong systemMask1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetResetParametersX86(target, out boot, out bootMask1, out reset, out resetMask1, out system, out systemMask1) : PS3_TMAPI.GetResetParametersX64(target, out boot, out bootMask1, out reset, out resetMask1, out system, out systemMask1);
        bootParameter = (PS3_TMAPI.BootParameter)boot;
        bootMask = (PS3_TMAPI.BootParameterMask)bootMask1;
        resetParameter = (PS3_TMAPI.ResetParameter)reset;
        resetMask = (PS3_TMAPI.ResetParameterMask)resetMask1;
        systemParameter = (PS3_TMAPI.SystemParameter)system;
        systemMask = (PS3_TMAPI.SystemParameterMask)systemMask1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetBootParameter", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetBootParameterX86(int target, ulong boot, ulong bootMask);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetBootParameter", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetBootParameterX64(int target, ulong boot, ulong bootMask);

    public static PS3_TMAPI.SNRESULT SetBootParameter(int target, PS3_TMAPI.BootParameter bootParameter, PS3_TMAPI.BootParameterMask bootMask)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetBootParameterX64(target, (ulong)bootParameter, (ulong)bootMask);
        else
            return PS3_TMAPI.SetBootParameterX86(target, (ulong)bootParameter, (ulong)bootMask);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetCurrentBootParameter", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetCurrentBootParameterX86(int target, out ulong boot);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetCurrentBootParameter", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetCurrentBootParameterX64(int target, out ulong boot);

    public static PS3_TMAPI.SNRESULT GetCurrentBootParameter(int target, out PS3_TMAPI.BootParameter bootParameter)
    {
        ulong boot;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetCurrentBootParameterX86(target, out boot) : PS3_TMAPI.GetCurrentBootParameterX64(target, out boot);
        bootParameter = (PS3_TMAPI.BootParameter)boot;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetSystemParameter", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetSystemParameterX86(int target, ulong system, ulong systemMask);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetSystemParameter", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetSystemParameterX64(int target, ulong system, ulong systemMask);

    public static PS3_TMAPI.SNRESULT SetSystemParameter(int target, PS3_TMAPI.SystemParameter systemParameter, PS3_TMAPI.SystemParameterMask systemMask)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetSystemParameterX64(target, (ulong)systemParameter, (ulong)systemMask);
        else
            return PS3_TMAPI.SetSystemParameterX86(target, (ulong)systemParameter, (ulong)systemMask);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTargetInfoX86(IntPtr unmanagedMem);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTargetInfoX64(IntPtr unmanagedMem);

    public static PS3_TMAPI.SNRESULT GetTargetInfo(ref PS3_TMAPI.TargetInfo targetInfo)
    {
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf((object)targetInfo));
        Marshal.StructureToPtr((object)targetInfo, num, false);
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetTargetInfoX86(num) : PS3_TMAPI.GetTargetInfoX64(num);
        if (PS3_TMAPI.SUCCEEDED(res))
            targetInfo = (PS3_TMAPI.TargetInfo)Marshal.PtrToStructure(num, typeof(PS3_TMAPI.TargetInfo));
        Marshal.FreeHGlobal(num);
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetTargetInfoX86(ref PS3_TMAPI.TargetInfo info);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetTargetInfoX64(ref PS3_TMAPI.TargetInfo info);

    public static PS3_TMAPI.SNRESULT SetTargetInfo(PS3_TMAPI.TargetInfo targetInfo)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetTargetInfoX64(ref targetInfo);
        else
            return PS3_TMAPI.SetTargetInfoX86(ref targetInfo);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ListTargetTypes", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ListTargetTypesX86(ref uint size, IntPtr targetTypes);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ListTargetTypes", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ListTargetTypesX64(ref uint size, IntPtr targetTypes);

    public static PS3_TMAPI.SNRESULT ListTargetTypes(out PS3_TMAPI.TargetType[] targetTypes)
    {
        targetTypes = (PS3_TMAPI.TargetType[])null;
        IntPtr targetTypes1 = IntPtr.Zero;
        uint size = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ListTargetTypesX86(ref size, targetTypes1) : PS3_TMAPI.ListTargetTypesX64(ref size, targetTypes1);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)((long)Marshal.SizeOf(typeof(PS3_TMAPI.TargetType)) * (long)size));
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ListTargetTypesX86(ref size, num) : PS3_TMAPI.ListTargetTypesX64(ref size, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = num;
            targetTypes = new PS3_TMAPI.TargetType[size];
            for (uint index = 0U; index < size; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.TargetType>(unmanagedBuf, ref targetTypes[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3AddTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT AddTargetX86(string name, string type, int connParamsSize, IntPtr connectParams, out int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3AddTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT AddTargetX64(string name, string type, int connParamsSize, IntPtr connectParams, out int target);

    public static PS3_TMAPI.SNRESULT AddTarget(string name, string targetType, PS3_TMAPI.TCPIPConnectProperties connectProperties, out int target)
    {
        IntPtr num1 = IntPtr.Zero;
        int num2 = 0;
        if (connectProperties != null)
        {
            num2 = Marshal.SizeOf((object)connectProperties);
            num1 = Marshal.AllocHGlobal(num2);
            Marshal.StructureToPtr((object)connectProperties, num1, false);
        }
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.AddTargetX86(name, targetType, num2, num1, out target) : PS3_TMAPI.AddTargetX64(name, targetType, num2, num1, out target);
        if (num1 != IntPtr.Zero)
            Marshal.FreeHGlobal(num1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConnectionInfoX86(int target, IntPtr connectProperties);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConnectionInfoX64(int target, IntPtr connectProperties);

    public static PS3_TMAPI.SNRESULT GetConnectionInfo(int target, out PS3_TMAPI.TCPIPConnectProperties connectProperties)
    {
        connectProperties = (PS3_TMAPI.TCPIPConnectProperties)null;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PS3_TMAPI.TCPIPConnectProperties)));
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetConnectionInfoX86(target, num) : PS3_TMAPI.GetConnectionInfoX64(target, num);
        if (PS3_TMAPI.SUCCEEDED(res))
        {
            connectProperties = new PS3_TMAPI.TCPIPConnectProperties();
            Marshal.PtrToStructure(num, (object)connectProperties);
        }
        Marshal.FreeHGlobal(num);
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetConnectionInfoX86(int target, IntPtr connectProperties);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetConnectionInfoX64(int target, IntPtr connectProperties);

    public static PS3_TMAPI.SNRESULT SetConnectionInfo(int target, PS3_TMAPI.TCPIPConnectProperties connectProperties)
    {
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf((object)connectProperties));
        PS3_TMAPI.WriteDataToUnmanagedIncPtr<PS3_TMAPI.TCPIPConnectProperties>(connectProperties, num);
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.SetConnectionInfoX86(target, num) : PS3_TMAPI.SetConnectionInfoX64(target, num);
        Marshal.FreeHGlobal(num);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3DeleteTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DeleteTargetX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3DeleteTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DeleteTargetX64(int target);

    public static PS3_TMAPI.SNRESULT DeleteTarget(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.DeleteTargetX64(target);
        else
            return PS3_TMAPI.DeleteTargetX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ConnectX86(int target, string application);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ConnectX64(int target, string application);

    public static PS3_TMAPI.SNRESULT Connect(int target, string application)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ConnectX64(target, application);
        else
            return PS3_TMAPI.ConnectX86(target, application);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ConnectEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ConnectExX86(int target, string application, bool bForceFlag);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ConnectEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ConnectExX64(int target, string application, bool bForceFlag);

    public static PS3_TMAPI.SNRESULT ConnectEx(int target, string application, bool bForceFlag)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ConnectExX64(target, application, bForceFlag);
        else
            return PS3_TMAPI.ConnectExX86(target, application, bForceFlag);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Disconnect", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DisconnectX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Disconnect", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DisconnectX64(int target);

    public static PS3_TMAPI.SNRESULT Disconnect(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.DisconnectX64(target);
        else
            return PS3_TMAPI.DisconnectX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ForceDisconnect", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ForceDisconnectX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ForceDisconnect", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ForceDisconnectX64(int target);

    public static PS3_TMAPI.SNRESULT ForceDisconnect(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ForceDisconnectX64(target);
        else
            return PS3_TMAPI.ForceDisconnectX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetSystemInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSystemInfoX86(int target, uint reserved, out uint mask, out PS3_TMAPI.SystemInfo info);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetSystemInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSystemInfoX64(int target, uint reserved, out uint mask, out PS3_TMAPI.SystemInfo info);

    public static PS3_TMAPI.SNRESULT GetSystemInfo(int target, out PS3_TMAPI.SystemInfoFlag mask, out PS3_TMAPI.SystemInfo systemInfo)
    {
        systemInfo = new PS3_TMAPI.SystemInfo();
        uint mask1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSystemInfoX86(target, 0U, out mask1, out systemInfo) : PS3_TMAPI.GetSystemInfoX64(target, 0U, out mask1, out systemInfo);
        mask = (PS3_TMAPI.SystemInfoFlag)mask1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetExtraLoadFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetExtraLoadFlagsX86(int target, out ulong extraLoadFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetExtraLoadFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetExtraLoadFlagsX64(int target, out ulong extraLoadFlags);

    public static PS3_TMAPI.SNRESULT GetExtraLoadFlags(int target, out PS3_TMAPI.ExtraLoadFlag extraLoadFlags)
    {
        ulong extraLoadFlags1 = 0UL;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetExtraLoadFlagsX86(target, out extraLoadFlags1) : PS3_TMAPI.GetExtraLoadFlagsX64(target, out extraLoadFlags1);
        extraLoadFlags = (PS3_TMAPI.ExtraLoadFlag)extraLoadFlags1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetExtraLoadFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetExtraLoadFlagsX86(int target, ulong extraLoadFlags, ulong mask);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetExtraLoadFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetExtraLoadFlagsX64(int target, ulong extraLoadFlags, ulong mask);

    public static PS3_TMAPI.SNRESULT SetExtraLoadFlags(int target, PS3_TMAPI.ExtraLoadFlag extraLoadFlags, PS3_TMAPI.ExtraLoadFlagMask mask)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetExtraLoadFlagsX64(target, (ulong)extraLoadFlags, (ulong)mask);
        else
            return PS3_TMAPI.SetExtraLoadFlagsX86(target, (ulong)extraLoadFlags, (ulong)mask);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetSDKVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSDKVersionX86(int target, out ulong sdkVersion);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetSDKVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSDKVersionX64(int target, out ulong sdkVersion);

    public static PS3_TMAPI.SNRESULT GetSDKVersion(int target, out ulong sdkVersion)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetSDKVersionX64(target, out sdkVersion);
        else
            return PS3_TMAPI.GetSDKVersionX86(target, out sdkVersion);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetCPVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetCPVersionX86(int target, out ulong cpVersion);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetCPVersion", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetCPVersionX64(int target, out ulong cpVersion);

    public static PS3_TMAPI.SNRESULT GetCPVersion(int target, out ulong cpVersion)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetCPVersionX64(target, out cpVersion);
        else
            return PS3_TMAPI.GetCPVersionX86(target, out cpVersion);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetTimeouts", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetTimeoutsX86(int target, uint numTimeouts, PS3_TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetTimeouts", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetTimeoutsX64(int target, uint numTimeouts, PS3_TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    public static PS3_TMAPI.SNRESULT SetTimeouts(int target, PS3_TMAPI.TimeoutType[] timeoutTypes, uint[] timeoutValues)
    {
        if (timeoutTypes == null || timeoutTypes.Length < 1 || (timeoutValues == null || timeoutValues.Length != timeoutTypes.Length))
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetTimeoutsX64(target, (uint)timeoutTypes.Length, timeoutTypes, timeoutValues);
        else
            return PS3_TMAPI.SetTimeoutsX86(target, (uint)timeoutTypes.Length, timeoutTypes, timeoutValues);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTimeouts", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTimeoutsX86(int target, out uint numTimeouts, PS3_TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTimeouts", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetTimeoutsX64(int target, out uint numTimeouts, PS3_TMAPI.TimeoutType[] timeoutIds, uint[] timeoutValues);

    public static PS3_TMAPI.SNRESULT GetTimeouts(int target, out PS3_TMAPI.TimeoutType[] timeoutTypes, out uint[] timeoutValues)
    {
        timeoutTypes = (PS3_TMAPI.TimeoutType[])null;
        timeoutValues = (uint[])null;
        uint numTimeouts;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetTimeoutsX86(target, out numTimeouts, (PS3_TMAPI.TimeoutType[])null, (uint[])null) : PS3_TMAPI.GetTimeoutsX64(target, out numTimeouts, (PS3_TMAPI.TimeoutType[])null, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        timeoutTypes = new PS3_TMAPI.TimeoutType[numTimeouts];
        timeoutValues = new uint[numTimeouts];
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetTimeoutsX64(target, out numTimeouts, timeoutTypes, timeoutValues);
        else
            return PS3_TMAPI.GetTimeoutsX86(target, out numTimeouts, timeoutTypes, timeoutValues);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ListTTYStreams", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ListTtyStreamsX86(int target, ref uint size, IntPtr streamArray);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ListTTYStreams", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ListTtyStreamsX64(int target, ref uint size, IntPtr streamArray);

    public static PS3_TMAPI.SNRESULT ListTTYStreams(int target, out PS3_TMAPI.TTYStream[] streamArray)
    {
        streamArray = (PS3_TMAPI.TTYStream[])null;
        IntPtr streamArray1 = IntPtr.Zero;
        uint size = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ListTtyStreamsX86(target, ref size, streamArray1) : PS3_TMAPI.ListTtyStreamsX64(target, ref size, streamArray1);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)((long)Marshal.SizeOf(typeof(PS3_TMAPI.TTYStream)) * (long)size));
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ListTtyStreamsX86(target, ref size, num) : PS3_TMAPI.ListTtyStreamsX64(target, ref size, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = num;
            streamArray = new PS3_TMAPI.TTYStream[size];
            for (uint index = 0U; index < size; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.TTYStream>(unmanagedBuf, ref streamArray[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterTTYEventHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterTtyEventHandlerX86(int target, uint streamIndex, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterTTYEventHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterTtyEventHandlerX64(int target, uint streamIndex, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterTTYEventHandler(int target, uint streamID, PS3_TMAPI.TTYCallback callback, ref object userData)
    {
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterTtyEventHandlerX86(target, streamID, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3_TMAPI.RegisterTtyEventHandlerX64(target, streamID, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3_TMAPI.FAILED(res))
            return res;
        List<PS3_TMAPI.TTYChannel> list = new List<PS3_TMAPI.TTYChannel>();
        if ((int)streamID == -1)
        {
            PS3_TMAPI.TTYStream[] streamArray = (PS3_TMAPI.TTYStream[])null;
            res = PS3_TMAPI.ListTTYStreams(target, out streamArray);
            if (PS3_TMAPI.FAILED(res) || streamArray == null || streamArray.Length == 0)
                return res;
            foreach (PS3_TMAPI.TTYStream ttyStream in streamArray)
                list.Add(new PS3_TMAPI.TTYChannel(target, ttyStream.Index));
        }
        else
            list.Add(new PS3_TMAPI.TTYChannel(target, streamID));
        PS3_TMAPI.TTYCallbackAndUserData callbackAndUserData = new PS3_TMAPI.TTYCallbackAndUserData();
        callbackAndUserData.m_callback = callback;
        callbackAndUserData.m_userData = userData;
        foreach (PS3_TMAPI.TTYChannel index in list)
            PS3_TMAPI.ms_userTtyCallbacks[index] = callbackAndUserData;
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CancelTTYEvents", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelTtyEventsX86(int target, uint index);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3CancelTTYEvents", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelTtyEventsX64(int target, uint index);

    public static PS3_TMAPI.SNRESULT CancelTTYEvents(int target, uint streamID)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.CancelTtyEventsX86(target, streamID) : PS3_TMAPI.CancelTtyEventsX64(target, streamID);
        if (PS3_TMAPI.SUCCEEDED(res))
        {
            if ((int)streamID == -1)
            {
                List<PS3_TMAPI.TTYChannel> list = new List<PS3_TMAPI.TTYChannel>();
                foreach (KeyValuePair<PS3_TMAPI.TTYChannel, PS3_TMAPI.TTYCallbackAndUserData> keyValuePair in PS3_TMAPI.ms_userTtyCallbacks)
                {
                    if (keyValuePair.Key.Target == target)
                        list.Add(keyValuePair.Key);
                }
                foreach (PS3_TMAPI.TTYChannel key in list)
                    PS3_TMAPI.ms_userTtyCallbacks.Remove(key);
            }
            else
                PS3_TMAPI.ms_userTtyCallbacks.Remove(new PS3_TMAPI.TTYChannel(target, streamID));
        }
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SendTTY", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SendTTYX86(int target, uint index, string text);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SendTTY", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SendTTYX64(int target, uint index, string text);

    public static PS3_TMAPI.SNRESULT SendTTY(int target, uint streamID, string text)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SendTTYX64(target, streamID, text);
        else
            return PS3_TMAPI.SendTTYX86(target, streamID, text);
    }

    private static void MarshalTTYEvent(int target, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        PS3_TMAPI.TTYChannel key = new PS3_TMAPI.TTYChannel(target, param);
        PS3_TMAPI.TTYCallbackAndUserData callbackAndUserData;
        if (!PS3_TMAPI.ms_userTtyCallbacks.TryGetValue(key, out callbackAndUserData))
            return;
        string data1 = Marshal.PtrToStringAnsi(data, (int)length);
        callbackAndUserData.m_callback(target, param, result, data1, callbackAndUserData.m_userData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ClearTTYCache", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ClearTTYCacheX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ClearTTYCache", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ClearTTYCacheX64(int target);

    public static PS3_TMAPI.SNRESULT ClearTTYCache(int target)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ClearTTYCacheX86(target) : PS3_TMAPI.ClearTTYCacheX64(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Kick", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT KickX86();

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Kick", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT KickX64();

    public static PS3_TMAPI.SNRESULT Kick()
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.KickX64();
        else
            return PS3_TMAPI.KickX86();
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetStatusX86(int target, PS3_TMAPI.UnitType unit, out long status, IntPtr reasonCode);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetStatusX64(int target, PS3_TMAPI.UnitType unit, out long status, IntPtr reasonCode);

    public static PS3_TMAPI.SNRESULT GetStatus(int target, PS3_TMAPI.UnitType unit, out PS3_TMAPI.UnitStatus unitStatus)
    {
        long status;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetStatusX86(target, unit, out status, IntPtr.Zero) : PS3_TMAPI.GetStatusX64(target, unit, out status, IntPtr.Zero);
        unitStatus = (PS3_TMAPI.UnitStatus)status;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessLoad", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessLoadX86(int target, uint priority, string fileName, int argCount, string[] args, int envCount, string[] env, out uint processId, out ulong threadId, uint flags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessLoad", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessLoadX64(int target, uint priority, string fileName, int argCount, string[] args, int envCount, string[] env, out uint processId, out ulong threadId, uint flags);

    public static PS3_TMAPI.SNRESULT ProcessLoad(int target, uint priority, string fileName, string[] argv, string[] envv, out uint processID, out ulong threadID, PS3_TMAPI.LoadFlag loadFlags)
    {
        int argCount = 0;
        if (argv != null)
            argCount = argv.Length;
        int envCount = 0;
        if (envv != null)
            envCount = envv.Length;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessLoadX64(target, priority, fileName, argCount, argv, envCount, envv, out processID, out threadID, (uint)loadFlags);
        else
            return PS3_TMAPI.ProcessLoadX86(target, priority, fileName, argCount, argv, envCount, envv, out processID, out threadID, (uint)loadFlags);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessListX86(int target, ref uint count, IntPtr processIdArray);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessListX64(int target, ref uint count, IntPtr processIdArray);

    public static PS3_TMAPI.SNRESULT GetProcessList(int target, out uint[] processIDs)
    {
        processIDs = (uint[])null;
        IntPtr processIdArray = IntPtr.Zero;
        uint count = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessListX86(target, ref count, processIdArray) : PS3_TMAPI.GetProcessListX64(target, ref count, processIdArray);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal(4 * (int)count);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessListX86(target, ref count, num) : PS3_TMAPI.GetProcessListX64(target, ref count, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = num;
            processIDs = new uint[count];
            for (uint index = 0U; index < count; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UserProcessList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetUserProcessListX86(int target, ref uint count, IntPtr processIdArray);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UserProcessList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetUserProcessListX64(int target, ref uint count, IntPtr processIdArray);

    public static PS3_TMAPI.SNRESULT GetUserProcessList(int target, out uint[] processIDs)
    {
        IntPtr processIdArray = IntPtr.Zero;
        uint count = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetUserProcessListX86(target, ref count, processIdArray) : PS3_TMAPI.GetUserProcessListX64(target, ref count, processIdArray);
        if (PS3_TMAPI.FAILED(res1))
        {
            processIDs = (uint[])null;
            return res1;
        }
        else
        {
            IntPtr num = Marshal.AllocHGlobal(4 * (int)count);
            PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetUserProcessListX86(target, ref count, num) : PS3_TMAPI.GetUserProcessListX64(target, ref count, num);
            if (PS3_TMAPI.FAILED(res2))
            {
                Marshal.FreeHGlobal(num);
                processIDs = (uint[])null;
                return res2;
            }
            else
            {
                IntPtr unmanagedBuf = num;
                processIDs = new uint[count];
                for (uint index = 0U; index < count; ++index)
                    unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[index]);
                Marshal.FreeHGlobal(num);
                return res2;
            }
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessStop", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessStopX86(int target, uint processId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessStop", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessStopX64(int target, uint processId);

    public static PS3_TMAPI.SNRESULT ProcessStop(int target, uint processID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessStopX64(target, processID);
        else
            return PS3_TMAPI.ProcessStopX86(target, processID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessContinueX86(int target, uint processId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessContinueX64(int target, uint processId);

    public static PS3_TMAPI.SNRESULT ProcessContinue(int target, uint processID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessContinueX64(target, processID);
        else
            return PS3_TMAPI.ProcessContinueX86(target, processID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessKill", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessKillX86(int target, uint processId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessKill", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessKillX64(int target, uint processId);

    public static PS3_TMAPI.SNRESULT ProcessKill(int target, uint processID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessKillX64(target, processID);
        else
            return PS3_TMAPI.ProcessKillX86(target, processID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3TerminateGameProcess", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT TerminateGameProcessX86(int target, uint processId, uint timeout);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3TerminateGameProcess", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT TerminateGameProcessX64(int target, uint processId, uint timeout);

    public static PS3_TMAPI.SNRESULT TerminateGameProcess(int target, uint processID, uint timeout)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.TerminateGameProcessX64(target, processID, timeout);
        else
            return PS3_TMAPI.TerminateGameProcessX86(target, processID, timeout);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetThreadListX86(int target, uint processId, ref uint numPPUThreads, ulong[] ppuThreadIds, ref uint numSPUThreadGroups, ulong[] spuThreadGroupIds);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetThreadListX64(int target, uint processId, ref uint numPPUThreads, ulong[] ppuThreadIds, ref uint numSPUThreadGroups, ulong[] spuThreadGroupIds);

    public static PS3_TMAPI.SNRESULT GetThreadList(int target, uint processID, out ulong[] ppuThreadIDs, out ulong[] spuThreadGroupIDs)
    {
        ppuThreadIDs = (ulong[])null;
        spuThreadGroupIDs = (ulong[])null;
        uint numPPUThreads = 0U;
        uint numSPUThreadGroups = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetThreadListX86(target, processID, ref numPPUThreads, (ulong[])null, ref numSPUThreadGroups, (ulong[])null) : PS3_TMAPI.GetThreadListX64(target, processID, ref numPPUThreads, (ulong[])null, ref numSPUThreadGroups, (ulong[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        ppuThreadIDs = new ulong[numPPUThreads];
        spuThreadGroupIDs = new ulong[numSPUThreadGroups];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetThreadListX86(target, processID, ref numPPUThreads, ppuThreadIDs, ref numSPUThreadGroups, spuThreadGroupIDs) : PS3_TMAPI.GetThreadListX64(target, processID, ref numPPUThreads, ppuThreadIDs, ref numSPUThreadGroups, spuThreadGroupIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadStop", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadStopX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadStop", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadStopX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId);

    public static PS3_TMAPI.SNRESULT ThreadStop(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ThreadStopX64(target, unit, processID, threadID);
        else
            return PS3_TMAPI.ThreadStopX86(target, unit, processID, threadID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadContinue", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadContinueX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadContinue", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadContinueX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId);

    public static PS3_TMAPI.SNRESULT ThreadContinue(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ThreadContinueX64(target, unit, processID, threadID);
        else
            return PS3_TMAPI.ThreadContinueX86(target, unit, processID, threadID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadGetRegisters", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadGetRegistersX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadGetRegisters", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadGetRegistersX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    public static PS3_TMAPI.SNRESULT ThreadGetRegisters(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, uint[] registerNums, out ulong[] registerValues)
    {
        registerValues = (ulong[])null;
        if (registerNums == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        registerValues = new ulong[registerNums.Length];
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ThreadGetRegistersX64(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
        else
            return PS3_TMAPI.ThreadGetRegistersX86(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadSetRegisters", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadSetRegistersX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadSetRegisters", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadSetRegistersX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, uint numRegisters, uint[] registerNums, ulong[] registerValues);

    public static PS3_TMAPI.SNRESULT ThreadSetRegisters(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, uint[] registerNums, ulong[] registerValues)
    {
        if (registerNums == null || registerValues == null || registerNums.Length != registerValues.Length)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ThreadSetRegistersX64(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
        else
            return PS3_TMAPI.ThreadSetRegistersX86(target, unit, processID, threadID, (uint)registerNums.Length, registerNums, registerValues);
    }

    private static void ProcessInfoMarshalHelper(IntPtr unmanagedBuf, ref PS3_TMAPI.ProcessInfo processInfo)
    {
        IntPtr unmanagedBuf1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.ProcessInfoHdr>(unmanagedBuf, ref processInfo.Hdr);
        uint num = processInfo.Hdr.NumPPUThreads + processInfo.Hdr.NumSPUThreads;
        processInfo.ThreadIDs = new ulong[num];
        for (int index = 0; (long)index < (long)num; ++index)
            unmanagedBuf1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf1, ref processInfo.ThreadIDs[index]);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessInfoX86(int target, uint processId, ref uint bufferSize, IntPtr processInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessInfoX64(int target, uint processId, ref uint bufferSize, IntPtr processInfo);

    public static PS3_TMAPI.SNRESULT GetProcessInfo(int target, uint processID, out PS3_TMAPI.ProcessInfo processInfo)
    {
        processInfo = new PS3_TMAPI.ProcessInfo();
        IntPtr processInfo1 = IntPtr.Zero;
        uint bufferSize = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessInfoX86(target, processID, ref bufferSize, processInfo1) : PS3_TMAPI.GetProcessInfoX64(target, processID, ref bufferSize, processInfo1);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessInfoX86(target, processID, ref bufferSize, num) : PS3_TMAPI.GetProcessInfoX64(target, processID, ref bufferSize, num);
        if (PS3_TMAPI.SUCCEEDED(res2))
            PS3_TMAPI.ProcessInfoMarshalHelper(num, ref processInfo);
        Marshal.FreeHGlobal(num);
        return res2;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessInfoEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessInfoExX86(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3_TMAPI.ExtraProcessInfo extraProcessInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessInfoEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessInfoExX64(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3_TMAPI.ExtraProcessInfo extraProcessInfo);

    public static PS3_TMAPI.SNRESULT GetProcessInfoEx(int target, uint processID, out PS3_TMAPI.ProcessInfo processInfo, out PS3_TMAPI.ExtraProcessInfo extraProcessInfo)
    {
        processInfo = new PS3_TMAPI.ProcessInfo();
        extraProcessInfo = new PS3_TMAPI.ExtraProcessInfo();
        IntPtr processInfo1 = IntPtr.Zero;
        uint bufferSize = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessInfoExX86(target, processID, ref bufferSize, processInfo1, out extraProcessInfo) : PS3_TMAPI.GetProcessInfoExX64(target, processID, ref bufferSize, processInfo1, out extraProcessInfo);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessInfoExX86(target, processID, ref bufferSize, num, out extraProcessInfo) : PS3_TMAPI.GetProcessInfoExX64(target, processID, ref bufferSize, num, out extraProcessInfo);
        if (PS3_TMAPI.SUCCEEDED(res2))
            PS3_TMAPI.ProcessInfoMarshalHelper(num, ref processInfo);
        Marshal.FreeHGlobal(num);
        return res2;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessInfoEx2", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessInfoEx2X86(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3_TMAPI.ExtraProcessInfo extraProcessInfo, out PS3_TMAPI.ProcessLoadInfo processLoadInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessInfoEx2", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessInfoEx2X64(int target, uint processId, ref uint bufferSize, IntPtr processInfo, out PS3_TMAPI.ExtraProcessInfo extraProcessInfo, out PS3_TMAPI.ProcessLoadInfo processLoadInfo);

    public static PS3_TMAPI.SNRESULT GetProcessInfoEx2(int target, uint processID, out PS3_TMAPI.ProcessInfo processInfo, out PS3_TMAPI.ExtraProcessInfo extraProcessInfo, out PS3_TMAPI.ProcessLoadInfo processLoadInfo)
    {
        IntPtr processInfo1 = IntPtr.Zero;
        uint bufferSize = 0U;
        processInfo = new PS3_TMAPI.ProcessInfo();
        extraProcessInfo = new PS3_TMAPI.ExtraProcessInfo();
        processLoadInfo = new PS3_TMAPI.ProcessLoadInfo();
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessInfoEx2X86(target, processID, ref bufferSize, processInfo1, out extraProcessInfo, out processLoadInfo) : PS3_TMAPI.GetProcessInfoEx2X64(target, processID, ref bufferSize, processInfo1, out extraProcessInfo, out processLoadInfo);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessInfoEx2X86(target, processID, ref bufferSize, num, out extraProcessInfo, out processLoadInfo) : PS3_TMAPI.GetProcessInfoEx2X64(target, processID, ref bufferSize, num, out extraProcessInfo, out processLoadInfo);
        if (PS3_TMAPI.SUCCEEDED(res2))
            PS3_TMAPI.ProcessInfoMarshalHelper(num, ref processInfo);
        Marshal.FreeHGlobal(num);
        return res2;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetModuleList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetModuleListX86(int target, uint processId, ref uint numModules, uint[] moduleList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetModuleList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetModuleListX64(int target, uint processId, ref uint numModules, uint[] moduleList);

    public static PS3_TMAPI.SNRESULT GetModuleList(int target, uint processID, out uint[] modules)
    {
        modules = (uint[])null;
        uint numModules = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetModuleListX86(target, processID, ref numModules, (uint[])null) : PS3_TMAPI.GetModuleListX64(target, processID, ref numModules, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        modules = new uint[numModules];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetModuleListX86(target, processID, ref numModules, modules) : PS3_TMAPI.GetModuleListX64(target, processID, ref numModules, modules);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetModuleInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetModuleInfoX86(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetModuleInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetModuleInfoX64(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfo);

    public static PS3_TMAPI.SNRESULT GetModuleInfo(int target, uint processID, uint moduleID, out PS3_TMAPI.ModuleInfo moduleInfo)
    {
        moduleInfo = new PS3_TMAPI.ModuleInfo();
        IntPtr moduleInfo1 = IntPtr.Zero;
        ulong bufferSize = 0UL;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetModuleInfoX86(target, processID, moduleID, ref bufferSize, moduleInfo1) : PS3_TMAPI.GetModuleInfoX64(target, processID, moduleID, ref bufferSize, moduleInfo1);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        if (bufferSize > (ulong)int.MaxValue)
            return PS3_TMAPI.SNRESULT.SN_E_ERROR;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetModuleInfoX86(target, processID, moduleID, ref bufferSize, num) : PS3_TMAPI.GetModuleInfoX64(target, processID, moduleID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.ModuleInfoHdr>(num, ref moduleInfo.Hdr);
            moduleInfo.Segments = new PS3_TMAPI.PRXSegment[moduleInfo.Hdr.NumSegments];
            for (int index = 0; (long)index < (long)moduleInfo.Hdr.NumSegments; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.PRXSegment>(unmanagedBuf, ref moduleInfo.Segments[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetModuleInfoEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetModuleInfoExX86(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfoEx, out IntPtr mselfInfo, out PS3_TMAPI.ExtraModuleInfo extraModuleInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetModuleInfoEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetModuleInfoExX64(int target, uint processId, uint moduleId, ref ulong bufferSize, IntPtr moduleInfoEx, out IntPtr mselfInfo, out PS3_TMAPI.ExtraModuleInfo extraModuleInfo);

    public static PS3_TMAPI.SNRESULT GetModuleInfoEx(int target, uint processID, uint moduleID, out PS3_TMAPI.ModuleInfoEx moduleInfoEx, out PS3_TMAPI.MSELFInfo mselfInfo, out PS3_TMAPI.ExtraModuleInfo extraModuleInfo)
    {
        moduleInfoEx = new PS3_TMAPI.ModuleInfoEx();
        mselfInfo = new PS3_TMAPI.MSELFInfo();
        extraModuleInfo = new PS3_TMAPI.ExtraModuleInfo();
        IntPtr moduleInfoEx1 = IntPtr.Zero;
        ulong bufferSize = 0UL;
        IntPtr mselfInfo1 = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetModuleInfoExX86(target, processID, moduleID, ref bufferSize, moduleInfoEx1, out mselfInfo1, out extraModuleInfo) : PS3_TMAPI.GetModuleInfoExX64(target, processID, moduleID, ref bufferSize, moduleInfoEx1, out mselfInfo1, out extraModuleInfo);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        if (bufferSize > (ulong)int.MaxValue)
            return PS3_TMAPI.SNRESULT.SN_E_ERROR;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetModuleInfoExX86(target, processID, moduleID, ref bufferSize, num, out mselfInfo1, out extraModuleInfo) : PS3_TMAPI.GetModuleInfoExX64(target, processID, moduleID, ref bufferSize, num, out mselfInfo1, out extraModuleInfo);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.ModuleInfoHdr>(num, ref moduleInfoEx.Hdr);
            moduleInfoEx.Segments = new PS3_TMAPI.PRXSegmentEx[moduleInfoEx.Hdr.NumSegments];
            for (int index = 0; (long)index < (long)moduleInfoEx.Hdr.NumSegments; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.PRXSegmentEx>(unmanagedBuf, ref moduleInfoEx.Segments[index]);
            PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.MSELFInfo>(mselfInfo1, ref mselfInfo);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetThreadInfoX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetThreadInfoX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetPPUThreadInfo(int target, uint processID, ulong threadID, out PS3_TMAPI.PPUThreadInfo threadInfo)
    {
        threadInfo = new PS3_TMAPI.PPUThreadInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetThreadInfoX86(target, PS3_TMAPI.UnitType.PPU, processID, threadID, ref bufferSize, buffer) : PS3_TMAPI.GetThreadInfoX64(target, PS3_TMAPI.UnitType.PPU, processID, threadID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetThreadInfoX86(target, PS3_TMAPI.UnitType.PPU, processID, threadID, ref bufferSize, num) : PS3_TMAPI.GetThreadInfoX64(target, PS3_TMAPI.UnitType.PPU, processID, threadID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.PPUThreadInfoPriv storage = new PS3_TMAPI.PPUThreadInfoPriv();
            IntPtr ptr = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.PPUThreadInfoPriv>(num, ref storage);
            threadInfo.ThreadID = storage.ThreadID;
            threadInfo.Priority = storage.Priority;
            threadInfo.State = (PS3_TMAPI.PPUThreadState)storage.State;
            threadInfo.StackAddress = storage.StackAddress;
            threadInfo.StackSize = storage.StackSize;
            if (storage.ThreadNameLen > 0U)
                threadInfo.ThreadName = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PPUThreadInfoEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetPPUThreadInfoExX86(int target, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PPUThreadInfoEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetPPUThreadInfoExX64(int target, uint processId, ulong threadId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetPPUThreadInfoEx(int target, uint processID, ulong threadID, out PS3_TMAPI.PPUThreadInfoEx threadInfoEx)
    {
        threadInfoEx = new PS3_TMAPI.PPUThreadInfoEx();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetPPUThreadInfoExX86(target, processID, threadID, ref bufferSize, buffer) : PS3_TMAPI.GetPPUThreadInfoExX64(target, processID, threadID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetPPUThreadInfoExX86(target, processID, threadID, ref bufferSize, num) : PS3_TMAPI.GetPPUThreadInfoExX64(target, processID, threadID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.PPUThreadInfoExPriv storage = new PS3_TMAPI.PPUThreadInfoExPriv();
            IntPtr ptr = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.PPUThreadInfoExPriv>(num, ref storage);
            threadInfoEx.ThreadID = storage.ThreadId;
            threadInfoEx.Priority = storage.Priority;
            threadInfoEx.BasePriority = storage.BasePriority;
            threadInfoEx.State = (PS3_TMAPI.PPUThreadState)storage.State;
            threadInfoEx.StackAddress = storage.StackAddress;
            threadInfoEx.StackSize = storage.StackSize;
            if (storage.ThreadNameLen > 0U)
                threadInfoEx.ThreadName = Marshal.PtrToStringAnsi(ptr);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    public static PS3_TMAPI.SNRESULT GetSPUThreadInfo(int target, uint processID, ulong threadID, out PS3_TMAPI.SPUThreadInfo threadInfo)
    {
        threadInfo = new PS3_TMAPI.SPUThreadInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetThreadInfoX86(target, PS3_TMAPI.UnitType.SPU, processID, threadID, ref bufferSize, buffer) : PS3_TMAPI.GetThreadInfoX64(target, PS3_TMAPI.UnitType.SPU, processID, threadID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetThreadInfoX86(target, PS3_TMAPI.UnitType.SPU, processID, threadID, ref bufferSize, num) : PS3_TMAPI.GetThreadInfoX64(target, PS3_TMAPI.UnitType.SPU, processID, threadID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.SpuThreadInfoPriv storage = new PS3_TMAPI.SpuThreadInfoPriv();
            IntPtr ptr1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.SpuThreadInfoPriv>(num, ref storage);
            threadInfo.ThreadGroupID = storage.ThreadGroupId;
            threadInfo.ThreadID = storage.ThreadId;
            if (storage.FilenameLen > 0U)
                threadInfo.Filename = Marshal.PtrToStringAnsi(ptr1);
            IntPtr ptr2 = new IntPtr(ptr1.ToInt64() + (long)storage.FilenameLen);
            if (storage.ThreadNameLen > 0U)
                threadInfo.ThreadName = Marshal.PtrToStringAnsi(ptr2);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetDefaultPPUThreadStackSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDefaultPPUThreadStackSizeX86(int target, PS3_TMAPI.ELFStackSize size);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetDefaultPPUThreadStackSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDefaultPPUThreadStackSizeX64(int target, PS3_TMAPI.ELFStackSize size);

    public static PS3_TMAPI.SNRESULT SetDefaultPPUThreadStackSize(int target, PS3_TMAPI.ELFStackSize stackSize)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetDefaultPPUThreadStackSizeX64(target, stackSize);
        else
            return PS3_TMAPI.SetDefaultPPUThreadStackSizeX86(target, stackSize);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetDefaultPPUThreadStackSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDefaultPPUThreadStackSizeX86(int target, out PS3_TMAPI.ELFStackSize size);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetDefaultPPUThreadStackSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDefaultPPUThreadStackSizeX64(int target, out PS3_TMAPI.ELFStackSize size);

    public static PS3_TMAPI.SNRESULT GetDefaultPPUThreadStackSize(int target, out PS3_TMAPI.ELFStackSize stackSize)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetDefaultPPUThreadStackSizeX64(target, out stackSize);
        else
            return PS3_TMAPI.GetDefaultPPUThreadStackSizeX86(target, out stackSize);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetSPULoopPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetSPULoopPointX86(int target, uint processId, ulong threadId, uint address, int bCurrentPc);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetSPULoopPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetSPULoopPointX64(int target, uint processId, ulong threadId, uint address, int bCurrentPc);

    public static PS3_TMAPI.SNRESULT SetSPULoopPoint(int target, uint processID, ulong threadID, uint address, bool bCurrentPC)
    {
        int bCurrentPc = bCurrentPC ? 1 : 0;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetSPULoopPointX64(target, processID, threadID, address, bCurrentPc);
        else
            return PS3_TMAPI.SetSPULoopPointX86(target, processID, threadID, address, bCurrentPc);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ClearSPULoopPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ClearSPULoopPointX86(int target, uint processId, ulong threadId, uint address, bool bCurrentPc);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ClearSPULoopPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ClearSPULoopPointX64(int target, uint processId, ulong threadId, uint address, bool bCurrentPc);

    public static PS3_TMAPI.SNRESULT ClearSPULoopPoint(int target, uint processID, ulong threadID, uint address, bool bCurrentPC)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ClearSPULoopPointX64(target, processID, threadID, address, bCurrentPC);
        else
            return PS3_TMAPI.ClearSPULoopPointX86(target, processID, threadID, address, bCurrentPC);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetBreakPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetBreakPointX86(int target, uint unit, uint processId, ulong threadId, ulong address);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetBreakPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetBreakPointX64(int target, uint unit, uint processId, ulong threadId, ulong address);

    public static PS3_TMAPI.SNRESULT SetBreakPoint(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, ulong address)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetBreakPointX64(target, (uint)unit, processID, threadID, address);
        else
            return PS3_TMAPI.SetBreakPointX86(target, (uint)unit, processID, threadID, address);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ClearBreakPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ClearBreakPointX86(int target, uint unit, uint processId, ulong threadId, ulong address);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ClearBreakPoint", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ClearBreakPointX64(int target, uint unit, uint processId, ulong threadId, ulong address);

    public static PS3_TMAPI.SNRESULT ClearBreakPoint(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, ulong address)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ClearBreakPointX64(target, (uint)unit, processID, threadID, address);
        else
            return PS3_TMAPI.ClearBreakPointX86(target, (uint)unit, processID, threadID, address);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetBreakPoints", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetBreakPointsX86(int target, uint unit, uint processId, ulong threadId, out uint numBreakpoints, ulong[] addresses);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetBreakPoints", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetBreakPointsX64(int target, uint unit, uint processId, ulong threadId, out uint numBreakpoints, ulong[] addresses);

    public static PS3_TMAPI.SNRESULT GetBreakPoints(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, out ulong[] bpAddresses)
    {
        bpAddresses = (ulong[])null;
        uint numBreakpoints;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetBreakPointsX86(target, (uint)unit, processID, threadID, out numBreakpoints, (ulong[])null) : PS3_TMAPI.GetBreakPointsX64(target, (uint)unit, processID, threadID, out numBreakpoints, (ulong[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        bpAddresses = new ulong[numBreakpoints];
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetBreakPointsX64(target, (uint)unit, processID, threadID, out numBreakpoints, bpAddresses);
        else
            return PS3_TMAPI.GetBreakPointsX86(target, (uint)unit, processID, threadID, out numBreakpoints, bpAddresses);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetDebugThreadControlInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDebugThreadControlInfoX86(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetDebugThreadControlInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDebugThreadControlInfoX64(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetDebugThreadControlInfo(int target, uint processID, out PS3_TMAPI.DebugThreadControlInfo threadCtrlInfo)
    {
        threadCtrlInfo = new PS3_TMAPI.DebugThreadControlInfo();
        IntPtr buffer = IntPtr.Zero;
        uint bufferSize = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetDebugThreadControlInfoX86(target, processID, ref bufferSize, buffer) : PS3_TMAPI.GetDebugThreadControlInfoX64(target, processID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num1 = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetDebugThreadControlInfoX86(target, processID, ref bufferSize, num1) : PS3_TMAPI.GetDebugThreadControlInfoX64(target, processID, ref bufferSize, num1);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num1);
            return res2;
        }
        else
        {
            PS3_TMAPI.DebugThreadControlInfoPriv storage = new PS3_TMAPI.DebugThreadControlInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.DebugThreadControlInfoPriv>(num1, ref storage);
            threadCtrlInfo.ControlFlags = storage.ControlFlags;
            uint num2 = storage.NumEntries;
            threadCtrlInfo.ControlKeywords = new PS3_TMAPI.ControlKeywordEntry[num2];
            for (uint index = 0U; index < num2; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.ControlKeywordEntry>(unmanagedBuf, ref threadCtrlInfo.ControlKeywords[index]);
            Marshal.FreeHGlobal(num1);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetDebugThreadControlInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDebugThreadControlInfoX86(int target, uint processId, IntPtr threadCtrlInfo, out uint maxEntries);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetDebugThreadControlInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDebugThreadControlInfoX64(int target, uint processId, IntPtr threadCtrlInfo, out uint maxEntries);

    public static PS3_TMAPI.SNRESULT SetDebugThreadControlInfo(int target, uint processID, PS3_TMAPI.DebugThreadControlInfo threadCtrlInfo, out uint maxEntries)
    {
        PS3_TMAPI.DebugThreadControlInfoPriv storage = new PS3_TMAPI.DebugThreadControlInfoPriv();
        storage.ControlFlags = threadCtrlInfo.ControlFlags;
        if (threadCtrlInfo.ControlKeywords != null)
            storage.NumEntries = (uint)threadCtrlInfo.ControlKeywords.Length;
        IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf((object)storage) + (int)storage.NumEntries * Marshal.SizeOf(typeof(PS3_TMAPI.ControlKeywordEntry)));
        IntPtr unmanagedBuf = PS3_TMAPI.WriteDataToUnmanagedIncPtr<PS3_TMAPI.DebugThreadControlInfoPriv>(storage, num);
        for (int index = 0; (long)index < (long)storage.NumEntries; ++index)
            unmanagedBuf = PS3_TMAPI.WriteDataToUnmanagedIncPtr<PS3_TMAPI.ControlKeywordEntry>(threadCtrlInfo.ControlKeywords[index], unmanagedBuf);
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.SetDebugThreadControlInfoX86(target, processID, num, out maxEntries) : PS3_TMAPI.SetDebugThreadControlInfoX64(target, processID, num, out maxEntries);
        Marshal.FreeHGlobal(num);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ThreadExceptionClean", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadExceptionCleanX86(int target, uint processId, ulong threadId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ThreadExceptionClean", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ThreadExceptionCleanX64(int target, uint processId, ulong threadId);

    public static PS3_TMAPI.SNRESULT ThreadExceptionClean(int target, uint processID, ulong threadID)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ThreadExceptionCleanX86(target, processID, threadID) : PS3_TMAPI.ThreadExceptionCleanX64(target, processID, threadID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetRawSPULogicalIDs", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetRawSPULogicalIdsX86(int target, uint processId, ulong[] logicalIds);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetRawSPULogicalIDs", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetRawSPULogicalIdsX64(int target, uint processId, ulong[] logicalIds);

    public static PS3_TMAPI.SNRESULT GetRawSPULogicalIDs(int target, uint processID, out ulong[] logicalIDs)
    {
        logicalIDs = new ulong[8];
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetRawSPULogicalIdsX64(target, processID, logicalIDs);
        else
            return PS3_TMAPI.GetRawSPULogicalIdsX86(target, processID, logicalIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SPUThreadGroupStop", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SPUThreadGroupStopX86(int target, uint processId, ulong threadGroupId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SPUThreadGroupStop", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SPUThreadGroupStopX64(int target, uint processId, ulong threadGroupId);

    public static PS3_TMAPI.SNRESULT SPUThreadGroupStop(int target, uint processID, ulong threadGroupID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SPUThreadGroupStopX64(target, processID, threadGroupID);
        else
            return PS3_TMAPI.SPUThreadGroupStopX86(target, processID, threadGroupID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SPUThreadGroupContinue", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SPUThreadGroupContinueX86(int target, uint processId, ulong threadGroupId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SPUThreadGroupContinue", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SPUThreadGroupContinueX64(int target, uint processId, ulong threadGroupId);

    public static PS3_TMAPI.SNRESULT SPUThreadGroupContinue(int target, uint processID, ulong threadGroupID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SPUThreadGroupContinueX64(target, processID, threadGroupID);
        else
            return PS3_TMAPI.SPUThreadGroupContinueX86(target, processID, threadGroupID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetProcessTree", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessTreeX86(int target, ref uint numProcesses, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetProcessTree", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetProcessTreeX64(int target, ref uint numProcesses, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetProcessTree(int target, out PS3_TMAPI.ProcessTreeBranch[] processTree)
    {
        processTree = (PS3_TMAPI.ProcessTreeBranch[])null;
        IntPtr buffer = IntPtr.Zero;
        uint numProcesses = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessTreeX86(target, ref numProcesses, buffer) : PS3_TMAPI.GetProcessTreeX64(target, ref numProcesses, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)numProcesses * Marshal.SizeOf(typeof(PS3_TMAPI.ProcessTreeBranchPriv)));
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetProcessTreeX86(target, ref numProcesses, num) : PS3_TMAPI.GetProcessTreeX64(target, ref numProcesses, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            processTree = new PS3_TMAPI.ProcessTreeBranch[numProcesses];
            for (int index1 = 0; (long)index1 < (long)numProcesses; ++index1)
            {
                PS3_TMAPI.ProcessTreeBranchPriv processTreeBranchPriv = (PS3_TMAPI.ProcessTreeBranchPriv)Marshal.PtrToStructure(num, typeof(PS3_TMAPI.ProcessTreeBranchPriv));
                processTree[index1].ProcessID = processTreeBranchPriv.ProcessId;
                processTree[index1].ProcessState = processTreeBranchPriv.ProcessState;
                processTree[index1].ProcessFlags = processTreeBranchPriv.ProcessFlags;
                processTree[index1].RawSPU = processTreeBranchPriv.RawSPU;
                processTree[index1].PPUThreadStatuses = new PS3_TMAPI.PPUThreadStatus[processTreeBranchPriv.NumPpuThreads];
                processTree[index1].SPUThreadGroupStatuses = new PS3_TMAPI.SPUThreadGroupStatus[processTreeBranchPriv.NumSpuThreadGroups];
                for (int index2 = 0; (long)index2 < (long)processTreeBranchPriv.NumPpuThreads; ++index2)
                    processTreeBranchPriv.PpuThreadStatuses = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.PPUThreadStatus>(processTreeBranchPriv.PpuThreadStatuses, ref processTree[index1].PPUThreadStatuses[index2]);
                for (int index2 = 0; (long)index2 < (long)processTreeBranchPriv.NumSpuThreadGroups; ++index2)
                    processTreeBranchPriv.SpuThreadGroupStatuses = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.SPUThreadGroupStatus>(processTreeBranchPriv.SpuThreadGroupStatuses, ref processTree[index1].SPUThreadGroupStatuses[index2]);
            }
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetSPUThreadGroupInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSPUThreadGroupInfoX86(int target, uint processId, ulong threadGroupId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetSPUThreadGroupInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSPUThreadGroupInfoX64(int target, uint processId, ulong threadGroupId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetSPUThreadGroupInfo(int target, uint processID, ulong threadGroupID, out PS3_TMAPI.SPUThreadGroupInfo threadGroupInfo)
    {
        threadGroupInfo = new PS3_TMAPI.SPUThreadGroupInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSPUThreadGroupInfoX86(target, processID, threadGroupID, ref bufferSize, buffer) : PS3_TMAPI.GetSPUThreadGroupInfoX64(target, processID, threadGroupID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num1 = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSPUThreadGroupInfoX86(target, processID, threadGroupID, ref bufferSize, num1) : PS3_TMAPI.GetSPUThreadGroupInfoX64(target, processID, threadGroupID, ref bufferSize, num1);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num1);
            return res2;
        }
        else
        {
            PS3_TMAPI.SpuThreadGroupInfoPriv storage = new PS3_TMAPI.SpuThreadGroupInfoPriv();
            IntPtr num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.SpuThreadGroupInfoPriv>(num1, ref storage);
            threadGroupInfo.ThreadGroupID = storage.ThreadGroupId;
            threadGroupInfo.State = (PS3_TMAPI.SPUThreadGroupState)storage.State;
            threadGroupInfo.Priority = storage.Priority;
            threadGroupInfo.ThreadIDs = new uint[storage.NumThreads];
            for (int index = 0; (long)index < (long)storage.NumThreads; ++index)
                num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num2, ref threadGroupInfo.ThreadIDs[index]);
            if (storage.ThreadGroupNameLen > 0U)
                threadGroupInfo.GroupName = Marshal.PtrToStringAnsi(num2);
            Marshal.FreeHGlobal(num1);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessGetMemoryX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessGetMemoryX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    public static PS3_TMAPI.SNRESULT ProcessGetMemory(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, ulong address, ref byte[] buffer)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessGetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
        else
            return PS3_TMAPI.ProcessGetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessSetMemoryX86(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessSetMemoryX64(int target, PS3_TMAPI.UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);

    public static PS3_TMAPI.SNRESULT ProcessSetMemory(int target, PS3_TMAPI.UnitType unit, uint processID, ulong threadID, ulong address, byte[] buffer)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessSetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
        else
            return PS3_TMAPI.ProcessSetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMemoryCompressed", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMemoryCompressedX86(int target, uint processId, uint compressionLevel, uint address, uint size, byte[] buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMemoryCompressed", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMemoryCompressedX64(int target, uint processId, uint compressionLevel, uint address, uint size, byte[] buffer);

    public static PS3_TMAPI.SNRESULT GetMemoryCompressed(int target, uint processID, PS3_TMAPI.MemoryCompressionLevel compressionLevel, uint address, ref byte[] buffer)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetMemoryCompressedX64(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
        else
            return PS3_TMAPI.GetMemoryCompressedX86(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMemory64Compressed", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMemory64CompressedX86(int target, uint processId, uint compressionLevel, ulong address, uint size, byte[] buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMemory64Compressed", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMemory64CompressedX64(int target, uint processId, uint compressionLevel, ulong address, uint size, byte[] buffer);

    public static PS3_TMAPI.SNRESULT GetMemory64Compressed(int target, uint processID, PS3_TMAPI.MemoryCompressionLevel compressionLevel, ulong address, ref byte[] buffer)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetMemory64CompressedX64(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
        else
            return PS3_TMAPI.GetMemory64CompressedX86(target, processID, (uint)compressionLevel, address, (uint)buffer.Length, buffer);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetVirtualMemoryInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetVirtualMemoryInfoX86(int target, uint processId, bool bStatsOnly, out uint areaCount, out uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetVirtualMemoryInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetVirtualMemoryInfoX64(int target, uint processId, bool bStatsOnly, out uint areaCount, out uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetVirtualMemoryInfo(int target, uint processID, bool bStatsOnly, out PS3_TMAPI.VirtualMemoryArea[] vmAreas)
    {
        vmAreas = (PS3_TMAPI.VirtualMemoryArea[])null;
        uint areaCount;
        uint bufferSize;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetVirtualMemoryInfoX86(target, processID, bStatsOnly, out areaCount, out bufferSize, IntPtr.Zero) : PS3_TMAPI.GetVirtualMemoryInfoX64(target, processID, bStatsOnly, out areaCount, out bufferSize, IntPtr.Zero);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetVirtualMemoryInfoX86(target, processID, bStatsOnly, out areaCount, out bufferSize, num) : PS3_TMAPI.GetVirtualMemoryInfoX64(target, processID, bStatsOnly, out areaCount, out bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            vmAreas = new PS3_TMAPI.VirtualMemoryArea[areaCount];
            IntPtr unmanagedBuf1 = num;
            for (int index = 0; (long)index < (long)areaCount; ++index)
            {
                IntPtr unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf1, ref vmAreas[index].Address), ref vmAreas[index].Flags), ref vmAreas[index].VSize), ref vmAreas[index].Options), ref vmAreas[index].PageFaultPPU), ref vmAreas[index].PageFaultSPU), ref vmAreas[index].PageIn), ref vmAreas[index].PageOut), ref vmAreas[index].PMemTotal), ref vmAreas[index].PMemUsed), ref vmAreas[index].Time);
                ulong storage1 = 0UL;
                IntPtr unmanagedBuf3 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf2, ref storage1);
                vmAreas[index].Pages = new ulong[storage1];
                IntPtr storage2 = IntPtr.Zero;
                unmanagedBuf1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<IntPtr>(unmanagedBuf3, ref storage2);
            }
            for (int index1 = 0; (long)index1 < (long)areaCount; ++index1)
            {
                int length = vmAreas[index1].Pages.Length;
                for (int index2 = 0; index2 < length; ++index2)
                    unmanagedBuf1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf1, ref vmAreas[index1].Pages[index2]);
            }
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetSyncPrimitiveCountsEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSyncPrimitiveCountsExX86(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetSyncPrimitiveCountsEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSyncPrimitiveCountsExX64(int target, uint processId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetSyncPrimitiveCounts(int target, uint processID, out PS3_TMAPI.SyncPrimitiveCounts primitiveCounts)
    {
        primitiveCounts = new PS3_TMAPI.SyncPrimitiveCounts();
        uint bufferSize = 28U;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSyncPrimitiveCountsExX86(target, processID, ref bufferSize, num) : PS3_TMAPI.GetSyncPrimitiveCountsExX64(target, processID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res))
        {
            Marshal.FreeHGlobal(num);
            return res;
        }
        else
        {
            primitiveCounts = (PS3_TMAPI.SyncPrimitiveCounts)Marshal.PtrToStructure(num, typeof(PS3_TMAPI.SyncPrimitiveCounts));
            Marshal.FreeHGlobal(num);
            return res;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMutexList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMutexListX86(int target, uint processId, ref uint numMutexes, uint[] mutexList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMutexList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMutexListX64(int target, uint processId, ref uint numMutexes, uint[] mutexList);

    public static PS3_TMAPI.SNRESULT GetMutexList(int target, uint processID, out uint[] mutexIDs)
    {
        mutexIDs = (uint[])null;
        uint numMutexes = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMutexListX86(target, processID, ref numMutexes, (uint[])null) : PS3_TMAPI.GetMutexListX64(target, processID, ref numMutexes, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        mutexIDs = new uint[numMutexes];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMutexListX86(target, processID, ref numMutexes, mutexIDs) : PS3_TMAPI.GetMutexListX64(target, processID, ref numMutexes, mutexIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMutexInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMutexInfoX86(int target, uint processId, uint mutexId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMutexInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMutexInfoX64(int target, uint processId, uint mutexId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetMutexInfo(int target, uint processID, uint mutexID, out PS3_TMAPI.MutexInfo mutexInfo)
    {
        mutexInfo = new PS3_TMAPI.MutexInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMutexInfoX86(target, processID, mutexID, ref bufferSize, buffer) : PS3_TMAPI.GetMutexInfoX64(target, processID, mutexID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMutexInfoX86(target, processID, mutexID, ref bufferSize, num) : PS3_TMAPI.GetMutexInfoX64(target, processID, mutexID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.MutexInfoPriv storage = new PS3_TMAPI.MutexInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.MutexInfoPriv>(num, ref storage);
            mutexInfo.ID = storage.Id;
            mutexInfo.Attribute = storage.Attribute;
            mutexInfo.OwnerThreadID = storage.OwnerThreadId;
            mutexInfo.LockCounter = storage.LockCounter;
            mutexInfo.ConditionRefCounter = storage.ConditionRefCounter;
            mutexInfo.ConditionVarID = storage.ConditionVarId;
            mutexInfo.NumWaitAllThreads = storage.NumWaitAllThreads;
            mutexInfo.WaitingThreads = new ulong[storage.NumWaitingThreads];
            for (int index = 0; (long)index < (long)storage.NumWaitingThreads; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref mutexInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetLightWeightMutexList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightMutexListX86(int target, uint processId, ref uint numLWMutexes, uint[] lwMutexList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetLightWeightMutexList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightMutexListX64(int target, uint processId, ref uint numLWMutexes, uint[] lwMutexList);

    public static PS3_TMAPI.SNRESULT GetLightWeightMutexList(int target, uint processID, out uint[] lwMutexIDs)
    {
        lwMutexIDs = (uint[])null;
        uint numLWMutexes = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightMutexListX86(target, processID, ref numLWMutexes, (uint[])null) : PS3_TMAPI.GetLightWeightMutexListX64(target, processID, ref numLWMutexes, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        lwMutexIDs = new uint[numLWMutexes];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightMutexListX86(target, processID, ref numLWMutexes, lwMutexIDs) : PS3_TMAPI.GetLightWeightMutexListX64(target, processID, ref numLWMutexes, lwMutexIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetLightWeightMutexInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightMutexInfoX86(int target, uint processId, uint lwMutexId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetLightWeightMutexInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightMutexInfoX64(int target, uint processId, uint lwMutexId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetLightWeightMutexInfo(int target, uint processID, uint lwMutexID, out PS3_TMAPI.LWMutexInfo lwMutexInfo)
    {
        lwMutexInfo = new PS3_TMAPI.LWMutexInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightMutexInfoX86(target, processID, lwMutexID, ref bufferSize, buffer) : PS3_TMAPI.GetLightWeightMutexInfoX64(target, processID, lwMutexID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightMutexInfoX86(target, processID, lwMutexID, ref bufferSize, num) : PS3_TMAPI.GetLightWeightMutexInfoX64(target, processID, lwMutexID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.LwMutexInfoPriv storage = new PS3_TMAPI.LwMutexInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.LwMutexInfoPriv>(num, ref storage);
            lwMutexInfo.ID = storage.Id;
            lwMutexInfo.Attribute = storage.Attribute;
            lwMutexInfo.OwnerThreadID = storage.OwnerThreadId;
            lwMutexInfo.LockCounter = storage.LockCounter;
            lwMutexInfo.NumWaitAllThreads = storage.NumWaitAllThreads;
            lwMutexInfo.WaitingThreads = new ulong[storage.NumWaitingThreads];
            for (int index = 0; (long)index < (long)storage.NumWaitingThreads; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref lwMutexInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConditionalVariableList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConditionalVariableListX86(int target, uint processId, ref uint numConditionVars, uint[] conditionVarList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConditionalVariableList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConditionalVariableListX64(int target, uint processId, ref uint numConditionVars, uint[] conditionVarList);

    public static PS3_TMAPI.SNRESULT GetConditionalVariableList(int target, uint processID, out uint[] conditionVarIDs)
    {
        conditionVarIDs = (uint[])null;
        uint numConditionVars = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetConditionalVariableListX86(target, processID, ref numConditionVars, (uint[])null) : PS3_TMAPI.GetConditionalVariableListX64(target, processID, ref numConditionVars, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        conditionVarIDs = new uint[numConditionVars];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetConditionalVariableListX86(target, processID, ref numConditionVars, conditionVarIDs) : PS3_TMAPI.GetConditionalVariableListX64(target, processID, ref numConditionVars, conditionVarIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConditionalVariableInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConditionalVariableInfoX86(int target, uint processId, uint conditionVarId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConditionalVariableInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetConditionalVariableInfoX64(int target, uint processId, uint conditionVarId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetConditionalVariableInfo(int target, uint processID, uint conditionVarID, out PS3_TMAPI.ConditionVarInfo conditionVarInfo)
    {
        conditionVarInfo = new PS3_TMAPI.ConditionVarInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetConditionalVariableInfoX86(target, processID, conditionVarID, ref bufferSize, buffer) : PS3_TMAPI.GetConditionalVariableInfoX64(target, processID, conditionVarID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetConditionalVariableInfoX86(target, processID, conditionVarID, ref bufferSize, num) : PS3_TMAPI.GetConditionalVariableInfoX64(target, processID, conditionVarID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.ConditionVarInfoPriv storage = new PS3_TMAPI.ConditionVarInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.ConditionVarInfoPriv>(num, ref storage);
            conditionVarInfo.ID = storage.Id;
            conditionVarInfo.Attribute = storage.Attribute;
            conditionVarInfo.MutexID = storage.MutexId;
            conditionVarInfo.NumWaitAllThreads = storage.NumWaitAllThreads;
            conditionVarInfo.WaitingThreads = new ulong[storage.NumWaitingThreads];
            for (int index = 0; (long)index < (long)storage.NumWaitingThreads; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref conditionVarInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetLightWeightConditionalList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightConditionalListX86(int target, uint processId, ref uint numLWConditionVars, uint[] lwConditionVarList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetLightWeightConditionalList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightConditionalListX64(int target, uint processId, ref uint numLWConditionVars, uint[] lwConditionVarList);

    public static PS3_TMAPI.SNRESULT GetLightWeightConditionalList(int target, uint processID, out uint[] lwConditionVarIDs)
    {
        lwConditionVarIDs = (uint[])null;
        uint numLWConditionVars = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightConditionalListX86(target, processID, ref numLWConditionVars, (uint[])null) : PS3_TMAPI.GetLightWeightConditionalListX64(target, processID, ref numLWConditionVars, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        lwConditionVarIDs = new uint[numLWConditionVars];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightConditionalListX86(target, processID, ref numLWConditionVars, lwConditionVarIDs) : PS3_TMAPI.GetLightWeightConditionalListX64(target, processID, ref numLWConditionVars, lwConditionVarIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetLightWeightConditionalInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightConditionalInfoX86(int target, uint processId, uint lwCondVarId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetLightWeightConditionalInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLightWeightConditionalInfoX64(int target, uint processId, uint lwCondVarId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetLightWeightConditionalInfo(int target, uint processID, uint lwCondVarID, out PS3_TMAPI.LWConditionVarInfo lwConditonVarInfo)
    {
        lwConditonVarInfo = new PS3_TMAPI.LWConditionVarInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightConditionalInfoX86(target, processID, lwCondVarID, ref bufferSize, buffer) : PS3_TMAPI.GetLightWeightConditionalInfoX64(target, processID, lwCondVarID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLightWeightConditionalInfoX86(target, processID, lwCondVarID, ref bufferSize, num) : PS3_TMAPI.GetLightWeightConditionalInfoX64(target, processID, lwCondVarID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.LwConditionVarInfoPriv storage = new PS3_TMAPI.LwConditionVarInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.LwConditionVarInfoPriv>(num, ref storage);
            lwConditonVarInfo = new PS3_TMAPI.LWConditionVarInfo();
            lwConditonVarInfo.ID = storage.Id;
            lwConditonVarInfo.Attribute = storage.Attribute;
            lwConditonVarInfo.LWMutexID = storage.LwMutexId;
            lwConditonVarInfo.NumWaitAllThreads = storage.NumWaitAllThreads;
            lwConditonVarInfo.WaitingThreads = new ulong[storage.NumWaitingThreads];
            for (int index = 0; (long)index < (long)storage.NumWaitingThreads; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref lwConditonVarInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetReadWriteLockList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetReadWriteLockListX86(int target, uint processId, ref uint numRWLocks, uint[] rwLockList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetReadWriteLockList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetReadWriteLockListX64(int target, uint processId, ref uint numRWLocks, uint[] rwLockList);

    public static PS3_TMAPI.SNRESULT GetReadWriteLockList(int target, uint processID, out uint[] rwLockList)
    {
        rwLockList = (uint[])null;
        uint numRWLocks = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetReadWriteLockListX86(target, processID, ref numRWLocks, (uint[])null) : PS3_TMAPI.GetReadWriteLockListX64(target, processID, ref numRWLocks, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        rwLockList = new uint[numRWLocks];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetReadWriteLockListX86(target, processID, ref numRWLocks, rwLockList) : PS3_TMAPI.GetReadWriteLockListX64(target, processID, ref numRWLocks, rwLockList);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetReadWriteLockInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetReadWriteLockInfoX86(int target, uint processId, uint rwLockId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetReadWriteLockInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetReadWriteLockInfoX64(int target, uint processId, uint rwLockId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetReadWriteLockInfo(int target, uint processID, uint rwLockID, out PS3_TMAPI.RWLockInfo rwLockInfo)
    {
        rwLockInfo = new PS3_TMAPI.RWLockInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetReadWriteLockInfoX86(target, processID, rwLockID, ref bufferSize, buffer) : PS3_TMAPI.GetReadWriteLockInfoX64(target, processID, rwLockID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num1 = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetReadWriteLockInfoX86(target, processID, rwLockID, ref bufferSize, num1) : PS3_TMAPI.GetReadWriteLockInfoX64(target, processID, rwLockID, ref bufferSize, num1);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num1);
            return res2;
        }
        else
        {
            PS3_TMAPI.RwLockInfoPriv storage = new PS3_TMAPI.RwLockInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.RwLockInfoPriv>(num1, ref storage);
            rwLockInfo.ID = storage.Id;
            rwLockInfo.Attribute = storage.Attribute;
            rwLockInfo.NumWaitingReadThreads = storage.NumWaitingReadThreads;
            rwLockInfo.NumWaitAllReadThreads = storage.NumWaitAllReadThreads;
            rwLockInfo.NumWaitingWriteThreads = storage.NumWaitingWriteThreads;
            rwLockInfo.NumWaitAllWriteThreads = storage.NumWaitAllWriteThreads;
            uint num2 = rwLockInfo.NumWaitingReadThreads + rwLockInfo.NumWaitingWriteThreads;
            rwLockInfo.WaitingThreads = new ulong[num2];
            for (int index = 0; (long)index < (long)num2; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref rwLockInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num1);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetSemaphoreList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSemaphoreListX86(int target, uint processId, ref uint numSemaphores, uint[] semaphoreList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetSemaphoreList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSemaphoreListX64(int target, uint processId, ref uint numSemaphores, uint[] semaphoreList);

    public static PS3_TMAPI.SNRESULT GetSemaphoreList(int target, uint processID, out uint[] semaphoreIDs)
    {
        semaphoreIDs = (uint[])null;
        uint numSemaphores = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSemaphoreListX86(target, processID, ref numSemaphores, (uint[])null) : PS3_TMAPI.GetSemaphoreListX64(target, processID, ref numSemaphores, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        semaphoreIDs = new uint[numSemaphores];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSemaphoreListX86(target, processID, ref numSemaphores, semaphoreIDs) : PS3_TMAPI.GetSemaphoreListX64(target, processID, ref numSemaphores, semaphoreIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetSemaphoreInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSemaphoreInfoX86(int target, uint processId, uint semaphoreId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetSemaphoreInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetSemaphoreInfoX64(int target, uint processId, uint semaphoreId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetSemaphoreInfo(int target, uint processID, uint semaphoreID, out PS3_TMAPI.SemaphoreInfo semaphoreInfo)
    {
        semaphoreInfo = new PS3_TMAPI.SemaphoreInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSemaphoreInfoX86(target, processID, semaphoreID, ref bufferSize, buffer) : PS3_TMAPI.GetSemaphoreInfoX64(target, processID, semaphoreID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetSemaphoreInfoX86(target, processID, semaphoreID, ref bufferSize, num) : PS3_TMAPI.GetSemaphoreInfoX64(target, processID, semaphoreID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.SemaphoreInfoPriv storage = new PS3_TMAPI.SemaphoreInfoPriv();
            IntPtr unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.SemaphoreInfoPriv>(num, ref storage);
            semaphoreInfo.ID = storage.Id;
            semaphoreInfo.Attribute = storage.Attribute;
            semaphoreInfo.MaxValue = storage.MaxValue;
            semaphoreInfo.CurrentValue = storage.CurrentValue;
            semaphoreInfo.NumWaitAllThreads = storage.NumWaitAllThreads;
            semaphoreInfo.WaitingThreads = new ulong[storage.NumWaitingThreads];
            for (int index = 0; (long)index < (long)storage.NumWaitingThreads; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf, ref semaphoreInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetEventQueueList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventQueueListX86(int target, uint processId, ref uint numEventQueues, uint[] eventQueueList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetEventQueueList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventQueueListX64(int target, uint processId, ref uint numEventQueues, uint[] eventQueueList);

    public static PS3_TMAPI.SNRESULT GetEventQueueList(int target, uint processID, out uint[] eventQueueIDs)
    {
        eventQueueIDs = (uint[])null;
        uint numEventQueues = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventQueueListX86(target, processID, ref numEventQueues, (uint[])null) : PS3_TMAPI.GetEventQueueListX64(target, processID, ref numEventQueues, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        eventQueueIDs = new uint[numEventQueues];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventQueueListX86(target, processID, ref numEventQueues, eventQueueIDs) : PS3_TMAPI.GetEventQueueListX64(target, processID, ref numEventQueues, eventQueueIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetEventQueueInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventQueueInfoX86(int target, uint processId, uint eventQueueId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetEventQueueInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventQueueInfoX64(int target, uint processId, uint eventQueueId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetEventQueueInfo(int target, uint processID, uint eventQueueID, out PS3_TMAPI.EventQueueInfo eventQueueInfo)
    {
        eventQueueInfo = new PS3_TMAPI.EventQueueInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventQueueInfoX86(target, processID, eventQueueID, ref bufferSize, buffer) : PS3_TMAPI.GetEventQueueInfoX64(target, processID, eventQueueID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventQueueInfoX86(target, processID, eventQueueID, ref bufferSize, num) : PS3_TMAPI.GetEventQueueInfoX64(target, processID, eventQueueID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            PS3_TMAPI.EventQueueInfoPriv eventQueueInfoPriv = (PS3_TMAPI.EventQueueInfoPriv)Marshal.PtrToStructure(num, typeof(PS3_TMAPI.EventQueueInfoPriv));
            eventQueueInfo.ID = eventQueueInfoPriv.Id;
            eventQueueInfo.Attribute = eventQueueInfoPriv.Attribute;
            eventQueueInfo.Key = eventQueueInfoPriv.Key;
            eventQueueInfo.Size = eventQueueInfoPriv.Size;
            eventQueueInfo.NumWaitAllThreads = eventQueueInfoPriv.NumWaitAllThreads;
            eventQueueInfo.NumReadableAllEvQueue = eventQueueInfoPriv.NumReadableAllEvQueue;
            eventQueueInfo.WaitingThreadIDs = new ulong[eventQueueInfoPriv.NumWaitingThreads];
            IntPtr unmanagedBuf1 = eventQueueInfoPriv.WaitingThreadIds;
            for (int index = 0; (long)index < (long)eventQueueInfoPriv.NumWaitingThreads; ++index)
                unmanagedBuf1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf1, ref eventQueueInfo.WaitingThreadIDs[index]);
            eventQueueInfo.QueueEntries = new PS3_TMAPI.SystemEvent[eventQueueInfoPriv.NumReadableEvQueue];
            IntPtr unmanagedBuf2 = eventQueueInfoPriv.QueueEntries;
            for (int index = 0; (long)index < (long)eventQueueInfoPriv.NumReadableEvQueue; ++index)
                unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.SystemEvent>(unmanagedBuf2, ref eventQueueInfo.QueueEntries[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetEventFlagList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventFlagListX86(int target, uint processId, ref uint numEventFlags, uint[] eventFlagList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetEventFlagList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventFlagListX64(int target, uint processId, ref uint numEventFlags, uint[] eventFlagList);

    public static PS3_TMAPI.SNRESULT GetEventFlagList(int target, uint processID, out uint[] eventFlagIDs)
    {
        eventFlagIDs = (uint[])null;
        uint numEventFlags = 0U;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventFlagListX86(target, processID, ref numEventFlags, (uint[])null) : PS3_TMAPI.GetEventFlagListX64(target, processID, ref numEventFlags, (uint[])null);
        if (PS3_TMAPI.FAILED(res))
            return res;
        eventFlagIDs = new uint[numEventFlags];
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventFlagListX86(target, processID, ref numEventFlags, eventFlagIDs) : PS3_TMAPI.GetEventFlagListX64(target, processID, ref numEventFlags, eventFlagIDs);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetEventFlagInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventFlagInfoX86(int target, uint processId, uint eventFlagId, ref uint bufferSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetEventFlagInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetEventFlagInfoX64(int target, uint processId, uint eventFlagId, ref uint bufferSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT GetEventFlagInfo(int target, uint processID, uint eventFlagID, out PS3_TMAPI.EventFlagInfo eventFlagInfo)
    {
        eventFlagInfo = new PS3_TMAPI.EventFlagInfo();
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventFlagInfoX86(target, processID, eventFlagID, ref bufferSize, buffer) : PS3_TMAPI.GetEventFlagInfoX64(target, processID, eventFlagID, ref bufferSize, buffer);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetEventFlagInfoX86(target, processID, eventFlagID, ref bufferSize, num) : PS3_TMAPI.GetEventFlagInfoX64(target, processID, eventFlagID, ref bufferSize, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf1 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.EventFlagAttr>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num, ref eventFlagInfo.ID), ref eventFlagInfo.Attribute), ref eventFlagInfo.BitPattern);
            uint storage = 0U;
            IntPtr unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf1, ref storage), ref eventFlagInfo.NumWaitAllThreads);
            eventFlagInfo.WaitingThreads = new PS3_TMAPI.EventFlagWaitThread[storage];
            for (int index = 0; (long)index < (long)storage; ++index)
                unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.EventFlagWaitThread>(unmanagedBuf2, ref eventFlagInfo.WaitingThreads[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PickTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT PickTargetX86(IntPtr hWndOwner, out int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PickTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT PickTargetX64(IntPtr hWndOwner, out int target);

    public static PS3_TMAPI.SNRESULT PickTarget(IntPtr hWndOwner, out int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.PickTargetX64(hWndOwner, out target);
        else
            return PS3_TMAPI.PickTargetX86(hWndOwner, out target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3EnableAutoStatusUpdate", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnableAutoStatusUpdateX86(int target, uint enabled, out uint previousState);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3EnableAutoStatusUpdate", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnableAutoStatusUpdateX64(int target, uint enabled, out uint previousState);

    public static PS3_TMAPI.SNRESULT EnableAutoStatusUpdate(int target, bool bEnabled, out bool bPreviousState)
    {
        uint enabled = bEnabled ? 1U : 0U;
        uint previousState;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.EnableAutoStatusUpdateX86(target, enabled, out previousState) : PS3_TMAPI.EnableAutoStatusUpdateX64(target, enabled, out previousState);
        bPreviousState = (int)previousState != 0;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetPowerStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetPowerStatusX86(int target, out PS3_TMAPI.PowerStatus status);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetPowerStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetPowerStatusX64(int target, out PS3_TMAPI.PowerStatus status);

    public static PS3_TMAPI.SNRESULT GetPowerStatus(int target, out PS3_TMAPI.PowerStatus status)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetPowerStatusX64(target, out status);
        else
            return PS3_TMAPI.GetPowerStatusX86(target, out status);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PowerOn", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT PowerOnX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PowerOn", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT PowerOnX64(int target);

    public static PS3_TMAPI.SNRESULT PowerOn(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.PowerOnX64(target);
        else
            return PS3_TMAPI.PowerOnX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PowerOff", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT PowerOffX86(int target, uint force);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PowerOff", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT PowerOffX64(int target, uint force);

    public static PS3_TMAPI.SNRESULT PowerOff(int target, bool bForce)
    {
        uint force = bForce ? 1U : 0U;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.PowerOffX64(target, force);
        else
            return PS3_TMAPI.PowerOffX86(target, force);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetUserMemoryStats", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetUserMemoryStatsX86(int target, uint processId, out PS3_TMAPI.UserMemoryStats memoryStats);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetUserMemoryStats", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetUserMemoryStatsX64(int target, uint processId, out PS3_TMAPI.UserMemoryStats memoryStats);

    public static PS3_TMAPI.SNRESULT GetUserMemoryStats(int target, uint processID, out PS3_TMAPI.UserMemoryStats memoryStats)
    {
        memoryStats = new PS3_TMAPI.UserMemoryStats();
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetUserMemoryStatsX64(target, processID, out memoryStats);
        else
            return PS3_TMAPI.GetUserMemoryStatsX86(target, processID, out memoryStats);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetDefaultLoadPriority", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDefaultLoadPriorityX86(int target, uint priority);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetDefaultLoadPriority", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDefaultLoadPriorityX64(int target, uint priority);

    public static PS3_TMAPI.SNRESULT SetDefaultLoadPriority(int target, uint priority)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetDefaultLoadPriorityX64(target, priority);
        else
            return PS3_TMAPI.SetDefaultLoadPriorityX86(target, priority);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetDefaultLoadPriority", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDefaultLoadPriorityX86(int target, out uint priority);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetDefaultLoadPriority", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDefaultLoadPriorityX64(int target, out uint priority);

    public static PS3_TMAPI.SNRESULT GetDefaultLoadPriority(int target, out uint priority)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetDefaultLoadPriorityX64(target, out priority);
        else
            return PS3_TMAPI.GetDefaultLoadPriorityX86(target, out priority);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetGamePortIPAddrData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetGamePortIPAddrDataX86(int target, string deviceName, out PS3_TMAPI.GamePortIPAddressData ipAddressData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetGamePortIPAddrData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetGamePortIPAddrDataX64(int target, string deviceName, out PS3_TMAPI.GamePortIPAddressData ipAddressData);

    public static PS3_TMAPI.SNRESULT GetGamePortIPAddrData(int target, string deviceName, out PS3_TMAPI.GamePortIPAddressData ipAddressData)
    {
        ipAddressData = new PS3_TMAPI.GamePortIPAddressData();
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetGamePortIPAddrDataX64(target, deviceName, out ipAddressData);
        else
            return PS3_TMAPI.GetGamePortIPAddrDataX86(target, deviceName, out ipAddressData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetGamePortDebugIPAddrData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetGamePortDebugIPAddrDataX86(int target, string deviceName, out PS3_TMAPI.GamePortIPAddressData data);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetGamePortDebugIPAddrData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetGamePortDebugIPAddrDataX64(int target, string deviceName, out PS3_TMAPI.GamePortIPAddressData data);

    public static PS3_TMAPI.SNRESULT GetGamePortDebugIPAddrData(int target, string deviceName, out PS3_TMAPI.GamePortIPAddressData ipAddressData)
    {
        ipAddressData = new PS3_TMAPI.GamePortIPAddressData();
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetGamePortDebugIPAddrDataX64(target, deviceName, out ipAddressData);
        else
            return PS3_TMAPI.GetGamePortDebugIPAddrDataX86(target, deviceName, out ipAddressData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetDABR", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDABRX86(int target, uint processId, ulong address);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetDABR", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetDABRX64(int target, uint processId, ulong address);

    public static PS3_TMAPI.SNRESULT SetDABR(int target, uint processID, ulong address)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetDABRX64(target, processID, address);
        else
            return PS3_TMAPI.SetDABRX86(target, processID, address);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetDABR", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDABRX86(int target, uint processId, out ulong address);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetDABR", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDABRX64(int target, uint processId, out ulong address);

    public static PS3_TMAPI.SNRESULT GetDABR(int target, uint processID, out ulong address)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.GetDABRX64(target, processID, out address);
        else
            return PS3_TMAPI.GetDABRX86(target, processID, out address);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetRSXProfilingFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetRSXProfilingFlagsX86(int target, ulong rsxFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetRSXProfilingFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetRSXProfilingFlagsX64(int target, ulong rsxFlags);

    public static PS3_TMAPI.SNRESULT SetRSXProfilingFlags(int target, PS3_TMAPI.RSXProfilingFlag rsxFlags)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetRSXProfilingFlagsX64(target, (ulong)rsxFlags);
        else
            return PS3_TMAPI.SetRSXProfilingFlagsX86(target, (ulong)rsxFlags);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetRSXProfilingFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetRSXProfilingFlagsX86(int target, out ulong rsxFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetRSXProfilingFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetRSXProfilingFlagsX64(int target, out ulong rsxFlags);

    public static PS3_TMAPI.SNRESULT GetRSXProfilingFlags(int target, out PS3_TMAPI.RSXProfilingFlag rsxFlags)
    {
        ulong rsxFlags1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetRSXProfilingFlagsX86(target, out rsxFlags1) : PS3_TMAPI.GetRSXProfilingFlagsX64(target, out rsxFlags1);
        rsxFlags = (PS3_TMAPI.RSXProfilingFlag)rsxFlags1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetCustomParamSFOMappingDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetCustomParamSFOMappingDirectoryX86(int target, string paramSfoDir);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetCustomParamSFOMappingDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetCustomParamSFOMappingDirectoryX64(int target, string paramSfoDir);

    public static PS3_TMAPI.SNRESULT SetCustomParamSFOMappingDirectory(int target, string paramSFODir)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetCustomParamSFOMappingDirectoryX64(target, paramSFODir);
        else
            return PS3_TMAPI.SetCustomParamSFOMappingDirectoryX86(target, paramSFODir);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3EnableXMBSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnableXMBSettingsX86(int target, int enable);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3EnableXMBSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnableXMBSettingsX64(int target, int enable);

    public static PS3_TMAPI.SNRESULT EnableXMBSettings(int target, bool bEnable)
    {
        int enable = bEnable ? 1 : 0;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.EnableXMBSettingsX64(target, enable);
        else
            return PS3_TMAPI.EnableXMBSettingsX86(target, enable);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetXMBSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetXMBSettingsX86(int target, IntPtr buffer, ref uint bufferSize, bool bUpdateCache);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetXMBSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetXMBSettingsX64(int target, IntPtr buffer, ref uint bufferSize, bool bUpdateCache);

    public static PS3_TMAPI.SNRESULT GetXMBSettings(int target, out string xmbSettings, bool bUpdateCache)
    {
        xmbSettings = (string)null;
        uint bufferSize = 0U;
        IntPtr buffer = IntPtr.Zero;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetXMBSettingsX86(target, buffer, ref bufferSize, bUpdateCache) : PS3_TMAPI.GetXMBSettingsX64(target, buffer, ref bufferSize, bUpdateCache);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)bufferSize);
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetXMBSettingsX86(target, num, ref bufferSize, bUpdateCache) : PS3_TMAPI.GetXMBSettingsX64(target, num, ref bufferSize, bUpdateCache);
        if (PS3_TMAPI.SUCCEEDED(res2))
            xmbSettings = Marshal.PtrToStringAnsi(num);
        Marshal.FreeHGlobal(num);
        return res2;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetXMBSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetXMBSettingsX86(int target, string xmbSettings, bool bUpdateCache);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetXMBSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetXMBSettingsX64(int target, string xmbSettings, bool bUpdateCache);

    public static PS3_TMAPI.SNRESULT SetXMBSettings(int target, string xmbSettings, bool bUpdateCache)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.SetXMBSettingsX86(target, xmbSettings, bUpdateCache) : PS3_TMAPI.SetXMBSettingsX64(target, xmbSettings, bUpdateCache);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3FootswitchControl", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FootswitchControlX86(int target, uint enabled);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3FootswitchControl", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FootswitchControlX64(int target, uint enabled);

    public static PS3_TMAPI.SNRESULT FootswitchControl(int target, bool bEnabled)
    {
        uint enabled = bEnabled ? 1U : 0U;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.FootswitchControlX64(target, enabled);
        else
            return PS3_TMAPI.FootswitchControlX86(target, enabled);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3TriggerCoreDump", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT TriggerCoreDumpX86(int target, uint processId, ulong userData1, ulong userData2, ulong userData3);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3TriggerCoreDump", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT TriggerCoreDumpX64(int target, uint processId, ulong userData1, ulong userData2, ulong userData3);

    public static PS3_TMAPI.SNRESULT TriggerCoreDump(int target, uint processID, ulong userData1, ulong userData2, ulong userData3)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.TriggerCoreDumpX64(target, processID, userData1, userData2, userData3);
        else
            return PS3_TMAPI.TriggerCoreDumpX86(target, processID, userData1, userData2, userData3);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetCoreDumpFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetCoreDumpFlagsX86(int target, out ulong flags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetCoreDumpFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetCoreDumpFlagsX64(int target, out ulong flags);

    public static PS3_TMAPI.SNRESULT GetCoreDumpFlags(int target, out PS3_TMAPI.CoreDumpFlag flags)
    {
        ulong flags1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetCoreDumpFlagsX86(target, out flags1) : PS3_TMAPI.GetCoreDumpFlagsX64(target, out flags1);
        flags = (PS3_TMAPI.CoreDumpFlag)flags1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetCoreDumpFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetCoreDumpFlagsX86(int tarSet, ulong flags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetCoreDumpFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetCoreDumpFlagsX64(int tarSet, ulong flags);

    public static PS3_TMAPI.SNRESULT SetCoreDumpFlags(int tarSet, PS3_TMAPI.CoreDumpFlag flags)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetCoreDumpFlagsX64(tarSet, (ulong)flags);
        else
            return PS3_TMAPI.SetCoreDumpFlagsX86(tarSet, (ulong)flags);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessAttachX86(int target, uint unitId, uint processId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessAttachX64(int target, uint unitId, uint processId);

    public static PS3_TMAPI.SNRESULT ProcessAttach(int target, PS3_TMAPI.UnitType unit, uint processID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ProcessAttachX64(target, (uint)unit, processID);
        else
            return PS3_TMAPI.ProcessAttachX86(target, (uint)unit, processID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3FlashTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FlashTargetX86(int target, string updaterToolPath, string flashImagePath);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3FlashTarget", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FlashTargetX64(int target, string updaterToolPath, string flashImagePath);

    public static PS3_TMAPI.SNRESULT FlashTarget(int target, string updaterToolPath, string flashImagePath)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.FlashTargetX64(target, updaterToolPath, flashImagePath);
        else
            return PS3_TMAPI.FlashTargetX86(target, updaterToolPath, flashImagePath);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMacAddress", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMacAddressX86(int target, out IntPtr stringPtr);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMacAddress", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMacAddressX64(int target, out IntPtr stringPtr);

    public static PS3_TMAPI.SNRESULT GetMACAddress(int target, out string macAddress)
    {
        IntPtr stringPtr;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMacAddressX86(target, out stringPtr) : PS3_TMAPI.GetMacAddressX64(target, out stringPtr);
        macAddress = Marshal.PtrToStringAnsi(stringPtr);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessScatteredSetMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessScatteredSetMemoryX86(int target, uint processId, uint numWrites, uint writeSize, IntPtr writeData, out uint errorCode, out uint failedAddress);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessScatteredSetMemory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessScatteredSetMemoryX64(int target, uint processId, uint numWrites, uint writeSize, IntPtr writeData, out uint errorCode, out uint failedAddress);

    public static PS3_TMAPI.SNRESULT ProcessScatteredSetMemory(int target, uint processID, PS3_TMAPI.ScatteredWrite[] writeData, out uint errorCode, out uint failedAddress)
    {
        errorCode = 0U;
        failedAddress = 0U;
        if (writeData == null || writeData.Length == 0)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        int length1 = writeData.Length;
        if (writeData[0].Data == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        int length2 = writeData[0].Data.Length;
        IntPtr num1 = Marshal.AllocHGlobal(length1 * (Marshal.SizeOf((object)writeData[0].Address) + length2));
        IntPtr num2 = num1;
        for (int index = 0; index < length1; ++index)
        {
            num2 = PS3_TMAPI.WriteDataToUnmanagedIncPtr<uint>(writeData[index].Address, num2);
            if (writeData[index].Data == null || writeData[index].Data.Length != length2)
            {
                Marshal.FreeHGlobal(num1);
                return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
            }
            else
            {
                Marshal.Copy(writeData[index].Data, 0, num2, writeData[index].Data.Length);
                num2 = new IntPtr(num2.ToInt64() + (long)writeData[index].Data.Length);
            }
        }
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ProcessScatteredSetMemoryX86(target, processID, (uint)length1, (uint)length2, num1, out errorCode, out failedAddress) : PS3_TMAPI.ProcessScatteredSetMemoryX64(target, processID, (uint)length1, (uint)length2, num1, out errorCode, out failedAddress);
        Marshal.FreeHGlobal(num1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMATRanges", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMATRangesX86(int target, uint processId, ref uint rangeCount, IntPtr matRanges);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMATRanges", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMATRangesX64(int target, uint processId, ref uint rangeCount, IntPtr matRanges);

    public static PS3_TMAPI.SNRESULT GetMATRanges(int target, uint processID, out PS3_TMAPI.MATRange[] matRanges)
    {
        matRanges = (PS3_TMAPI.MATRange[])null;
        IntPtr matRanges1 = IntPtr.Zero;
        uint rangeCount = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMATRangesX86(target, processID, ref rangeCount, matRanges1) : PS3_TMAPI.GetMATRangesX64(target, processID, ref rangeCount, matRanges1);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        if ((int)rangeCount == 0)
        {
            matRanges = new PS3_TMAPI.MATRange[0];
            return PS3_TMAPI.SNRESULT.SN_S_OK;
        }
        else
        {
            IntPtr num = Marshal.AllocHGlobal((int)((long)(2 * Marshal.SizeOf(typeof(uint))) * (long)rangeCount));
            PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMATRangesX86(target, processID, ref rangeCount, num) : PS3_TMAPI.GetMATRangesX64(target, processID, ref rangeCount, num);
            if (PS3_TMAPI.FAILED(res2))
            {
                Marshal.FreeHGlobal(num);
                return res2;
            }
            else
            {
                IntPtr unmanagedBuf = num;
                matRanges = new PS3_TMAPI.MATRange[rangeCount];
                for (uint index = 0U; index < rangeCount; ++index)
                    unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref matRanges[index].StartAddress), ref matRanges[index].Size);
                Marshal.FreeHGlobal(num);
                return res2;
            }
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetMATConditions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMATConditionsX86(int target, uint processId, ref uint rangeCount, IntPtr ranges, ref uint bufSize, IntPtr outputBuf);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetMATConditions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetMATConditionsX64(int target, uint processId, ref uint rangeCount, IntPtr ranges, ref uint bufSize, IntPtr outputBuf);

    public static PS3_TMAPI.SNRESULT GetMATConditions(int target, uint processID, ref PS3_TMAPI.MATRange[] matRanges)
    {
        if (matRanges == null || matRanges.Length < 1)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        uint rangeCount = (uint)matRanges.Length;
        IntPtr num1 = Marshal.AllocHGlobal(8 * (int)rangeCount);
        IntPtr unmanagedBuf1 = num1;
        foreach (PS3_TMAPI.MATRange matRange in matRanges)
        {
            IntPtr unmanagedBuf2 = PS3_TMAPI.WriteDataToUnmanagedIncPtr<uint>(matRange.StartAddress, unmanagedBuf1);
            unmanagedBuf1 = PS3_TMAPI.WriteDataToUnmanagedIncPtr<uint>(matRange.Size, unmanagedBuf2);
        }
        uint bufSize = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMATConditionsX86(target, processID, ref rangeCount, num1, ref bufSize, IntPtr.Zero) : PS3_TMAPI.GetMATConditionsX64(target, processID, ref rangeCount, num1, ref bufSize, IntPtr.Zero);
        if (PS3_TMAPI.FAILED(res1))
        {
            Marshal.FreeHGlobal(num1);
            return res1;
        }
        else
        {
            IntPtr num2 = Marshal.AllocHGlobal((int)bufSize);
            PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetMATConditionsX86(target, processID, ref rangeCount, num1, ref bufSize, num2) : PS3_TMAPI.GetMATConditionsX64(target, processID, ref rangeCount, num1, ref bufSize, num2);
            if (PS3_TMAPI.FAILED(res2))
            {
                Marshal.FreeHGlobal(num1);
                Marshal.FreeHGlobal(num2);
                return res2;
            }
            else
            {
                IntPtr unmanagedBuf2 = num2;
                for (int index1 = 0; (long)index1 < (long)rangeCount; ++index1)
                {
                    unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf2, ref matRanges[index1].StartAddress), ref matRanges[index1].Size);
                    uint num3 = matRanges[index1].Size / 4096U;
                    matRanges[index1].PageConditions = new PS3_TMAPI.MATCondition[num3];
                    for (int index2 = 0; (long)index2 < (long)num3; ++index2)
                    {
                        byte storage = (byte)0;
                        unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<byte>(unmanagedBuf2, ref storage);
                        matRanges[index1].PageConditions[index2] = (PS3_TMAPI.MATCondition)storage;
                    }
                    bufSize -= 8U + num3;
                }
                if ((int)bufSize != 0)
                    res2 = PS3_TMAPI.SNRESULT.SN_E_ERROR;
                Marshal.FreeHGlobal(num1);
                Marshal.FreeHGlobal(num2);
                return res2;
            }
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetMATConditions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetMATConditionsX86(int target, uint processId, uint rangeCount, uint bufSize, IntPtr buffer);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetMATConditions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetMATConditionsX64(int target, uint processId, uint rangeCount, uint bufSize, IntPtr buffer);

    public static PS3_TMAPI.SNRESULT SetMATConditions(int target, uint processID, PS3_TMAPI.MATRange[] matRanges)
    {
        if (matRanges == null || matRanges.Length < 1)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        int length = matRanges.Length;
        int num1 = 0;
        foreach (PS3_TMAPI.MATRange matRange in matRanges)
        {
            if (matRange.PageConditions == null)
                return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
            num1 += matRange.PageConditions.Length;
        }
        IntPtr num2 = Marshal.AllocHGlobal(num1 + 2 * length * 4);
        IntPtr unmanagedBuf1 = num2;
        foreach (PS3_TMAPI.MATRange matRange in matRanges)
        {
            IntPtr unmanagedBuf2 = PS3_TMAPI.WriteDataToUnmanagedIncPtr<uint>(matRange.StartAddress, unmanagedBuf1);
            unmanagedBuf1 = PS3_TMAPI.WriteDataToUnmanagedIncPtr<uint>(matRange.Size, unmanagedBuf2);
            foreach (byte storage in matRange.PageConditions)
                PS3_TMAPI.WriteDataToUnmanagedIncPtr<byte>(storage, unmanagedBuf1);
        }
        uint bufSize = 1U;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.SetMATConditionsX86(target, processID, (uint)length, bufSize, num2) : PS3_TMAPI.SetMATConditionsX64(target, processID, (uint)length, bufSize, num2);
        Marshal.FreeHGlobal(num2);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SaveSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SaveSettingsX86();

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SaveSettings", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SaveSettingsX64();

    public static PS3_TMAPI.SNRESULT SaveSettings()
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SaveSettingsX64();
        else
            return PS3_TMAPI.SaveSettingsX86();
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Exit", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ExitX86();

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Exit", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ExitX64();

    public static PS3_TMAPI.SNRESULT Exit()
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ExitX64();
        else
            return PS3_TMAPI.ExitX86();
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ExitEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ExitExX86(uint millisecondTimeout);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ExitEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ExitExX64(uint millisecondTimeout);

    public static PS3_TMAPI.SNRESULT ExitEx(uint millisecondTimeout)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.ExitExX64(millisecondTimeout);
        else
            return PS3_TMAPI.ExitExX86(millisecondTimeout);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterPadPlaybackNotificationHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterPadPlaybackNotificationHandlerX86(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterPadPlaybackNotificationHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterPadPlaybackNotificationHandlerX64(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterPadPlaybackHandler(int target, PS3_TMAPI.PadPlaybackCallback callback, ref object userData)
    {
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterPadPlaybackNotificationHandlerX86(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3_TMAPI.RegisterPadPlaybackNotificationHandlerX64(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userPadPlaybackCallbacks[target] = new PS3_TMAPI.PadPlaybackCallbackAndUserData()
            {
                m_callback = callback,
                m_userData = userData
            };
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UnRegisterPadPlaybackNotificationHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterPadPlaybackHandlerX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UnRegisterPadPlaybackNotificationHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterPadPlaybackHandlerX64(int target);

    public static PS3_TMAPI.SNRESULT UnregisterPadPlaybackHandler(int target)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.UnregisterPadPlaybackHandlerX86(target) : PS3_TMAPI.UnregisterPadPlaybackHandlerX64(target);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userPadPlaybackCallbacks.Remove(target);
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StartPadPlayback", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StartPadPlaybackX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StartPadPlayback", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StartPadPlaybackX64(int target);

    public static PS3_TMAPI.SNRESULT StartPadPlayback(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StartPadPlaybackX64(target);
        else
            return PS3_TMAPI.StartPadPlaybackX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StopPadPlayback", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StopPadPlaybackX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StopPadPlayback", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StopPadPlaybackX64(int target);

    public static PS3_TMAPI.SNRESULT StopPadPlayback(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StopPadPlaybackX64(target);
        else
            return PS3_TMAPI.StopPadPlaybackX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SendPadPlaybackData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SendPadPlaybackDataX86(int target, ref PS3_TMAPI.PadData data);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SendPadPlaybackData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SendPadPlaybackDataX64(int target, ref PS3_TMAPI.PadData data);

    public static PS3_TMAPI.SNRESULT SendPadPlaybackData(int target, PS3_TMAPI.PadData padData)
    {
        if (padData.buttons == null || padData.buttons.Length != 24)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SendPadPlaybackDataX64(target, ref padData);
        else
            return PS3_TMAPI.SendPadPlaybackDataX86(target, ref padData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterPadCaptureHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterPadCaptureHandlerX86(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterPadCaptureHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterPadCaptureHandlerX64(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterPadCaptureHandler(int target, PS3_TMAPI.PadCaptureCallback callback, ref object userData)
    {
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterPadCaptureHandlerX86(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3_TMAPI.RegisterPadCaptureHandlerX64(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userPadCaptureCallbacks[target] = new PS3_TMAPI.PadCaptureCallbackAndUserData()
            {
                m_callback = callback,
                m_userData = userData
            };
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UnRegisterPadCaptureHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterPadCaptureHandlerX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UnRegisterPadCaptureHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterPadCaptureHandlerX64(int target);

    public static PS3_TMAPI.SNRESULT UnregisterPadCaptureHandler(int target)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.UnregisterPadCaptureHandlerX86(target) : PS3_TMAPI.UnregisterPadCaptureHandlerX64(target);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userPadCaptureCallbacks.Remove(target);
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StartPadCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StartPadCaptureX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StartPadCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StartPadCaptureX64(int target);

    public static PS3_TMAPI.SNRESULT StartPadCapture(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StartPadCaptureX64(target);
        else
            return PS3_TMAPI.StartPadCaptureX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StopPadCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StopPadCaptureX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StopPadCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StopPadCaptureX64(int target);

    public static PS3_TMAPI.SNRESULT StopPadCapture(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StopPadCaptureX64(target);
        else
            return PS3_TMAPI.StopPadCaptureX86(target);
    }

    private static void MarshalPadCaptureEvent(int target, uint param, PS3_TMAPI.SNRESULT res, uint length, IntPtr data)
    {
        if ((int)length != 1)
            return;
        PS3_TMAPI.PadData[] padData = new PS3_TMAPI.PadData[1];
        padData[0].buttons = new short[24];
        PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.PadData>(data, ref padData[0]);
        PS3_TMAPI.ms_userPadCaptureCallbacks[target].m_callback(target, res, padData, PS3_TMAPI.ms_userPadCaptureCallbacks[target].m_userData);
    }

    private static void MarshalPadPlaybackEvent(int target, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        if ((int)length != 1)
            return;
        uint storage = 0U;
        PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref storage);
        PS3_TMAPI.ms_userPadPlaybackCallbacks[target].m_callback(target, result, (PS3_TMAPI.PadPlaybackResponse)storage, PS3_TMAPI.ms_userPadPlaybackCallbacks[target].m_userData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetVRAMCaptureFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetVRAMCaptureFlagsX86(int target, out ulong vramFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetVRAMCaptureFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetVRAMCaptureFlagsX64(int target, out ulong vramFlags);

    public static PS3_TMAPI.SNRESULT GetVRAMCaptureFlags(int target, out PS3_TMAPI.VRAMCaptureFlag vramFlags)
    {
        ulong vramFlags1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetVRAMCaptureFlagsX86(target, out vramFlags1) : PS3_TMAPI.GetVRAMCaptureFlagsX64(target, out vramFlags1);
        vramFlags = (PS3_TMAPI.VRAMCaptureFlag)vramFlags1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetVRAMCaptureFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetVRAMCaptureFlagsX86(int target, ulong vramFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetVRAMCaptureFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetVRAMCaptureFlagsX64(int target, ulong vramFlags);

    public static PS3_TMAPI.SNRESULT SetVRAMCaptureFlags(int target, PS3_TMAPI.VRAMCaptureFlag vramFlags)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetVRAMCaptureFlagsX64(target, (ulong)vramFlags);
        else
            return PS3_TMAPI.SetVRAMCaptureFlagsX86(target, (ulong)vramFlags);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3EnableVRAMCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnableVRAMCaptureX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3EnableVRAMCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT EnableVRAMCaptureX864(int target);

    public static PS3_TMAPI.SNRESULT EnableVRAMCapture(int target)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.EnableVRAMCaptureX864(target);
        else
            return PS3_TMAPI.EnableVRAMCaptureX86(target);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetVRAMInformation", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetVRAMInformationX86(int target, uint processId, out PS3_TMAPI.VramInfoPriv primaryVRAMInfo, out PS3_TMAPI.VramInfoPriv secondaryVRAMInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetVRAMInformation", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetVRAMInformationX64(int target, uint processId, out PS3_TMAPI.VramInfoPriv primaryVRAMInfo, out PS3_TMAPI.VramInfoPriv secondaryVRAMInfo);

    public static PS3_TMAPI.SNRESULT GetVRAMInformation(int target, uint processID, out PS3_TMAPI.VRAMInfo primaryVRAMInfo, out PS3_TMAPI.VRAMInfo secondaryVRAMInfo)
    {
        primaryVRAMInfo = (PS3_TMAPI.VRAMInfo)null;
        secondaryVRAMInfo = (PS3_TMAPI.VRAMInfo)null;
        PS3_TMAPI.VramInfoPriv primaryVRAMInfo1 = new PS3_TMAPI.VramInfoPriv();
        PS3_TMAPI.VramInfoPriv secondaryVRAMInfo1 = new PS3_TMAPI.VramInfoPriv();
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetVRAMInformationX86(target, processID, out primaryVRAMInfo1, out secondaryVRAMInfo1) : PS3_TMAPI.GetVRAMInformationX64(target, processID, out primaryVRAMInfo1, out secondaryVRAMInfo1);
        if (PS3_TMAPI.FAILED(res))
            return res;
        primaryVRAMInfo = new PS3_TMAPI.VRAMInfo();
        primaryVRAMInfo.BPAddress = primaryVRAMInfo1.BpAddress;
        primaryVRAMInfo.TopAddressPointer = primaryVRAMInfo1.TopAddressPointer;
        primaryVRAMInfo.Width = primaryVRAMInfo1.Width;
        primaryVRAMInfo.Height = primaryVRAMInfo1.Height;
        primaryVRAMInfo.Pitch = primaryVRAMInfo1.Pitch;
        primaryVRAMInfo.Colour = primaryVRAMInfo1.Colour;
        secondaryVRAMInfo = new PS3_TMAPI.VRAMInfo();
        secondaryVRAMInfo.BPAddress = secondaryVRAMInfo1.BpAddress;
        secondaryVRAMInfo.TopAddressPointer = secondaryVRAMInfo1.TopAddressPointer;
        secondaryVRAMInfo.Width = secondaryVRAMInfo1.Width;
        secondaryVRAMInfo.Height = secondaryVRAMInfo1.Height;
        secondaryVRAMInfo.Pitch = secondaryVRAMInfo1.Pitch;
        secondaryVRAMInfo.Colour = secondaryVRAMInfo1.Colour;
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3VRAMCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT VRAMCaptureX86(int target, uint processId, IntPtr vramInfo, string fileName);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3VRAMCapture", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT VRAMCaptureX64(int target, uint processId, IntPtr vramInfo, string fileName);

    public static PS3_TMAPI.SNRESULT VRAMCapture(int target, uint processID, PS3_TMAPI.VRAMInfo vramInfo, string fileName)
    {
        IntPtr num = IntPtr.Zero;
        if (vramInfo != null)
        {
            PS3_TMAPI.VramInfoPriv vramInfoPriv = new PS3_TMAPI.VramInfoPriv();
            vramInfoPriv.BpAddress = vramInfo.BPAddress;
            vramInfoPriv.TopAddressPointer = vramInfo.TopAddressPointer;
            vramInfoPriv.Width = vramInfo.Width;
            vramInfoPriv.Height = vramInfo.Height;
            vramInfoPriv.Pitch = vramInfo.Pitch;
            vramInfoPriv.Colour = vramInfo.Colour;
            num = Marshal.AllocHGlobal(Marshal.SizeOf((object)vramInfoPriv));
            Marshal.StructureToPtr((object)vramInfoPriv, num, false);
        }
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.VRAMCaptureX86(target, processID, num, fileName) : PS3_TMAPI.VRAMCaptureX64(target, processID, num, fileName);
        if (vramInfo != null)
            Marshal.FreeHGlobal(num);
        return snresult;
    }

    private static void CustomProtocolHandler(int target, PS3_TMAPI.PS3Protocol ps3Protocol, IntPtr unmanagedBuf, uint length, IntPtr userData)
    {
        PS3_TMAPI.PS3ProtocolPriv protocol = new PS3_TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
        PS3_TMAPI.CustomProtocolId key = new PS3_TMAPI.CustomProtocolId(target, protocol);
        PS3_TMAPI.CusProtoCallbackAndUserData callbackAndUserData;
        if (!PS3_TMAPI.ms_userCustomProtoCallbacks.TryGetValue(key, out callbackAndUserData))
            return;
        byte[] numArray = new byte[length];
        Marshal.Copy(unmanagedBuf, numArray, 0, numArray.Length);
        callbackAndUserData.m_callback(target, ps3Protocol, numArray, callbackAndUserData.m_userData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterCustomProtocolEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterCustomProtocolExX86(int target, uint protocol, uint port, string lparDesc, uint priority, out PS3_TMAPI.PS3Protocol ps3Protocol, PS3_TMAPI.CustomProtocolCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterCustomProtocolEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterCustomProtocolExX64(int target, uint protocol, uint port, string lparDesc, uint priority, out PS3_TMAPI.PS3Protocol ps3Protocol, PS3_TMAPI.CustomProtocolCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterCustomProtocol(int target, uint protocol, uint port, string lparDesc, uint priority, out PS3_TMAPI.PS3Protocol ps3Protocol, PS3_TMAPI.CustomProtocolCallback callback, ref object userData)
    {
        ps3Protocol = new PS3_TMAPI.PS3Protocol();
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterCustomProtocolExX86(target, protocol, port, lparDesc, priority, out ps3Protocol, PS3_TMAPI.ms_customProtoCallbackPriv, IntPtr.Zero) : PS3_TMAPI.RegisterCustomProtocolExX64(target, protocol, port, lparDesc, priority, out ps3Protocol, PS3_TMAPI.ms_customProtoCallbackPriv, IntPtr.Zero);
        if (PS3_TMAPI.SUCCEEDED(res))
        {
            PS3_TMAPI.PS3ProtocolPriv protocol1 = new PS3_TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
            PS3_TMAPI.CustomProtocolId index = new PS3_TMAPI.CustomProtocolId(target, protocol1);
            PS3_TMAPI.ms_userCustomProtoCallbacks[index] = new PS3_TMAPI.CusProtoCallbackAndUserData()
            {
                m_callback = callback,
                m_userData = userData
            };
        }
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UnRegisterCustomProtocol", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterCustomProtocolX86(int target, ref PS3_TMAPI.PS3Protocol protocol);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UnRegisterCustomProtocol", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterCustomProtocolX64(int target, ref PS3_TMAPI.PS3Protocol protocol);

    public static PS3_TMAPI.SNRESULT UnregisterCustomProtocol(int target, PS3_TMAPI.PS3Protocol ps3Protocol)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.UnregisterCustomProtocolX86(target, ref ps3Protocol) : PS3_TMAPI.UnregisterCustomProtocolX64(target, ref ps3Protocol);
        if (PS3_TMAPI.SUCCEEDED(res))
        {
            PS3_TMAPI.PS3ProtocolPriv protocol = new PS3_TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
            PS3_TMAPI.CustomProtocolId key = new PS3_TMAPI.CustomProtocolId(target, protocol);
            PS3_TMAPI.ms_userCustomProtoCallbacks.Remove(key);
        }
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ForceUnRegisterCustomProtocol", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ForceUnregisterCustomProtocolX86(int target, ref PS3_TMAPI.PS3Protocol protocol);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ForceUnRegisterCustomProtocol", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ForceUnregisterCustomProtocolX64(int target, ref PS3_TMAPI.PS3Protocol protocol);

    public static PS3_TMAPI.SNRESULT ForceUnregisterCustomProtocol(int target, PS3_TMAPI.PS3Protocol ps3Protocol)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ForceUnregisterCustomProtocolX86(target, ref ps3Protocol) : PS3_TMAPI.ForceUnregisterCustomProtocolX64(target, ref ps3Protocol);
        if (PS3_TMAPI.SUCCEEDED(res))
        {
            PS3_TMAPI.PS3ProtocolPriv protocol = new PS3_TMAPI.PS3ProtocolPriv(ps3Protocol.Protocol, ps3Protocol.Port);
            PS3_TMAPI.CustomProtocolId key = new PS3_TMAPI.CustomProtocolId(target, protocol);
            PS3_TMAPI.ms_userCustomProtoCallbacks.Remove(key);
        }
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SendCustomProtocolData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SendCustomProtocolDataX86(int target, ref PS3_TMAPI.PS3Protocol protocol, byte[] data, int length);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SendCustomProtocolData", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SendCustomProtocolDataX64(int target, ref PS3_TMAPI.PS3Protocol protocol, byte[] data, int length);

    public static PS3_TMAPI.SNRESULT SendCustomProtocolData(int target, PS3_TMAPI.PS3Protocol ps3Protocol, byte[] data)
    {
        if (data == null || data.Length < 1)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SendCustomProtocolDataX64(target, ref ps3Protocol, data, data.Length);
        else
            return PS3_TMAPI.SendCustomProtocolDataX86(target, ref ps3Protocol, data, data.Length);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetFileServingEventFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetFileServingEventFlagsX86(int target, ulong eventFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetFileServingEventFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetFileServingEventFlagsX64(int target, ulong eventFlags);

    public static PS3_TMAPI.SNRESULT SetFileServingEventFlags(int target, PS3_TMAPI.FileServingEventFlag eventFlags)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetFileServingEventFlagsX64(target, (ulong)eventFlags);
        else
            return PS3_TMAPI.SetFileServingEventFlagsX86(target, (ulong)eventFlags);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetFileServingEventFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetFileServingEventFlagsX86(int target, ref ulong eventFlags);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetFileServingEventFlags", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetFileServingEventFlagsX64(int target, ref ulong eventFlags);

    public static PS3_TMAPI.SNRESULT GetFileServingEventFlags(int target, out PS3_TMAPI.FileServingEventFlag eventFlags)
    {
        ulong eventFlags1 = 0UL;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetFileServingEventFlagsX86(target, ref eventFlags1) : PS3_TMAPI.GetFileServingEventFlagsX64(target, ref eventFlags1);
        eventFlags = (PS3_TMAPI.FileServingEventFlag)eventFlags1;
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetCaseSensitiveFileServing", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetCaseSensitiveFileServingX86(int target, bool bOn, out bool bOldSetting);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetCaseSensitiveFileServing", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetCaseSensitiveFileServingX64(int target, bool bOn, out bool bOldSetting);

    public static PS3_TMAPI.SNRESULT SetCaseSensitiveFileServing(int target, bool bOn, out bool bOldSetting)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetCaseSensitiveFileServingX64(target, bOn, out bOldSetting);
        else
            return PS3_TMAPI.SetCaseSensitiveFileServingX86(target, bOn, out bOldSetting);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterFTPEventHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterFTPEventHandlerX86(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterFTPEventHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterFTPEventHandlerX64(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterFTPEventHandler(int target, PS3_TMAPI.FTPEventCallback callback, ref object userData)
    {
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterFTPEventHandlerX86(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3_TMAPI.RegisterFTPEventHandlerX64(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userFtpCallbacks[target] = new PS3_TMAPI.FtpCallbackAndUserData()
            {
                m_callback = callback,
                m_userData = userData
            };
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CancelFTPEvents", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelFTPEventsX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3CancelFTPEvents", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelFTPEventsX64(int target);

    public static PS3_TMAPI.SNRESULT CancelFTPEvents(int target)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.CancelFTPEventsX86(target) : PS3_TMAPI.CancelFTPEventsX64(target);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userFtpCallbacks.Remove(target);
        return res;
    }

    private static void MarshalFTPEvent(int target, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        PS3_TMAPI.FTPNotification[] ftpNotifications = new PS3_TMAPI.FTPNotification[0];
        if (length > 0U)
        {
            uint num = (uint)((ulong)length / (ulong)Marshal.SizeOf(typeof(PS3_TMAPI.FTPNotification)));
            ftpNotifications = new PS3_TMAPI.FTPNotification[num];
            for (int index = 0; (long)index < (long)num; ++index)
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FTPNotification>(data, ref ftpNotifications[index]);
        }
        PS3_TMAPI.ms_userFtpCallbacks[target].m_callback(target, result, ftpNotifications, PS3_TMAPI.ms_userFtpCallbacks[target].m_userData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterFileTraceHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterFileTraceHandlerX86(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterFileTraceHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterFileTraceHandlerX64(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterFileTraceHandler(int target, PS3_TMAPI.FileTraceCallback callback, ref object userData)
    {
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterFileTraceHandlerX86(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3_TMAPI.RegisterFileTraceHandlerX64(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userFileTraceCallbacks[target] = new PS3_TMAPI.FileTraceCallbackAndUserData()
            {
                m_callback = callback,
                m_userData = userData
            };
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UnRegisterFileTraceHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterFileTraceHandlerX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UnRegisterFileTraceHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UnregisterFileTraceHandlerX64(int target);

    public static PS3_TMAPI.SNRESULT UnregisterFileTraceHandler(int target)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.UnregisterFileTraceHandlerX86(target) : PS3_TMAPI.UnregisterFileTraceHandlerX64(target);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userFileTraceCallbacks.Remove(target);
        return res;
    }

    private static void MarshalFileTraceEvent(int target, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        PS3_TMAPI.FileTraceEvent fileTraceEvent = new PS3_TMAPI.FileTraceEvent();
        IntPtr unmanagedBuf1 = data;
        uint num1 = 44U;
        if (length < num1)
            return;
        IntPtr unmanagedBuf2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf1, ref fileTraceEvent.SerialID);
        int storage1 = 0;
        IntPtr unmanagedBuf3 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<int>(unmanagedBuf2, ref storage1);
        fileTraceEvent.TraceType = (PS3_TMAPI.FileTraceType)storage1;
        int storage2 = 0;
        IntPtr unmanagedBuf4 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<int>(unmanagedBuf3, ref storage2);
        fileTraceEvent.Status = (PS3_TMAPI.FileTraceNotificationStatus)storage2;
        IntPtr unmanagedBuf5 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf4, ref fileTraceEvent.ProcessID), ref fileTraceEvent.ThreadID), ref fileTraceEvent.TimeBaseStartOfTrace), ref fileTraceEvent.TimeBase);
        uint storage3 = 0U;
        IntPtr unmanagedBuf6 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf5, ref storage3);
        uint num2 = num1 + storage3;
        if (length < num2)
            return;
        fileTraceEvent.BackTraceData = new byte[storage3];
        for (int index = 0; (long)index < (long)storage3; ++index)
            unmanagedBuf6 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<byte>(unmanagedBuf6, ref fileTraceEvent.BackTraceData[index]);
        IntPtr num3;
        switch (fileTraceEvent.TraceType)
        {
            case PS3_TMAPI.FileTraceType.GetBlockSize:
            case PS3_TMAPI.FileTraceType.Stat:
            case PS3_TMAPI.FileTraceType.WidgetStat:
            case PS3_TMAPI.FileTraceType.Unlink:
            case PS3_TMAPI.FileTraceType.WidgetUnlink:
            case PS3_TMAPI.FileTraceType.RMDir:
            case PS3_TMAPI.FileTraceType.WidgetRMDir:
                fileTraceEvent.LogData.LogType1 = new PS3_TMAPI.FileTraceLogType1();
                uint storage4 = 0U;
                IntPtr unmanagedBuf7 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf6, ref storage4);
                if (storage4 > 0U)
                {
                    PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf7, ref fileTraceEvent.LogData.LogType1.Path);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.FileTraceType.Rename:
            case PS3_TMAPI.FileTraceType.WidgetRename:
                fileTraceEvent.LogData.LogType2 = new PS3_TMAPI.FileTraceLogType2();
                uint storage5 = 0U;
                IntPtr unmanagedBuf8 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf6, ref storage5);
                uint storage6 = 0U;
                IntPtr unmanagedBuf9 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf8, ref storage6);
                if (storage5 > 0U)
                    unmanagedBuf9 = PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf9, ref fileTraceEvent.LogData.LogType2.Path1);
                if (storage6 > 0U)
                {
                    PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf9, ref fileTraceEvent.LogData.LogType2.Path2);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.FileTraceType.Truncate:
            case PS3_TMAPI.FileTraceType.TruncateNoAlloc:
            case PS3_TMAPI.FileTraceType.Truncate2:
            case PS3_TMAPI.FileTraceType.Truncate2NoInit:
                fileTraceEvent.LogData.LogType3 = new PS3_TMAPI.FileTraceLogType3();
                IntPtr unmanagedBuf10 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType3.Arg);
                uint storage7 = 0U;
                IntPtr unmanagedBuf11 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf10, ref storage7);
                if (storage7 > 0U)
                {
                    PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf11, ref fileTraceEvent.LogData.LogType3.Path);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.FileTraceType.OpenDir:
            case PS3_TMAPI.FileTraceType.WidgetOpenDir:
            case PS3_TMAPI.FileTraceType.CHMod:
            case PS3_TMAPI.FileTraceType.MkDir:
                fileTraceEvent.LogData.LogType4 = new PS3_TMAPI.FileTraceLogType4();
                IntPtr unmanagedBuf12 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType4.Mode);
                uint storage8 = 0U;
                IntPtr unmanagedBuf13 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf12, ref storage8);
                if (storage8 > 0U)
                {
                    PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf13, ref fileTraceEvent.LogData.LogType4.Path);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.FileTraceType.UTime:
                fileTraceEvent.LogData.LogType6 = new PS3_TMAPI.FileTraceLogType6();
                IntPtr unmanagedBuf14 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType6.Arg1), ref fileTraceEvent.LogData.LogType6.Arg2);
                uint storage9 = 0U;
                IntPtr unmanagedBuf15 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf14, ref storage9);
                if (storage9 > 0U)
                {
                    PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf15, ref fileTraceEvent.LogData.LogType6.Path);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.FileTraceType.Open:
            case PS3_TMAPI.FileTraceType.WidgetOpen:
                fileTraceEvent.LogData.LogType8 = new PS3_TMAPI.FileTraceLogType8();
                IntPtr unmanagedBuf16 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType8.ProcessInfo), ref fileTraceEvent.LogData.LogType8.Arg1), ref fileTraceEvent.LogData.LogType8.Arg2), ref fileTraceEvent.LogData.LogType8.Arg3), ref fileTraceEvent.LogData.LogType8.Arg4);
                uint storage10 = 0U;
                IntPtr unmanagedBuf17 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf16, ref storage10);
                uint storage11 = 0U;
                IntPtr unmanagedBuf18 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf17, ref storage11);
                fileTraceEvent.LogData.LogType8.VArg = new byte[storage10];
                for (int index = 0; (long)index < (long)storage10; ++index)
                    unmanagedBuf18 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<byte>(unmanagedBuf18, ref fileTraceEvent.LogData.LogType8.VArg[index]);
                if (storage11 > 0U)
                {
                    PS3_TMAPI.ReadAnsiStringFromUnmanagedIncPtr(unmanagedBuf18, ref fileTraceEvent.LogData.LogType8.Path);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.FileTraceType.Close:
            case PS3_TMAPI.FileTraceType.CloseDir:
            case PS3_TMAPI.FileTraceType.FSync:
            case PS3_TMAPI.FileTraceType.ReadDir:
            case PS3_TMAPI.FileTraceType.FStat:
            case PS3_TMAPI.FileTraceType.FGetBlockSize:
                fileTraceEvent.LogData.LogType9 = new PS3_TMAPI.FileTraceLogType9();
                num3 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType9.ProcessInfo);
                break;
            case PS3_TMAPI.FileTraceType.Read:
            case PS3_TMAPI.FileTraceType.Write:
            case PS3_TMAPI.FileTraceType.GetDirEntries:
                fileTraceEvent.LogData.LogType10 = new PS3_TMAPI.FileTraceLogType10();
                num3 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType10.ProcessInfo), ref fileTraceEvent.LogData.LogType10.Size), ref fileTraceEvent.LogData.LogType10.Address), ref fileTraceEvent.LogData.LogType10.TxSize);
                break;
            case PS3_TMAPI.FileTraceType.ReadOffset:
            case PS3_TMAPI.FileTraceType.WriteOffset:
                fileTraceEvent.LogData.LogType11 = new PS3_TMAPI.FileTraceLogType11();
                PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType11.ProcessInfo), ref fileTraceEvent.LogData.LogType11.Size), ref fileTraceEvent.LogData.LogType11.Address), ref fileTraceEvent.LogData.LogType11.Offset), ref fileTraceEvent.LogData.LogType11.TxSize);
                break;
            case PS3_TMAPI.FileTraceType.FTruncate:
            case PS3_TMAPI.FileTraceType.FTruncateNoAlloc:
                fileTraceEvent.LogData.LogType12 = new PS3_TMAPI.FileTraceLogType12();
                PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType12.ProcessInfo), ref fileTraceEvent.LogData.LogType12.TargetSize);
                break;
            case PS3_TMAPI.FileTraceType.LSeek:
                fileTraceEvent.LogData.LogType13 = new PS3_TMAPI.FileTraceLogType13();
                PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType13.ProcessInfo), ref fileTraceEvent.LogData.LogType13.Size), ref fileTraceEvent.LogData.LogType13.Offset), ref fileTraceEvent.LogData.LogType13.CurPos);
                break;
            case PS3_TMAPI.FileTraceType.SetIOBuffer:
                fileTraceEvent.LogData.LogType14 = new PS3_TMAPI.FileTraceLogType14();
                PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTraceProcessInfo>(unmanagedBuf6, ref fileTraceEvent.LogData.LogType14.ProcessInfo), ref fileTraceEvent.LogData.LogType14.MaxSize), ref fileTraceEvent.LogData.LogType14.Page), ref fileTraceEvent.LogData.LogType14.ContainerID);
                break;
        }
        PS3_TMAPI.ms_userFileTraceCallbacks[target].m_callback(target, result, fileTraceEvent, PS3_TMAPI.ms_userFileTraceCallbacks[target].m_userData);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StartFileTrace", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StartFileTraceX86(int target, uint processId, uint size, string filename);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StartFileTrace", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StartFileTraceX64(int target, uint processId, uint size, string filename);

    public static PS3_TMAPI.SNRESULT StartFileTrace(int target, uint processID, uint size, string filename)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StartFileTraceX64(target, processID, size, filename);
        else
            return PS3_TMAPI.StartFileTraceX86(target, processID, size, filename);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StopFileTrace", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StopFileTraceX86(int target, uint processId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StopFileTrace", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StopFileTraceX64(int target, uint processId);

    public static PS3_TMAPI.SNRESULT StopFileTrace(int target, uint processID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StopFileTraceX64(target, processID);
        else
            return PS3_TMAPI.StopFileTraceX86(target, processID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3InstallPackage", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT InstallPackageX86(int target, string packagePath);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3InstallPackage", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT InstallPackageX64(int target, string packagePath);

    public static PS3_TMAPI.SNRESULT InstallPackage(int target, string packagePath)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.InstallPackageX86(target, packagePath) : PS3_TMAPI.InstallPackageX64(target, packagePath);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UploadFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UploadFileX86(int target, string source, string dest, out uint transactionId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UploadFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UploadFileX64(int target, string source, string dest, out uint transactionId);

    public static PS3_TMAPI.SNRESULT UploadFile(int target, string source, string dest, out uint txID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.UploadFileX64(target, source, dest, out txID);
        else
            return PS3_TMAPI.UploadFileX86(target, source, dest, out txID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetFileTransferList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetFileTransferListX86(int target, ref uint count, IntPtr fileTransferInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetFileTransferList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetFileTransferListX64(int target, ref uint count, IntPtr fileTransferInfo);

    public static PS3_TMAPI.SNRESULT GetFileTransferList(int target, out PS3_TMAPI.FileTransferInfo[] fileTransfers)
    {
        fileTransfers = (PS3_TMAPI.FileTransferInfo[])null;
        IntPtr fileTransferInfo = IntPtr.Zero;
        uint count = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetFileTransferListX86(target, ref count, fileTransferInfo) : PS3_TMAPI.GetFileTransferListX64(target, ref count, fileTransferInfo);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)((long)Marshal.SizeOf(typeof(PS3_TMAPI.FileTransferInfoPriv)) * (long)count));
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetFileTransferListX86(target, ref count, num) : PS3_TMAPI.GetFileTransferListX64(target, ref count, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = num;
            fileTransfers = new PS3_TMAPI.FileTransferInfo[count];
            for (uint index = 0U; index < count; ++index)
            {
                PS3_TMAPI.FileTransferInfoPriv storage = new PS3_TMAPI.FileTransferInfoPriv();
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.FileTransferInfoPriv>(unmanagedBuf, ref storage);
                fileTransfers[index] = new PS3_TMAPI.FileTransferInfo();
                fileTransfers[index].TransferID = storage.TransferId;
                fileTransfers[index].Status = (PS3_TMAPI.FileTransferStatus)storage.Status;
                fileTransfers[index].SourcePath = storage.SourcePath;
                fileTransfers[index].DestinationPath = storage.DestinationPath;
                fileTransfers[index].Size = storage.Size;
                fileTransfers[index].BytesRead = storage.BytesRead;
            }
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetFileTransferInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetFileTransferInfoX86(int target, uint txId, out PS3_TMAPI.FileTransferInfoPriv fileTransferInfo);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetFileTransferInfo", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetFileTransferInfoX64(int target, uint txId, out PS3_TMAPI.FileTransferInfoPriv fileTransferInfo);

    public static PS3_TMAPI.SNRESULT GetFileTransferInfo(int target, uint txID, out PS3_TMAPI.FileTransferInfo fileTransferInfo)
    {
        fileTransferInfo = new PS3_TMAPI.FileTransferInfo();
        PS3_TMAPI.FileTransferInfoPriv fileTransferInfo1 = new PS3_TMAPI.FileTransferInfoPriv();
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetFileTransferInfoX86(target, txID, out fileTransferInfo1) : PS3_TMAPI.GetFileTransferInfoX64(target, txID, out fileTransferInfo1);
        if (PS3_TMAPI.FAILED(res))
            return res;
        fileTransferInfo.TransferID = fileTransferInfo1.TransferId;
        fileTransferInfo.Status = (PS3_TMAPI.FileTransferStatus)fileTransferInfo1.Status;
        fileTransferInfo.SourcePath = fileTransferInfo1.SourcePath;
        fileTransferInfo.DestinationPath = fileTransferInfo1.DestinationPath;
        fileTransferInfo.Size = fileTransferInfo1.Size;
        fileTransferInfo.BytesRead = fileTransferInfo1.BytesRead;
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CancelFileTransfer", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelFileTransferX86(int target, uint txID);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3CancelFileTransfer", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelFileTransferX64(int target, uint txID);

    public static PS3_TMAPI.SNRESULT CancelFileTransfer(int target, uint txID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.CancelFileTransferX64(target, txID);
        else
            return PS3_TMAPI.CancelFileTransferX86(target, txID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RetryFileTransfer", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RetryFileTransferX86(int target, uint txID, bool bForce);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RetryFileTransfer", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RetryFileTransferX64(int target, uint txID, bool bForce);

    public static PS3_TMAPI.SNRESULT RetryFileTransfer(int target, uint txID, bool bForce)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.RetryFileTransferX64(target, txID, bForce);
        else
            return PS3_TMAPI.RetryFileTransferX86(target, txID, bForce);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RemoveTransferItemsByStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RemoveTransferItemsByStatusX86(int target, uint filter);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RemoveTransferItemsByStatus", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SNPS3RemoveTransferItemsByStatusX64(int target, uint filter);

    public static PS3_TMAPI.SNRESULT RemoveTransferItemsByStatus(int target, uint filter)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SNPS3RemoveTransferItemsByStatusX64(target, filter);
        else
            return PS3_TMAPI.RemoveTransferItemsByStatusX86(target, filter);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetDirectoryList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDirectoryListX86(int target, string directory, ref uint numDirEntries, IntPtr dirEntryList);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetDirectoryList", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDirectoryListX64(int target, string directory, ref uint numDirEntries, IntPtr dirEntryList);

    public static PS3_TMAPI.SNRESULT GetDirectoryList(int target, string directory, out PS3_TMAPI.DirEntry[] dirEntries)
    {
        IntPtr dirEntryList = IntPtr.Zero;
        dirEntries = (PS3_TMAPI.DirEntry[])null;
        uint numDirEntries = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetDirectoryListX86(target, directory, ref numDirEntries, dirEntryList) : PS3_TMAPI.GetDirectoryListX64(target, directory, ref numDirEntries, dirEntryList);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)numDirEntries * Marshal.SizeOf(typeof(PS3_TMAPI.DirEntry)));
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetDirectoryListX86(target, directory, ref numDirEntries, num) : PS3_TMAPI.GetDirectoryListX64(target, directory, ref numDirEntries, num);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = num;
            dirEntries = new PS3_TMAPI.DirEntry[numDirEntries];
            for (int index = 0; (long)index < (long)numDirEntries; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.DirEntry>(unmanagedBuf, ref dirEntries[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetDirectoryListEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDirectoryListExX86(int target, string directory, ref uint numDirEntries, IntPtr dirEntryListEx, ref PS3_TMAPI.TargetTimezone timeZone);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetDirectoryListEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetDirectoryListExX64(int target, string directory, ref uint numDirEntries, IntPtr dirEntryListEx, ref PS3_TMAPI.TargetTimezone timeZone);

    public static PS3_TMAPI.SNRESULT GetDirectoryListEx(int target, string directory, out PS3_TMAPI.DirEntryEx[] dirEntries, out PS3_TMAPI.TargetTimezone timeZone)
    {
        IntPtr dirEntryListEx = IntPtr.Zero;
        dirEntries = (PS3_TMAPI.DirEntryEx[])null;
        timeZone = new PS3_TMAPI.TargetTimezone();
        uint numDirEntries = 0U;
        PS3_TMAPI.SNRESULT res1 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetDirectoryListExX86(target, directory, ref numDirEntries, dirEntryListEx, ref timeZone) : PS3_TMAPI.GetDirectoryListExX64(target, directory, ref numDirEntries, dirEntryListEx, ref timeZone);
        if (PS3_TMAPI.FAILED(res1))
            return res1;
        IntPtr num = Marshal.AllocHGlobal((int)numDirEntries * Marshal.SizeOf(typeof(PS3_TMAPI.DirEntryEx)));
        PS3_TMAPI.SNRESULT res2 = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetDirectoryListExX86(target, directory, ref numDirEntries, num, ref timeZone) : PS3_TMAPI.GetDirectoryListExX64(target, directory, ref numDirEntries, num, ref timeZone);
        if (PS3_TMAPI.FAILED(res2))
        {
            Marshal.FreeHGlobal(num);
            return res2;
        }
        else
        {
            IntPtr unmanagedBuf = num;
            dirEntries = new PS3_TMAPI.DirEntryEx[numDirEntries];
            for (int index = 0; (long)index < (long)numDirEntries; ++index)
                unmanagedBuf = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<PS3_TMAPI.DirEntryEx>(unmanagedBuf, ref dirEntries[index]);
            Marshal.FreeHGlobal(num);
            return res2;
        }
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3MakeDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT MakeDirectoryX86(int target, string directory, uint mode);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3MakeDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT MakeDirectoryX64(int target, string directory, uint mode);

    public static PS3_TMAPI.SNRESULT MakeDirectory(int target, string directory, uint mode)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.MakeDirectoryX64(target, directory, mode);
        else
            return PS3_TMAPI.MakeDirectoryX86(target, directory, mode);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Delete", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DeleteFileX86(int target, string path);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Delete", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DeleteFileX64(int target, string path);

    public static PS3_TMAPI.SNRESULT DeleteFile(int target, string path)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.DeleteFileX64(target, path);
        else
            return PS3_TMAPI.DeleteFileX86(target, path);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3DeleteEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DeleteFileExX86(int target, string path, uint msTimeout);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3DeleteEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DeleteFileExX64(int target, string path, uint msTimeout);

    public static PS3_TMAPI.SNRESULT DeleteFileEx(int target, string path, uint msTimeout)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.DeleteFileExX64(target, path, msTimeout);
        else
            return PS3_TMAPI.DeleteFileExX86(target, path, msTimeout);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Rename", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RenameFileX86(int target, string source, string dest);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Rename", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RenameFileX64(int target, string source, string dest);

    public static PS3_TMAPI.SNRESULT RenameFile(int target, string source, string dest)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.RenameFileX64(target, source, dest);
        else
            return PS3_TMAPI.RenameFileX86(target, source, dest);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3DownloadFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DownloadFileX86(int target, string source, string dest, out uint transactionId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3DownloadFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DownloadFileX64(int target, string source, string dest, out uint transactionId);

    public static PS3_TMAPI.SNRESULT DownloadFile(int target, string source, string dest, out uint txID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.DownloadFileX64(target, source, dest, out txID);
        else
            return PS3_TMAPI.DownloadFileX86(target, source, dest, out txID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3DownloadDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DownloadDirectoryX86(int target, string source, string dest, out uint lastTransactionId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3DownloadDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT DownloadDirectoryX64(int target, string source, string dest, out uint lastTransactionId);

    public static PS3_TMAPI.SNRESULT DownloadDirectory(int target, string source, string dest, out uint lastTxID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.DownloadDirectoryX64(target, source, dest, out lastTxID);
        else
            return PS3_TMAPI.DownloadDirectoryX86(target, source, dest, out lastTxID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UploadDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UploadDirectoryX86(int target, string source, string dest, out uint lastTransactionId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UploadDirectory", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UploadDirectoryX64(int target, string source, string dest, out uint lastTransactionId);

    public static PS3_TMAPI.SNRESULT UploadDirectory(int target, string source, string dest, out uint lastTxID)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.UploadDirectoryX64(target, source, dest, out lastTxID);
        else
            return PS3_TMAPI.UploadDirectoryX86(target, source, dest, out lastTxID);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StatTargetFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StatTargetFileX86(int target, string file, out PS3_TMAPI.DirEntry dirEntry);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StatTargetFile", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StatTargetFileX64(int target, string file, out PS3_TMAPI.DirEntry dirEntry);

    public static PS3_TMAPI.SNRESULT StatTargetFile(int target, string file, out PS3_TMAPI.DirEntry dirEntry)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StatTargetFileX64(target, file, out dirEntry);
        else
            return PS3_TMAPI.StatTargetFileX86(target, file, out dirEntry);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3StatTargetFileEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StatTargetFileExX86(int target, string file, out PS3_TMAPI.DirEntryEx dirEntry, out PS3_TMAPI.TargetTimezone timeZone);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3StatTargetFileEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT StatTargetFileExX64(int target, string file, out PS3_TMAPI.DirEntryEx dirEntry, out PS3_TMAPI.TargetTimezone timeZone);

    public static PS3_TMAPI.SNRESULT StatTargetFileEx(int target, string file, out PS3_TMAPI.DirEntryEx dirEntryEx, out PS3_TMAPI.TargetTimezone timeZone)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.StatTargetFileExX64(target, file, out dirEntryEx, out timeZone);
        else
            return PS3_TMAPI.StatTargetFileExX86(target, file, out dirEntryEx, out timeZone);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CHMod", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CHModX86(int target, string filePath, uint mode);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3CHMod", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CHModX64(int target, string filePath, uint mode);

    public static PS3_TMAPI.SNRESULT ChMod(int target, string filePath, PS3_TMAPI.ChModFilePermission mode)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.CHModX64(target, filePath, (uint)mode);
        else
            return PS3_TMAPI.CHModX86(target, filePath, (uint)mode);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetFileTime", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetFileTimeX86(int target, string filePath, ulong accessTime, ulong modifiedTime);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetFileTime", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetFileTimeX64(int target, string filePath, ulong accessTime, ulong modifiedTime);

    public static PS3_TMAPI.SNRESULT SetFileTime(int target, string filePath, ulong accessTime, ulong modifiedTime)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.SetFileTimeX64(target, filePath, accessTime, modifiedTime);
        else
            return PS3_TMAPI.SetFileTimeX86(target, filePath, accessTime, modifiedTime);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3InstallGameEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT InstallGameExX86(int target, string paramSfoPath, out IntPtr titleId, out IntPtr targetPath, out uint txId);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3InstallGameEx", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT InstallGameExX64(int target, string paramSfoPath, out IntPtr titleId, out IntPtr targetPath, out uint txId);

    public static PS3_TMAPI.SNRESULT InstallGameEx(int target, string paramSFOPath, out string titleID, out string targetPath, out uint txID)
    {
        IntPtr titleId;
        IntPtr targetPath1;
        PS3_TMAPI.SNRESULT snresult = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.InstallGameExX86(target, paramSFOPath, out titleId, out targetPath1, out txID) : PS3_TMAPI.InstallGameExX64(target, paramSFOPath, out titleId, out targetPath1, out txID);
        titleID = Marshal.PtrToStringAnsi(titleId);
        targetPath = Marshal.PtrToStringAnsi(targetPath1);
        return snresult;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3FormatHDD", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FormatHDDX86(int target, uint initRegistry);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3FormatHDD", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FormatHDDX64(int target, uint initRegistry);

    public static PS3_TMAPI.SNRESULT FormatHDD(int target, uint initRegistry)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.FormatHDDX64(target, initRegistry);
        else
            return PS3_TMAPI.FormatHDDX86(target, initRegistry);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3UninstallGame", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UninstallGameX86(int target, string gameDirectory);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3UninstallGame", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT UninstallGameX64(int target, string gameDirectory);

    public static PS3_TMAPI.SNRESULT UninstallGame(int target, string gameDirectory)
    {
        if (!PS3_TMAPI.Is32Bit())
            return PS3_TMAPI.UninstallGameX64(target, gameDirectory);
        else
            return PS3_TMAPI.UninstallGameX86(target, gameDirectory);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3WaitForFileTransfer", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT WaitForFileTransferX86(int target, uint txId, out PS3_TMAPI.FileTransferNotificationType notificationType, uint msTimeout);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3WaitForFileTransfer", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT WaitForFileTransferX64(int target, uint txId, out PS3_TMAPI.FileTransferNotificationType notificationType, uint msTimeout);

    public static PS3_TMAPI.SNRESULT WaitForFileTransfer(int target, uint txID, out PS3_TMAPI.FileTransferNotificationType notificationType, uint msTimeout)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.WaitForFileTransferX86(target, txID, out notificationType, msTimeout) : PS3_TMAPI.WaitForFileTransferX64(target, txID, out notificationType, msTimeout);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3FSGetFreeSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FSGetFreeSizeX86(int target, string fsDir, out uint blockSize, out ulong freeBlockCount);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3FSGetFreeSize", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT FSGetFreeSizeX64(int target, string fsDir, out uint blockSize, out ulong freeBlockCount);

    public static PS3_TMAPI.SNRESULT FSGetFreeSize(int target, string fsDir, out uint blockSize, out ulong freeBlockCount)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.FSGetFreeSizeX86(target, fsDir, out blockSize, out freeBlockCount) : PS3_TMAPI.FSGetFreeSizeX64(target, fsDir, out blockSize, out freeBlockCount);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetLogOptions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLogOptionsX86(out PS3_TMAPI.LogCategory category);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetLogOptions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT GetLogOptionsX64(out PS3_TMAPI.LogCategory category);

    public static PS3_TMAPI.SNRESULT GetLogOptions(out PS3_TMAPI.LogCategory category)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.GetLogOptionsX86(out category) : PS3_TMAPI.GetLogOptionsX64(out category);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3SetLogOptions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetLogOptionsX86(PS3_TMAPI.LogCategory category);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3SetLogOptions", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT SetLogOptionsX64(PS3_TMAPI.LogCategory category);

    public static PS3_TMAPI.SNRESULT SetLogOptions(PS3_TMAPI.LogCategory category)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.SetLogOptionsX86(category) : PS3_TMAPI.SetLogOptionsX64(category);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessOfflineFileTrace", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessOfflineFileTraceX86(int target, string path);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessOfflineFileTrace", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT ProcessOfflineFileTraceX64(int target, string path);

    public static PS3_TMAPI.SNRESULT ProcessOfflineFileTrace(int target, string path)
    {
        return PS3_TMAPI.Is32Bit() ? PS3_TMAPI.ProcessOfflineFileTraceX86(target, path) : PS3_TMAPI.ProcessOfflineFileTraceX64(target, path);
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3RegisterTargetEventHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterTargetEventHandlerX86(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3RegisterTargetEventHandler", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT RegisterTargetEventHandlerX64(int target, PS3_TMAPI.HandleEventCallbackPriv callback, IntPtr userData);

    public static PS3_TMAPI.SNRESULT RegisterTargetEventHandler(int target, PS3_TMAPI.TargetEventCallback callback, ref object userData)
    {
        if (callback == null)
            return PS3_TMAPI.SNRESULT.SN_E_BAD_PARAM;
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.RegisterTargetEventHandlerX86(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero) : PS3_TMAPI.RegisterTargetEventHandlerX64(target, PS3_TMAPI.ms_eventHandlerWrapper, IntPtr.Zero);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userTargetCallbacks[target] = new PS3_TMAPI.TargetCallbackAndUserData()
            {
                m_callback = callback,
                m_userData = userData
            };
        return res;
    }

    [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3CancelTargetEvents", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelTargetEventsX86(int target);

    [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3CancelTargetEvents", CallingConvention = CallingConvention.Cdecl)]
    private static extern PS3_TMAPI.SNRESULT CancelTargetEventsX64(int target);

    public static PS3_TMAPI.SNRESULT CancelTargetEvents(int target)
    {
        PS3_TMAPI.SNRESULT res = PS3_TMAPI.Is32Bit() ? PS3_TMAPI.CancelTargetEventsX86(target) : PS3_TMAPI.CancelTargetEventsX64(target);
        if (PS3_TMAPI.SUCCEEDED(res))
            PS3_TMAPI.ms_userTargetCallbacks.Remove(target);
        return res;
    }

    private static void MarshalTargetEvent(int target, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data)
    {
        List<PS3_TMAPI.TargetEvent> list = new List<PS3_TMAPI.TargetEvent>();
        uint num1 = length;
        while (num1 > 0U)
        {
            PS3_TMAPI.TargetEvent targetEvent = new PS3_TMAPI.TargetEvent();
            uint storage = 0U;
            IntPtr num2 = data;
            num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num2, ref storage);
            num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num2, ref targetEvent.TargetID);
            int num3 = Marshal.ReadInt32(num2, 0);
            num2 = new IntPtr(num2.ToInt64() + (long)Marshal.SizeOf((object)num3));
            targetEvent.Type = (PS3_TMAPI.TargetEventType)num3;
            targetEvent.Type.GetType();
            switch (targetEvent.Type)
            {
                case PS3_TMAPI.TargetEventType.UnitStatusChange:
                    targetEvent.EventData = new PS3_TMAPI.TargetEventData();
                    int num4 = Marshal.ReadInt32(num2, 0);
                    num2 = new IntPtr(num2.ToInt64() + (long)Marshal.SizeOf((object)num4));
                    targetEvent.EventData.UnitStatusChangeData.Unit = (PS3_TMAPI.UnitType)num4;
                    int num5 = Marshal.ReadInt32(num2, 0);
                    num2 = new IntPtr(num2.ToInt64() + (long)Marshal.SizeOf((object)num5));
                    targetEvent.EventData.UnitStatusChangeData.Status = (PS3_TMAPI.UnitStatus)num5;
                    break;
                case PS3_TMAPI.TargetEventType.Details:
                    targetEvent.EventData = new PS3_TMAPI.TargetEventData();
                    num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num2, ref targetEvent.EventData.DetailsData.Flags);
                    break;
                case PS3_TMAPI.TargetEventType.ModuleLoad:
                case PS3_TMAPI.TargetEventType.ModuleRunning:
                case PS3_TMAPI.TargetEventType.ModuleStopped:
                    targetEvent.EventData = new PS3_TMAPI.TargetEventData();
                    num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num2, ref targetEvent.EventData.ModuleEventData.Unit);
                    num2 = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(num2, ref targetEvent.EventData.ModuleEventData.ModuleID);
                    break;
                case PS3_TMAPI.TargetEventType.TargetSpecific:
                    targetEvent.TargetSpecific = PS3_TMAPI.MarshalTargetSpecificEvent(storage, num2);
                    break;
            }
            list.Add(targetEvent);
            num1 -= storage;
            data = new IntPtr(data.ToInt64() + (long)storage);
        }
        PS3_TMAPI.ms_userTargetCallbacks[target].m_callback(target, result, list.ToArray(), PS3_TMAPI.ms_userTargetCallbacks[target].m_userData);
    }

    private static PS3_TMAPI.TargetSpecificEvent MarshalTargetSpecificEvent(uint eventSize, IntPtr data)
    {
        PS3_TMAPI.TargetSpecificEvent targetSpecificEvent = new PS3_TMAPI.TargetSpecificEvent();
        PS3_TMAPI.TargetSpecificData targetSpecificData = new PS3_TMAPI.TargetSpecificData();
        uint storage = 0U;
        data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificEvent.CommandID);
        data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificEvent.RequestID);
        data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref storage);
        data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificEvent.ProcessID);
        data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificEvent.Result);
        int num1 = Marshal.ReadInt32(data, 0);
        data = new IntPtr(data.ToInt64() + (long)Marshal.SizeOf((object)num1));
        targetSpecificData.Type = (PS3_TMAPI.TargetSpecificEventType)num1;
        int num2 = 20;
        switch (targetSpecificData.Type)
        {
            case PS3_TMAPI.TargetSpecificEventType.CoreDumpComplete:
                targetSpecificData.CoreDumpComplete = new PS3_TMAPI.CoreDumpComplete();
                targetSpecificData.CoreDumpComplete.Filename = Marshal.PtrToStringAnsi(data);
                break;
            case PS3_TMAPI.TargetSpecificEventType.Footswitch:
                targetSpecificData.Footswitch = new PS3_TMAPI.FootswitchData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.Footswitch.EventSource);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.Footswitch.EventData1);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.Footswitch.EventData2);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.Footswitch.EventData3);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.Footswitch.Reserved);
                break;
            case PS3_TMAPI.TargetSpecificEventType.InstallPackageProgress:
                targetSpecificData.InstallPackageProgress = new PS3_TMAPI.InstallPackageProgress();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.InstallPackageProgress.Percent);
                break;
            case PS3_TMAPI.TargetSpecificEventType.InstallPackagePath:
                targetSpecificData.InstallPackagePath = new PS3_TMAPI.InstallPackagePath();
                targetSpecificData.InstallPackagePath.Path = Marshal.PtrToStringAnsi(data);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PRXLoad:
                targetSpecificData.PRXLoad = new PS3_TMAPI.NotifyPRXLoadData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PRXLoad.PPUThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.PRXLoad.PRXID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PRXLoad.Timestamp);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PRXUnload:
                targetSpecificData.PRXUnload = new PS3_TMAPI.NotifyPRXUnloadData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PRXUnload.PPUThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.PRXUnload.PRXID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PRXUnload.Timestamp);
                break;
            case PS3_TMAPI.TargetSpecificEventType.ProcessCreate:
                targetSpecificData.PPUProcessCreate = new PS3_TMAPI.PPUProcessCreateData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.PPUProcessCreate.ParentProcessID);
                if ((long)storage - (long)num2 - 4L > 0L)
                {
                    targetSpecificData.PPUProcessCreate.Filename = Marshal.PtrToStringAnsi(data);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.TargetSpecificEventType.ProcessExit:
                targetSpecificData.PPUProcessExit = new PS3_TMAPI.PPUProcessExitData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUProcessExit.ExitCode);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PPUExcTrap:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcPrevInt:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcIllInst:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcTextHtabMiss:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcTextSlbMiss:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcDataHtabMiss:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcFloat:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcDataSlbMiss:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcDabrMatch:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcStop:
            case PS3_TMAPI.TargetSpecificEventType.PPUExcStopInit:
                targetSpecificData.PPUException = new PS3_TMAPI.PPUExceptionData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUException.ThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.PPUException.HWThreadNumber);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUException.PC);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUException.SP);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PPUExcAlignment:
                targetSpecificData.PPUAlignmentException = new PS3_TMAPI.PPUAlignmentExceptionData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUAlignmentException.ThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.PPUAlignmentException.HWThreadNumber);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUAlignmentException.DSISR);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUAlignmentException.DAR);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUAlignmentException.PC);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUAlignmentException.SP);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PPUExcDataMAT:
                targetSpecificData.PPUDataMatException = new PS3_TMAPI.PPUDataMatExceptionData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUDataMatException.ThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.PPUDataMatException.HWThreadNumber);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUDataMatException.DSISR);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUDataMatException.DAR);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUDataMatException.PC);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUDataMatException.SP);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PPUThreadCreate:
                targetSpecificData.PPUThreadCreate = new PS3_TMAPI.PPUThreadCreateData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUThreadCreate.ThreadID);
                break;
            case PS3_TMAPI.TargetSpecificEventType.PPUThreadExit:
                targetSpecificData.PPUThreadExit = new PS3_TMAPI.PPUThreadExitData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.PPUThreadExit.ThreadID);
                break;
            case PS3_TMAPI.TargetSpecificEventType.SPUThreadStart:
                targetSpecificData.SPUThreadStart = new PS3_TMAPI.SPUThreadStartData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStart.ThreadGroupID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStart.ThreadID);
                if ((long)storage - (long)num2 - 8L > 0L)
                {
                    targetSpecificData.SPUThreadStart.ElfFilename = Marshal.PtrToStringAnsi(data);
                    break;
                }
                else
                    break;
            case PS3_TMAPI.TargetSpecificEventType.SPUThreadStop:
            case PS3_TMAPI.TargetSpecificEventType.SPUThreadStopInit:
                targetSpecificData.SPUThreadStop = new PS3_TMAPI.SPUThreadStopData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStop.ThreadGroupID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStop.ThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStop.PC);
                int num3 = Marshal.ReadInt32(data, 0);
                data = new IntPtr(data.ToInt64() + (long)Marshal.SizeOf((object)num3));
                targetSpecificData.SPUThreadStop.Reason = (PS3_TMAPI.SPUThreadStopReason)num3;
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStop.SP);
                break;
            case PS3_TMAPI.TargetSpecificEventType.SPUThreadGroupDestroy:
                targetSpecificData.SPUThreadGroupDestroyData = new PS3_TMAPI.SPUThreadGroupDestroyData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadGroupDestroyData.ThreadGroupID);
                break;
            case PS3_TMAPI.TargetSpecificEventType.SPUThreadStopEx:
                targetSpecificData.SPUThreadStopEx = new PS3_TMAPI.SPUThreadStopExData();
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStopEx.ThreadGroupID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStopEx.ThreadID);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStopEx.PC);
                int num4 = Marshal.ReadInt32(data, 0);
                data = new IntPtr(data.ToInt64() + (long)Marshal.SizeOf((object)num4));
                targetSpecificData.SPUThreadStopEx.Reason = (PS3_TMAPI.SPUThreadStopReason)num4;
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<uint>(data, ref targetSpecificData.SPUThreadStopEx.SP);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.SPUThreadStopEx.MFCDSISR);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.SPUThreadStopEx.MFCDSIPR);
                data = PS3_TMAPI.ReadDataFromUnmanagedIncPtr<ulong>(data, ref targetSpecificData.SPUThreadStopEx.MFCDAR);
                break;
        }
        targetSpecificEvent.Data = targetSpecificData;
        return targetSpecificEvent;
    }

    private static void EventHandlerWrapper(int target, PS3_TMAPI.EventType type, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data, IntPtr userData)
    {
        switch (type)
        {
            case PS3_TMAPI.EventType.TTY:
                PS3_TMAPI.MarshalTTYEvent(target, param, result, length, data);
                break;
            case PS3_TMAPI.EventType.Target:
                PS3_TMAPI.MarshalTargetEvent(target, param, result, length, data);
                break;
            case PS3_TMAPI.EventType.FTP:
                PS3_TMAPI.MarshalFTPEvent(target, param, result, length, data);
                break;
            case PS3_TMAPI.EventType.PadCapture:
                PS3_TMAPI.MarshalPadCaptureEvent(target, param, result, length, data);
                break;
            case PS3_TMAPI.EventType.FileTrace:
                PS3_TMAPI.MarshalFileTraceEvent(target, param, result, length, data);
                break;
            case PS3_TMAPI.EventType.PadPlayback:
                PS3_TMAPI.MarshalPadPlaybackEvent(target, param, result, length, data);
                break;
        }
    }

    private static IntPtr WriteDataToUnmanagedIncPtr<T>(T storage, IntPtr unmanagedBuf)
    {
        bool fDeleteOld = false;
        Marshal.StructureToPtr((object)storage, unmanagedBuf, fDeleteOld);
        return new IntPtr(unmanagedBuf.ToInt64() + (long)Marshal.SizeOf((object)storage));
    }

    private static IntPtr ReadDataFromUnmanagedIncPtr<T>(IntPtr unmanagedBuf, ref T storage)
    {
        storage = (T)Marshal.PtrToStructure(unmanagedBuf, typeof(T));
        return new IntPtr(unmanagedBuf.ToInt64() + (long)Marshal.SizeOf((object)storage));
    }

    private static IntPtr ReadAnsiStringFromUnmanagedIncPtr(IntPtr unmanagedBuf, ref string inputString)
    {
        inputString = Marshal.PtrToStringAnsi(unmanagedBuf);
        return new IntPtr(unmanagedBuf.ToInt64() + (long)inputString.Length + 1L);
    }

    public enum SNRESULT
    {
        SN_E_ERROR = -2147483648,
        SN_E_COMMS_EVENT_MISMATCHED_ERR = -39,
        SN_E_CONNECTED = -38,
        SN_E_PROTOCOL_ALREADY_REGISTERED = -37,
        SN_E_COMMAND_CANCELLED = -36,
        SN_E_CONNECT_TO_GAMEPORT_FAILED = -35,
        SN_E_MODULE_NOT_FOUND = -34,
        SN_E_CHECK_TARGET_CONFIGURATION = -33,
        SN_E_LICENSE_ERROR = -32,
        SN_E_LOAD_MODULE_FAILED = -31,
        SN_E_NOT_SUPPORTED_IN_SDK_VERSION = -30,
        SN_E_FILE_ERROR = -29,
        SN_E_BAD_ALIGN = -28,
        SN_E_DEPRECATED = -27,
        SN_E_DATA_TOO_LONG = -26,
        SN_E_INSUFFICIENT_DATA = -25,
        SN_E_EXISTING_CALLBACK = -24,
        SN_E_DECI_ERROR = -23,
        SN_E_BUSY = -22,
        SN_E_BAD_PARAM = -21,
        SN_E_NO_SEL = -20,
        SN_E_NO_TARGETS = -19,
        SN_E_BAD_MEMSPACE = -18,
        SN_E_TARGET_RUNNING = -17,
        SN_E_DLL_NOT_INITIALISED = -15,
        SN_E_TM_VERSION = -14,
        SN_E_NOT_LISTED = -13,
        SN_E_OUT_OF_MEM = -12,
        SN_E_BAD_UNIT = -11,
        SN_E_LOAD_ELF_FAILED = -10,
        SN_E_TARGET_IN_USE = -9,
        SN_E_HOST_NOT_FOUND = -8,
        SN_E_TIMEOUT = -7,
        SN_E_TM_COMMS_ERR = -6,
        SN_E_COMMS_ERR = -5,
        SN_E_NOT_CONNECTED = -4,
        SN_E_BAD_TARGET = -3,
        SN_E_TM_NOT_RUNNING = -2,
        SN_E_NOT_IMPL = -1,
        SN_S_OK = 0,
        SN_S_PENDING = 1,
        SN_S_NO_MSG = 3,
        SN_S_TM_VERSION = 4,
        SN_S_REPLACED = 5,
        SN_S_NO_ACTION = 6,
        SN_S_TARGET_STILL_REGISTERED = 7,
    }

    public enum ConnectStatus
    {
        Connected,
        Connecting,
        NotConnected,
        InUse,
        Unavailable,
    }

    public delegate int EnumerateTargetsCallback(int target);

    public delegate int EnumerateTargetsExCallback(int target, object userData);

    private delegate int EnumerateTargetsExCallbackPriv(int target, IntPtr unused);

    [Flags]
    public enum BootParameter : ulong
    {
        Default = 0UL,
        SystemMode = 17UL,
        ReleaseMode = 1UL,
        DebugMode = 16UL,
        MemSizeConsole = 2UL,
        BluRayEmuOff = 4UL,
        HDDSpeedBluRayEmu = 8UL,
        BluRayEmuUSB = 32UL,
        HostFSTarget = 64UL,
        DualNIC = 128UL,
    }

    [Flags]
    public enum BootParameterMask : ulong
    {
        BootMode = 17UL,
        Memsize = 2UL,
        BlurayEmulation = 4UL,
        HDDSpeed = 8UL,
        BlurayEmuSelect = 32UL,
        HostFS = 64UL,
        NIC = 128UL,
        All = NIC | HostFS | BlurayEmuSelect | HDDSpeed | BlurayEmulation | Memsize | BootMode,
    }

    [Flags]
    public enum ResetParameter : ulong
    {
        Soft = 0UL,
        Hard = 1UL,
        Quick = 2UL,
        ResetEx = 9223372036854775808UL,
    }

    [Flags]
    public enum ResetParameterMask : ulong
    {
        All = 9223372036854775811UL,
    }

    [Flags]
    public enum SystemParameter : ulong
    {
        TargetModel60GB = 281474976710656UL,
        TargetModel20GB = 562949953421312UL,
        ReleaseCheckMode = 140737488355328UL,
    }

    [Flags]
    public enum SystemParameterMask : ulong
    {
        TargetModel = 71776119061217280UL,
        ReleaseCheck = 140737488355328UL,
        All = ReleaseCheck | TargetModel,
    }

    [Flags]
    public enum TargetInfoFlag : uint
    {
        TargetID = 1U,
        Name = 2U,
        Info = 4U,
        HomeDir = 8U,
        FileServingDir = 16U,
        Boot = 32U,
    }

    public struct TargetInfo
    {
        public PS3_TMAPI.TargetInfoFlag Flags;
        public int Target;
        [MarshalAs(UnmanagedType.LPStr)]
        public string Name;
        [MarshalAs(UnmanagedType.LPStr)]
        public string Type;
        [MarshalAs(UnmanagedType.LPStr)]
        public string Info;
        [MarshalAs(UnmanagedType.LPStr)]
        public string HomeDir;
        [MarshalAs(UnmanagedType.LPStr)]
        public string FSDir;
        public PS3_TMAPI.BootParameter Boot;
    }

    public struct TargetType
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Description;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TCPIPConnectProperties
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string IPAddress;
        public uint Port;
    }

    [Flags]
    public enum SystemInfoFlag : uint
    {
        SDKVersion = 1U,
        TimebaseFreq = 2U,
        RTSDKVersion = 4U,
        TotalSystemMem = 8U,
        AvailableSysMem = 16U,
        DCMBufferSize = 32U,
    }

    public struct SystemInfo
    {
        public uint CellSDKVersion;
        public ulong TimebaseFrequency;
        public uint CellRuntimeSDKVersion;
        public uint TotalSystemMemory;
        public uint AvailableSystemMemory;
        public uint DCMBufferSize;
    }

    [Flags]
    public enum ExtraLoadFlag : ulong
    {
        EnableLv2ExceptionHandler = 1UL,
        EnableRemotePlay = 2UL,
        EnableGCMDebug = 4UL,
        LoadLibprofSPRXAutomatically = 8UL,
        EnableCoreDump = 16UL,
        EnableAccForRemotePlay = 32UL,
        EnableHUDRSXTools = 64UL,
        EnableMAT = 128UL,
        EnableMiscSettings = 9223372036854775808UL,
        GameAttributeInviteMessage = 256UL,
        GameAttributeCustomMessage = 512UL,
        LoadingPatch = 4096UL,
    }

    [Flags]
    public enum ExtraLoadFlagMask : ulong
    {
        GameAttributeMessageMask = 3840UL,
        All = 9223372036854783999UL,
        OverrideTVGUIMask = 9223372036854775808UL,
    }

    public struct TTYStream
    {
        public uint Index;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;
    }

    private enum SNPS3_TM_TIMEOUT
    {
        DEFAULT_TIMEOUT,
        RESET_TIMEOUT,
        CONNECT_TIMEOUT,
        LOAD_TIMEOUT,
        GET_STATUS_TIMEOUT,
        RECONNECT_TIMEOUT,
        GAMEPORT_TIMEOUT,
        GAMEEXIT_TIMEOUT,
    }

    public enum TimeoutType
    {
        Default,
        Reset,
        Connect,
        Load,
        GetStatus,
        Reconnect,
        GamePort,
        GameExit,
    }

    public delegate void TTYCallback(int target, uint streamID, PS3_TMAPI.SNRESULT res, string data, object userData);

    private class TTYCallbackAndUserData
    {
        public PS3_TMAPI.TTYCallback m_callback;
        public object m_userData;
    }

    private struct TTYChannel
    {
        public readonly int Target;
        public readonly uint Channel;

        public TTYChannel(int target, uint channel)
        {
            this.Target = target;
            this.Channel = channel;
        }
    }

    public enum UnitType
    {
        PPU,
        SPU,
        SPURAW,
    }

    public enum UnitStatus : uint
    {
        Unknown,
        Running,
        Stopped,
        Signalled,
        Resetting,
        Missing,
        Reset,
        NotConnected,
        Connected,
        StatusChange,
    }

    [Flags]
    public enum LoadFlag : uint
    {
        EnableDebugging = 1U,
        UseELFPriority = 256U,
        UseELFStackSize = 512U,
        WaitBDMounted = 8192U,
        PPUNotDebug = 65536U,
        SPUNotDebug = 131072U,
        IgnoreDefaults = 2147483648U,
        ParamSFOUseELFDir = 1048576U,
        ParamSFOUseCustomDir = 2097152U,
    }

    public enum ProcessStatus : uint
    {
        Creating = 1U,
        Ready = 2U,
        Exited = 3U,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ProcessInfoHdr
    {
        public PS3_TMAPI.ProcessStatus Status;
        public uint NumPPUThreads;
        public uint NumSPUThreads;
        public uint ParentProcessID;
        public ulong MaxMemorySize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string ELFPath;
    }

    public struct ProcessInfo
    {
        public PS3_TMAPI.ProcessInfoHdr Hdr;
        public ulong[] ThreadIDs;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ExtraProcessInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] PPUGUIDs;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ProcessLoadParams
    {
        public ulong Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public ulong[] Data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ProcessLoadInfo
    {
        public uint InfoValid;
        public uint DebugFlags;
        public PS3_TMAPI.ProcessLoadParams LoadInfo;
    }

    public struct ModuleInfoHdr
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public string Name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public sbyte[] Version;
        public uint Attribute;
        public uint StartEntry;
        public uint StopEntry;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string ELFName;
        public uint NumSegments;
    }

    public struct PRXSegment
    {
        public ulong Base;
        public ulong FileSize;
        public ulong MemSize;
        public ulong Index;
        public ulong ELFType;
    }

    public struct ModuleInfo
    {
        public PS3_TMAPI.ModuleInfoHdr Hdr;
        public PS3_TMAPI.PRXSegment[] Segments;
    }

    public struct PRXSegmentEx
    {
        public ulong Base;
        public ulong FileSize;
        public ulong MemSize;
        public ulong Index;
        public ulong ELFType;
        public ulong Flags;
        public ulong Align;
    }

    public struct ModuleInfoEx
    {
        public PS3_TMAPI.ModuleInfoHdr Hdr;
        public PS3_TMAPI.PRXSegmentEx[] Segments;
    }

    public struct MSELFInfo
    {
        public ulong MSELFFileOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Reserved;
    }

    public struct ExtraModuleInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public uint[] PPUGUIDs;
    }

    public enum PPUThreadState
    {
        Idle,
        Runnable,
        OnProc,
        Sleep,
        Suspended,
        SleepSuspended,
        Stop,
        Zombie,
        Deleted,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct PPUThreadInfoPriv
    {
        public ulong ThreadID;
        public uint Priority;
        public uint State;
        public ulong StackAddress;
        public ulong StackSize;
        public uint ThreadNameLen;
    }

    public struct PPUThreadInfo
    {
        public ulong ThreadID;
        public uint Priority;
        public PS3_TMAPI.PPUThreadState State;
        public ulong StackAddress;
        public ulong StackSize;
        public string ThreadName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct PPUThreadInfoExPriv
    {
        public ulong ThreadId;
        public uint Priority;
        public uint BasePriority;
        public uint State;
        public ulong StackAddress;
        public ulong StackSize;
        public uint ThreadNameLen;
    }

    public struct PPUThreadInfoEx
    {
        public ulong ThreadID;
        public uint Priority;
        public uint BasePriority;
        public PS3_TMAPI.PPUThreadState State;
        public ulong StackAddress;
        public ulong StackSize;
        public string ThreadName;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SpuThreadInfoPriv
    {
        public uint ThreadGroupId;
        public uint ThreadId;
        public uint FilenameLen;
        public uint ThreadNameLen;
    }

    public struct SPUThreadInfo
    {
        public uint ThreadGroupID;
        public uint ThreadID;
        public string Filename;
        public string ThreadName;
    }

    [Flags]
    public enum ELFStackSize : uint
    {
        Stack32k = 32U,
        Stack64k = 64U,
        Stack96k = Stack64k | Stack32k,
        Stack128k = 128U,
        Stack256k = 256U,
        Stack512k = 512U,
        Stack1024k = 1024U,
        StackDefault = Stack64k,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DebugThreadControlInfoPriv
    {
        public ulong ControlFlags;
        public uint NumEntries;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ControlKeywordEntry
    {
        public uint MatchConditionFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Keyword;
    }

    public struct DebugThreadControlInfo
    {
        public ulong ControlFlags;
        public PS3_TMAPI.ControlKeywordEntry[] ControlKeywords;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ProcessTreeBranchPriv
    {
        public uint ProcessId;
        public PS3_TMAPI.ProcessStatus ProcessState;
        public uint NumPpuThreads;
        public uint NumSpuThreadGroups;
        public ushort ProcessFlags;
        public ushort RawSPU;
        public IntPtr PpuThreadStatuses;
        public IntPtr SpuThreadGroupStatuses;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PPUThreadStatus
    {
        public ulong ThreadID;
        public PS3_TMAPI.PPUThreadState ThreadState;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SPUThreadGroupStatus
    {
        public uint ThreadGroupID;
        public PS3_TMAPI.SPUThreadGroupState ThreadGroupState;
    }

    public struct ProcessTreeBranch
    {
        public uint ProcessID;
        public PS3_TMAPI.ProcessStatus ProcessState;
        public ushort ProcessFlags;
        public ushort RawSPU;
        public PS3_TMAPI.PPUThreadStatus[] PPUThreadStatuses;
        public PS3_TMAPI.SPUThreadGroupStatus[] SPUThreadGroupStatuses;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SpuThreadGroupInfoPriv
    {
        public uint ThreadGroupId;
        public uint State;
        public uint Priority;
        public uint NumThreads;
        public uint ThreadGroupNameLen;
    }

    public enum SPUThreadGroupState : uint
    {
        NotConfigured,
        Configured,
        Ready,
        Waiting,
        Suspended,
        WaitingSuspended,
        Running,
        Stopped,
    }

    public struct SPUThreadGroupInfo
    {
        public uint ThreadGroupID;
        public PS3_TMAPI.SPUThreadGroupState State;
        public uint Priority;
        public string GroupName;
        public uint[] ThreadIDs;
    }

    public enum MemoryCompressionLevel : uint
    {
        None = 0U,
        BestSpeed = 1U,
        BestCompression = 9U,
        Default = 4294967295U,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VirtualMemoryArea
    {
        public ulong Address;
        public ulong Flags;
        public ulong VSize;
        public ulong Options;
        public ulong PageFaultPPU;
        public ulong PageFaultSPU;
        public ulong PageIn;
        public ulong PageOut;
        public ulong PMemTotal;
        public ulong PMemUsed;
        public ulong Time;
        public ulong[] Pages;
    }

    public struct SyncPrimitiveCounts
    {
        public uint NumMutexes;
        public uint NumConditionVariables;
        public uint NumRWLocks;
        public uint NumLWMutexes;
        public uint NumEventQueues;
        public uint NumSemaphores;
        public uint NumLWConditionVariables;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct MutexInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.MutexAttr Attribute;
        public ulong OwnerThreadId;
        public uint LockCounter;
        public uint ConditionRefCounter;
        public uint ConditionVarId;
        public uint NumWaitingThreads;
        public uint NumWaitAllThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MutexAttr
    {
        public uint Protocol;
        public uint Recursive;
        public uint PShared;
        public uint Adaptive;
        public ulong Key;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct MutexInfo
    {
        public uint ID;
        public PS3_TMAPI.MutexAttr Attribute;
        public ulong OwnerThreadID;
        public uint LockCounter;
        public uint ConditionRefCounter;
        public uint ConditionVarID;
        public uint NumWaitAllThreads;
        public ulong[] WaitingThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct LwMutexInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.LWMutexAttr Attribute;
        public ulong OwnerThreadId;
        public uint LockCounter;
        public uint NumWaitingThreads;
        public uint NumWaitAllThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LWMutexAttr
    {
        public uint Protocol;
        public uint Recursive;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct LWMutexInfo
    {
        public uint ID;
        public PS3_TMAPI.LWMutexAttr Attribute;
        public ulong OwnerThreadID;
        public uint LockCounter;
        public uint NumWaitAllThreads;
        public ulong[] WaitingThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ConditionVarInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.ConditionVarAttr Attribute;
        public uint MutexId;
        public uint NumWaitingThreads;
        public uint NumWaitAllThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConditionVarAttr
    {
        public uint PShared;
        public ulong Key;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct ConditionVarInfo
    {
        public uint ID;
        public PS3_TMAPI.ConditionVarAttr Attribute;
        public uint MutexID;
        public uint NumWaitAllThreads;
        public ulong[] WaitingThreads;
    }

    private struct LwConditionVarInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.LWConditionVarAttr Attribute;
        public uint LwMutexId;
        public uint NumWaitingThreads;
        public uint NumWaitAllThreads;
    }

    public struct LWConditionVarAttr
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct LWConditionVarInfo
    {
        public uint ID;
        public PS3_TMAPI.LWConditionVarAttr Attribute;
        public uint LWMutexID;
        public uint NumWaitAllThreads;
        public ulong[] WaitingThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct RwLockInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.RWLockAttr Attribute;
        public ulong OwnerThreadId;
        public uint NumWaitingReadThreads;
        public uint NumWaitAllReadThreads;
        public uint NumWaitingWriteThreads;
        public uint NumWaitAllWriteThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RWLockAttr
    {
        public uint Protocol;
        public uint PShared;
        public ulong Key;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct RWLockInfo
    {
        public uint ID;
        public PS3_TMAPI.RWLockAttr Attribute;
        public ulong OwnerThreadID;
        public uint NumWaitingReadThreads;
        public uint NumWaitAllReadThreads;
        public uint NumWaitingWriteThreads;
        public uint NumWaitAllWriteThreads;
        public ulong[] WaitingThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SemaphoreInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.SemaphoreAttr Attribute;
        public uint MaxValue;
        public uint CurrentValue;
        public uint NumWaitingThreads;
        public uint NumWaitAllThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SemaphoreAttr
    {
        public uint Protocol;
        public uint PShared;
        public ulong Key;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct SemaphoreInfo
    {
        public uint ID;
        public PS3_TMAPI.SemaphoreAttr Attribute;
        public uint MaxValue;
        public uint CurrentValue;
        public uint NumWaitAllThreads;
        public ulong[] WaitingThreads;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct EventQueueInfoPriv
    {
        public uint Id;
        public PS3_TMAPI.EventQueueAttr Attribute;
        public ulong Key;
        public uint Size;
        public uint NumWaitingThreads;
        public uint NumWaitAllThreads;
        public uint NumReadableEvQueue;
        public uint NumReadableAllEvQueue;
        public IntPtr WaitingThreadIds;
        public IntPtr QueueEntries;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventQueueAttr
    {
        public uint Protocol;
        public uint Type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct SystemEvent
    {
        public ulong Source;
        public ulong Data1;
        public ulong Data2;
        public ulong Data3;
    }

    public struct EventQueueInfo
    {
        public uint ID;
        public PS3_TMAPI.EventQueueAttr Attribute;
        public ulong Key;
        public uint Size;
        public uint NumWaitAllThreads;
        public uint NumReadableAllEvQueue;
        public ulong[] WaitingThreadIDs;
        public PS3_TMAPI.SystemEvent[] QueueEntries;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventFlagWaitThread
    {
        public ulong ID;
        public ulong BitPattern;
        public uint Mode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventFlagAttr
    {
        public uint Protocol;
        public uint PShared;
        public ulong Key;
        public uint Flags;
        public uint Type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Name;
    }

    public struct EventFlagInfo
    {
        public uint ID;
        public PS3_TMAPI.EventFlagAttr Attribute;
        public ulong BitPattern;
        public uint NumWaitAllThreads;
        public PS3_TMAPI.EventFlagWaitThread[] WaitingThreads;
    }

    public enum PowerStatus
    {
        Off,
        On,
        Suspended,
        Unknown,
        SwitchingOn,
    }

    public struct UserMemoryStats
    {
        public uint CreatedSharedMemorySize;
        public uint AttachedSharedMemorySize;
        public uint ProcessLocalMemorySize;
        public uint ProcessLocalTextSize;
        public uint PRXTextSize;
        public uint PRXDataSize;
        public uint MiscMemorySize;
    }

    public struct GamePortIPAddressData
    {
        public uint ReturnValue;
        public uint IPAddress;
        public uint SubnetMask;
        public uint BroadcastAddress;
    }

    [Flags]
    public enum RSXProfilingFlag : ulong
    {
        UseRSXProfilingTools = 1UL,
        UseFullHUDFeatures = 2UL,
    }

    [Flags]
    public enum CoreDumpFlag : ulong
    {
        ToDevMS = 1UL,
        ToAppHome = 2UL,
        ToDevUSB = 4UL,
        ToDevHDD0 = 8UL,
        DisablePPUExceptionDetection = 36028797018963968UL,
        DisableSPUExceptionDetection = 18014398509481984UL,
        DisableRSXExceptionDetection = 9007199254740992UL,
        DisableFootSwitchDetection = 4503599627370496UL,
        DisableMemoryDump = 3489660928UL,
        EnableRestartProcess = 32768UL,
        EnableKeepRunningHandler = 8192UL,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ScatteredWrite
    {
        public uint Address;
        public byte[] Data;
    }

    public enum MATCondition : byte
    {
        Transparent,
        Write,
        ReadWrite,
        Error,
    }

    public struct MATRange
    {
        public uint StartAddress;
        public uint Size;
        public PS3_TMAPI.MATCondition[] PageConditions;
    }

    public enum PadPlaybackResponse : uint
    {
        Ok = 0U,
        InvalidPacket = 2147549186U,
        InsufficientMemory = 2147549188U,
        Busy = 2147549194U,
        NoDev = 2147549229U,
    }

    public delegate void PadPlaybackCallback(int target, PS3_TMAPI.SNRESULT res, PS3_TMAPI.PadPlaybackResponse playbackResult, object userData);

    private class PadPlaybackCallbackAndUserData
    {
        public PS3_TMAPI.PadPlaybackCallback m_callback;
        public object m_userData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PadData
    {
        public uint TimeHi;
        public uint TimeLo;
        public uint Reserved0;
        public uint Reserved1;
        public byte Port;
        public byte PortStatus;
        public byte Length;
        public byte Reserved2;
        public uint Reserved3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public short[] buttons;
    }

    public delegate void PadCaptureCallback(int target, PS3_TMAPI.SNRESULT res, PS3_TMAPI.PadData[] padData, object userData);

    private class PadCaptureCallbackAndUserData
    {
        public PS3_TMAPI.PadCaptureCallback m_callback;
        public object m_userData;
    }

    [Flags]
    public enum VRAMCaptureFlag : ulong
    {
        Enabled = 1UL,
        Disabled = 0UL,
    }

    public class VRAMInfo
    {
        public ulong BPAddress;
        public ulong TopAddressPointer;
        public uint Width;
        public uint Height;
        public uint Pitch;
        public byte Colour;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct VramInfoPriv
    {
        public ulong BpAddress;
        public ulong TopAddressPointer;
        public uint Width;
        public uint Height;
        public uint Pitch;
        public byte Colour;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PS3Protocol
    {
        public uint Protocol;
        public uint Port;
        public uint LPARDesc;
    }

    private struct PS3ProtocolPriv
    {
        public readonly uint Protocol;
        public readonly uint Port;

        public PS3ProtocolPriv(uint protocol, uint port)
        {
            this.Protocol = port;
            this.Port = protocol;
        }
    }

    private struct CustomProtocolId
    {
        public readonly int Target;
        public readonly PS3_TMAPI.PS3ProtocolPriv Protocol;

        public CustomProtocolId(int target, PS3_TMAPI.PS3ProtocolPriv protocol)
        {
            this.Target = target;
            this.Protocol = protocol;
        }
    }

    private delegate void CustomProtocolCallbackPriv(int target, PS3_TMAPI.PS3Protocol protocol, IntPtr unmanagedBuf, uint length, IntPtr userData);

    public delegate void CustomProtocolCallback(int target, PS3_TMAPI.PS3Protocol protocol, byte[] data, object userData);

    private class CusProtoCallbackAndUserData
    {
        public PS3_TMAPI.CustomProtocolCallback m_callback;
        public object m_userData;
    }

    [Flags]
    public enum FileServingEventFlag : ulong
    {
        Create = 1UL,
        Close = 4UL,
        Read = 8UL,
        Write = 16UL,
        Seek = 32UL,
        Delete = 64UL,
        Rename = 128UL,
        SetAttr = 256UL,
        GetAttr = 512UL,
        SetTime = 1024UL,
        MKDir = 2048UL,
        RMDir = 4096UL,
        OpenDir = 8192UL,
        CloseDir = 16384UL,
        ReadDir = 32768UL,
        Truncate = 65536UL,
        FGetAttr64 = 131072UL,
        GetAttr64 = 262144UL,
        All = GetAttr64 | FGetAttr64 | Truncate | ReadDir | CloseDir | OpenDir | RMDir | MKDir | SetTime | GetAttr | SetAttr | Rename | Delete | Seek | Write | Read | Close | Create,
    }

    public enum FileTransferNotificationType : uint
    {
        Progress = 0U,
        Finish = 1U,
        Skipped = 2U,
        Cancelled = 3U,
        Error = 4U,
        Pending = 5U,
        Unknown = 6U,
        RefreshList = 2147483648U,
    }

    public struct FTPNotification
    {
        public PS3_TMAPI.FileTransferNotificationType Type;
        public uint TransferID;
        public ulong BytesTransferred;
    }

    public delegate void FTPEventCallback(int target, PS3_TMAPI.SNRESULT res, PS3_TMAPI.FTPNotification[] ftpNotifications, object userData);

    private class FtpCallbackAndUserData
    {
        public PS3_TMAPI.FTPEventCallback m_callback;
        public object m_userData;
    }

    public enum FileTraceType
    {
        GetBlockSize = 1,
        Stat = 2,
        WidgetStat = 3,
        Unlink = 4,
        WidgetUnlink = 5,
        RMDir = 6,
        WidgetRMDir = 7,
        Rename = 14,
        WidgetRename = 15,
        Truncate = 18,
        TruncateNoAlloc = 19,
        Truncate2 = 20,
        Truncate2NoInit = 21,
        OpenDir = 24,
        WidgetOpenDir = 25,
        CHMod = 26,
        MkDir = 27,
        UTime = 29,
        Open = 33,
        WidgetOpen = 34,
        Close = 35,
        CloseDir = 36,
        FSync = 37,
        ReadDir = 38,
        FStat = 39,
        FGetBlockSize = 40,
        Read = 47,
        Write = 48,
        GetDirEntries = 49,
        ReadOffset = 50,
        WriteOffset = 51,
        FTruncate = 52,
        FTruncateNoAlloc = 53,
        LSeek = 56,
        SetIOBuffer = 57,
        OfflineEnd = 9999,
    }

    public enum FileTraceNotificationStatus
    {
        Processed,
        Received,
        Waiting,
        Processing,
        Suspended,
        Finished,
    }

    public struct FileTraceLogData
    {
        public PS3_TMAPI.FileTraceLogType1 LogType1;
        public PS3_TMAPI.FileTraceLogType2 LogType2;
        public PS3_TMAPI.FileTraceLogType3 LogType3;
        public PS3_TMAPI.FileTraceLogType4 LogType4;
        public PS3_TMAPI.FileTraceLogType6 LogType6;
        public PS3_TMAPI.FileTraceLogType8 LogType8;
        public PS3_TMAPI.FileTraceLogType9 LogType9;
        public PS3_TMAPI.FileTraceLogType10 LogType10;
        public PS3_TMAPI.FileTraceLogType11 LogType11;
        public PS3_TMAPI.FileTraceLogType12 LogType12;
        public PS3_TMAPI.FileTraceLogType13 LogType13;
        public PS3_TMAPI.FileTraceLogType14 LogType14;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType1
    {
        public string Path;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType2
    {
        public string Path1;
        public string Path2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType3
    {
        public ulong Arg;
        public string Path;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType4
    {
        public uint Mode;
        public string Path;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType6
    {
        public ulong Arg1;
        public ulong Arg2;
        public string Path;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceProcessInfo
    {
        public ulong VFSID;
        public ulong FD;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType8
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
        public uint Arg1;
        public uint Arg2;
        public uint Arg3;
        public uint Arg4;
        public byte[] VArg;
        public string Path;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType9
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType10
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
        public uint Size;
        public ulong Address;
        public uint TxSize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType11
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
        public uint Size;
        public ulong Address;
        public ulong Offset;
        public uint TxSize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType12
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
        public ulong TargetSize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType13
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
        public uint Size;
        public ulong Offset;
        public ulong CurPos;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceLogType14
    {
        public PS3_TMAPI.FileTraceProcessInfo ProcessInfo;
        public uint MaxSize;
        public uint Page;
        public uint ContainerID;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FileTraceEvent
    {
        public ulong SerialID;
        public PS3_TMAPI.FileTraceType TraceType;
        public PS3_TMAPI.FileTraceNotificationStatus Status;
        public uint ProcessID;
        public uint ThreadID;
        public ulong TimeBaseStartOfTrace;
        public ulong TimeBase;
        public byte[] BackTraceData;
        public PS3_TMAPI.FileTraceLogData LogData;
    }

    public delegate void FileTraceCallback(int target, PS3_TMAPI.SNRESULT res, PS3_TMAPI.FileTraceEvent fileTraceEvent, object userData);

    private class FileTraceCallbackAndUserData
    {
        public PS3_TMAPI.FileTraceCallback m_callback;
        public object m_userData;
    }

    private struct FileTransferInfoPriv
    {
        public uint TransferId;
        public uint Status;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string SourcePath;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1056)]
        public string DestinationPath;
        public ulong Size;
        public ulong BytesRead;
    }

    public enum FileTransferStatus : uint
    {
        Pending = 1U,
        Failed = 2U,
        Succeeded = 4U,
        Skipped = 8U,
        InProgress = 16U,
        Cancelled = 32U,
    }

    public struct FileTransferInfo
    {
        public uint TransferID;
        public PS3_TMAPI.FileTransferStatus Status;
        public string SourcePath;
        public string DestinationPath;
        public ulong Size;
        public ulong BytesRead;
    }

    public struct Time
    {
        private int Sec;
        private int Min;
        private int Hour;
        private int MDay;
        private int Mon;
        private int Year;
        private int WDay;
        private int YDay;
        private int IsDST;
    }

    public enum DirEntryType : uint
    {
        Unknown,
        Directory,
        Regular,
        Symlink,
    }

    public struct DirEntry
    {
        public PS3_TMAPI.DirEntryType Type;
        public uint Mode;
        public PS3_TMAPI.Time AccessTime;
        public PS3_TMAPI.Time ModifiedTime;
        public PS3_TMAPI.Time CreateTime;
        public ulong Size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Name;
    }

    public struct DirEntryEx
    {
        public PS3_TMAPI.DirEntryType Type;
        public uint Mode;
        public ulong AccessTimeUTC;
        public ulong ModifiedTimeUTC;
        public ulong CreateTimeUTC;
        public ulong Size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Name;
    }

    public struct TargetTimezone
    {
        public int Timezone;
        public int DST;
    }

    public enum ChModFilePermission : uint
    {
        ReadOnly = 256U,
        ReadWrite = 384U,
    }

    public enum LogCategory : uint
    {
        Off = 0U,
        All = 4294967295U,
    }

    public enum TargetEventType : uint
    {
        UnitStatusChange = 0U,
        ResetStarted = 1U,
        ResetEnd = 2U,
        Details = 4U,
        ModuleLoad = 5U,
        ModuleRunning = 6U,
        ModuleDoneRemove = 7U,
        ModuleDoneResident = 8U,
        ModuleStopped = 9U,
        ModuleStoppedRemove = 10U,
        PowerStatusChange = 11U,
        TTYStreamAdded = 12U,
        TTYStreamDeleted = 13U,
        TargetSpecific = 2147483648U,
    }

    public struct TGTEventUnitStatusChangeData
    {
        public PS3_TMAPI.UnitType Unit;
        public PS3_TMAPI.UnitStatus Status;
    }

    public struct TGTEventDetailsData
    {
        public uint Flags;
    }

    public struct TGTEventModuleEventData
    {
        public uint Unit;
        public uint ModuleID;
    }

    public struct TargetEventData
    {
        public PS3_TMAPI.TGTEventUnitStatusChangeData UnitStatusChangeData;
        public PS3_TMAPI.TGTEventDetailsData DetailsData;
        public PS3_TMAPI.TGTEventModuleEventData ModuleEventData;
    }

    public struct TargetEvent
    {
        public uint TargetID;
        public PS3_TMAPI.TargetEventType Type;
        public PS3_TMAPI.TargetEventData EventData;
        public PS3_TMAPI.TargetSpecificEvent TargetSpecific;
    }

    public delegate void TargetEventCallback(int target, PS3_TMAPI.SNRESULT res, PS3_TMAPI.TargetEvent[] targetEventList, object userData);

    private class TargetCallbackAndUserData
    {
        public PS3_TMAPI.TargetEventCallback m_callback;
        public object m_userData;
    }

    public enum TargetSpecificEventType : uint
    {
        ProcessCreate = 0U,
        ProcessExit = 1U,
        ProcessKill = 2U,
        ProcessExitSpawn = 3U,
        PPUExcTrap = 16U,
        PPUExcPrevInt = 17U,
        PPUExcAlignment = 18U,
        PPUExcIllInst = 19U,
        PPUExcTextHtabMiss = 20U,
        PPUExcTextSlbMiss = 21U,
        PPUExcDataHtabMiss = 22U,
        PPUExcFloat = 23U,
        PPUExcDataSlbMiss = 24U,
        PPUExcDabrMatch = 25U,
        PPUExcStop = 26U,
        PPUExcStopInit = 27U,
        PPUExcDataMAT = 28U,
        PPUThreadCreate = 32U,
        PPUThreadExit = 33U,
        SPUThreadStart = 48U,
        SPUThreadStop = 49U,
        SPUThreadStopInit = 50U,
        SPUThreadGroupDestroy = 51U,
        SPUThreadStopEx = 52U,
        PRXLoad = 64U,
        PRXUnload = 65U,
        DAInitialised = 96U,
        Footswitch = 112U,
        InstallPackageProgress = 128U,
        InstallPackagePath = 129U,
        CoreDumpComplete = 256U,
        RawNotify = 4026531855U,
    }

    public struct PPUProcessCreateData
    {
        public uint ParentProcessID;
        public string Filename;
    }

    public struct PPUProcessExitData
    {
        public ulong ExitCode;
    }

    public struct PPUExceptionData
    {
        public ulong ThreadID;
        public uint HWThreadNumber;
        public ulong PC;
        public ulong SP;
    }

    public struct PPUAlignmentExceptionData
    {
        public ulong ThreadID;
        public uint HWThreadNumber;
        public ulong DSISR;
        public ulong DAR;
        public ulong PC;
        public ulong SP;
    }

    public struct PPUDataMatExceptionData
    {
        public ulong ThreadID;
        public uint HWThreadNumber;
        public ulong DSISR;
        public ulong DAR;
        public ulong PC;
        public ulong SP;
    }

    public struct PPUThreadCreateData
    {
        public ulong ThreadID;
    }

    public struct PPUThreadExitData
    {
        public ulong ThreadID;
    }

    public struct SPUThreadStartData
    {
        public uint ThreadGroupID;
        public uint ThreadID;
        public string ElfFilename;
    }

    public enum SPUThreadStopReason : uint
    {
        NoException = 0U,
        DMAAlignment = 1U,
        DMACommand = 2U,
        Error = 4U,
        MFCFIR = 8U,
        MFCSegment = 16U,
        MFCStorage = 32U,
        NoValue = 64U,
        StopCall = 256U,
        StopDCall = 512U,
        Halt = 1024U,
    }

    public struct SPUThreadStopData
    {
        public uint ThreadGroupID;
        public uint ThreadID;
        public uint PC;
        public PS3_TMAPI.SPUThreadStopReason Reason;
        public uint SP;
    }

    public struct SPUThreadStopExData
    {
        public uint ThreadGroupID;
        public uint ThreadID;
        public uint PC;
        public PS3_TMAPI.SPUThreadStopReason Reason;
        public uint SP;
        public ulong MFCDSISR;
        public ulong MFCDSIPR;
        public ulong MFCDAR;
    }

    public struct SPUThreadGroupDestroyData
    {
        public uint ThreadGroupID;
    }

    public struct NotifyPRXLoadData
    {
        public ulong PPUThreadID;
        public uint PRXID;
        public ulong Timestamp;
    }

    public struct NotifyPRXUnloadData
    {
        public ulong PPUThreadID;
        public uint PRXID;
        public ulong Timestamp;
    }

    public struct FootswitchData
    {
        public ulong EventSource;
        public ulong EventData1;
        public ulong EventData2;
        public ulong EventData3;
        public ulong Reserved;
    }

    public struct InstallPackageProgress
    {
        public uint Percent;
    }

    public struct InstallPackagePath
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Path;
    }

    public struct CoreDumpComplete
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Filename;
    }

    public struct TargetSpecificData
    {
        public PS3_TMAPI.TargetSpecificEventType Type;
        public PS3_TMAPI.PPUProcessCreateData PPUProcessCreate;
        public PS3_TMAPI.PPUProcessExitData PPUProcessExit;
        public PS3_TMAPI.PPUExceptionData PPUException;
        public PS3_TMAPI.PPUAlignmentExceptionData PPUAlignmentException;
        public PS3_TMAPI.PPUDataMatExceptionData PPUDataMatException;
        public PS3_TMAPI.PPUThreadCreateData PPUThreadCreate;
        public PS3_TMAPI.PPUThreadExitData PPUThreadExit;
        public PS3_TMAPI.SPUThreadStartData SPUThreadStart;
        public PS3_TMAPI.SPUThreadStopData SPUThreadStop;
        public PS3_TMAPI.SPUThreadStopExData SPUThreadStopEx;
        public PS3_TMAPI.SPUThreadGroupDestroyData SPUThreadGroupDestroyData;
        public PS3_TMAPI.NotifyPRXLoadData PRXLoad;
        public PS3_TMAPI.NotifyPRXUnloadData PRXUnload;
        public PS3_TMAPI.FootswitchData Footswitch;
        public PS3_TMAPI.InstallPackageProgress InstallPackageProgress;
        public PS3_TMAPI.InstallPackagePath InstallPackagePath;
        public PS3_TMAPI.CoreDumpComplete CoreDumpComplete;
    }

    public struct TargetSpecificEvent
    {
        public uint CommandID;
        public uint RequestID;
        public uint ProcessID;
        public uint Result;
        public PS3_TMAPI.TargetSpecificData Data;
    }

    private enum EventType
    {
        TTY = 100,
        Target = 101,
        System = 102,
        FTP = 103,
        PadCapture = 104,
        FileTrace = 105,
        PadPlayback = 106,
    }

    private delegate void HandleEventCallbackPriv(int target, PS3_TMAPI.EventType type, uint param, PS3_TMAPI.SNRESULT result, uint length, IntPtr data, IntPtr userData);
}
