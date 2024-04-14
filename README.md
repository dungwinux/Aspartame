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