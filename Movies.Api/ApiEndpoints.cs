namespace Movies.Api;

/// <summary>
/// A central class to manage the endpoint declarations, to be used in route declarations.
/// It's a good practice in order to avoid having routes out of sync across the project.
/// </summary>
public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
    }
}