# Knapsack Problem
I implemented a greedy algorithm to solve the Knapsack Problem, where the objective is to select a subset of items with a maximum total value while keeping the total weight within a specified capacity. 

## Classes
1. Item Class

    Represents an item with a value, weight, and id.

    Properties:

        int id: Unique identifier for the item.

        int value: The value of the item.

        int weight: The weight of the item.

    Methods:

        int CompareTo(Item other): compares items based on their value-to-weight ratio

        string ToString(): Returns a formatted string with the item's value and weight.

	Implementations:
	
		IComparable<Item>: Compares items in the descending order
2. Result Class

    Stores the result of problem, including the total value, total weight, and a list of selected item IDs.

    Properties:

        int weight: Total weight of selected items.

        int value: Total value of selected items.

        List<int> items: List of IDs of selected items.

    Methods:

        string ToString(): Returns a formatted string with the total weight, total value, and the selected item IDs.

3. Problem Class

    Represents the knapsack problem, including item generation and solving logic.

    Properties:

        List<Item> item_list: List of items available for selection.

    Methods:

        Problem(int item_count, int seed): Constructor that generates a specified number of random items with values and weights, seeded for randomness.

        Result solve(int capacity): Solves the knapsack problem using a greedy approach. It sorts items by value-to-weight ratio and selects items until the total weight exceeds the given capacity.

        string ToString(): Returns a string representation of all items, including their value and weight.

## Unit Tests
I wrote the following unit tests:

- TestItemCount verifies that the number of items in the Problem object matches the specified item count.
-  TestSolver verifies that the solve method returns at least one result when solving with some sane capacity.
- TestSmallBackpack verifies that the solve method returns no items when the backpack capacity is zero.
- TestCorrectSolution verifies that the solve method returns the correct solution for a specific test case.
- TestNoItems verifies that the solve method returns an empty result when no items are available.


## GUI

I created a simple GUI using a drag and drop in visual studio designer.
For input validation I used a numeric input widget so that the user has to input a number, so theres no need to validate the string.
When the user presses the run button, the gui displays all generated items and selected items









