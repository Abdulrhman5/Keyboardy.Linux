using System;
using System.Runtime.InteropServices;
using static Keyboardy.Linux.Keyboard.Constants;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Keyboardy.Linux.Keyboard;

public class EventSender 
{
    private string outputFilePath = "/dev/uinput";
    private int outputHandle = 0;

    int uinputUserDevLength;
    int inputEventLength;

    public EventSender()
    {
        uinputUserDevLength = Marshal.SizeOf(typeof(uinput_user_dev));
        inputEventLength = Marshal.SizeOf(typeof(input_event));
    }

    public void OpenChannels()
    {
        outputHandle = NativeMethods.Open(outputFilePath, O_WRONLYO_NONBLOCK);
        if (outputHandle < 0)
        {
            throw new InvalidOperationException("Error opening the output file");
        }

        if (NativeMethods.Ioctl(outputHandle, UI_SET_EVBIT, EV_SYN) < 0)
        {
            throw new InvalidOperationException("Error while setting the EVBIT EV_SYN");
        }
        if (NativeMethods.Ioctl(outputHandle, UI_SET_EVBIT, EV_KEY) < 0)
        {
            throw new InvalidOperationException("Error while setting the EVBIT EV_KEY");
        }
        if (NativeMethods.Ioctl(outputHandle, UI_SET_EVBIT, EV_MSC) < 0)
        {
            throw new InvalidOperationException("Error while setting the EVBIT EV_MSC");
        }

        for (int i = 0; i < KEY_MAX; i++)
        {
            if (NativeMethods.Ioctl(outputHandle, UI_SET_KEYBIT, i) < 0)
            {
                throw new InvalidOperationException($"Error while setting the KEYBIT {i}");
            }
        }

        var uinputUserDevLength = Marshal.SizeOf(typeof(uinput_user_dev));
        var inputUserDevPtr = Marshal.AllocHGlobal(uinputUserDevLength);

        NativeMethods.MemSet(inputUserDevPtr, 0, uinputUserDevLength);
        var inputUserDev = Marshal.PtrToStructure<uinput_user_dev>(inputUserDevPtr);

        inputUserDev.name = new char[UINPUT_MAX_NAME_SIZE];
        for (int i = 0; i < "KeyboardyDriver".Length; i++)
        {
            inputUserDev.name[i] = "KeyboardyDriver"[i];
        }

        inputUserDev.id.bustype = BUS_USB;
        inputUserDev.id.vendor = 0x1;
        inputUserDev.id.product = 0x1;
        inputUserDev.id.version = 1;

        Marshal.StructureToPtr(inputUserDev, inputUserDevPtr, true);
        if (NativeMethods.Write(outputHandle, inputUserDevPtr, uinputUserDevLength) < 0)
        {
            throw new InvalidOperationException($"Error while writing inputUserDevPtr to the outputHandle");
        }

        if (NativeMethods.Ioctl(outputHandle, UI_DEV_CREATE) < 0)
        {
            throw new InvalidOperationException($"Error while writing inputUserDevPtr to the UI_DEV_CREATE");
        }

        Marshal.FreeHGlobal(inputUserDevPtr);
    }

    public void CloseChannels()
    {
        if (NativeMethods.Ioctl(outputHandle, UI_DEV_DESTROY) < 0)
        {
            throw new InvalidOperationException("Error while setting the EVBIT EV_MSC");
        }

        NativeMethods.Close(outputHandle);
    }

    public void SendEvent(IntPtr inputEventPtr, input_event @event)
    {
        if (NativeMethods.Write(outputHandle, inputEventPtr, inputEventLength) < 0)
        {
            throw new InvalidOperationException($"Error while writing inputEventPtr");
        }
    }

    public void SendEvent(input_event @event)
    {
        var eventPtr = Marshal.AllocHGlobal(inputEventLength);
        Marshal.StructureToPtr(@event, eventPtr, true);

        if (NativeMethods.Write(outputHandle, eventPtr, inputEventLength) < 0)
        {
            throw new InvalidOperationException($"Error while writing inputEventPtr");
        }
        Marshal.FreeHGlobal(eventPtr);
    }

    public void SendMsc(ushort keycode)
    {
        var shiftDownMsc = new input_event() { value = keycode, code = 4, type = EV_MSC };
        SendEvent(shiftDownMsc);
    }

    public void SendSync()
    {
        var shiftDownMsc = new input_event() { value = 0, code = 0, type = 0 };
        SendEvent(shiftDownMsc);
    }
}
