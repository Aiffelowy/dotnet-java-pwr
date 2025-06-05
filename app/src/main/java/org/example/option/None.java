
package org.example.option;

public record None<T>() implements Option<T> {
  public static <T> None<T> of() {
    return new None<>();
  }

  @Override
  public String toString() {
    return "None";
  }
}
