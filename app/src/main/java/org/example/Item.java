package org.example;

public record Item(int value, int weight) {
  @Override
  public String toString() {
    return String.format("weight: %d value: %d    thing: %.2f", this.weight,
        this.value, (double) this.value() / this.weight());
  }
}
