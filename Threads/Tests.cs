using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Testing;

namespace Testing {
readonly struct Timed {
  readonly public long time_taken;
  private Timed(long time) { this.time_taken = time; }
  public static Timed time(Action func) {
    var watch = System.Diagnostics.Stopwatch.StartNew();
    func();
    watch.Stop();
    return new Timed(watch.ElapsedMilliseconds);
  }
}

readonly struct TestResult {
  readonly public long[] times;
  readonly public string name;

  public TestResult(long[] times, string name) {
    this.times = times;
    this.name = name;
  }
}

class TestResults {
  TestResult[] results;
  readonly public int[] data_sizes;

  public TestResults(TestResult[] results, int[] data_sizes) {
    this.results = results;
    this.data_sizes = data_sizes;
  }
  public string to_csv() {
    string csv = "";
    int s = this.data_sizes.Count();
    for (int t = -1; t < this.results.Count(); t++) {
      for (int ds = -1; ds < s; ds++) {
        if (t == -1) {
          if (ds == -1) {
            csv += "sizes:,";
          } else {
            csv += this.data_sizes[ds] + ",";
          }
          continue;
        }

        if (ds == -1) {
          csv += this.results[t].name + ",";
          continue;
        }

        csv += this.results[t].times[ds] + ",";
      }
      csv += "\n";
    }
    return csv;
  }
}

readonly struct Test {
  readonly public string name;
  readonly Func<Random, int, Timed> test_func;

  private Test(string name, Func<Random, int, Timed> test_func) {
    this.name = name;
    this.test_func = test_func;
  }
  public static Test New(string name, Func<Random, int, Timed> test_func) {
    return new Test(name, test_func);
  }
  public Timed run(Random rng, int data_size) {
    return this.test_func(rng, data_size);
  }
}

class TestSuite
() {
  List<Test> tests = new List<Test>();
  int repeats = 1;
  List<int> ds = new List<int>();

  public static TestSuite New() { return new TestSuite(); }
  public TestSuite add_test(Test t) {
    this.tests.Add(t);
    return this;
  }
  public TestSuite set_repeats(int r) {
    this.repeats = r;
    return this;
  }
  public TestSuite data_sizes(int[] sizes) {
    this.ds = new List<int>(sizes);
    return this;
  }
  public TestResults run() {
    Random rng = new Random();
    TestResult[] results = new TestResult[this.tests.Count];

    for (int t = 0; t < this.tests.Count; t++) {
      long[] times = new long[this.ds.Count];
      for (int data_size_i = 0; data_size_i < this.ds.Count; data_size_i++) {
        long avg = 0;
        for (int r = 0; r < this.repeats; r++) {
          Console.WriteLine(
              $"Testing data size {this.ds[data_size_i]} for {this.tests[t].name}: iteration {r+1}");
          avg += this.tests[t].run(rng, this.ds[data_size_i]).time_taken;
        }
        times[data_size_i] = avg / this.repeats;
      }
      results[t] = new TestResult(times, this.tests[t].name);
    }

    return new TestResults(results, this.ds.ToArray());
  }
}
}
