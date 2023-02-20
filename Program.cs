using Raven.Client.Documents;
using Rvn.Ch02;
using System.Runtime.CompilerServices;
using static System.Console;
int opc = 0;
var Tasks = new ToDoTask();
WriteLine("Orgnazing your days app..");
do
{

    WriteLine("Select your opctions.");
    WriteLine("Opction 1 : create a new task.");
    WriteLine("Opction 2 : get by Id to complete.");
    WriteLine("Opction 3 : delete a task.");
    WriteLine("Opction 4 : get all the task. ");
    WriteLine("Opction 5 : get page and records per page.");
    opc = int.Parse(Console.ReadLine()!);
    switch (opc)
    {
        case 1:
            {
                Write("Write your task: ");
                var task = Console.ReadLine();

                WriteLine("Date for the task? ");
                var date = Convert.ToDateTime(ReadLine());

                Tasks.Task = task;
                Tasks.DueDate = date;
                using (var session = DocumentStoreHolder._store.OpenSession())
                {
                    session.Store(Tasks);
                    session.SaveChanges();
                }
            }
            break;
      case 2:
            {
                Console.WriteLine("Write the id: ");
                var id = Console.ReadLine()!;
                Tasks.Id = id;

                using (var session = DocumentStoreHolder._store.OpenSession())
                {

                    var task = session.Load<ToDoTask>(Tasks.Id);
                    task.Completed = true;
                    session.SaveChanges();
                    Console.WriteLine($"the task: {task.Id}\t{task.Task}\t{task.Completed}\t{task.DueDate} was completed successfully.");
                }
                break;
            }
      case 3:
                {
                    Console.WriteLine("Write the id: ");
                    var id = Console.ReadLine()!;
                    Tasks.Id = id;

                    using (var session = DocumentStoreHolder._store.OpenSession())
                    {

                        session.Delete(Tasks.Id);
                        session.SaveChanges();
                        WriteLine($"The task with id:{Tasks.Id} was deleted successfuly.");
                    }

                }
                break;
      case 4:
            {
                WriteLine("List of task..");
                using (var session = DocumentStoreHolder._store.OpenAsyncSession())
                {
                    var tasks = await session.Query<ToDoTask>().ToListAsync();

                    foreach (var task in tasks)
                    {
                        WriteLine($" Task: {task.Id} {task.Task} {task.Completed} {task.DueDate.ToShortDateString()}");
                    }
                }
            }
            break;

        case 5:
            {
                WriteLine("Paging the result.");
                WriteLine("Page number: ");
                var pageNumber = int.Parse(ReadLine()!);
                WriteLine("Page size: ");
                var pageSize = int.Parse(ReadLine()!);
                var skip = (pageNumber - 1) * pageSize;
                var take = pageSize;
                using (var session = DocumentStoreHolder._store.OpenAsyncSession())
                {
                   
                    var tasks = await session.Query<ToDoTask>()
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();

                    foreach (var task in tasks)
                    {
                        WriteLine($" Task: {task.Id} {task.Task} {task.Completed} {task.DueDate.ToShortDateString()}");
                    }
                }
            }
            break;

    }



}
while (opc != 100);
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Thanks for comming..");     
}











