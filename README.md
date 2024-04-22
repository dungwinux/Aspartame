# Aspartame

### Build

```shell
make prereq             # Install pre-requisite
make linux BFLAT=...    # Build
make windows BFLAT=...  # Build
make uefi BFLAT=...     # Build
make clean              # Clean up
```

### Challenge

```shell
make prereq
make uefi BFLAT=D:\\tools\\bflat-8.0.2-windows-x64\\bflat.exe
```

### Attribution

The binary output is generated using a modified version of ZeroLib library of [bflat](https://github.com/bflattened/bflat) by [bflattened](https://github.com/bflattened) licensed under [GNU AGPL v3.0](https://github.com/bflattened/bflat/blob/v8.0.2/LICENSE).
All changes are shown in `console_key.patch`, which add additional cases for handling more keys in UEFI environment.
The `Makefile` shall contain commands used for cloning bflat version 8.0.2 (including its license) and applying the changes.

### License

The tetris game source code is licensed under MIT License. 
