package org.example;

import javafx.scene.canvas.Canvas;
import javafx.scene.image.Image;
import javafx.scene.image.PixelReader;
import javafx.scene.image.PixelWriter;
import javafx.scene.image.WritableImage;
import javafx.scene.paint.Color;

public class ImageOperations {
  public static Image scale(Image input, int new_x, int new_y) {
    WritableImage out = new WritableImage(new_x, new_y);
    PixelReader reader = input.getPixelReader();
    PixelWriter writer = out.getPixelWriter();

    double scale_x = input.getWidth() / new_x;
    double scale_y = input.getHeight() / new_y;

    for (int y = 0; y < new_y; y++) {
      for (int x = 0; x < new_x; x++) {
        int src_x = (int) (x * scale_x);
        int src_y = (int) (y * scale_y);
        writer.setArgb(x, y, reader.getArgb(src_x, src_y));
      }
    }

    return out;
  }

  public static Image rotate(Image in, int angle) {
    boolean swap = (angle % 180 != 0);

    int w = (int) in.getWidth();
    int h = (int) in.getHeight();

    int new_w = swap ? h : w;
    int new_h = swap ? w : h;

    var canvas = new Canvas(new_w, new_h);
    var gc = canvas.getGraphicsContext2D();

    gc.translate(new_w / 2, new_h / 2);
    gc.rotate(angle);
    gc.translate(-w / 2, -h / 2);
    gc.drawImage(in, 0, 0);

    WritableImage rotated = new WritableImage(new_w, new_h);
    canvas.snapshot(null, rotated);

    return rotated;
  }

  public static Image negative(Image in) {
    int width = (int) in.getWidth();
    int height = (int) in.getHeight();

    WritableImage output = new WritableImage(width, height);
    PixelReader reader = in.getPixelReader();
    PixelWriter writer = output.getPixelWriter();

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        Color original = reader.getColor(x, y);
        Color negative = new Color(1.0 - original.getRed(), 1.0 - original.getGreen(),
            1.0 - original.getBlue(), original.getOpacity());
        writer.setColor(x, y, negative);
      }
    }

    return output;
  }

  public static Image thresh(Image in, Integer value) {
    int width = (int) in.getWidth();
    int height = (int) in.getHeight();

    WritableImage output = new WritableImage(width, height);
    PixelReader reader = in.getPixelReader();
    PixelWriter writer = output.getPixelWriter();

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        Color color = reader.getColor(x, y);
        double brightness = color.getBrightness() * 255; // grayscale brightness
        Color outputColor = (brightness >= value) ? Color.WHITE : Color.BLACK;

        writer.setColor(x, y, outputColor);
      }
    }

    return output;
  }

  public static Image edge_detection(Image in) {
    int width = (int) in.getWidth();
    int height = (int) in.getHeight();

    WritableImage output = new WritableImage(width, height);
    PixelReader reader = in.getPixelReader();
    PixelWriter writer = output.getPixelWriter();

    for (int y = 0; y < height - 1; y++) {
      for (int x = 0; x < width - 1; x++) {
        Color c = reader.getColor(x, y);
        Color right = reader.getColor(x + 1, y);
        Color down = reader.getColor(x, y + 1);

        double currentGray = c.getBrightness();
        double dx = right.getBrightness() - currentGray;
        double dy = down.getBrightness() - currentGray;

        double edge = Math.sqrt(dx * dx + dy * dy); // gradient magnitude

        writer.setColor(x, y, Color.gray(edge));
      }
    }

    return output;
  }
}
