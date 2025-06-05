package org.example;

import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.control.TextFormatter;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class ThresholdMenuController implements WindowTrait {
  @FXML
  private TextField value;
  @FXML
  private VBox container;

  private ImageView view;
  private Image img;
  Stage stage;

  boolean error = false;

  @FXML
  public void handle_thresh() {
    if (this.value.getText().isEmpty()) {
      if (error) {
        return;
      }
      var label = new Label("Field is required");
      label.setStyle("-fx-text-fill: red");
      container.getChildren().add(2, label);
      error = true;
      return;
    }

    var threshvalue = Integer.parseInt(this.value.getText());
    this.view.setImage(ImageOperations.thresh(img, threshvalue));
    this.handle_cancel();
  }

  @FXML
  public void handle_cancel() {
    stage.close();
  }

  public void init(Stage stage) {
    this.stage = stage;
    this.value.setTextFormatter(new TextFormatter<>(change -> {
      try {
        var i = Integer.parseInt(change.getControlNewText());
        return i <= 255 && i >= 0 ? change : null;
      } catch (Exception e) {
        return null;
      }
    }));
  }

  public ThresholdMenuController attach_img(Image img, ImageView view) {
    this.img = img;
    this.view = view;
    return this;
  }

  public void show_blocking() {
    stage.showAndWait();
  }
}
