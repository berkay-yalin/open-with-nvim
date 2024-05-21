# open-with-nvim

## Introduction

A simple wrapper to set [Neovim](https://neovim.io/) as the default application
on Windows for text files with support for opening files in tabs instead of in separate windows.

Simply setting the `nvim.exe` executable as the default application will open a new instance of Neovim for each file,
whereas open-with-nvim allows for all files opened through File Explorer to spawn within a single instance of Neovim.

This allows Neovim to inherit the tab functionality of Windows Notepad whilst remaining completely within the terminal.

<div align="center">
    <img src="https://github.com/berkay-yalin/open-with-nvim/blob/main/docs/demo.gif"/>
</div>


<div align="center">
    <i>
        open-with-nvim is <b>NOT</b> a GUI, just a wrapper which sends commands to the terminal.
    </i>
</div>

## Installation

1. Install the `open-with-nvim.exe` executable from the lastest [Release](https://github.com/berkay-yalin/open-with-nvim/releases) or compile from source.

2. Place the executable inside `%LocalAppData%\nvim-data\`.

    `open-with-nvim.exe` is a standalone executable and can actually be placed anywhere, `%LocalAppData%\nvim-data\` is just a reccomendation.

3. Set the executable as the default app for the file types you would like.
