using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP1551_CO1106
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Example 1: Output
             * See more: https://www.w3schools.com/cs/cs_output.php
             */

            //Console.WriteLine("Hello World!");
            //Console.WriteLine(2025);
            //Console.WriteLine("Hello World! Hello " + 2025 + ".");
            //Console.WriteLine($"sum(3+4) = {3+4}");

            /*
             * Example 2: Variables
             * See more: https://www.w3schools.com/cs/cs_variables.php
             */

            //int x = 3;
            //int y = 4;

            //Console.WriteLine($"x={x}, y={y}, sum(x+y)={x+y}");

            /*
             * Example 3: Input & Type Casting
             * See more: https://www.w3schools.com/cs/cs_input.php
             *           https://www.w3schools.com/cs/cs_type_casting.php
             */

            //Console.WriteLine("What is your name? ");
            //string name = Console.ReadLine();
            //Console.WriteLine($"Hello {name}!");

            //Console.WriteLine("What is your age? ");
            //string ageString = Console.ReadLine();
            //int age = int.Parse(ageString);

            //Console.WriteLine($"You are {age} years old.");

            //Console.WriteLine($"I guess you were born in {2025 - age}.");

            /*
             * Example 4: Booleans
             * See more: https://www.w3schools.com/cs/cs_booleans.php
             */

            //int x = 3;
            //int y = 4;

            //Console.WriteLine($" x == y :{ x == y }");
            //Console.WriteLine($" x != y :{ x != y }");
            //Console.WriteLine($" x < y  :{ x < y  }");
            //Console.WriteLine($" x > y  :{ x > y  }");
            //Console.WriteLine($" x <= y :{ x <= y }");
            //Console.WriteLine($" x >= y :{ x >= y }");

            /*
             * Example 5: If...Else
             * See more: https://www.w3schools.com/cs/cs_conditions_elseif.php
             */

            //int x = 4;
            //int y = 4;

            //if (x < y)
            //{
            //    Console.WriteLine($"x is less than y");
            //}
            //else if (x > y)
            //{
            //    Console.WriteLine($"x is greater than y");
            //}
            //else
            //{
            //    Console.WriteLine($"x is equal to y");
            //}

            /*
             * Example 6: Switch
             * See more: https://www.w3schools.com/cs/cs_switch.php
             */

            //Console.WriteLine("What's your favorite day of the week? (1-7)");
            //string dayString = Console.ReadLine();
            //int day = int.Parse(dayString);

            //switch (day)
            //{
            //    case 1:
            //        Console.WriteLine("Monday");
            //        break;
            //    case 2:
            //        Console.WriteLine("Tuesday");
            //        break;
            //    case 3:
            //        Console.WriteLine("Wednesday");
            //        break;
            //    case 4:
            //        Console.WriteLine("Thursday");
            //        break;
            //    case 5:
            //        Console.WriteLine("Friday");
            //        break;
            //    case 6:
            //        Console.WriteLine("Saturday");
            //        break;
            //    case 7:
            //        Console.WriteLine("Sunday");
            //        break;
            //    default:
            //        Console.WriteLine("Invalid day");
            //        break;
            //}

            /*
             * Example 7: While Loop
             * See more: https://www.w3schools.com/cs/cs_while_loop.php
             */

            //int i = 0;

            //while (i < 10)
            //{
            //    Console.WriteLine($"{i}: Hello World!");
            //    i = i + 1; // =~ i++;
            //}

            //string name = " ";
            //while (name != "exit")
            //{
            //    Console.WriteLine("What's your name? ");
            //    name = Console.ReadLine();
            //    Console.WriteLine($"Hello {name}!");
            //}

            //while (true)
            //{
            //    Console.WriteLine("What's your name? ");
            //    string name = Console.ReadLine();

            //    if (name == "exit")
            //    {
            //        Console.WriteLine("Goodbye!");
            //        break;
            //    }

            //    Console.WriteLine($"Hello {name}!");
            //}

            /*
             * Example 8: For Loop
             * See more: https://www.w3schools.com/cs/cs_for_loop.php
             */

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("Hello World.");

            //    if (i == 2)
            //    {
            //        break;
            //    }
            //}

            
        }
    }
}
