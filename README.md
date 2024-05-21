# open-with-nvim
open-with-nvim is a simple wrapper to set [Neovim](https://neovim.io/) as the default application for
text files with support for opening files in tabs instead of in separate windows.

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
