import sys

text = sys.argv[1]          
key = int(sys.argv[2])      

result = ""

for ch in text:
    if 'A' <= ch <= 'Z':
        result += chr((ord(ch) - 65 + key) % 26 + 65)
    elif 'a' <= ch <= 'z':
        result += chr((ord(ch) - 97 + key) % 26 + 97)
    else:
        result += ch

print(result)  
