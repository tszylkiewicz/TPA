﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public interface IBrowseFile
    {
        string ChooseFile();
        string SavePath();
    }
}
