using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CustomerAccountDAL : _BaseDAL, IAccountDAL
    {
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {
        }

        public Account Authorize(string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string accountId, string oldpassword, string newpassword)
        {
            throw new NotImplementedException();
        }

        public Account Get(string accountId)
        {
            throw new NotImplementedException();
        }
    }
}