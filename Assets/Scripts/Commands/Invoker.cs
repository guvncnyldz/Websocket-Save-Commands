using System;
using System.Collections.Generic;
using UniRx;

public class Invoker
{
    public static Invoker instance;

    private List<ICommand> commands;
    private List<double> commandTime;

    private double startTime;
    private Invoker()
    {
        commandTime = new List<double>();
        commands = new List<ICommand>();

        startTime = GetTimeAsMilliseconds();
    }

    public void AddCommand(ICommand command)
    {
        commands?.Add(command);

        SaveExecuteTime();
        command.Execute();
    }

    void SaveExecuteTime()
    {
        commandTime.Add(GetTimeAsMilliseconds());
    }

    public void ExecuteAll()
    {
        if (commands == null)
            return;

        int j = 0;
        for (int i = 0; i < commands.Count; i++)
        {
            Observable.Timer(TimeSpan.FromMilliseconds(commandTime[i] - startTime)).Subscribe(_ =>
            {
                commands[j].Execute();
                j++;
            });
        }
    }

    public static Invoker GetInstance()
    {
        if (instance == null)
            instance = new Invoker();

        return instance;
    }

    Double GetTimeAsMilliseconds()
    {
        return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
    }
}