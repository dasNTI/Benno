import requests

req = requests.get("https://github.com/dasNTI/Benno/tree/main/Builds?raw=true")

print(req.content)