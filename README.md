# EnvLauncher

Windows `.LNK` shortcuts do not support setting environment variables; using batch scripts as wrappers requires creating additional files; and the `cmd /c` approach is often inconvenient.

This program allows you to specify environment variables directly within the command-line arguments.

## Usage

```
EnvLauncher <envVar1=value1> <envVar2=value2> ... -- <executable> <arg1> <arg2> ...
```