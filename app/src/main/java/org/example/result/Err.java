package org.example.result;

public record Err<T>(Exception error) implements Result<T> {
  public static <T> Err<T> of(Exception e) {
    return new Err<T>(e);
  }

  @Override
  public String toString() {
    return "Err(" + error + ")";
  }
}
