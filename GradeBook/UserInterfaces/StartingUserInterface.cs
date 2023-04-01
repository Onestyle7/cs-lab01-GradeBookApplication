using GradeBook.GradeBooks;
using System;

namespace GradeBook.UserInterfaces
{
    public static class StartingUserInterface
    {
        public static bool Quit = false;
        public static void CommandLoop()
        {
            while (!Quit)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(">> What would you like to do?");
                var command = Console.ReadLine().ToLower();
                CommandRoute(command);
            }
        }

        public static void CommandRoute(string command)
        {
            if (command.StartsWith("create"))
                CreateCommand(command);
            else if (command.StartsWith("load"))
                LoadCommand(command);
            else if (command == "help")
                HelpCommand();
            else if (command == "quit")
                Quit = true;
            else
                Console.WriteLine("{0} was not recognized, please try again.", command);
        }
       private static BaseGradeBook CreateGradeBook(string name, string type, bool isWeighted)
        {
            if (type.ToLower() == "standard")
            {
                return new StandardGradeBook(name);
            }
            else if (type.ToLower() == "ranked")
            {
                return new RankedGradeBook(name);
            }
            else
            {
                Console.WriteLine($"{type} is not a supported type of gradebook, please try again.");
                return null;
            }
        }
        public static BaseGradeBook CreateCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 4)
            {
                Console.WriteLine("Command not valid, Create requires a name and type of gradebook.");
                return null;
            }

          
            var name = parts[1];
            var type = parts[2];
            var isWeighted = bool.Parse(parts[3]);
            var gradeBook = CreateGradeBook(name, type, isWeighted);

            if (gradeBook != null)
            {
                Console.WriteLine($"Created gradebook {name} of type {type}.");
            }
            return gradeBook;
        }

        public static void LoadCommand(string command)
        {
            var parts = command.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Command not valid, Load requires a name.");
                return;
            }
            var name = parts[1];
            var gradeBook = BaseGradeBook.Load(name);

            if (gradeBook == null)
                return;

            GradeBookUserInterface.CommandLoop(gradeBook);
        }

        public static void HelpCommand()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  create 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook, 'Type' is what type of grading it should use, and 'Weighted' is whether or not grades should be weighted (true or false).");
            Console.WriteLine("  load 'Name' - Loads a gradebook by name.");
            Console.WriteLine("  save 'Name' - Saves a gradebook by name.");
            Console.WriteLine("  quit - Exits the application.");

        }
    }
}
