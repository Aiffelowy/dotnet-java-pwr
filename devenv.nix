{ pkgs, lib, config, inputs, ... }:
let
  gtkSchemas = "${pkgs.gtk3}/share/gsettings-schemas/${pkgs.gtk3.name}/glib-2.0/schemas";
in {
  packages = with pkgs; [
    git
    bashInteractive
    gradle
    libGL
    gtk3
    xdg-utils
    xorg.libX11
    xorg.libXrender
    xorg.libXxf86vm
    xorg.libXtst
    xorg.libXext
    xorg.libXrandr
    xorg.libXi
    gsettings-desktop-schemas
  ];

  languages.java.enable = true;

  env = {
    LD_LIBRARY_PATH = pkgs.lib.makeLibraryPath [
      pkgs.xorg.libX11
      pkgs.xorg.libXrender
      pkgs.xorg.libXxf86vm
      pkgs.libGL
      pkgs.xorg.libXtst
      pkgs.xorg.libXext
      pkgs.glib
      pkgs.xdg-utils
      pkgs.gtk3
      pkgs.xorg.libXi
      pkgs.xorg.libXrandr
    ];
    GSETTINGS_SCHEMA_DIR = gtkSchemas;
    GSETTINGS_SCHEMAS_PATH = gtkSchemas;
    XDG_DATA_DIRS = "${pkgs.gtk3}/share:/run/current-system/sw/share";
  };
}
