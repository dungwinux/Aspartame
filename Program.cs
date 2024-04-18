using System;
using Thread = System.Threading.Thread;

void clearScreen() {
  var height = 40;
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

// Wait 1 second for displaying on screen
Thread.Sleep(1000);

Console.Title = "TetrUEFI";


while (true) {
  clearScreen();
  Console.SetCursorPosition(27, 5);
  Console.WriteLine("<TetrUEFI>");
  Console.SetCursorPosition(27, 6);
  Console.WriteLine("by @dungwinux & @atch2203");
  Console.SetCursorPosition(27, 9);
  Console.WriteLine("Move : LArrow, RArrow");
  Console.SetCursorPosition(27, 10);
  Console.WriteLine("CCW  : F1, DArrow");
  Console.SetCursorPosition(27, 11);
  Console.WriteLine("CW   : F2, UArrow");
  Console.SetCursorPosition(27, 12);
  Console.WriteLine("Hard : Escape");

  Console.CursorVisible = false;

  var inLoop = true;
  var f = 0;
  char[] a = {'\\', '|', '/', '-'};

  GameArea g = new GameArea();
  Bag bag = new Bag();

  var spawn_x = 3;
  var spawn_y = -1;

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
  var DEBUG = false;
  var hit_tick_max = 10;
  var placed_count = 0;

  // Logic:
  // - Spawn new tetromino (random, "bag")
  // - If keypressed: propose additional change (Left, Right, Down, Rotate)
  // - Add gravity
  // - Compute change (gravity or change first?)
  // - If touched down (Gravity end), start counting tick of action. If tick is > X, commit change

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
      ++placed_count;
    }
    if (g.SolveLine() > 0) {
      g.Render(gx, gy, false);
    }
    
    if (placed_count > 69 && g.CountNonBlank() == 0) {
      inLoop = false;
      continue;
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
        case ConsoleKey.UpArrow: //rotate clockwise
        case ConsoleKey.F2:
          var nr = (r + 9) % 4;
          g.DrawRotate(x, y, t, r, nr);
          x = g.newPos[0];
          y = g.newPos[1];
          r = g.newPos[2];
          v = g.newPos[3];
          // v = g.Draw(x, y, t, nr);
          if(v == 1){
            // r = nr;
            g.Undraw(x, y, t, r);
            if ((v = g.Draw(x, y + 1, t, r)) != 0) {
              y += 1;
              continue;
            }
            v = g.Draw(x, y, t, r);
            continue;
          }
          break;
        case ConsoleKey.DownArrow: // rotate counterclockwise
        case ConsoleKey.F1:
          var nnr = (r + 11) % 4;
          g.DrawRotate(x, y, t, r, nnr);
          x = g.newPos[0];
          y = g.newPos[1];
          r = g.newPos[2];
          v = g.newPos[3];
          // v = g.Draw(x, y, t, nnr);
          if(v == 1){
            // r = nnr;
            g.Undraw(x, y, t, r);
            if ((v = g.Draw(x, y + 1, t, r)) != 0) {
              y += 1;
              continue;
            }
            v = g.Draw(x, y, t, r);
            continue;
          }
          break;
        // case ConsoleKey.F1: // rotate 180
          // var nnnr = (r + 3) % 4;
          // var nnnewPos = g.DrawRotate(x, y, t, r, nnnr);
          // x = nnnewPos[0];
          // y = nnnewPos[1];
          // r = nnnewPos[2];
          // v = nnnewPos[3];
          // if(v != 0){
          //   continue;
          // }
          // break;
        case ConsoleKey.Escape: //hard drop
          var ny = y < 0 ? 0 : y;
          while ((v = g.Draw(x, ny, t, r)) != 0) { //continuously move down until v==2 (ground hit)
            ny += 1;
            if (v == 2) {
              break;
            }
            g.Undraw(x, ny - 1, t, r);
          }
          y = ny - 1;
          hit_tick = hit_tick_max; //instant ground
          continue;
        case ConsoleKey.LeftArrow:
          if ((v = g.Draw(x - 1, y, t, r)) != 0) {
            x -= 1;
            g.Undraw(x, y, t, r);
            if ((v = g.Draw(x, y + 1, t, r)) != 0) {
              y += 1;
              continue;
            }
            v = g.Draw(x, y, t, r);
            continue;
          }
          break;
        case ConsoleKey.RightArrow:
          if ((v = g.Draw(x + 1, y, t, r)) != 0) {
            x += 1;
            g.Undraw(x, y, t, r);
            if ((v = g.Draw(x, y + 1, t, r)) != 0) {
              y += 1;
              continue;
            }
            v = g.Draw(x, y, t, r);
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
  Console.SetCursorPosition(0, 0);
  Console.ForegroundColor = ConsoleColor.Gray;
  Console.WriteLine("You win!");

  while (true) {}
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

  public int[] newPos = {0, 0, 0, 0};
  
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

  

  int[] aSRSList = {
   9, 9, -1, 9, -1, 1, 9, -2, -1, -2,
    9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
    9, 9, 1, 9, 1, 1, 9, -2, 1, -2,
    9, 9, 1, 9, 1, -1, 9, 2, 1, 2,
    9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
    9, 9, 1, 9, 1, -1, 9, 2, 1, 2,
    9, 9, 1, 9, 1, 1, 9, -2, 1, -2,
    9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
    9, 9, -1, 9, -1, 1, 9, -2, -1, -2,
    9, 9, -1, 9, -1, -1, 9, 2, -1, 2,
    9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
    9, 9, -1, 9, -1, -1, 9, 2, -1, 2,
  };

int[] iSRSList = {
  9, 9, -2, 9, 1, 9, -2, -1, 1, 2, 
  9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
  9, 9, -1, 9, 2, 9, -1, 2, 2, -1, 
  9, 9, -1, 9, 2, 9, -1, 2, 2, -1, 
  9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
  9, 9, 2, 9, -1, 9, 2, 1, -1, -2, 
  9, 9, 2, 9, -1, 9, 2, 1, -1, -2, 
  9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
  9, 9, 1, 9, -2, 9, 1, -2, -2, 1, 
  9, 9, 1, 9, -2, 9, 1, -2, -2, 1, 
  9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 
  9, 9, -2, 9, 1, 9, -2, -1, 1, 2, 
  };

         
  int aisrslen = 5;
  int osrslen = 1;

 

  int GetSRSLen(Tetromino t){
    if(t == Tetromino.O){
      return osrslen;
    }
    return aisrslen;
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

  int getsrsindex(Tetromino piece, int prevRot, int change, int s, int i){
    if(piece == Tetromino.O){
      return 0;
    }
    if(piece == Tetromino.I){
      return iSRSList[prevRot*30+change*10+s*2+i];
    }
    return aSRSList[prevRot*30+change*10+s*2+i];
  }

  public void DrawRotate(int x, int y, Tetromino piece, int prevRot, int rotation) { //ret
    printDebug("");
    char d = (char)(int)piece;
    var rc = 1;
    var pix = GetPiece(piece);
    var k = GetBase(piece);
    var change = (rotation - prevRot + 11) % 4;
    var srslen = GetSRSLen(piece);
    var kickX = 0;
    var kickY = 0;
    var kickValid = 1;
    var s = 0;
    for(s = 0; s < srslen; ++s){
      // int kickX = srs[s][0];
      // int kickY = -srs[s][1];
      kickX = getsrsindex(piece, prevRot, change, s, 0);
      if(kickX == 9){
        kickX = 0;
      }
      kickY = getsrsindex(piece, prevRot, change, s, 1);
      if(kickY == 9){
        kickY = 0;
      }
      kickY = -1 * kickY;
      kickValid = 1;
      rc = 1;
      for (int i = 0; i < k; ++i) {
        for (int j = 0; j < k; ++j) {
          if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
            var xj = x + j;
            var yj = y + i;
            var xpj = xj + kickX;
            var ypi = yj + kickY;
            if (xpj < 0 || xpj >= Width) { // oob, return!
              kickValid = 0;
              rc = 0;
              break;
            }
            if (ypi >= Height) { // in ground, return!
              kickValid = 0;
              rc = 0;
              break;
            }
            var areaIndex = xpj + ypi * Width;
            // if (z >= Area) {
            //   rc = 3;
            // }
            if (areaIndex >= Area || (areaIndex >= 0 && _area[areaIndex] != ' ')) { // collision, return!
              kickValid = 0;
              rc = 0;
              break;  
            }
            
            var ground = areaIndex + Width;
            if (ground >= Area || (ground >= 0 && _area[ground] != ' ')) { //ground hit, invalid
              rc = 2;
            }
          }
        }
        if(kickValid == 0){
          break;
        }
      }
      if(kickValid == 1){ // attempt to draw
        int newDraws = 0;
        for (int i = 0; i < k; ++i) {
          for (int j = 0; j < k; ++j) {
            if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
              var areaIndex = (x + j + kickX) + (y + i + kickY) * Width;
              if (areaIndex >= 0 && areaIndex < Area) {
                _area[areaIndex] = d;
                ++newDraws;
              }
            }
          }
        }
        if(newDraws == 0){
          rc = 0;
          continue;
        }
        newPos[0] = x+kickX;
        newPos[1] = y+kickY;
        newPos[2] = rotation;
        newPos[3] = rc;
        return;
        // return new int[]{x + kickX, y + kickY, rotation, rc};
      }
    }
    newPos[0] = x;
    newPos[1] = y;
    newPos[2] = prevRot;
    newPos[3] = rc;
    // return new int[]{x, y, prevRot, rc};
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
          if (xpj < 0 || xpj >= Width) { // oob, return!
            return 0;
          }
          if (ypi >= Height) { // oob, return!
            return 0;
          }
          var areaIndex = xpj + ypi * Width;
          // if (z >= Area) {
          //   rc = 3;
          // }
          if (areaIndex >= Area || (areaIndex >= 0 && _area[areaIndex] != ' ')) { // collision, return!
            return 0;
          }
          
          var ground = areaIndex + Width;
          if (ground >= Area || (ground >= 0 && _area[ground] != ' ')) { //ground hit, "return" 2!
            rc = 2;
          }
        }
      }
    }
    int newDraws = 0;
    for (int i = 0; i < k; ++i) {
      for (int j = 0; j < k; ++j) {
        if (pix[rotation * GetRotMulti(piece) + j + i * k] == 1) {
          var areaIndex = (x + j) + (y + i) * Width;
          if (areaIndex >= 0 && areaIndex < Area) {
            _area[areaIndex] = d;
            ++newDraws;
          }
        }
      }
    }
    return newDraws == 0 ? 0 : rc;
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
          Console.Write(' ');
          Console.Write(' ');
          //Console.Write('$');
          //Console.Write('$');
        }
      }
    }
  }
  public void Render(int offset_x, int offset_y, bool init) {
    if (init) RenderBorder(offset_x, offset_y);
    RenderBoard(offset_x + 2, offset_y);
  }
  public int CountNonBlank() {
    int blank = Height;
    for (var i = 0; i < Height; ++i) {
      blank -= IsBlankLine(i) ? 1 : 0;
    }
    return blank;
  }

  void printDebug(string input){
      Console.SetCursorPosition(30, 3);
      Console.WriteLine("                                                            ");
      Console.SetCursorPosition(30, 3);
      Console.WriteLine(input);
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

/*



*/