﻿using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.GenericRepository;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE160548_IdetityAjaxASP.NETCoreWebAPI.Repository.UnitOfwork
{
    public interface IUnitOfwork : IDisposable
    {
        IGenericRepository<Product> ProductRepo { get; set; }
        IGenericRepository<Category> CategoryRepo { get; set; }
        IGenericRepository<Member> MemberRepo { get; set; }
        IGenericRepository<Order> OrderRepo { get; set; }
        IGenericRepository<OrderDetail> OrderDetailRepo { get; set; }

        void Save();
    }
}
