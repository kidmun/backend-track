Console.WriteLine("Calculator");

Console.Write("Enter your name: ");
string studentName = Console.ReadLine();

int numberOfSubjects;
do
{
    Console.Write("Enter the number of subjects: ");
} while (!int.TryParse(Console.ReadLine(), out numberOfSubjects) || numberOfSubjects <= 0);

Dictionary<string, double> subject = new Dictionary<string, double>();
for (int i = 0; i < numberOfSubjects; i++)
{
    Console.Write($"Enter the name of subject number {i + 1}: ");
    string subjectName = Console.ReadLine();

    double grade;
    do
    {
        Console.Write($"Enter the grade for {subjectName}: ");
    } while (!double.TryParse(Console.ReadLine(), out grade) || grade < 0 || grade > 100);

    subject.Add(subjectName, grade);

}

double averageGrade = Calculate(subject);
Console.WriteLine();
Console.WriteLine($"Student Name: {studentName}");
Console.WriteLine("Subject Grades:");
foreach (KeyValuePair<string, double> entry in subject)
{
    string subjectName = entry.Key;
    double grade = entry.Value;
    Console.WriteLine($"{subjectName}: {grade}");
}
Console.WriteLine($"Average Grade: {averageGrade:F2}");

static double Calculate(Dictionary<string, double> subjects)
{
    if (subjects.Count == 0)
    {
        return 0;
    }

    double totalGrade = 0;
    foreach (KeyValuePair<string, double> entry in subjects)
    {
        double grade = entry.Value;
        totalGrade += grade;
    }

    return totalGrade / subjects.Count;
}
