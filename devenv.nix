{ pkgs, lib, config, inputs, ... }:

{
  packages = with pkgs; [ git bashInteractive gradle ];

  # https://devenv.sh/languages/
  # languages.rust.enable = true;
  
  languages.java.enable = true;

}
