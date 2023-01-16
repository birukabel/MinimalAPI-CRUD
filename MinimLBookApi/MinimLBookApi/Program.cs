var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book { Id = 1, Title = "The Hitchicker's Guide To The Galaxy", Author = "Douglas Adams" },
    new Book { Id = 2, Title = "1984", Author = "George Orwell" },
    new Book { Id=3, Title="Ready Player One", Author="Ernest Cline"},
    new Book { Id=4, Title="The Martin", Author="Andy Weir"},
};

app.MapGet("/book", () =>
{
    return books;
});

app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null)
    {
        return Results.NotFound("Sorry this book doesn't exist");
    }
    return Results.Ok(book);
});

app.MapPost("/book", (Book book) =>
{
    books.Add(book);
    return books;
});

app.MapPut("/book", (Book updatedBook) =>
{
    var book = books.Find(x => x.Id == updatedBook.Id);
    if (book is null)
    {
        return Results.NotFound("Sorry this book doesn't exist");
    }
    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;

    return Results.Ok(book);
});

app.MapDelete("/book/{id}",(int id) => 
{
    var book = books.Find(x => x.Id == id);
    if (book is null)
    {
        return Results.NotFound("Sorry this book doesn't exist");
    }
    books.Remove(book);

    return Results.Ok(books);
});

app.Run();

public class Book {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}
