# Knapsack Problem

I implemented a greedy algorithm to solve the Knapsack Problem, where the objective is to select a subset of items with a maximum total value while keeping the total weight within a specified capacity.

## Classes

1. **Item Record**

    Represents an item with a value and weight.

    Properties:

    - `int value`: The value of the item.
    - `int weight`: The weight of the item.

    Methods:

    - `String toString()`: Returns a formatted string with the item's weight, value, and value-to-weight ratio.

2. **ProblemResult Record**

    Stores the result of the problem, including the total value, total weight, and a list of selected item ids.

    Properties:

    - `Vector<Integer> item_ids`: List of ids of selected items.
    - `int item_count`: Number of items selected.
    - `int sum_weight`: Total weight of selected items.
    - `int sum_value`: Total value of selected items.

    Methods:

    - `String toString()`: Returns a formatted string with the total weight, total value, and selected item ids.

3. **Problem Class**

    Represents the knapsack problem, including item generation and solving logic.

    Properties:

    - `Option<Integer> seed`: Optional seed for reproducible item generation.
    - `Vector<Item> items`: List of items available for selection.
    - `int lower`: Lower bound for randomly generated item values and weights.
    - `int upper`: Upper bound for randomly generated item values and weights.

    Constructors:

    - `Problem(int lower, int upper)`: Creates a new problem instance without a fixed seed.
    - `Problem(int lower, int upper, int seed)`: Creates a new problem instance with a fixed seed.

    Methods:

    - `void generate(int n)`: Generates `n` random items with weights and values between the lower and upper bounds.
    - `ProblemResult solve(int capacity)`: Solves the knapsack problem using a greedy algorithm. Items are sorted by value-to-weight ratio, and selected until the capacity is exhausted.
    - `String toString()`: Returns a string representation of the problem configuration and the generated items.

## Unit Tests

The following unit tests were written:

- **TestItemCount**: Verifies that the number of items generated matches the specified number.
- **TestSolver**: Verifies that the `solve()` method returns a valid solution for a reasonable capacity.
- **TestSmallBackpack**: Verifies that no items are selected when the backpack capacity is zero.
- **TestCorrectSolution**: Verifies that the algorithm returns the expected result for a specific input case.
- **TestNoItems**: Verifies that the solver returns an empty result when no items are available.


## Unit Tests

The following unit tests were written:

- **test_at_least_one**: Ensures that the algorithm selects at least one item when the capacity is sufficient.  

- **test_no_solution**: Verifies that no items are selected when the knapsack capacity is zero.  

- **test_correct_capacity**: Confirms that the total weight of selected items does not exceed the knapsack’s capacity.  

- **test_rng**: Validates that item generation respects the specified lower and upper bounds for values and weights.  

- **test_correct_solution**: Verifies that the algorithm returns the correct solution with a deterministic seed.  
