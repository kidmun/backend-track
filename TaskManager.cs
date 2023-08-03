using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public enum TaskCategory
{
    Personal,
    Work,
    Errands,
    Others
}

public class Task
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
}

public class TaskManager
{
    private List<Task> tasks;

    public TaskManager()
    {
        tasks = new List<Task>();
    }

    public void AddTask(string name, string description, TaskCategory category)
    {
        tasks.Add(new Task
        {
            Name = name,
            Description = description,
            Category = category,
            IsCompleted = false
        });
    }

    public void ViewTasks()
    {
        foreach (var task in tasks)
        {
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Category: {task.Category}");
            Console.WriteLine($"Is Completed: {task.IsCompleted}");
            Console.WriteLine();
        }
    }
    

    public void ViewTasksByCategory(TaskCategory category)
    {
        var filteredTasks = tasks.Where(t => t.Category == category);
        foreach (var task in filteredTasks)
        {
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Category: {task.Category}");
            Console.WriteLine($"Is Completed: {task.IsCompleted}");
            Console.WriteLine();
        }
    }
    public bool UpdateTask(string name, string newName, string newDescription, TaskCategory newCategory, bool newIsCompleted)
    {
        var taskToUpdate = tasks.SingleOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (taskToUpdate != null)
        {
            taskToUpdate.Name = newName;
            taskToUpdate.Description = newDescription;
            taskToUpdate.Category = newCategory;
            taskToUpdate.IsCompleted = newIsCompleted;
            return true;
        }

        return false;
    }

    public async void SaveTasksToFileAsync(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");

                }

            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error while saving tasks to file: {ex.Message}");
        }

    }

    public async void LoadTasksFromFileAsync(string filePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    string[] taskData = line.Split(',');

                    if (Enum.TryParse(taskData[2], out TaskCategory category) &&
                        bool.TryParse(taskData[3], out bool isCompleted))
                    {
                        tasks.Add(new Task
                        {
                            Name = taskData[0],
                            Description = taskData[1],
                            Category = category,
                            IsCompleted = isCompleted
                        });
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Task data file not found. Creating a new file.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error while loading tasks from file: {ex.Message}");
        }
    }
}

public class Program
{
    static void Main(string[] args)

    {
        Console.WriteLine(@"
------------------------------------------
|                    Welcome!             |
|                                         |
------------------------------------------
");
        int numberOfTasks;
        do
        {
            Console.Write("Enter how many tasks you want to add: ");
        } while (!int.TryParse(Console.ReadLine(), out numberOfTasks));



        TaskManager taskManager = new TaskManager();
        for (int i = 0; i < numberOfTasks; i++)
        {
            string taskName, descriptionOfTask;
            Console.Write($"Enter The Name of Task #{i + 1}: ");
            taskName = Console.ReadLine();
            Console.Write($"Enter The description of Task #{i + 1}: ");
            descriptionOfTask = Console.ReadLine();
            int choosenTask;
            do
            {
                Console.Write($"Enter the Category of  Task #{i + 1}(  1: Personal 2: Work 3: Errands 4: Others): ");

            } while (!int.TryParse(Console.ReadLine(), out choosenTask) || (choosenTask != 1 && choosenTask != 2 && choosenTask != 3 && choosenTask != 4));


            TaskCategory choosenTaskName;

            if (choosenTask == 1)
            {
                choosenTaskName = TaskCategory.Personal;
            }
            else if (choosenTask == 2)
            {
                choosenTaskName = TaskCategory.Work;
            }
            else if (choosenTask == 3)
            {
                choosenTaskName = TaskCategory.Errands;
            }
            else
            {
                choosenTaskName = TaskCategory.Others;
            }
            taskManager.AddTask(taskName, descriptionOfTask, choosenTaskName);
        }



        Console.WriteLine("All Tasks:");
        taskManager.ViewTasks();
        taskManager.UpdateTask("nameee","checking",  "ddddd",TaskCategory.Personal, true);
        Console.WriteLine("\nPersonal Tasks:");
         taskManager.ViewTasks();
        taskManager.ViewTasksByCategory(TaskCategory.Personal);  
        taskManager.SaveTasksToFileAsync("tasks.txt");

        taskManager.LoadTasksFromFileAsync("tasks.txt");



    }
}
