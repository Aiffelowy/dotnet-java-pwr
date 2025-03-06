using System.Collections.Generic;

namespace fizzbuzz
{
    public class FizzBuzz
    {
        //variables
        private static Dictionary<int, string> conditions = new() { { 3, "Fizz" }, { 5, "Buzz" } };
        private int count;
        //constructor
        public FizzBuzz(int i) { this.count = i; }
        //functions
        private string fizzbuzz(int i)
        {
            string result = "";
            foreach (var (key, value) in conditions)
            {
                if (i % key != 0)
                    continue;

                result += value;
            }

            if (result == "")
                result = i.ToString();

            return result;
        }

        public void run()
        {
            for (int i = 1; i < this.count; i++)
            {
                Console.WriteLine(this.fizzbuzz(i));
            }
        }

    };

}
