package org.example.option;

public record Some<T>(T i) implements Option<T> {
  public static <T> Some<T> of(T i) {
    return new Some<>(i);
  }

  public T get() {
    return i;
  }

  @Override
  public String toString() {
    return "Some(" + i + ")";
  }
}
