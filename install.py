import requests
import os
import zipfile
import tkinter
import customtkinter
import winshell
from win32com.client import Dispatch

def download(location):
    print(location)

    if not str(location).endswith('BSG'):
        location += '\BSG'

    try:
        os.rmdir(location)
    except:
        pass

    #location = location.replace('\BSC', '')

    os.mkdir(location)

    req = requests.get("https://github.com/dasNTI/Benno/blob/main/Builds/Benno.zip?raw=true")

    file = open(location + '\Benno.zip', 'wb')

    file.write(req.content)
    file.close()

    with zipfile.ZipFile(location + '\Benno.zip', 'r') as zip:
        zip.extractall(location)
    
    os.remove(location + '\Benno.zip')

    create_shortcut(location)

def create_shortcut(location):
    desktop = winshell.desktop()
    path = os.path.join(desktop, "Benno: Schlösser's Conundrum.lnk")

    shell = Dispatch('WScript.Shell')
    shortcut = shell.CreateShortCut(path)

    shortcut.save()




# UI
customtkinter.set_appearance_mode('light')
customtkinter.set_default_color_theme('blue')

app = customtkinter.CTk()
app.geometry('720x480')
app.title("Benno: Schlösser's Conundrum - Installer")

folder = tkinter.StringVar()
folder.set("C:\ProgramData\BSC")

# Folder Input Page
title = customtkinter.CTkLabel(app, text="Benno: Schlösser's Conundrum - Installer")
title.pack()

input = customtkinter.CTkEntry(app, textvariable=folder)
input.pack()

btn = customtkinter.CTkButton(app, text="Installieren", command=lambda:download(folder.get()))
btn.pack()

app.mainloop()