import random

def loop(x, n):
    rep = "\\n" if x == ord('\n') else chr(x)
    return f'Write("{rep * n}");'

with open('flag.txt', 'rb') as fd:
    p = 0
    c = 0
    a = []
    for x in fd.read():
        if x == p:
            c += 1
        else:
            a.append((p, c))
            c = 1
            p = x
    a.append((p, c))

z = 0x2B
sub = '''
  for (var i = msg.Length - 1; i >= 0; --i) {{
    int c = msg[i] & 0b1111111;
    char p = (char)(((msg[i] >> 7) & 0xFF) ^ c ^ {});
    while (c-- > 0) Console.Write(p);
  }}
'''.format(z)

random.seed()

def get_filler():
    return random.randint(0, (1 << (32 - 15)) - 1) << 15

def i32(x):
    return x if (x < (1 << 31)) else x - (1 << 32)

with open('Flag.cs', 'w') as fd:
    fd.write("  int[] msg = new int [] {")
    [fd.write(f"{i32(get_filler() | (((x ^ z ^ y) << 7) | y))},") for x, y in reversed(a)]
    fd.write("};")
    fd.write(sub)
