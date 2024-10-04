using System.Diagnostics;
using Lyre;
using NativeMessagingExcuteConsole;

var host = new NativeMessagingHost();

try
{
    while (true)
    {
        var response = await host.Read<Message>();
        string path = response.Value;
        // 規定のアプリで開く
        ProcessStartInfo app = new();
        app.FileName = path;
        app.UseShellExecute = true;
        Process.Start(app);
        await host.Write(new Message($"You said {path} at {response.DateTime}"));
    }
}
catch (EndOfStreamException)
{
    // Disconnected
}
catch (Exception exception)
{
    await host.Write(new Message($"error: {exception}"));

    Console.WriteLine($"Oh: {exception}");
    Environment.FailFast("Oh", exception);
}
