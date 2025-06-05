package org.example;

import java.io.IOException;
import java.util.logging.FileHandler;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.logging.SimpleFormatter;

public class MyLogger {
  private static final Logger logger = Logger.getLogger(MyLogger.class.getName());

  static {
    try {
      FileHandler handler = new FileHandler("app.log", true);
      handler.setFormatter(new SimpleFormatter());
      logger.addHandler(handler);
      logger.setLevel(Level.ALL);
      logger.setUseParentHandlers(false);
    } catch (IOException e) {
      e.printStackTrace();
    }
  }

  public static Logger logger() {
    return logger;
  }
}
