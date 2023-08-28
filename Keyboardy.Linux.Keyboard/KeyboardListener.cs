using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Keyboardy.Linux.Keyboard.Constants;

namespace Keyboardy.Linux.Keyboard;

public delegate void KeyboardEventHandler(object sender, input_event e);
public class KeyboardListener
{
    private string inputFilePath = "/dev/input/by-path/platform-i8042-serio-0-event-kbd";

    private bool IsReflectorDown = false;

    int inputEventLength = Marshal.SizeOf(typeof(input_event));

    private CancellationTokenSource? _tokenSource;
    private CancellationToken? _ctoken;
    private int _inputFileHandle;

    public event KeyboardEventHandler OnKeyboardEvent;
    public KeyboardListener()
    {
    }

    public void SetHook()
    {
        _inputFileHandle = NativeMethods.Open(inputFilePath, O_RONLY);

        var ioctlResult = NativeMethods.Ioctl(_inputFileHandle, EVIOCGRAB, 1);

        _tokenSource = new CancellationTokenSource();
        _ctoken = _tokenSource.Token;
        Task.Factory.StartNew(() => Listen(), _ctoken.Value);
    }

    public void Unhook()
    {
        _tokenSource?.Cancel();

        NativeMethods.Close(_inputFileHandle);
    }

    private void Listen()
    {
        while (true)
        {
            var buffer = Marshal.AllocHGlobal(inputEventLength);

            var readingResult = NativeMethods.Read(_inputFileHandle, buffer, inputEventLength);

            _ctoken?.ThrowIfCancellationRequested();
            var result = Marshal.PtrToStructure<input_event>(buffer);
            OnKeyboardEvent?.Invoke(this, result);
        }
    }
}
