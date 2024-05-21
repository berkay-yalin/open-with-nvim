using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

[DllImport("user32.dll")]
static extern bool SetForegroundWindow(IntPtr hWnd);

static Process[] getApplications() {
    return Process.GetProcesses()
        .Where(p => p.ProcessName != "TextInputHost")
        .Where(p => p.MainWindowHandle != 0)
        .ToArray();
}

static void CreateTerminalAddTab(string filePath, string pipePath) {
    Process process = new();
    process.StartInfo.FileName = "nvim.exe";
    process.StartInfo.Arguments = $"\"{filePath}\" --listen {pipePath}";
    process.Start();
}

static void OpenTerminalAddTab(string filePath, string pipePath) {
    Process process = new();
    process.StartInfo.FileName = "nvim.exe";
    process.StartInfo.Arguments =$"--server {pipePath} --remote-send \":tabnew {filePath}<CR>\"";
    process.StartInfo.CreateNoWindow = true;
    process.Start();

    Process[] processes = getApplications();

    foreach (Process i in processes) {
        if (i.ProcessName == "nvim") {
            SetForegroundWindow(i.MainWindowHandle);
        }
    }
}

static bool isTerminalOpen(string pipePath) {
    return File.Exists(pipePath);
}

string filePath = Environment.GetCommandLineArgs()[1];
string pipePath = @"\\.\pipe\nvim";

if (isTerminalOpen(pipePath)) {
    OpenTerminalAddTab(filePath, pipePath);
}
else {
    CreateTerminalAddTab(filePath, pipePath);
}
