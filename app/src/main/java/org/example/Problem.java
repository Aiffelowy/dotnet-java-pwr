package org.example;

import java.util.Random;
import java.util.Vector;
import org.example.option.*;

public class Problem {
  Option<Integer> seed;
  Vector<Item> items = new Vector<>();
  int upper;
  int lower;

  public Problem(int lower, int upper) {
    this.seed = None.of();
    this.lower = lower;
    this.upper = upper;
  }

  public Problem(int lower, int upper, int seed) {
    this.seed = Some.of(seed);
    this.lower = lower;
    this.upper = upper;
  }

  public void generate(int n) {
    var rng = switch (seed) {
      case Some<Integer> s -> new Random(s.get());
      case None<?> _n -> new Random();
    };

    for (; n > 0; n--) {
      var w = rng.nextInt(this.lower, this.upper);
      var v = rng.nextInt(this.lower, this.upper);
      this.items.add(new Item(v, w));
    }
  }

  public ProblemResult solve(int capacity) {
    this.items.sort((a, b) -> {
      return Double.compare((double) b.value() / b.weight(), (double) a.value() / a.weight());
    });

    Vector<Integer> ids = new Vector<>();
    int sweight = 0;
    int svalue = 0;

    while (capacity > 0) {
      var found = false;
      for (int i = 0; i < this.items.size(); i++) {
        var item = this.items.get(i);

        if (item.weight() > capacity)
          continue;

        ids.add(i);
        sweight += item.weight();
        svalue += item.value();
        capacity -= item.weight();
        found = true;
        break;
      }

      if (!found)
        break;
    }

    return new ProblemResult(ids, ids.size(), sweight, svalue);
  }

  @Override
  public String toString() {
    var s = String.format("seed: %s   lower bound: %d   upper bound: %s\n",
        this.seed, this.lower, this.upper);
    for (Item item : items) {
      s += item + "\n";
    }
    return s;
  }
}
