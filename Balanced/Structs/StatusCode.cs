using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum StatusCode
    {
        Unknown = 0,

        Ok = 200,

        Created = 201,

        Deleted = 204,

        BadRequest = 400,

        Unauthorized = 401,

        Forbidden = 403,

        NotFound = 404,

        MethodNotAllowed = 405,

        Conflict = 409,

        InternalServerError500 = 500,

        InternalServerError502 = 502,

        InternalServerError503 = 503,

        InternalServerError504 = 504,

    }
}
