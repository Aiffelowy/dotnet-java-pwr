package org.example.result;

public record Ok<T>(T value) implements Result<T> {
  public static <T> Ok<T> of(T i) {
    return new Ok<>(i);
  }

  public T get() {
    return value;
  }

  @Override
  public String toString() {
    return "OK(" + value + ")";
  }
}
