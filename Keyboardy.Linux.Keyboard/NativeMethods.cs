using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Keyboardy.Linux.Keyboard;

internal static class NativeMethods
{
    [DllImport("libc", EntryPoint = "close", SetLastError = true)]
    internal static extern int Close(int handle);

    [DllImport("libc", EntryPoint = "ioctl", SetLastError = true)]
    internal static extern int Ioctl(int handle, ulong request, int x);

    [DllImport("libc", EntryPoint = "ioctl", SetLastError = true)]
    internal static extern int Ioctl(int handle, ulong request);

    [DllImport("libc", EntryPoint = "open", SetLastError = true)]
    internal static extern int Open(string path, int flag);

    [DllImport("libc", EntryPoint = "read")]
    internal static extern int Read(int fd, IntPtr buf, int nBytes);

    [DllImport("libc", EntryPoint = "memset")]
    internal static extern IntPtr MemSet(IntPtr s, int c, int size);

    [DllImport("libc", EntryPoint = "snprintf", CharSet = CharSet.Ansi)]
    internal static extern int SnPrintf(string s, int maxlen, [In, MarshalAs(UnmanagedType.LPStr)] string format);

    [DllImport("libc", EntryPoint = "write")]
    internal static extern int Write(int fd, IntPtr buf, int size);
}

