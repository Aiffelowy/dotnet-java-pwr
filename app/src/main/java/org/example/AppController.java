package org.example;

import java.util.Vector;
import java.util.logging.Level;
import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.VBox;
import javafx.stage.FileChooser;
import javafx.stage.Stage;
import org.example.result.*;

public class AppController {
  @FXML
  private ComboBox<String> operation_list;
  @FXML
  private ImageView original_img;
  @FXML
  private ImageView processed_img;
  @FXML
  private Button execute_button;
  @FXML
  private Button save_button;
  @FXML
  private Button resize_button;
  @FXML
  private Button rotate_left;
  @FXML
  private Button rotate_right;
  @FXML
  private VBox orig_img_container;
  @FXML
  private VBox processed_img_container;
  @FXML
  private VBox processed_img_container_threads;

  @FXML
  private ImageView processed_img_negative;
  @FXML
  private ImageView processed_img_tresh;
  @FXML
  private ImageView processed_img_edge;

  private Stage stage;
  private Image img;
  private Image preview_img;
  private Image processed_image = null;

  public void init(Stage stage) {
    this.stage = stage;
    // this.original_img.fitHeightProperty().bind(
    // ((VBox) this.original_img.getParent()).heightProperty());

    /*
     * orig_img_container.heightProperty().addListener(
     * (obs, old, newv) -> this.resize_view(orig_img_container, original_img));
     * processed_img_container.heightProperty().addListener(
     * (obs, old,
     * newv) -> this.resize_view(processed_img_container, processed_img));
     */
  }

  private void resize_view(VBox container, ImageView view) {
    Image img = original_img.getImage();
    if (img == null) {
      return;
    }
    System.out.println("RESIZING");
    double nh = img.getHeight();
    double ch = container.getHeight();

    if (nh <= ch) {
      view.setFitHeight(0);
    } else {
      view.setFitHeight(ch);
    }
  }

  @FXML
  public void handle_load_image() {
    var chooser = new FileChooser();
    chooser.setTitle("Select an image file");
    chooser.getExtensionFilters().add(
        new FileChooser.ExtensionFilter("JPG Files (*.jpg)", "*.jpg"));

    var selected = chooser.showOpenDialog(stage);
    if (selected == null) {
      return;
    }

    if (!selected.getName().toLowerCase().endsWith(".jpg")) {
      Toast.warning(stage, "Selected image is not a jpg", 3000);
      return;
    }

    try {
      this.img = new Image(selected.toURI().toString());
      this.original_img.setImage(this.img);
      this.preview_img = new Image(selected.toURI().toString());
      this.processed_img.setImage(null);
      this.processed_image = null;
      this.operation_list.setDisable(false);
      this.execute_button.setDisable(false);
      this.resize_button.setDisable(false);
      this.rotate_left.setDisable(false);
      this.rotate_right.setDisable(false);
      this.save_button.setDisable(false);

      Toast.normal(stage, "Image successfuly loaded!", 2000);
      MyLogger.logger().info("Image successfuly loaded!");

    } catch (Exception e) {
      Toast.alert(stage, "Error loading image", 4000);
      MyLogger.logger().log(Level.SEVERE, "Error loading image");
      return;
    }
  }

  @FXML
  public void handle_execute() {
    var operation = operation_list.getValue();
    if (operation == null) {
      Toast.alert(stage, "No operation selected!", 3000);
      return;
    }
    switch (operation) {
      case "Negative" -> {
        this.toggle_multiview(false);
        this.processed_image = ImageOperations.negative(this.preview_img);
        Toast.normal(stage, "Negative was generated successfully!", 3000);
        MyLogger.logger().info("Negative was generated successfully!");
      }
      case "Threshold" -> {
        this.toggle_multiview(false);
        var window = new ModalWindowBuilder<ThresholdMenuController>(
            "/threshold_menu.fxml")
            .title("Threshold Menu")
            .build(ThresholdMenuController::new);
        switch (window) {
          case Ok<ThresholdMenuController> ok -> {
            ok.get()
                .attach_img(this.preview_img, this.processed_img)
                .show_blocking();
            this.processed_image = this.processed_img.getImage();
            Toast.normal(stage, "Threshold was generated successfully!", 3000);
            MyLogger.logger().info("Threshold was generated successfully!");
          }
          case Err<?> e -> {
            System.out.println(e);
            return;
          }
        }
      }
      case "Edge Detection" -> {
        this.toggle_multiview(false);
        this.processed_image = ImageOperations.edge_detection(this.preview_img);
        Toast.normal(stage, "Edge detection run successfuly!", 3000);
        MyLogger.logger().info("Edge detection run successfuly!");
      }
      case "All" -> {
        this.toggle_multiview(true);
        this.process_threads();
      }
      default -> {
        return;
      }
    }

    this.processed_img.setImage(this.processed_image);
  }

  @FXML
  public void handle_save() {
    var window = new ModalWindowBuilder<SaveMenuController>("/save_menu.fxml")
        .title("Save Image")
        .build(SaveMenuController::new);

    switch (window) {
      case Ok<SaveMenuController> ok -> ok.get()
          .attach_img(this.processed_image)
          .show_blocking();
      case Err<?> e -> {
        System.out.println(e);
        return;
      }
    }
  }

  @FXML
  public void handle_resize() {
    var window = new ModalWindowBuilder<ChangeMenuController>("/resize_menu.fxml")
        .title("Resize Image")
        .build(ChangeMenuController::new);

    switch (window) {
      case Ok<ChangeMenuController> ok -> ok.get()
          .attach_img(this.preview_img, this.img, this.original_img)
          .show_blocking();
      case Err<?> e -> {
        System.out.println(e);
        return;
      }
    }

    this.preview_img = this.original_img.getImage();
  }

  @FXML
  public void handle_rotate_left() {
    this.preview_img = ImageOperations.rotate(this.preview_img, -90);
    this.original_img.setImage(this.preview_img);
  }

  @FXML
  public void handle_rotate_right() {
    this.preview_img = ImageOperations.rotate(this.preview_img, 90);
    this.original_img.setImage(this.preview_img);
  }

  private void process_threads() {
    var threads = new Vector<Thread>();
    threads.add(new Thread(() -> {
      var result = ImageOperations.negative(this.preview_img);
      Platform.runLater(() -> {
        this.processed_img_negative.setImage(result);
      });
    }));

    threads.add(new Thread(() -> {
      var result = ImageOperations.thresh(this.preview_img, 128);
      Platform.runLater(() -> {
        this.processed_img_tresh.setImage(result);
      });
    }));

    threads.add(new Thread(() -> {
      var result = ImageOperations.edge_detection(this.preview_img);
      Platform.runLater(() -> {
        this.processed_img_edge.setImage(result);
      });
    }));

    for (var thread : threads) {
      thread.start();
    }
  }

  private void toggle_multiview(boolean b) {
    this.processed_img_container_threads.setVisible(b);
    this.processed_img_container_threads.setManaged(b);
    this.processed_img_container.setVisible(!b);
    this.processed_img_container.setManaged(!b);
  }
}
