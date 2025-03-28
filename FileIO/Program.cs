using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * SimpleWrite
             */
            //SimpleWrite();

            /*
             * WriteWithStreamWriter
             */
            //WriteWithStreamWriter();

            /*
             * AppendWithStreamWriter
             */
            //AppendWithStreamWriter();

            /*
             * WriteWithErrorHandling
             */
            WriteWithErrorHandling();
        }

        public static void SimpleWrite()
        {
            // Writes entire content at once
            File.WriteAllText("simple_write_example.txt", "Hello, World!\nThis is a test file.");
        }

        public static void WriteWithStreamWriter()
        {
            // Using statement ensures proper file closing
            using (StreamWriter writer = new StreamWriter("with_stream_writer_example.txt"))
            {
                writer.WriteLine("First line");
                writer.WriteLine("Second line");
                writer.Write("Third line without newline");
            }
        }

        public static void AppendWithStreamWriter()
        {
            // Using statement ensures proper file closing
            using (StreamWriter writer = new StreamWriter("with_stream_writer_example.txt", true))
            {
                writer.WriteLine("Fourth line");
                writer.WriteLine("Fifth line");
                writer.Write("Sixth line without newline");
            }
        }

        public static void WriteWithErrorHandling()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("with_error_handling_example.txt"))
                {
                    // Writing formatted data
                    string name = "John";
                    int age = 30;
                    double salary = 50000.50;

                    writer.WriteLine($"Name: {name}");
                    writer.WriteLine($"Age: {age}");
                    writer.WriteLine($"Salary: {salary:C}"); // Currency format
                }
                Console.WriteLine("File written successfully");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
            }
        }

        public static void WriteListItems()
        {
            List<string> items = new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };
            using (StreamWriter writer = new StreamWriter("list_items.txt"))
            {
                foreach (string item in items)
                {
                    writer.WriteLine(item);
                }
            }
    }

    
}
