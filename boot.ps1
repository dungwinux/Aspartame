# https://github.com/bflattened/bflat
$BFLAT = 'D:\tools\bflat-8.0.2-windows-x64\'

function build {
  &"${BFLAT}bflat.exe" build --stdlib:zero --no-reflection --no-stacktrace-data --no-globalization --no-exception-messages --os:uefi -o:bootx64.efi
}

function boot {
  param($file)
  # https://www.qemu.org/download/
  $QEMU = 'C:\Program Files\qemu\qemu-system-x86_64.exe'
  # https://www.kraxel.org/repos/jenkins/edk2/edk2.git-ovmf-x64-0-20220719.209.gf0064ac3af.EOL.no.nore.updates.noarch.rpm
  # https://retrage.github.io/edk2-nightly/
  $OVMF = 'D:\tools\ovmf-x64\OVMF_CODE-pure-efi.fd'
  &$QEMU -m 1G -net none -serial stdio -bios $OVMF -kernel $file
}
