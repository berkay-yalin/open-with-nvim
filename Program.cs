using System.Diagnostics;
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
    process.StartInfo.FileName = "nvim";
    process.StartInfo.Arguments = $"\"{filePath}\" --listen {pipePath}";
    process.Start();
}

static void OpenTerminalAddTab(string filePath) {
    Process process = new();
    process.StartInfo.FileName = "python.exe";
    process.StartInfo.Arguments = "-c \"import pynvim; nvim=pynvim.attach('socket', path=r'\\\\.\\pipe\\nvim');" + " nvim.command(r'tabnew " + filePath + "'); nvim.close();\"";
    process.StartInfo.CreateNoWindow = true;
    process.Start();

    Process[] processes = getApplications();

    foreach (Process i in processes) {
        if (i.ProcessName == "nvim" && i.MainWindowTitle.Contains("open-with-nvim.exe")) {
            SetForegroundWindow(i.MainWindowHandle);
        }
    }
}

static bool isTerminalOpen() {
    Process process = new();
    process.StartInfo.FileName = "python.exe";
    process.StartInfo.Arguments = "-c \"import pynvim; nvim=pynvim.attach('socket', path=r'\\\\.\\pipe\\nvim'); nvim.close()\"";
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    process.StartInfo.CreateNoWindow = true;
    process.Start();

    StreamReader errorReader = process.StandardError;
    string errorOutput = errorReader.ReadToEnd();

    if (!string.IsNullOrEmpty(errorOutput)) {
        return false;
    }
    else {
        return true;
    }
}

string filePath = Environment.GetCommandLineArgs()[1];
string pipePath = "\\\\.\\pipe\\nvim";

if (isTerminalOpen()) {
    OpenTerminalAddTab(filePath);
}
else {
    CreateTerminalAddTab(filePath, pipePath);
}
