Door door = new();
Console.WriteLine("Welcome to you new door with super lock.  The default code is 1234.\nPlease change the code for security purposes as soon as possible.");
while (true)
{
    Console.WriteLine("\n" + Door.GetState(door));
    Console.Write("What do you want to do? (Type ? for help) ");
    var input = Console.ReadLine();
    door.CommandInput(door, input);
    
}
class Door
{
    public State State { get; set; }
    public int LockCode { get; set; }
    public List<string> CommandList = new List<string> { "open", "close", "lock", "unlock", "change code", "?" };

    public Door()
    {
        State = State.Closed;
        LockCode = 1234;
    }

    public Door(State s, int lockcode)
    {
        State = s;
        LockCode = lockcode;
    }

    public void CommandInput(Door door, string input)
    {
        if (CommandList.Contains(input.ToLower()))
        {
            if (input == "open")
                Console.WriteLine(SetStateOpen(door));
            else if (input == "close")
                Console.WriteLine(SetStateClosed(door));
            else if (input == "lock")
                Console.WriteLine(SetStateLocked(door));
            else if (input == "unlock")
                Console.WriteLine(SetStateUnlocked(door));
            else if (input == "change code")
                Console.WriteLine(ChangeCode(door));
           else if (input == CommandList[5])
               Console.WriteLine(GetHelp(door));
        }
        else
            Console.WriteLine("You can't do that to a door...");
    }

    public bool EnterPasscode(Door door)
    {
        Console.Write("Please enter the passcode for the lock: ");
        string input = Console.ReadLine()!;
        bool valid = int.TryParse(input, out int code);
        if (!valid || code != LockCode)
            return false;
        else if (code == LockCode)
            return true;
        else return false;
    }

    public string SetStateOpen(Door door)
    {
        if (door.State == State.Closed)
        {
            door.State = State.Opened;
            return "The door opens with a creak.";
        }

        else if (door.State == State.Opened)
            return "You can't open this door any wider!";

        else if (door.State == State.Locked)
            return "The door is locked.";

        else return "Something is horribly wrong.";
    }

    public string SetStateClosed(Door door)
    {
        if (door.State == State.Opened)
        {
            door.State = State.Closed;
            return $"The door slams shut.";
        }

        else if (door.State == State.Closed)
            return "The door is already closed as much as it can be.";

        else if (door.State == State.Locked)
            return "Okay not only is the door already closed, but it's locked as well!";

        else return "Something is horribly wrong.";
    }

    public string SetStateLocked(Door door)
    {
        if (door.State == State.Closed)
        {
            door.State = State.Locked;
            return "You lock the door with a *click*.";
        }

        else if (door.State == State.Locked)
            return "The door is already locked.";

        else if (door.State == State.Opened)
            return "Let's close the door before you try to lock it!";

        else return "Something is horribly wrong.";
    }

    public string SetStateUnlocked(Door door)
    {
        if (door.State == State.Locked)
        {
            bool correctCode = EnterPasscode(door);
            if (correctCode)
            {
                door.State = State.Closed;
                return "You successfully unlock the door!";
            }
            else
                return "Dang the password is not correct.";
        }

        else if (door.State == State.Opened)
            return "The door is wide open and therefore obviously not locked.";

        else if (door.State == State.Closed)
            return "Looks like the door isn't locked afterall!";

        else return "Something is horribly wrong.";
    }

    public string ChangeCode(Door door)
    {
        if (door.State == State.Locked)
            return "Unable to change the code while the lock is, well, locked.";
        else
        {
            bool correctCode = EnterPasscode(door);
            if (correctCode)
            {
                Console.Write("What do you want the new code to be? ");
                string input = Console.ReadLine()!;
                bool valid = int.TryParse(input, out int code);
                if (valid)
                {
                    door.LockCode = code;
                    return "You successfully changed the code!";
                }
                else return "There is something wrong with the new code.";
            }
            else return "The code you entered was incorrect.";
        }
    }

    public string GetHelp(Door door)
    {
        if (door.State == State.Closed)
            return "When the door is closed you can either \"open\" it or \"lock\" it.  You can also \"change code\" on your lock.";

        if (door.State == State.Opened)
            return "When the door is open all you can do is \"close\" it.  You can also \"change code\" on your lock.";

        if (door.State == State.Locked)
            return "When the door is locked you have to \"unlock\" it.";

        else return "I don't know what is up with the door.";
    }

    public static string GetState(Door door)
    {
        if (door.State == State.Closed)
            return "The door is closed.";

        if (door.State == State.Opened)
            return "The door is open.";

        if (door.State == State.Locked)
            return "The door is locked.";

        else return "I don't know what is up with the door.";
    }
}

enum State
{
    Opened,
    Closed,
    Locked
}