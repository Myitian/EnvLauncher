using System.Diagnostics;

ReadOnlySpan<string> argsSpan = args;
if (argsSpan.IsEmpty)
{
    Console.Error.WriteLine("Usage: EnvLauncher <envVar1=value1> <envVar2=value2> ... -- <executable> <arg1> <arg2> ...");
    return;
}
int splitter = argsSpan.IndexOf("--");
ReadOnlySpan<string> envVars;
string exe;
ReadOnlySpan<string> commandArgs;
if (splitter < 0)
{
    envVars = [];
    exe = argsSpan[0];
    commandArgs = argsSpan[1..];
}
else
{
    envVars = argsSpan[..splitter];
    exe = argsSpan[splitter + 1];
    commandArgs = argsSpan[(splitter + 2)..];
}
ProcessStartInfo psi = new(exe);
foreach (string arg in commandArgs)
{
    psi.ArgumentList.Add(arg);
}
foreach (string envVar in envVars)
{
    int equalsIndex = envVar.IndexOf('=');
    if (equalsIndex < 0)
    {
        Console.Error.WriteLine($"Invalid environment variable format: {envVar}");
        return;
    }
    string key = envVar[..equalsIndex];
    string value = envVar[(equalsIndex + 1)..];
    psi.Environment[key] = value;
}
if (Process.Start(psi) is null)
{
    Console.Error.WriteLine("Failed to start process.");
}