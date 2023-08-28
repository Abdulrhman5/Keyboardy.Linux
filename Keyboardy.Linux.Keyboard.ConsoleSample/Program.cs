using System.Reflection.Metadata;

namespace Keyboardy.Linux.Keyboard.ConsoleSample;

internal class Program
{
    static void Main(string[] args)
    { 
        var listener = new KeyboardListener();

        var eventHandler = new KeyboardEventHandler();
        listener.SetHook(eventHandler);

        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();

        listener.Unhook();
        eventHandler.EventSender.CloseChannels();
    }
}


class KeyboardEventHandler : IKeyEventHandler
{
    public EventSender EventSender { get; set; }

    public KeyboardEventHandler()
    {
        EventSender = new EventSender();
        EventSender.OpenChannels();
    }

    public void Handle(input_event inputEvent)
    {
        if (inputEvent.type == Constants.EV_KEY)
        {
            if (inputEvent.value == Constants.KEY_DOWN || inputEvent.value == Constants.KEY_REPEAT)
            {

                Console.WriteLine($"Key: {inputEvent.code} has been pressed");
            }
        }

        Console.WriteLine("Forwarding the event");
        EventSender.SendEvent(inputEvent);
    }
}