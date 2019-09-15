using Obsidian.Net;
using Obsidian.Net.Packets;
using Obsidian.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Obsidian.PacketExporter
{
    internal class Program
    {
        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            Packet packet = new Handshake()
            {
                NextState = ClientState.Status,
                ServerAddress = "localhost",
                Version = ProtocolVersion.v1_13_2,
                ServerPort = 25565
            };

            string path = $"{packet.GetType().FullName}.bin";

            if (args.Contains("-t"))
            {
                path = $@"..\..\..\..\Obsidian.Tests\bin\Debug\netcoreapp3.0\{path}";
            }

            path = Path.GetFullPath(path);

            using (FileStream fileStream = File.OpenWrite(path))
            using (var minecraftStream = new MinecraftStream(fileStream))
            {
                //await PacketHandler.CreateAsync(packet, minecraftStream);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("explorer.exe", $"/select,\"{path}\"") { UseShellExecute = true });
            }
        }

        ///private static void AlternativeMain()
        ///{
        ///    start:
        ///
        ///    Console.ForegroundColor = ConsoleColor.White;
        ///    Console.Write("Enter packet type: ");
        ///    Console.ResetColor();
        ///
        ///    string typeName = Console.ReadLine();
        ///
        ///    Type type;
        ///
        ///    type = typeof(Packet).Assembly.GetType(typeName);
        ///
        ///    if (type == null)
        ///    {
        ///        Console.ForegroundColor = ConsoleColor.Red;
        ///        Console.WriteLine("Couldn't find type");
        ///        Console.ResetColor();
        ///        goto start;
        ///    }
        ///
        ///    Packet packet = (Packet)Activator.CreateInstance(type);
        ///
        ///    Console.ForegroundColor = ConsoleColor.DarkGray;
        ///    Console.WriteLine("=> " + type.FullName);
        ///    Console.ResetColor();
        ///
        ///    foreach (PropertyInfo propertyInfo in type.GetProperties())
        ///    {
        ///        VariableAttribute attribute = propertyInfo.GetCustomAttribute<VariableAttribute>();
        ///
        ///        if (attribute == null)
        ///        {
        ///            continue;
        ///        }
        ///
        ///        Console.ForegroundColor = ConsoleColor.White;
        ///        Console.Write($"{propertyInfo.Name} ({attribute.Type}/{propertyInfo.DeclaringType.Name}): ");
        ///        Console.ResetColor();
        ///
        ///        string input = Console.ReadLine();
        ///        object value;
        ///
        ///        value = Convert.ChangeType(input, propertyInfo.DeclaringType);
        ///        propertyInfo.SetValue(packet, value);
        ///    }
        ///}
    }
}