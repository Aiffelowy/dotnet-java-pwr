package org.example;

import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.image.BufferedImage;
import java.nio.file.Paths;
import java.util.logging.Level;
import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.control.TextFormatter;
import javafx.scene.image.Image;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import javax.imageio.ImageIO;

public class SaveMenuController implements WindowTrait {
  @FXML
  private TextField input;
  @FXML
  private VBox container;

  Stage stage;
  Image img;

  Boolean error = false;

  @FXML
  public void handle_save() {
    if (img == null) {
      Toast.warning(stage, "Image was not processed", 3000);
      return;
    }

    var name = input.getText();
    if (name.length() < 3) {
      if (error) {
        return;
      }
      var label = new Label("Enter at least 3 characters");
      label.setStyle("-fx-text-fill: red");
      container.getChildren().add(2, label);
      error = true;
      return;
    }

    if (!name.toLowerCase().endsWith(".jpg")) {
      name += ".jpg";
    }

    var home = System.getProperty("user.home");
    var pictures_path = Paths.get(home, "Pictures");
    var out = pictures_path.resolve(name).toFile();

    if (!pictures_path.toFile().exists()) {
      pictures_path.toFile().mkdirs();
    }

    if (out.exists()) {
      Toast.warning(stage, "File " + name + " already exists!", 3000);
      return;
    }

    var buffer = javafx.embed.swing.SwingFXUtils.fromFXImage(this.img, null);
    var fixed = new BufferedImage(buffer.getWidth(), buffer.getHeight(),
        BufferedImage.TYPE_INT_RGB);

    Graphics2D g = fixed.createGraphics();
    g.drawImage(buffer, 0, 0, Color.WHITE, null);
    g.dispose();

    try {
      if (!ImageIO.write(fixed, "jpg", out)) {
        Toast.alert(stage, "No valid Image writer found!", 2000);
        MyLogger.logger().log(Level.SEVERE, "No valid Image writer found!");
        return;
      }
      Toast.normal(stage, "Image " + name + " saved successfuly!", 3000);
      MyLogger.logger().info("Image " + name + " saved successfuly!");
    } catch (Exception e) {
      Toast.alert(stage, "Image saving failed!", 4000);
      MyLogger.logger().log(Level.SEVERE, "Image saving failed!");
    }
  }

  @FXML
  public void handle_cancel() {
    stage.close();
  }

  public void init(Stage stage) {
    this.stage = stage;
    input.setTextFormatter(new TextFormatter<String>(change -> {
      return change.getControlNewText().length() <= 100 ? change : null;
    }));
  }

  public void show_blocking() {
    stage.showAndWait();
  }

  public SaveMenuController attach_img(Image img) {
    this.img = img;
    return this;
  }
}
