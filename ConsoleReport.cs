using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleReport
{
    public class ReportItem{
        public string Message;
        public ConsoleColor BackgroundColor;
        public ConsoleColor TextColor;
        public ReportItem(string message, ConsoleColor BackroungColor = ConsoleColor.Black, ConsoleColor TextColor = ConsoleColor.White){
            Message = message;
            this.BackgroundColor = BackroungColor;
            this.TextColor = TextColor;
        }
    }
    public static class ConsoleRep
    {

        private static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        private static string GenerateStarz(int length){
            var starz = "";
            for(var i = 0; i < length; i++)
                starz+="*";
            return starz;
        }
        private static string GenerateSpace(int length){
            var space = "";
            for(var i = 0; i < length; i++)
                space+=" ";
            return space;
        }
        public static void Log(string data, ConsoleColor color = ConsoleColor.Black, ConsoleColor TextColor = ConsoleColor.White)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = TextColor;
            var maxLength = (data.Length);
            var outputString = "";
            var border =  GenerateStarz(maxLength+4) + "\n";
            outputString += border;
            outputString += $"* {data} *\n";
            outputString += border;
            // System.Console.WriteLine(outputString);
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            // Console.ReadKey();
        }

        public static void Log(string[] data, ConsoleColor color = ConsoleColor.Black, ConsoleColor TextColor = ConsoleColor.White){
            Log(data.ToList(), color, TextColor);
        }
        public static void Log(List<string> data, ConsoleColor color = ConsoleColor.Black, ConsoleColor TextColor = ConsoleColor.White)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = TextColor;
            var maxLength = (data.OrderByDescending( s => s.Length ).First().Length);
            var outputString = "";
            var border =  GenerateStarz(maxLength+4) + "\n";
            outputString += border;
        
            foreach(var line in data){
                var extraspace = maxLength - line.Length;
                outputString += $"* {line + GenerateSpace(extraspace)} *\n";
            }
            outputString += border;
            System.Console.WriteLine(outputString);
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            // Console.ReadKey();
        }
    
        public static void Log(List<ReportItem> data, ConsoleColor BackgroundColor = ConsoleColor.Black, ConsoleColor TextColor = ConsoleColor.White)
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            var maxLength = (data.OrderByDescending( s => s.Message.Length ).First().Message.Length);
            var border =  GenerateStarz(maxLength+4);
             System.Console.WriteLine(border);
        
            foreach(var line in data){
                Console.BackgroundColor = line.BackgroundColor;
                Console.ForegroundColor = line.TextColor;
                var extraspace = maxLength - line.Message.Length;
                System.Console.WriteLine($"* {line.Message + GenerateSpace(extraspace)} *");
                Console.ResetColor();
            }
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = TextColor;
            System.Console.WriteLine(border);
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Black;
            // Console.ReadKey();
        }
    }
}