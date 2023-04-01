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
       private static BaseGradeBook CreateGradeBook(string name, string type)
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
            if (parts.Length != 3)
            {
                Console.WriteLine("Command not valid, Create requires a name and type of gradebook.");
                return null;
            }
          
            var name = parts[1];
            var type = parts[2];
            var gradeBook = CreateGradeBook(name, type);

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
            //Console.WriteLine($"Create 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook, 'Type' is what type of grading it should use, and 'Weighted' is whether or not grades should be weighted (true or false).");
           
            Console.WriteLine("Commands:");
            Console.WriteLine("Create 'Name' 'Type' - Creates a new gradebook where 'Name' is the name of the gradebook and 'Type' is what type of grading it should use.");
            Console.WriteLine("Add 'Name' 'Grade' - Adds a grade to the gradebook where 'Name' is the name of the gradebook and 'Grade' is the grade to be added.");
            Console.WriteLine("Remove 'Name' 'Grade' - Removes a grade from the gradebook where 'Name' is the name of the gradebook and 'Grade' is the grade to be removed.");
            Console.WriteLine("Stats 'Name' - Prints statistics for the gradebook where 'Name' is the name of the gradebook.");
            Console.WriteLine("Exit - Exits the program.");

        }
    }
}
