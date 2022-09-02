using System;

namespace MatchCore.Helpers;

public interface IWriter
{
    void WriteToConsole(string message);
}

public class Writer : IWriter
{
    public void WriteToConsole(string message)
    {
        Console.WriteLine(message);
    }
}