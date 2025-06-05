package org.example;

import java.util.Vector;

public record ProblemResult(Vector<Integer> item_ids, int item_count,
    int sum_weight, int sum_value) {

  @Override
  public String toString() {
    return String.format("items in the backpack: %d  with combined value: %d "
        + "and weight: %d\n item ids: %s",
        this.item_count, this.sum_value, this.sum_weight,
        this.item_ids);
  }
}
