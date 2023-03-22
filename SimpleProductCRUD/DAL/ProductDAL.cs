using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SimpleProductCRUD.DAL
{
    public class ProductDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["addConnectionString"].ToString();


    }
}