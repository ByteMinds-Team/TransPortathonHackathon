﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Dtos
{
    public class GetedAllEmployee 
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImagePath { get; set; }
        public List<Vehicle> vehicle { get; set; }
    }
}
