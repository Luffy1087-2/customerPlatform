﻿using System.Collections.Generic;
using CustomerPlatform.Core.Abstract;

namespace CustomerPlatform.Data.Abstract
{
    public interface ICustomersDataRepository
    {
        /// <summary>
        /// It should access to the database
        /// </summary>
        /// <returns>IEnumerable&lt;ICustomer&gt; all of the customers</returns>
        IEnumerable<ICustomer> GetAllCustomers();
    }
}