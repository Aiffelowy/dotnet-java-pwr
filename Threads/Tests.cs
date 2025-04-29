using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Testing
{
    class TestResult
    {
        long time;
        string name;
        int repeats;

        public TestResult(string name, int repeats, long avg)
        {
            this.name = name;
            this.repeats = repeats;
            this.time = avg;
        }

        public override string ToString()
        {
            return $"Test {this.name} ({this.repeats} samples): {this.time}ms average";
        }
    }

    class Timed
    {
        public long millis;
        public Timed(long time) { this.millis = time; }
        public static Timed time(Action func) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            func();
            return new Timed(watch.ElapsedMilliseconds);
        }
    }
    class Test
    {
        string test_name;
        int repeats;
        Func<Random, Timed> test_func;
       
        public TestResult run()
        {
            Random rng = new Random();
            long avg = 0;
            for(int i = 0; i < this.repeats; i++)
            {
                avg += this.test_func(rng).millis;
            }

            return new TestResult(this.test_name, this.repeats, avg / this.repeats);
        }

        public Test(Func<Random, Timed> test_func, string name, int repeats)
        {
            this.test_name = name;
            this.repeats = repeats;
            this.test_func = test_func;
        }
    }
}
