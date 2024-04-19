using System.Collections.Generic;

// Модель книги
class Book
{
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public uint Year { get; set; }
    public string Publisher { get; set; }
    public uint PageCount { get; set; }
}
