ifeq ($(OS),Windows_NT)
    CP ?= copy /y
    MV ?= move
    RM = del
    RD ?= rmdir /S /Q
    WH ?= where.exe
    SEP ?= \\
else
    CP ?= cp
    MV ?= mv -f

    RD ?= rm -rf
    WH ?= which
    SEP ?= /
endif

.PHONY: all build clean prereq

clean:
	$(RM) bootx64.efi
	$(RM) bootx64.pdb
	$(RD) bflat

prereq:
	git clone -b v8.0.2 --depth 1 https://github.com/bflattened/bflat.git
	cd bflat && git apply ../console_key.patch

BFLAT ?= $(shell $(WH) bflat)
ifeq ($(BFLAT),)
$(error Cannot find bflat)
endif
BUILD_PATH:=bflat$(SEP)src$(SEP)zerolib$(SEP)
BUILD_FLAG:=--stdlib:none --no-reflection --no-stacktrace-data --no-globalization --no-exception-messages

%:
	$(CP) Program.cs $(BUILD_PATH)
	cd $(BUILD_PATH) && $(BFLAT) build $(BUILD_FLAG) --os:$@ -o:"$(CURDIR)/bootx64.efi"
