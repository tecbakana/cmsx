﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CMSBLL
{
    public class MenuBLL
    {
        Guid menuId { get; set; }

        public static MenuBLL ObterNovoMenu()
        {
            return new MenuBLL();
        }
    }
}
