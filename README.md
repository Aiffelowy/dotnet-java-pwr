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
