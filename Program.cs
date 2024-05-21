using System.Diagnostics;

static void CreateTerminalAddTab(string filePath) {
    ProcessStartInfo psi = new() {
        FileName = "powershell.exe",
        Arguments = "-NoExit",
        RedirectStandardInput = true,
        UseShellExecute = false,
        CreateNoWindow = false
    };

    Process process = Process.Start(psi);

    StreamWriter sw = process.StandardInput;

    sw.WriteLine($"nvim --listen \\\\.\\pipe\\nvim {filePath}");
}

static void OpenTerminalAddTab(string filePath) {
    string pythonCommand = "-c \"import pynvim; nvim=pynvim.attach('socket', path=r'\\\\.\\pipe\\nvim');" + " nvim.command(r'tabnew " + filePath + "'); nvim.close();\"";

    Process process = new();
    process.StartInfo.FileName = "python.exe";
    process.StartInfo.Arguments = pythonCommand;
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.CreateNoWindow = false;

    process.Start();
    process.WaitForExit();
    process.Close();
}

static bool isTerminalOpen() {
    const string executable = "python.exe";
    const string pythonCommand = "-c \"import pynvim; nvim=pynvim.attach('socket', path=r'\\\\.\\pipe\\nvim'); nvim.close()\"";

    Process process = new();
    process.StartInfo.FileName = executable;
    process.StartInfo.Arguments = pythonCommand;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.CreateNoWindow = true;

    process.Start();

    StreamReader errorReader = process.StandardError;
    string errorOutput = errorReader.ReadToEnd();

    process.WaitForExit();
    process.Close();

    if (!string.IsNullOrEmpty(errorOutput)) {
        return false;
    }
    else {
        return true;
    }
}

string filePath = Environment.GetCommandLineArgs()[1];

if (isTerminalOpen()) {
    OpenTerminalAddTab(filePath);
}
else {
    CreateTerminalAddTab(filePath);
}
