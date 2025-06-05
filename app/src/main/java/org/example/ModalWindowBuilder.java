package org.example;

import java.util.function.Supplier;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Modality;
import javafx.stage.Stage;
import org.example.option.*;
import org.example.result.*;

public class ModalWindowBuilder<Window extends WindowTrait> {
  String resource;
  String title = "Window Title";
  Option<Stage> owner = None.of();

  public ModalWindowBuilder(String resource) {
    this.resource = resource;
  }

  public ModalWindowBuilder<Window> title(String title) {
    this.title = title;
    return this;
  }

  public ModalWindowBuilder<Window> owner(Stage owner) {
    this.owner = Some.of(owner);
    return this;
  }

  public Result<Window> build(Supplier<Window> clazz) {
    var loader = new FXMLLoader(getClass().getResource(this.resource));
    var controller = clazz.get();
    var new_stage = new Stage();

    loader.setController(controller);
    Parent root = null;
    try {
      root = loader.load();
    } catch (Exception e) {
      return Err.of(e);
    }

    controller.init(new_stage);
    new_stage.setTitle(this.title);
    new_stage.setScene(new Scene(root));
    new_stage.initModality(Modality.WINDOW_MODAL);
    switch (this.owner) {
      case Some<Stage> s -> new_stage.initOwner(s.get());
      case None<?> _n -> new_stage.initOwner(null);
    }

    return Ok.of(controller);
  }
}
