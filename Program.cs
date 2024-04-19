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
int[] msg = {926360898,1825148929,-371944575,894543170,76156929,-1129081983,888317250,1845497857,-1824451711,1022361608,-1061320828,-124418298,1959036034,16450818,1292534914,606373122,-1918598014,-2096855806,-678198142,899843330,969148292,2104558850,-1577384830,-76708606,-1888615794,839548936,-309358591,-848260223,-1821212406,504694660,1056309256,1474135940,-1699281146,-1726053246,-1832221180,-602863740,2068808962,1039008898,599458058,-615709566,-1800634360,-150532095,819172225,30998794,734233730,1834942472,-2044361598,-542341370,-124746622,1436878082,1689814916,1472038146,-1871149950,-1145731580,539395202,423691522,-950335866,-1457978110,-1074101118,-543260664,-1596682239,2087457665,-2130509300,378438786,1944094470,1785004424,279840002,-318798718,128976130,-1157626750,-1244659704,-1668381566,1719174402,1362167430,1566049538,865305730,522420232,67964929,829002625,-558858232,422282370,1673856258,1364492942,1661207052,1663895428,574883074,1306166402,-1087109886,-830863738,77530370,1334281346,181829640,-111144959,-732458111,-1247674102,1733429124,-500169470,-982449022,1305904902,-1639086458,1538032898,-1341356926,-1256389374,1715242630,-2017164030,-2057468798,-1351220982,-806255486,978288648,-325218303,-243428479,545459216,1225688194,1758496002,441156738,1779893506,-2067004286,-589201400,-472349566,-1106149368,1479246478,-64421880,-600666111,-1330244735,-58490872,-1226307708,2121598468,1374028930,45614854,-683604346,1481344516,-872610686,1288406532,271385730,-590444520,60231681,1621005185,-1196851192,-1643837564,-1439038206,1932362636,1845592588,1679230852,-419855102,-1012004988,1993049346,-761691006,-1840970494,-1045428348,-1626341368,-1170272255,-1060990079,1153204748,2089747586,-1577254646,2021557894,-1607695102,-1973549950,-1710717436,2109473922,-1262254846,129729666,-2068085502,2133919366,-1640135922,1766166529,-1649601663,-1920922608,1116243844,-1421376254,2129429634,2071921922,-2132670842,-835517182,-607287674,-940505596,-1807154046,208997908,356028417,743576449,-1024294904,-1195408254,1637189382,793281666,-1118961656,1915421826,486278660,-2105931900,-1367341822,2037286534,-1573388280,1332413570,619578626,1062995074,-729776120,-1168240639,263361409,171999498,69764226,-593787646,1280050306,44598788,-334133626,999753220,-357727102,-1184333816,-1353644410,465339650,1171653762,879232258,1727431810,1225360642,-128973694,-1356005368,1141379073,-750087295,-594214388,-538147710,1118242050,167085186,387515908,-1574207096,665912836,-294386042,1320421126,-775583868,392136454,1555563650,-2032599032,2112360449,1012798337,1885864460,387253378,-775322108,627050372,-425163518,-1132558460,-1978202878,-1317337982,-395901694,880183172,1840907522,-557054330,1482294530,1972832132,-1054145278,-1027504252,1607106568,-1163456511,-848456831,1598095368,-687209342,-1826519290,-217315452,-1014692348,1477674884,-1601993470,1541211266,1162282242,-1004370814,-305953534,831849602,1681262342,-916715644,1354007810,1536165510,1868005384,1831374849,-1533930623,-294256632,1102283144,-799243006,1263633538,108627718,-875756414,-1224635376,1221822086,-1230600700,1464895364,-1515421688,-1622437887,-567929983,196086300,1882883202,716734472,-551713662,1452706074,-1461448703,-143584383,400818184,1744568974,-1798175486,-1612544894,979666178,-1733491582,1986266370,-785906558,-753859326,2069103746,197854466,886867074,922617090,-949845362,-1877508088,2004979713,1581093761,-1263468536,1016962,-450199286,48858242,1907427078,1882228358,1167656194,-1050049406,-2135030526,1299317890,-1241545470,972948610,-204308214,736789634,337903624,-100134911,1326814081,1264123912,714933378,-1234598654,-31848826,166855938,-425687934,2141291778,1123680648,-88636158,-2146236794,-779975164,-1277492094,-1112472318,-341473658,-38206206,-1398864766,-53936120,-1574629375,-1534258303,-2359288,-871070590,1550877954,-1807645050,920257794,-1932688254,-1339063038,1486029192,152600584,1139999874,-848853758,1818330242,-1314487038,1686242950,131073282,-621935486,634486792,728174593,-1778674815,162988040,-336624510,199853314,1847199366,1023935746,-1546746750,1883768582,-444562302,895746056,388138884,-1916467966,-861600638,-1328282366,123307654,114132226,-1929378686,357400584,-912945151,-2048584831,-1965293560,-1737489278,1474199818,-72383358,2074412290,166265986,2101182988,875562884,-1251048190,-792722302,-1893564150,-1083046782,-1281064952,-1538060287,2005111681,-820445176,-1248066930,-1110506238,123831426,1221689352,2114258820,-1593309950,2080375938,-1981020926,-442400114,-905609208,870617089,739840897,-1932155582,1646006273,61641601,877077826,889655297,765334401,-1846401726,-1205103232,};

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
  for (var i = msg.Length - 1; i >= 0; --i) {
    int c = msg[i] & 0b1111111;
    char p = (char)(((msg[i] >> 7) & 0xFF) ^ c ^ 43);
    while (c-- > 0) Console.Write(p);
  }

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