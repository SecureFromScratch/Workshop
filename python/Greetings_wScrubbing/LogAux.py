def Scrub(s):
    return "".join([c if c.isascii() and c.isprintable() else '?' for c in s])

def Obscure(s):
    if len(s) < 5:
        return s[0] + '***' + s[len(s) - 1]
    return s[0] + '*' * (len(s) - 2) + s[len(s) - 1]

def ObscureUsername(s):
    parts = s.split('\\')
    return '\\'.join([Obscure(part) for part in s.split('\\')])
