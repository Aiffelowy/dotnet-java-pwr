package org.example;

import javafx.animation.FadeTransition;
import javafx.application.Platform;
import javafx.scene.control.Label;
import javafx.scene.layout.StackPane;
import javafx.stage.Popup;
import javafx.stage.Stage;
import javafx.util.Duration;

public class Toast {
  private static void show(Stage stage, String msg, int duration,
      String style) {
    Platform.runLater(() -> {
      var toast_l = new Label(msg);
      toast_l.setStyle(style);

      var pane = new StackPane(toast_l);
      pane.setStyle("-fx-padding: 10");

      var popup = new Popup();
      popup.getContent().add(pane);
      popup.setAutoFix(true);
      // popup.setAutoHide(true);
      //
      popup.show(stage);
      Platform.runLater(() -> { // move the popup after the size has been
                                // computed (next render cycle)
        popup.setX(stage.getWidth() + stage.getX() - pane.getWidth());
        popup.setY(stage.getY());
      });

      var trans_in = new FadeTransition(Duration.millis(300), toast_l);
      trans_in.setFromValue(0);
      trans_in.setToValue(1);
      trans_in.play();

      new Thread(() -> {
        try {
          Thread.sleep(duration);
        } catch (Exception e) { // WHY
        }

        Platform.runLater(() -> {
          var trans_out = new FadeTransition(Duration.millis(300), toast_l);
          trans_out.setFromValue(1);
          trans_out.setToValue(0);
          trans_out.setOnFinished(e -> popup.hide());
          trans_out.play();
        });
      }).start();
    });
  }

  public static void normal(Stage stage, String msg, int duration) {
    Toast.show(stage, msg, duration,
        "-fx-background-color: lightblue; -fx-background-radius: 10; "
            + "-fx-padding: 10;");
  }

  public static void alert(Stage stage, String msg, int duration) {
    Toast.show(stage, msg, duration,
        "-fx-background-color: salmon; -fx-background-radius: 10; "
            + "-fx-padding: 10;");
  }

  public static void warning(Stage stage, String msg, int duration) {
    Toast.show(stage, msg, duration,
        "-fx-background-color: orange; -fx-background-radius: 10; "
            + "-fx-padding: 10;");
  }
}
