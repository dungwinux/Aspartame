using System;
using Thread = System.Threading.Thread;

void clearScreen() {
  var height = 30;
  var width = 120;
  Console.CursorVisible = false;
  for (int i = 0; i < height; ++i) {
    Console.SetCursorPosition(0, i);
    for (int j = 0; j < width; ++j) {
      Console.Write(' ');
    }
  }
  Console.SetCursorPosition(0, 0);
}

void writeHexDigit(uint x) {
  if (x >= 0 && x < 10) {
    Console.Write((char)(((uint)'0') + x));
  } else if (x >= 10 && x < 16) {
    
    Console.Write((char)(((uint)'A') + x - 10));
  }
}
void writeHex(uint x, uint z = 8) {
  uint y = 0;
  for (uint i = 0; i < z; ++i) {
    y = (y << 4) | (x & 0b1111);
    x >>= 4;
  }
  for (uint i = 0; i < z; ++i) {
    writeHexDigit(y & 0b1111);
    y >>= 4;
  }
}

Thread.Sleep(1000);

// for (int i = 0; i <= 16; ++i)
// {
//    unsafe
//    {
//        x.items[i] = 0x41;
//   }
// }
// System.Console.ReadKey(false);
Console.Title = "TetrUEFI";

while (true) {
  clearScreen();
  //                  ||$$$$$$$$$$$$$$$$$$$$||
  Console.SetCursorPosition(30, 5);
  Console.WriteLine("<TetrUEFI>");

  Console.CursorVisible = false;

  var inLoop = true;
  var f = 0;
  char[] a = {'\\', '|', '/', '-'};

  GameArea g = new GameArea();
  Bag bag = new Bag();

  var spawn_x = 3;
  var spawn_y = -2;

  var t = (Tetromino)bag.getRandom();
  var r = 0;
  var x = spawn_x;
  var y = spawn_y;
  var gx = 1;
  var gy = 0;
  var hit_tick = 0;
  var v = 0;
  var noti = "Welcome to TetrUEFI!";
  var noti_update = true;
  var DEBUG = true;
  var hit_tick_max = 30;

  // Logic:
  // - Spawn new tetromino (random, "bag")
  // - If keypressed: propose additional change (Left, Right, Down, Rotate)
  // - Add gravity
  // - Compute change (gravity or change first?)
  // - If touched down (Gravity end), start counting tick of action. If tick is > X, commit change
  // TODO: HitTest

  g.Render(gx, gy, true);
  while (inLoop) {
    Thread.Sleep(100);
    g.Render(gx, gy, false);
    if (v == 2) {
      ++hit_tick;
      noti = "Ground!";
      noti_update = true;
    }
    if (hit_tick < hit_tick_max) {
      // Uncommit
      g.Undraw(x, y, t, r);
    } else {
      // Spawn new tetrimio
      x = spawn_x;
      y = spawn_y;
      r = 0;
      hit_tick = 0;
      v = 0;
      t = (Tetromino)bag.getRandom();
      noti = "New drop!";
      noti_update = true;
    }
    if (g.SolveLine() > 0) {
      g.Render(gx, gy, false);
    }

    Console.ForegroundColor = ConsoleColor.Gray;
    if (noti_update) {
      Console.SetCursorPosition(30, 0);
      Console.WriteLine("                                                            ");
      Console.SetCursorPosition(30, 0);
      Console.WriteLine(noti);
      noti_update = false;
    }
    if (DEBUG) {
      Console.SetCursorPosition(30, 1);
      Console.Write(a[(f++) % 4]);
      Console.Write('X');
      writeHex((uint)x, 2);
      Console.Write('Y');
      writeHex((uint)y, 2);
      Console.Write('R');
      writeHex((uint)r, 2);
      Console.Write('T');
      writeHex((uint)t, 2);
      Console.Write('I');
      writeHex((uint)hit_tick, 2);
    }

    if (Console.KeyAvailable) {
      var keyInfo = Console.ReadKey(intercept: true);
      switch (keyInfo.Key) {
        case ConsoleKey.UpArrow:
          var nr = (r + 1) % 4;
          if ((v = g.Draw(x, y, t, nr)) != 0) {
            r = nr;
            continue;
          }
          break;
        case ConsoleKey.DownArrow:
          while ((v = g.Draw(x, y + 1, t, r)) != 0) {
            y += 1;
            if (v == 2) {
              break;
            }
            g.Undraw(x, y, t, r);
          }
          hit_tick = hit_tick_max;
          continue;
        case ConsoleKey.LeftArrow:
          if ((v = g.Draw(x - 1, y, t, r)) != 0) {
            x -= 1;
            continue;
          }
          break;
        case ConsoleKey.RightArrow:
          if ((v = g.Draw(x + 1, y, t, r)) != 0) {
            x += 1;
            continue;
          }
          break;
      }
    }
    if ((v = g.Draw(x, y + 1, t, r)) != 0) {
      y += 1;
      continue;
    }
    v = g.Draw(x, y, t, r);
  }
}

enum Tetromino: uint {
  I, O, T, J, L, S, Z
}

unsafe struct GameArea {
  public const int Height = 20;
  public const int Width = 10;
  const int Area = Height * Width;
  fixed char _area[Area];

  public GameArea(){
    Wipe();
  }
  
  int[] TI = {
    0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0,
    0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
    0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0
  };
  const int TI_base = 4;

  int[] TJ = {
    1, 0, 0, 1, 1, 1, 0, 0, 0,
    0, 1, 1, 0, 1, 0, 0, 1, 0,
    0, 0, 0, 1, 1, 1, 0, 0, 1,
    0, 1, 0, 0, 1, 0, 1, 1, 0
  };
  const int TJ_base = 3;

  int[] TL = {
    0, 0, 1, 1, 1, 1, 0, 0, 0,
    0, 1, 0, 0, 1, 0, 0, 1, 1,
    0, 0, 0, 1, 1, 1, 1, 0, 0,
    1, 1, 0, 0, 1, 0, 0, 1, 0,
  };
  const int TL_base = 3;

  int[] TO = {
    1, 1, 1, 1,
    1, 1, 1, 1,
    1, 1, 1, 1,
    1, 1, 1, 1,
  };
  const int TO_base = 2;

  int[] TS = { 
    0, 1, 1, 1, 1, 0, 0, 0, 0,
    0, 1, 0, 0, 1, 1, 0, 0, 1,
    0, 0, 0, 0, 1, 1, 1, 1, 0,
    1, 0, 0, 1, 1, 0, 0, 1, 0,
  };
  const int TS_base = 3;

  int[] TT = {
    0, 1, 0, 1, 1, 1, 0, 0, 0,
    0, 1, 0, 0, 1, 1, 0, 1, 0,
    0, 0, 0, 1, 1, 1, 0, 1, 0,
    0, 1, 0, 1, 1, 0, 0, 1, 0,
  };
  const int TT_base = 3;

  int[] TZ = {
    1, 1, 0, 0, 1, 1, 0, 0, 0,
    0, 0, 1, 0, 1, 1, 0, 1, 0,
    0, 0, 0, 1, 1, 0, 0, 1, 1,
    0, 1, 0, 1, 1, 0, 1, 0, 0,
  };
  const int TZ_base = 3;


  int[] GetPiece(Tetromino t) {
    switch (t) {
      case Tetromino.I: return TI;
      case Tetromino.O: return TO;
      case Tetromino.T: return TT;
      case Tetromino.J: return TJ;
      case Tetromino.L: return TL;
      case Tetromino.S: return TS;
      case Tetromino.Z: return TZ;
    }
    return TI;
  }

  int GetBase(Tetromino t) {
    switch (t) {
      case Tetromino.I: return TI_base;
      case Tetromino.O: return TO_base;
      case Tetromino.T: return TT_base;
      case Tetromino.J: return TJ_base;
      case Tetromino.L: return TL_base;
      case Tetromino.S: return TS_base;
      case Tetromino.Z: return TZ_base;
    }
    return TI_base;
  }

  int GetRotMulti(Tetromino t) {
    var d = GetBase(t);
    return d * d;
  }

  ConsoleColor GetColor(Tetromino t) {
    switch (t) {
      case Tetromino.I: return ConsoleColor.Cyan;
      case Tetromino.O: return ConsoleColor.Yellow;
      case Tetromino.T: return ConsoleColor.Magenta;
      case Tetromino.J: return ConsoleColor.Blue;
      case Tetromino.L: return ConsoleColor.DarkRed;
      case Tetromino.S: return ConsoleColor.Green;
      case Tetromino.Z: return ConsoleColor.Red;
    }
    return ConsoleColor.Gray;
  }

  public void Wipe() {
    for (var i = 0; i < Area; ++i) {
      _area[i] = ' ';
    }
  }

  void SetPixel(int x, int y, char ch) {
    _area[x + y * Width] = ch;
  }

  public int Draw(int x, int y, Tetromino piece, int rotation) {
    char d = (char)(int)piece;
    int rc = 1;
    var pix = GetPiece(piece);
    int k = GetBase(piece);
    for (int i = 0; i < k; ++i) {
      for (int j = 0; j < k; ++j) {
        if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
          var xpj = x + j;
          var ypi = y + i;
          if (xpj < 0 || xpj >= Width) {
            return 0;
          }
          if (ypi >= Height) {
            return 0;
          }
          var z = xpj + ypi * Width;
          // if (z >= Area) {
          //   rc = 3;
          // }
          if (z >= Area || (z >= 0 && _area[z] != ' ')) {
            return 0;
          }
          
          var ground = z + Width;
          if (ground >= Area || _area[ground] != ' ') {
            rc = 2;
          }
        }
      }
    }
    int c = 0;
    for (int i = 0; i < k; ++i) {
      for (int j = 0; j < k; ++j) {
        if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
          var z = (x + j) + (y + i) * Width;
          if (z >= 0 && z < Area) {
            _area[z] = d;
            ++c;
          }
        }
      }
    }
    return c == 0 ? 0 : rc;
  }

  public int Undraw(int x, int y, Tetromino piece, int rotation) {
    char d = (char)(int)piece;
    int rc = 1;
    var pix = GetPiece(piece);
    int k = GetBase(piece);
    for (int i = 0; i < k; ++i) {
      for (int j = 0; j < k; ++j) {
        if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
          var xpj = x + j;
          var ypi = y + i;
          if (xpj < 0 || xpj >= Width) {
            return 0;
          }
          if (ypi >= Height) {
            return 0;
          }
          var z = xpj + ypi * Width;
          // if (z >= Area) {
          //   rc = 3;
          // }
          if (z >= Area || (z >= 0 && _area[z] != d)) {
            return 0;
          }
        }
      }
    }
    int c = 0;
    for (int i = 0; i < k; ++i) {
      for (int j = 0; j < k; ++j) {
        if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
          var z = (x + j) + (y + i) * Width;
          if (z >= 0 && z < Area) {
            _area[z] = ' ';
            ++c;
          }
        }
      }
    }
    return c == 0 ? 0 : rc;
  }

  bool IsSolveLine(int y) {
    for (int j = 0; j < Width; ++j) {
      if (_area[y * Width + j] == ' ') {
        return false;
      }
    }
    return true;
  }
  bool IsBlankLine(int y) {
    for (int j = 0; j < Width; ++j) {
      if (_area[y * Width + j] != ' ') {
        return false;
      }
    }
    return true;
  }

  public int SolveLine() {
    int count = 0;
    for (int new_i = Height - 1, old_i = Height - 1; new_i >= 0 && !IsBlankLine(new_i); --new_i, --old_i) {
      while (old_i > 0 && IsSolveLine(old_i)) {
        --old_i;
        ++count;
      }
      if (old_i != new_i) {
        if (old_i < 0) {
          for (int j = 0; j < Width; ++j) {
            _area[new_i * Width + j] = ' ';
          }
        } else {
          for (int j = 0; j < Width; ++j) {
            _area[new_i * Width + j] = _area[old_i * Width + j];
          }
        }
      }
    }
    return count;
  }

  void RenderBorder(int x, int y) {
    Console.ForegroundColor = ConsoleColor.White;
    for (int i = 0; i < Height; ++i) {
      Console.SetCursorPosition(x, i + y);
      Console.Write('|');
      Console.Write('|');
      Console.SetCursorPosition(x + 2 * (Width + 1), i + y);
      Console.Write('|');
      Console.Write('|');
    }
    Console.SetCursorPosition(x, y + Height);
    for (int j = 0; j < Width + 2; ++j) {
      Console.Write('@');
      Console.Write('@');
    }
  }

  void RenderBoard(int x, int y) {
    for (int i = 0; i < Height; ++i) {
      Console.SetCursorPosition(x, y + i);
      for (int j = 0; j < Width; ++j) {
        var d = _area[j + i * Width];
        if ((int)d < 7) {
          Console.ForegroundColor = GetColor((Tetromino)d);
          Console.Write('#');
          Console.Write('#');
        } else {
          Console.ForegroundColor = ConsoleColor.DarkGray;
          Console.Write('$');
          Console.Write('$');
        }
      }
    }
  }
  public void Render(int offset_x, int offset_y, bool init) {
    if (init) RenderBorder(offset_x, offset_y);
    RenderBoard(offset_x + 2, offset_y);
  }
}

// https://en.wikipedia.org/wiki/Xorshift
struct Random
{
    private uint _state;

    public Random(uint seed)
    {
      _state = seed;
    }
    public uint Next() {
      uint x = _state;
      x ^= x << 13;
      x ^= x >> 17;
      x ^= x << 5;
      return _state = x;
    }
}

struct Bag
{
  int bag = 0x01234567;
  int bagSize = 7;
  Random random = new Random(10000007);

  public Bag() {}

  void Remove(int idx) {
    var bi = idx << 2;
    var lbag = bag & ((1 << bi) - 1);
    var rbag = (bag >> (bi + 4)) << bi;
    bag = lbag | rbag;
    --bagSize;
    if (bagSize == 0) {
      bagSize = 7;
      bag = 0x01234567;
    }
  }

  public int getRandom() {
    int idx = (int)(random.Next() % (uint)bagSize);
    int t = (bag >> (idx << 2)) & 0b1111;
    Remove(idx);
    return t - 1;
  }
}