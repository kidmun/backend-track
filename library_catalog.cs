
Console.WriteLine(@"
------------------------------------------
|       Welcome to our  Library!          |
|                                         |
------------------------------------------
");
Library library = new Library("Test Library", "address test");

Book book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", "978-1408855652", 1997);
library.AddBook(book);

MediaItem media = new MediaItem("New Movie", "DVD", 120);
library.AddMediaItem(media);

library.PrintCatalog();


class Library
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public List<Book> Books { get; private set; }
    public List<MediaItem> MediaItems { get; private set; }

    public Library(string name, string address)
    {
        Name = name;
        Address = address;
        Books = new List<Book>();
        MediaItems = new List<MediaItem>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Book '{book.Title}' added to the library.");
    }

    public void RemoveBook(Book book)
    {
        if (Books.Remove(book))
        {
            Console.WriteLine($"Book '{book.Title}' removed from the library.");
        }
        else
        {
            Console.WriteLine($"Book '{book.Title}' not found in the library.");
        }
    }

    public void AddMediaItem(MediaItem item)
    {
        MediaItems.Add(item);
        Console.WriteLine($"Media item '{item.Title}' added to the library.");
    }

    public void RemoveMediaItem(MediaItem item)
    {
        if (MediaItems.Remove(item))
        {
            Console.WriteLine($"Media item '{item.Title}' removed from the library.");
        }
        else
        {
            Console.WriteLine($"Media item '{item.Title}' not found in the library.");
        }
    }

    public void PrintCatalog()
    {
        Console.WriteLine("Library Catalog:");
        Console.WriteLine("Books:");
        foreach (var book in Books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Publication Year: {book.PublicationYear}");
        }

        Console.WriteLine("Media Items:");
        foreach (var item in MediaItems)
        {
            Console.WriteLine($"Title: {item.Title}, Type: {item.MediaType}, Duration: {item.Duration} minutes");
        }
    }
}

class Book
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public int PublicationYear { get; private set; }

    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
}

class MediaItem
{
    public string Title { get; private set; }
    public string MediaType { get; private set; }
    public int Duration { get; private set; }

    public MediaItem(string title, string mediaType, int duration)
    {
        Title = title;
        MediaType = mediaType;
        Duration = duration;
    }
}
