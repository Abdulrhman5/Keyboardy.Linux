# Keyboard logger and Keyboard event sender.
This repository contains a C# library that help you log keyboard strokes and send keyboard events, it is built to be used for [Keyboardy](https://Keyboardy.net).


* The console sample here or any app that you build that uses this library has to run as sudoer in order to open the /dev/input and /dev/uinput files
* When a keyboard key being pressed. three events will be triggered, see https://www.kernel.org/doc/html/v4.17/input/event-codes.html#event-types, the first event is MSC event then a KEY event and finally a SYN event.
