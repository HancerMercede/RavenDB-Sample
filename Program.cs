using Rvn.Ch02;
using static System.Console;

//using (var session = DocumentStoreHolder.Store.OpenSession())
//{
//    var task = new ToDoTask
//    {
//        DueDate = DateTime.Today.AddDays(1),
//        Task = "Buy milk"

//    };
//    session.Store(task);
//    session.SaveChanges();
//}

//using (var session = DocumentStoreHolder._store.OpenSession())
//{
//    var task = new ToDoTask
//    {
//        DueDate = DateTime.Today.AddDays(2),
//        Task = "Program the session page.",
//        Completed = false,
//    };

//    session.Store(task);
//    session.SaveChanges();

//}


int opc = 0;
var Tasks = new ToDoTask();
WriteLine("Orgnazing your days app..");
do
{

    WriteLine("Select your opctions.");
    WriteLine("Opction 1 : create a new task.");
    WriteLine("Opction 2 : get by Id to complete.");
    WriteLine("Opction 3 : delete a task.");
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
                    Console.WriteLine(task.Task);
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

    }



}
while (opc != 5);
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Thanks for comming..");     
}











