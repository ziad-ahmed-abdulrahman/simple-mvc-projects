from Crypto.Cipher import AES
import sys

def pad(text):
    pad_len = 16 - (len(text) % 16)
    return text + chr(pad_len) * pad_len

# ============================
# Read Input Like Caesar Code
# ============================
text = sys.argv[1]        # text
key = sys.argv[2]         # key

# ============================
# Validation
# ============================
if len(key) != 16:
      print("Error: Key must be exactly 16 characters long.")
      sys.exit(1)

# ============================
# Encrypt
# ============================
text_padded = pad(text)

cipher = AES.new(key.encode(), AES.MODE_ECB)
ciphertext = cipher.encrypt(text_padded.encode())

# ============================
# Output
# ============================
print(ciphertext.hex())
