using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboardy.Linux.Keyboard;
public class Constants
{
    public const int O_RONLY = 0;
    public const int O_WRONLYO_NONBLOCK = 2049;
    public const int EV_SYN = 0;
    public const int EV_KEY = 1;
    public const int EV_MSC = 4;
    public const int UI_SET_KEYBIT = 1074025829;
    public const int UI_SET_EVBIT = 1074025828;
    public const int UINPUT_MAX_NAME_SIZE = 80;
    public const int BUS_USB = 3;
    public const int UI_DEV_CREATE = 21761;
    public const int KEY_MAX = 767;
    public const int ABS_CNT = 0x3f + 1;
    public const int EVIOCGRAB = 1074021776;
    public const int KEY_REPEAT = 2;
    public const int KEY_DOWN = 1;
    public const int KEY_UP = 0;
    public const int UI_DEV_DESTROY = 21762;
}
