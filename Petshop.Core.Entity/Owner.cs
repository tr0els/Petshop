﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Entity
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
