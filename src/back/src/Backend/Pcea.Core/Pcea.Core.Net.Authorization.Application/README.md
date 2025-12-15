# Introduction

Authorization engine to handle permissions.
This is the application level for permissions handling.

## Dependencies

- DotNet core 9
- MediatR
- [Pcea.Core.Net.Authorization.Persistence]

## Usage

To use this project, call [AddPceaCoreNetAuthorizationApplication](./ConfigureServices.cs) to add services in dependency injection.

You must provide an implementation for:

- [EntityAuthorizationBehavior<TRequest, TResponse, T_EntityId>](./Behaviors/EntityAuthorizationBehavior.cs) with a concrete `T_EntityId` definition
- [ICurrentUserEntityPermissionsProvider<T_EntityId>](./Interfaces/Services/ICurrentUserEntityPermissionsProvider.cs) (you can find a base class in [Pcea.Core.Net.Authorization.Persistence](https://apollo-ssc.visualstudio.com/Apollo.Core/_git/Apollo.Core.Net.Authorization.Web))
- [ICurrentUserPermissionProvider](./Interfaces/Services/ICurrentUserPermissionsProvider.cs) (you can find a base class in [Pcea.Core.Net.Authorization.Persistence](https://apollo-ssc.visualstudio.com/Apollo.Core/_git/Apollo.Core.Net.Authorization.Web))
