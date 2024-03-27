using Shared;

namespace Identity.DataAccess.Errors;

public static class DomainErrors
{
    public static readonly Error InternalError = new(
        "Error.InternalError",
        "Internal error.",
        500);

    public static readonly Error UnknownError = new(
        "Unknown.UnknownError",
        "Unknown error occured");

    public static class User
    {
        public static readonly Error AlreadyInRole = new(
            "User.AlreadyInRole",
            "User is already in role.",
            400);
        
        public static readonly Error UserNotInRole = new(
            "User.NotInRole",
            "User is not in role.",
            400);

        public static readonly Error RoleNotAdded = new(
            "User.RoleNotAdded",
            "Role has not been added.",
            400);

        public static readonly Error RoleNotDeleted = new(
            "User.RoleNotDeleted",
            "Role has not been deleted.",
            400);

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "Invalid login or password.",
            401);

        public static readonly Error InvalidAccessOrRefreshTokenToken = new(
            "User.InvalidAccessOrRefreshToken",
            "Invalid access or refresh token.",
            401);

        public static readonly Error UserNotFoundById = new(
            "User.NotFoundById",
            "User with specified id not found.",
            404);

        public static readonly Error UserNotFoundByUsername = new(
            "User.NotFoundByUsername",
            "User with specified username not found.",
            404);

        public static readonly Error UsernameConflict = new(
            "User.UsernameConflict",
            "The requested username is already in use.",
            409);
    }

    public static class Role
    {
        public static readonly Error RoleNotFoundById = new(
            "Role.NotFoundById",
            "Role with specified id not found.",
            404);

        public static readonly Error RoleNotFoundByName = new(
            "Role.NotFoundByName",
            "Role with specified name not found.",
            404);

        public static readonly Error NameConflict = new(
            "Role.NameConflict",
            "Role with provided name already exists.",
            409);
    }
}