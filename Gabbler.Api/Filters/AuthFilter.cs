using System;
using WebMatrix.WebData;

namespace Gabbler.Api.Filters
{
    public class AuthFilter
    {
        public static void RegisterAuth()
        {

            try
            {

                WebSecurity.InitializeDatabaseConnection("DansTesComsSqlServeur", "Users", "id", "Mail", true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Impossible d'initialiser la base de données ASP.NET Simple Membership. Pour plus d'informations, consultez la page http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }
    }
}