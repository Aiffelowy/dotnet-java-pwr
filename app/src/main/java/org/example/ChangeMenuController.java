package org.example;

import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.control.TextFormatter;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class ChangeMenuController implements WindowTrait {
  @FXML
  private TextField width_input;
  @FXML
  private TextField height_input;
  @FXML
  private VBox container;

  private ImageView view;

  Stage stage;
  Image img;
  Image orig;

  Boolean error1 = false;
  Boolean error2 = false;

  int origx;
  int origy;

  @FXML
  public void handle_change() {
    if (width_input.getText().isEmpty()) {
      if (error1) {
        return;
      }
      var label = new Label("Field is required");
      label.setStyle("-fx-text-fill: red");
      container.getChildren().add(2, label);
      error1 = true;
      return;
    }

    if (height_input.getText().isEmpty()) {
      if (error2) {
        return;
      }
      var label = new Label("Field is required");
      label.setStyle("-fx-text-fill: red");
      container.getChildren().add(5, label);
      error2 = true;
      return;
    }

    var new_x = Integer.parseInt(width_input.getText());
    var new_y = Integer.parseInt(height_input.getText());
    img = ImageOperations.scale(img, new_x, new_y);
    view.setImage(img);

    this.handle_cancel();
  }

  @FXML
  public void handle_revert() {
    view.setImage(this.orig);
    this.handle_cancel();
  }

  @FXML
  public void handle_cancel() {
    stage.close();
  }

  public void init(Stage stage) {
    this.stage = stage;

    this.height_input.setTextFormatter(new TextFormatter<String>(change -> {
      try {
        var i = Integer.parseInt(change.getControlNewText());
        return i < 3000 && i > 0 ? change : null;
      } catch (Exception e) {
        return null;
      }
    }));

    this.width_input.setTextFormatter(new TextFormatter<String>(change -> {
      try {
        var i = Integer.parseInt(change.getControlNewText());
        return i < 3000 && i > 0 ? change : null;
      } catch (Exception e) {
        return null;
      }
    }));
  }

  public void show_blocking() {
    stage.showAndWait();
  }

  public ChangeMenuController attach_img(Image img, Image orig,
      ImageView view) {
    this.img = img;
    this.view = view;
    this.orig = orig;
    return this;
  }
}
