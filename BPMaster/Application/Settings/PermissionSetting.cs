using Common.Application.Models;
using Common.Application.Settings;

namespace Application.Settings
{
    public class PermissionSetting : BasePermissionSetting
    {
        public override Dictionary<string, List<string>> ApiPermissions => new()
        {
            //{ "POST - /api/v1/identityusers/login", ["ADMIN", AuthenticatedUserModel.GuestRole]},
            //{ "POST - /api/v1/identityusers/register", ["ADMIN", AuthenticatedUserModel.GuestRole]},
            //{ "GET - /api/v1/building/getbuildingbyid", new List<string> { "GetbuildingbyidPermission" } },
            //{ "POST - /api/v1/building/create", new List<string> { "CreateBuildingPermission" } },
            //{ "PUT - /api/v1/building/update", new List<string> { "UpdateBuildingPermission" } },
            //{ "DELETE - /api/v1/building/delete", new List<string> { "DeleteBuildingPermission" } },

            //{ "GET - /api/v1/contract/getcontractbyid", new List<string> { "GetcontractbyidPermission" } },
            //{ "POST - /api/v1/contract/create", new List<string> { "CreateContractPermission" } },
            //{ "PUT - /api/v1/contract/update", new List<string> { "UpdateContractPermission" } },
            //{ "DELETE - /api/v1/contract/delete", new List<string> { "DeleteContractPermission" } },

            //{ "GET - /api/v1/customer/getcustomerbyid", new List<string> { "GetCustomerbyidPermission" } },
            //{ "POST - /api/v1/customer/create", new List<string> { "CreateCustomerPermission" } },
            //{ "PUT - /api/v1/customer/update", new List<string> { "UpdateCustomerPermission" } },
            //{ "DELETE - /api/v1/customer/delete", new List<string> { "DeleteCustomerPermission" } },

            //{ "GET - /api/v1/deposit/getdepositbyid", new List<string> { "GetdepositidPermission" } },
            //{ "POST - /api/v1/deposit/create", new List<string> { "CreatedepositPermission" } },
            //{ "PUT - /api/v1/deposit/update", new List<string> { "UpdatedepositPermission" } },
            //{ "DELETE - /api/v1/deposit/delete", new List<string> { "DeletedepositPermission" } },

            //{ "GET - /api/v1/depositor/getdepositbyid", new List<string> { "GetdepositoridPermission" } },
            //{ "POST - /api/v1/depositor/create", new List<string> { "CreatedepositorPermission" } },
            //{ "PUT - /api/v1/depositor/update", new List<string> { "UpdatedepositorPermission" } },
            //{ "DELETE - /api/v1/depositor/delete", new List<string> { "DeletedepositorPermission" } },

            //{ "GET - /api/v1/problem/getproblembyid", new List<string> { "GetproblemidPermission" } },
            //{ "POST - /api/v1/problem/create", new List<string> { "CreateproblemPermission" } },
            //{ "PUT - /api/v1/problem/update", new List<string> { "UpdateproblemPermission" } },
            //{ "DELETE - /api/v1/problem/delete", new List<string> { "DeleteproblemPermission" } },

            //{ "GET - /api/v1/room/getroombyid", new List<string> { "GetroomidPermission" } },
            //{ "POST - /api/v1/room/create", new List<string> { "CreateroomPermission" } },
            //{ "PUT - /api/v1/room/update", new List<string> { "UpdateroomPermission" } },
            //{ "DELETE - /api/v1/room/delete", new List<string> { "DeleteroomPermission" } },
            //{ "GET - /api/v1/room/getallroombybuildingid", new List<string> { "GetroomidPermission" } },
            //{ "GET - /api/v1/room/getroombystatus", new List<string> { "GetroomidPermission" } },


            //{ "GET - /api/v1/service/getservicebyid", new List<string> { "GetserviceidPermission" } },
            //{ "POST - /api/v1/service/create", new List<string> { "CreateservicePermission" } },
            //{ "PUT - /api/v1/service/update", new List<string> { "UpdateservicePermission" } },
            //{ "DELETE - /api/v1/service/delete", new List<string> { "DeleteservicePermission" } },
        };
    }
}
