﻿using ManagementSystem.DLL.Abstract;
using ManagementSystem.DLL.Database;
using ManagementSystem.DLL.Repository;
using ManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.DLL.EntityFramework
{
    public class EFEventRepository:GenericRepository<Event>,IEventDAL
    {
        
    }
}
