using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Exceptions.Errors
{
    public enum ErrorCode
    {
        None = 0,

        // Common
        Validation,
        InvalidParameter,
        InvalidStatus,
        ResourceAlreadyExists,
        NotFound,
        MissingData,
        GenericServerError,

        // Auth
        AccessRights,
        AuthenticationFailed,

        // Tech
        ConfigurationMissing,
        DataSeeding,
        Storage,
        Export,
    }
}
