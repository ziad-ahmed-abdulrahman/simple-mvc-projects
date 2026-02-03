from Crypto.Cipher import AES
import sys

def unpad(text_bytes):
    pad_len = text_bytes[-1]
    return text_bytes[:-pad_len]

# ============================
# Read Input
# ============================
if len(sys.argv) < 3:
    print("Error: Missing arguments. Usage: python aes_decrypt.py <cipher_hex> <key>")
    sys.exit(1)

cipher_hex = sys.argv[1]   # Ciphertext in hex
key = sys.argv[2]           # key

# ============================
# Validation (only 16 chars)
# ============================
if len(key) != 16:
    print("Error: Key must be exactly 16 characters long.")
    sys.exit(1)

# ============================
# Decrypt
# ============================
try:
    ciphertext = bytes.fromhex(cipher_hex)
    cipher = AES.new(key.encode(), AES.MODE_ECB)
    plaintext_padded = cipher.decrypt(ciphertext)
    plaintext = unpad(plaintext_padded).decode()
    print(plaintext)
except Exception as e:
    print(f"Error: {str(e)}")
