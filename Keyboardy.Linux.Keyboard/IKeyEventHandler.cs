﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboardy.Linux.Keyboard;
interface IKeyEventHandler
{
    public void Handle(input_event inputEvent);
}
