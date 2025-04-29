# Matrix Class

Represents a 2D matrix and provides operations for random generation, multiplication (standard, parallel, and threaded), and formatted output.

---

## Properties

- `int[,] data`: Internal 2D array holding matrix elements

---

## Methods:
- `private Matrix(int[,] data)`: Constructs a matrix using the provided 2D array
- `static Matrix from(int[,] matrix)`: Constructs a new `Matrix` instance from a 2D integer array
- `static Matrix random(Random rng, int m, int n)`: Generates an `m x n` matrix filled with random integers between 0 and 420
- `private static (int, int) calc_block_size(int rows, int cols, int threads)`: Calculates optimal block size for threaded multiplication based on matrix size and number of threads
- `private static void mult_block(int[,] A, int[,] B, int[,] result, int row, int column, int block_rows, int block_cols)`: Computes a assigned block using standard matrix multiplication logic
- `Option<Matrix> mult(Matrix other)`: Performs standard matrix multiplication; returns `None` if dimensions are incompatible
- `Option<Matrix> mult_parallel(Matrix other, int max_threads)`: Performs parallel matrix multiplication using `Parallel.For`; limited to `max_threads` threads; returns `None` if dimensions are incompatible

- `Option<Matrix> mult_threads(Matrix other, int max_threads)`: Divides the result matrix into blocks and assigns them to `max_threads` manually created threads; returns `None` if dimensions are incompatible

- `string ToString()`: Returns a formatted string representation of the Matrix

---


## Testing result

### Two Threads

#### Normal, Parallel, and Threads Timing

| Size | Normal | Parallel | Threads |
| -------| --------| ----------| ---------|
| 50 | 0 | 3 | 11 |
| 100 | 4 | 2 | 23 |
| 150 | 15 | 8 | 15 |
| 250 | 77 | 39 | 45 |
| 500 | 629 | 327 | 318 |
| 1000 | 6183 | 2973 | 2586 |
| 2000 | 54611 | 27181 | 25559 |

---

### Four Threads

#### Parallel and Threads Timing

| Size | Parallel | Threads |
| -------| ----------| ---------|
| 50 | 1 | 11 |
| 100 | 1 | 30 |
| 150 | 4 | 19 |
| 250 | 21 | 39 |
| 500 | 182 | 173 |
| 1000 | 1587 | 1389 |
| 2000 | 14697 | 13052 |

---

### Eight Threads

#### Parallel and Threads Timing

| Size | Parallel | Threads |
| -------| ----------| ---------|
| 50 | 2 | 23 |
| 100 | 1 | 33 |
| 150 | 2 | 39 |
| 250 | 17 | 59 |
| 500 | 118 | 139 |
| 1000 | 1057 | 888 |
| 2000 | 9515 | 8252 |

---

### Sixteen Threads

#### Parallel and Threads Timing

| Size | Parallel | Threads |
| -------| ----------| ---------|
| 50 | 2 | 49 |
| 100 | 3 | 53 |
| 150 | 4 | 68 |
| 250 | 13 | 114 |
| 500 | 99 | 197 |
| 1000 | 831 | 770 |
| 2000 | 7604 | 7029 |



# ImgProcessing

Contains structures and functions for image processing, including some basic filters 

---

## Pixel

Represents a single pixel in an image.

### Properties
- `int x`: X-coordinate of the pixel
- `int y`: Y-coordinate of the pixel
- `Color color`: Color of the pixel

### Methods

- `Pixel(int x, int y, Color color)`: Constructs a pixel with coordinates and color
- `Pixel clone()`: Returns a copy of the current pixel
- `Pixel greyscale()`: Returns a grayscale version of the pixel
- `Pixel transform_color(Color new_color)`: Returns a copy of the pixel with updated color
- `Pixel transform(int x, int y)`: Returns a copy of the pixel with new coordinates

---

## BitmapInfo

Holds metadata about a `Bitmap`'s dimensions.

### Properties:

- `int width`: Width of the image
- `int height`: Height of the image

### Methods

- `static BitmapInfo from_bitmap(in Bitmap bm)`: Creates a `BitmapInfo` instance from a given `Bitmap`

---

## Filter Class

Provides image filters that can be applied to bitmaps.

### Methods

- `static Bitmap apply(Func<Pixel, BitmapInfo, Pixel> filter, Bitmap source)`: Applies a filter function to an entire bitmap
- `static Pixel grayscale(Pixel orig_pixel, BitmapInfo source_info)`: Converts a pixel to grayscale
- `static Pixel mirror(Pixel orig_pixel, BitmapInfo source_info)`: Mirrors the pixel horizontally
- `static Pixel negative(Pixel orig_pixel, BitmapInfo source_info)`: Inverts the pixels color
- `static Bitmap detect_edges(Bitmap source)`: Applies Sobel edge detection to a bitmap


# GUI
I also written a simple GUI that shows an image before and after applying the filters. All the filters are applied in parallel using `Threads`. The image can be picked using `OpenFileDialog`
