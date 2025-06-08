# Image Processing App

## Classes

1. **AppController**

    Main class for handling javafx events.

    - `public void init(Stage stage)` - initializes the stage property
    - `private void resize_view` - not used (not working)
    - `public void handle_load_image()` - a handler for loading new images
    - `public void handle_execute()` - a handler for selecting and executing operations on an image
    - `public void handle_save()` - a handler for saving images
    - `public void handle_resize()` - a handler for resizing images
    - `public void handle_rotate_left()` - a handler for rotating images left
    - `public void handle_rotate_right()` - a handler for rotating images right
    - `public void process_threads()` - a function that executes all operations on an image at the same time using threads
    - `public void toggle_multiview()` - a function that toggles between two image view modes

2. **WindowTrait**
    A trait that defines a basic modal window
    requires implementing `void init(Stage)` and `void show_blocking()`

3. **ModalWindowBuilder<Window extends WindowTrait>**

    A helper class for instantiating modal windows

    - `ModalWindowBuilder(String resource)` - a constructor that takes a path for the fxml resource
    - `public ModalWindowBuilder<Window> title(String)` - sets the window title
    - `public ModalWindowBuilder<Window> owner(Stage)` - sets the stage owner
    - `public Result<Window> build(Supplier<Window> clazz)` - builds the window and calls init on a created controller

4. **ChangeMenuController**

    A class for handling events from the resize menu

     - `public void handle_change()` - a handler that reads inputed dimensions and rescales the image
     - `public void handle_revert()` - a handler that reverts the image to its original size
     - `public void handle_cancel()` - closes the window
     - `public void init(Stage stage)` - displays the save menu window and blocks the current thread until the window is closed.
     - `public ChangeMenuController attach_img(Image image, Image orig, ImageView view)` - sets up necessary references

5. **SaveMenuController**

    A class for handling events from the save menu

   - `public void handle_save()`  
     handles saving the current image to the user's `Pictures` directory. 

   - `public void handle_cancel()`  
     closes the save menu window.

   - `public void init(Stage stage)`  
     initializes the controller with a `Stage` reference and sets up a text formatter

   - `public void show_blocking()`  
     displays the save menu window and blocks the current thread until the window is closed.

   - `public SaveMenuController attach_img(Image img)`  
     attaches an image reference to be saved.

6. **ThresholdMenuController**

    A class for handling threshold operations on an image through a user-input menu.

   - `public void handle_thresh()`  
     applies a threshold filter to the attached image using the input value

   - `public void handle_cancel()`  
     closes the threshold menu window.

   - `public void init(Stage stage)`  
     sets up the stage and a text formatter

   - `public ThresholdMenuController attach_img(Image img, ImageView view)` - sets up necessary references

   - `public void show_blocking()`  
     displays the threshold menu window and blocks the current thread until the window is closed.

7. **Toast**
    a class for creating toasts

    - `private static void show(Stage stage, String msg, int duration, String style)` - creates and displays a toast with a provided style
    - `public static void normal(Stage stage, String msg, int duration)` - creates and displays a preformatted toast (normal style)
    - `public static void warning(Stage stage, String msg, int duration)`- creates and displays a preformatted toast (warning style)
    - `public static void alert(Stage stage, String msg, int duration)`- creates and displays a preformatted toast (error style)


8. **ImageOperations**
    A class that can apply operations to images
    
    - `public static Image scale(Image in, int new_x, int new_y)` - scales the image
    - `public static Image rotate(Image in, int angle)` - rotates the image
    - `public static Image negative(Image in)` - applies a negative filter
    - `public static Image thresh(Image in, Integer value)` - applies a threshold operation on an Image
    - `public static Image edge_detection` - run a simple edge detection on an Image

9. **MyLogger**
    A class used for logging. Creates a static kava.util.logging.Logger with a simple formatter. Outputs to app.log

    - `public static Logger logger()` - returns the static logger reference



