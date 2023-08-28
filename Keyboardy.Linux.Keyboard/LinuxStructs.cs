using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Keyboardy.Linux.Keyboard.Constants;

namespace Keyboardy.Linux.Keyboard;


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct input_event
{
    public timeval time;

    public ushort type;

    public ushort code;

    public int value;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct timeval
{
    public long tv_sec;

    public long tv_usec;

}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct uinput_user_dev
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = UINPUT_MAX_NAME_SIZE)]
    public char[] name = default;
    public input_id id = default;
    public UInt32 ff_effects_max = default;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = ABS_CNT)]
    public Int32[] absmax = default;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = ABS_CNT)]
    public Int32[] absmin = default;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = ABS_CNT)]
    public Int32[] absfuzz = default;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = ABS_CNT)]
    public Int32[] absflat = default;

    public uinput_user_dev()
    {
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct input_id
{
    public UInt16 bustype;
    public UInt16 vendor;
    public UInt16 product;
    public UInt16 version;
}