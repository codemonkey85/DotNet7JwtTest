namespace DotNet7JwtTest.Server.Endpoints;

public static class Users
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder apiRootBuilder)
    {
        var usersGroupBuilder = apiRootBuilder.MapGroup("users").RequireAuthorization();

        usersGroupBuilder.MapGet(string.Empty, GetUsersAsync);
        usersGroupBuilder.MapGet("/{userId:int}", GetUserAsync);
        usersGroupBuilder.MapPost(string.Empty, CreateUserAsync);
        usersGroupBuilder.MapPut("/{userId:int}", UpdateUserAsync);
        usersGroupBuilder.MapDelete("/{userId:int}", DeleteUserAsync);

        return apiRootBuilder;
    }

    private static readonly List<User> users = new()
    {
        new (Id : 1, Name : "Michael Bond"),
        new (Id : 2, Name : "Shannan Bond"),
        new (Id : 3, Name : "Caleb Bond"),
        new (Id : 4, Name : "Ethan Bond"),
    };

    private static async Task<Ok<List<User>>> GetUsersAsync()
    {
        return TypedResults.Ok(users);
    }

    private static async Task<Results<Ok<User>, NotFound>> GetUserAsync(int userId)
    {
        var user = users.FirstOrDefault(user => user.Id == userId);
        return user is null ? TypedResults.NotFound() : TypedResults.Ok(user);
    }

    private static async Task<Ok<User>> CreateUserAsync(User user)
    {
        users.Add(user);
        return TypedResults.Ok(user);
    }

    private static async Task<Results<Ok<User>, NotFound>> UpdateUserAsync(int userId, User user)
    {
        var foundUser = users.FirstOrDefault(user => user.Id == userId);
        if (foundUser is null)
        {
            return TypedResults.NotFound();
        }

        var index = users.IndexOf(foundUser);

        foundUser = foundUser with { Name = user.Name };
        users.Remove(foundUser);
        users.Insert(index, user);

        return TypedResults.Ok(foundUser);
    }

    private static async Task<Results<Ok, NotFound>> DeleteUserAsync(int userId)
    {
        var foundUser = users.FirstOrDefault(user => user.Id == userId);
        if (foundUser is null)
        {
            return TypedResults.NotFound();
        }

        users.Remove(foundUser);
        return TypedResults.Ok();
    }
}
