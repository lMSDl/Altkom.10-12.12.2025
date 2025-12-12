
List<Task<string>> tasks = new List<Task<string>>();
for (int i = 1; i <= 3; i++)
{
    var taskId = i;
    //var task = Task.Run(() => DoWork(taskId));
    var task = DoWorkAsync(taskId);

    tasks.Add(task);
}

//HandleTaskOneByOne(tasks);
HandleTasksAllAtOnce(tasks);


var cts = new CancellationTokenSource();
Console.WriteLine("Rozpoczynanie pobierania danych... (naciśnij dowolny klawisz, aby anulować)");
Task cancelTask = Task.Run(() =>
{
    Console.ReadKey();
    cts.Cancel();
});

try
{
    var result = await DownloadDataAsync(cts.Token);
    Console.WriteLine(result);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Pobieranie anulowane przez użytkownika.");

}
cts.Dispose();


Console.ReadLine();


    string DoWork(int taskId)
{
    Console.WriteLine($"Zadanie {taskId} rozpoczęte. {Thread.CurrentThread.ManagedThreadId}");
    Task.Delay(1000 * Random.Shared.Next(1,5)).Wait(); // symulacja pracy synchronicznie
    Console.WriteLine($"Zadanie {taskId} zakończone. {Thread.CurrentThread.ManagedThreadId}");
    return $"Wynik zadania {taskId}.";
}

//async - słowo kluczowe, które oznacza, że metoda jest asynchroniczna
//jest ono wymamagane aby można było w metodzie użyć await
//Task - metody asynchroniczne zwracają obiekt Task, który reprezentuje operację asynchroniczną
//zasady nazewnictwa mówią, że metody asynchroniczne powinny kończyć się na Async
async Task<string> DoWorkAsync(int taskId)
{
    Console.WriteLine($"Zadanie {taskId} rozpoczęte. {Thread.CurrentThread.ManagedThreadId}");
    //przez użycie await metoda automatycznie opakuje zwracany resultat w Task
    var delay = 1000 * Random.Shared.Next(1, 5);
    if (delay < 2000)
        Task.Delay(delay).Wait();
    else
        await Task.Delay(delay);
    Console.WriteLine($"Zadanie {taskId} zakończone. {Thread.CurrentThread.ManagedThreadId}");
    return $"Wynik zadania {taskId}.";
}



static void HandleTaskOneByOne(List<Task<string>> tasks)
{
    while (tasks.Any())
    {

        //czekamy na zakończenie jakiegokolwiek zadania
        int index = Task.WaitAny(tasks.ToArray());
        var completedTask = tasks[index];

        Console.WriteLine(completedTask.Result + $"  {Thread.CurrentThread.ManagedThreadId}");
        tasks.RemoveAt(index); // usuwamy zakończone zadanie z listy
    }
}

static void HandleTasksAllAtOnce(List<Task<string>> tasks)
{
    // czekamy na zakończenie wszystkich zadań
    Task.WaitAll(tasks);

    foreach (var item in tasks)
    {
        Console.WriteLine(item.Result + $" {Thread.CurrentThread.ManagedThreadId}");
    }
}


async Task<string> DownloadDataAsync(CancellationToken cancellationToken)
{
    // Symulacja 5-sekundowego pobierania z obsługą anulowania
    for (int i = 0; i < 5; i++)
    {
        if (cancellationToken.IsCancellationRequested)
            return "Zadanie przerwane";
        Console.WriteLine($"Pobieranie... {i + 1}/5");
        await Task.Delay(1000, cancellationToken); // asynchroniczne opóźnienie z obsługą anulowania
    }

    return "Dane z serwera";
}