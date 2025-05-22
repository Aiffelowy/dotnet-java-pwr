# Blazor

I added Authentication Certificates to Program.cs so blazor can authenticate user sessions.

# Weather.razor

I added a counter that displays a number of warm days (> 15*C) using LINQ functions.


## Methods

---

### `filter_warm_days()`

Filters the forecast list to show only warm days (> 15*C).

### `restore_forecasts()`

Removes any filter that was applied to the forecasts array.


### `filter(ChangeEventArgs arg)`

Filters the forecasts based on input from the form using LINQ `Where` and `Contains`.
