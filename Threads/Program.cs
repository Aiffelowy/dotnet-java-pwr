// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Errors;
using Testing;

// WAY TO LAZY to rewrite tests that have multiple custom parameters
class Skibidi {
  public static Timed test_single(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult(matrix2); });
  }
  public static Timed test_parallel2(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_parallel(matrix2, 2); });
  }

  public static Timed test_parallel4(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_parallel(matrix2, 4); });
  }

  public static Timed test_parallel8(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_parallel(matrix2, 8); });
  }
  public static Timed test_parallel16(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_parallel(matrix2, 16); });
  }
  public static Timed test_threads2(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_threads(matrix2, 2); });
  }

  public static Timed test_threads4(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_threads(matrix2, 4); });
  }
  public static Timed test_threads8(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_threads(matrix2, 8); });
  }
  public static Timed test_threads16(Random rng, int data_size) {
    var matrix1 = Matrix.random(rng, data_size, data_size);
    var matrix2 = Matrix.random(rng, data_size, data_size);
    return Timed.time(() => { matrix1.mult_threads(matrix2, 16); });
  }
}

class Matrix {
  private int[,] data;
  private Matrix(int[,] data) { this.data = data; }

  public static Matrix from(int[,] matrix) { return new Matrix(matrix); }

  private static (int, int) calc_block_size(int rows, int cols, int threads) {
    int elements = rows * cols;

    if (elements <= 64 || rows < 9 || cols < 9)
      return (rows, cols);

    int blocks = threads * 4;
    int area = Math.Max(1, elements / blocks);
    double aspect_ratio = (double)rows / cols;
    double sqrt_area = Math.Sqrt(area);

    int rows_new = (int)(sqrt_area * Math.Sqrt(aspect_ratio));
    int cols_new = (int)(sqrt_area / Math.Sqrt(aspect_ratio));

    return (Math.Clamp(rows_new, 4, rows), Math.Clamp(cols_new, 4, cols));
  }
  private static void mult_block(int[,] A, int[,] B, int[,] result, int row,
                                 int column, int block_rows, int block_cols) {
    int rA = A.GetLength(0);
    int cA = A.GetLength(1);
    int cB = B.GetLength(1);

    int row_end = Math.Min(row + block_rows, rA);
    int col_end = Math.Min(column + block_cols, cB);

    for (int i = row; i < row_end; i++) {
      for (int j = column; j < col_end; j++) {
        int sum = 0;
        for (int k = 0; k < cA; k++) {
          sum += A[i, k] * B[k, j];
        }
        result[i, j] = sum;
      }
    }
  }

  public static Matrix random(Random rng, int m, int n) {
    var matrix = new int[m, n];

    for (int i = 0; i < m; i++) {
      for (int j = 0; j < n; j++) {
        matrix[i, j] = rng.Next(0, 420);
      }
    }

    return new Matrix(matrix);
  }

  public Option<Matrix> mult(Matrix other) {
    var rA = this.data.GetLength(0);
    var cA = this.data.GetLength(1);
    var rB = other.data.GetLength(0);
    var cB = other.data.GetLength(1);

    if (cA != rB)
      return Option.None<Matrix>();

    var new_matrix = new int[rA, cB];

    for (int x = 0; x < rA; x++) {
      for (int i = 0; i < cB; i++) {
        var temp = 0;
        for (int j = 0; j < cA; j++) {
          temp += this.data[x, j] * other.data[j, i];
        }
        new_matrix[x, i] = temp;
      }
    }

    return Matrix.from(new_matrix).as_some();
  }

  public Option<Matrix> mult_parallel(Matrix other, int max_threads) {
    var rA = this.data.GetLength(0);
    var cA = this.data.GetLength(1);
    var rB = other.data.GetLength(0);
    var cB = other.data.GetLength(1);

    if (cA != rB)
      return Option.None<Matrix>();

    ParallelOptions opts =
        new ParallelOptions() { MaxDegreeOfParallelism = max_threads };
    var new_matrix = new int[rA, cB];

    Parallel.For(0, rA, opts, x => {
      for (int i = 0; i < cB; i++) {
        var temp = 0;
        for (int j = 0; j < cA; j++) {
          temp += this.data[x, j] * other.data[j, i];
        }
        new_matrix[x, i] = temp;
      }
    });

    return Matrix.from(new_matrix).as_some();
  }

  public Option<Matrix> mult_threads(Matrix other, int max_threads) {
    var rA = this.data.GetLength(0);
    var cA = this.data.GetLength(1);
    var rB = other.data.GetLength(0);
    var cB = other.data.GetLength(1);

    if (cA != rB)
      return Option.None<Matrix>();

    var new_matrix = new int[rA, cB];

    var blocks = new List<(int, int)>();
    var (block_rows, block_cols) = Matrix.calc_block_size(rA, cB, max_threads);
    for (int i = 0; i < rA; i += block_rows) {
      for (int j = 0; j < cB; j += block_cols) {
        blocks.Add((i, j));
      }
    }

    // var thread_queues =
    //    Enumerable.Range(0, max_threads).Select(queue => new Queue<(int,
    //    int)>()).ToList();

    // for (int i = 0; i < blocks.Count; i++)
    //{
    //     thread_queues[i % max_threads].Enqueue(blocks[i]);
    // }

    var next_block = -1;

    var threads =
        Enumerable.Range(0, max_threads)
            .Select(thread => new Thread(() => {
                      // var queue = thread_queues[thread];
                      int index;
                      // while (queue.TryDequeue(out var block))
                      while ((index = Interlocked.Increment(ref next_block)) <
                             blocks.Count) {
                        var (row, column) = blocks[index];
                        // var (row, column) = block;
                        Matrix.mult_block(this.data, other.data, new_matrix,
                                          row, column, block_rows, block_cols);
                      }
                    }))
            .ToList();
    threads.ForEach(thread => thread.Start());
    threads.ForEach(thread => thread.Join());

    return Matrix.from(new_matrix).as_some();
  }

  public override string ToString() {
    string result = "";
    int rows = this.data.GetLength(0);
    int cols = this.data.GetLength(1);

    int[] cols_widths = new int[cols];
    for (int j = 0; j < cols; j++) {
      int max_width = 0;
      for (int i = 0; i < rows; i++) {
        int len = this.data[i, j].ToString().Length;
        if (len > max_width)
          max_width = len;
      }
      cols_widths[j] = max_width;
    }

    for (int i = 0; i < rows; i++) {
      result += "| ";
      for (int j = 0; j < cols; j++) {
        string value = this.data[i, j].ToString();
        int width = cols_widths[j];

        int left_padding = ((width - value.Length) / 2);
        int right_padding = (width - value.Length - left_padding);

        result += new string(' ', left_padding);
        result += value;
        result += new string(' ', right_padding) + ' ';
      }
      result += "|\n";
    }

    return result;
  }
}

class MyApp {
  public static void Main(string[] args) {
    var suite2 =
        TestSuite.New()
            .set_repeats(6)
            .data_sizes(new int[] { 50, 100, 150, 250, 500, 1000, 2000 })
            .add_test(Test.New("normal", Skibidi.test_single))
            .add_test(Test.New("parallel", Skibidi.test_parallel2))
            .add_test(Test.New("threads", Skibidi.test_threads2));

    var suite4 =
        TestSuite.New()
            .set_repeats(6)
            .data_sizes(new int[] { 50, 100, 150, 250, 500, 1000, 2000 })
            .add_test(Test.New("parallel", Skibidi.test_parallel4))
            .add_test(Test.New("threads", Skibidi.test_threads4));

    var suite8 =
        TestSuite.New()
            .set_repeats(6)
            .data_sizes(new int[] { 50, 100, 150, 250, 500, 1000, 2000 })
            .add_test(Test.New("parallel", Skibidi.test_parallel8))
            .add_test(Test.New("threads", Skibidi.test_threads8));

    var suite16 =
        TestSuite.New()
            .set_repeats(6)
            .data_sizes(new int[] { 50, 100, 150, 250, 500, 1000, 2000 })
            .add_test(Test.New("parallel", Skibidi.test_parallel16))
            .add_test(Test.New("threads", Skibidi.test_threads16));

    var results2 = suite2.run();
    var results4 = suite4.run();
    var results8 = suite8.run();
    var results16 = suite16.run();

    Console.WriteLine("TWO THREADS");
    Console.WriteLine(results2.to_csv());

    Console.WriteLine("FOUR THREADS");
    Console.WriteLine(results4.to_csv());

    Console.WriteLine("EIGHT THREADS");
    Console.WriteLine(results8.to_csv());

    Console.WriteLine("SIXTEEN THREADS");
    Console.WriteLine(results16.to_csv());
  }
}
